using System.Collections;
using System.Reflection;
using Bortle.NINA.Emitter.Models;

namespace Bortle.NINA.Emitter.Tests.Models
{

    // Guards the hand-written IEquatable partials in src/Bortle.NINA.Emitter/Models/ (see ADR-004)
    // against drift from schema/regenerated changes to Generated/EmitterModels.cs.
    //
    // For every generated model type that opts into value equality, this reflects over its public
    // read/write properties and proves each one is actually load-bearing in Equals: mutate exactly
    // one property away from a fully-populated baseline and assert the instances stop comparing
    // equal. A property added to the schema but never wired into a hand-written Equals/GetHashCode
    // fails this test instead of silently breaking "skip duplicates from internal nina polling".
    public class ModelEqualityCoverageTests
    {
        public static IEnumerable<object[]> EquatableModelTypes() =>
            typeof(SwitchDeviceInfoData).Assembly.GetTypes()
                .Where(t => t.Namespace == "Bortle.NINA.Emitter.Models" && t.IsClass && !t.IsAbstract)
                .Where(IsSelfEquatable)
                .Select(t => new object[] { t });

        [Theory]
        [MemberData(nameof(EquatableModelTypes))]
        public void Equals_ChangesWhenAnyPropertyChanges(Type modelType)
        {
            var baseline = Populate(Activator.CreateInstance(modelType)!);

            foreach (var prop in WritableProperties(modelType))
            {
                var mutated = Clone(baseline);
                var currentValue = prop.GetValue(baseline);
                prop.SetValue(mutated, CreateDifferentValue(prop.PropertyType, currentValue));

                Assert.False(
                    baseline.Equals(mutated),
                    $"{modelType.Name}.Equals ignores '{prop.Name}' — mutating it alone didn't change " +
                    "equality. Update the hand-written equality partial in Models/ (see ADR-004).");
            }
        }

        [Theory]
        [MemberData(nameof(EquatableModelTypes))]
        public void Equals_TrueForEquivalentInstances(Type modelType)
        {
            var a = Populate(Activator.CreateInstance(modelType)!);
            var b = Clone(a);

            Assert.True(a.Equals(b));
            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }

        private static bool IsSelfEquatable(Type type) =>
            type.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEquatable<>) && i.GetGenericArguments()[0] == type);

        private static IEnumerable<PropertyInfo> WritableProperties(Type type) =>
            type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead && p.CanWrite);

        private static object Populate(object instance)
        {
            foreach (var prop in WritableProperties(instance.GetType()))
            {
                prop.SetValue(instance, CreateValue(prop.PropertyType));
            }
            return instance;
        }

        private static object Clone(object source)
        {
            var clone = Activator.CreateInstance(source.GetType())!;
            foreach (var prop in WritableProperties(source.GetType()))
            {
                prop.SetValue(clone, prop.GetValue(source));
            }
            return clone;
        }

        // Produces a representative non-default value for any property type appearing on the
        // generated models: primitives, enums, nullable wrappers, List<T> (including nested lists),
        // and other model types (populated recursively).
        private static object CreateValue(Type type)
        {
            if (type == typeof(bool)) return true;
            if (type == typeof(long)) return 1L;
            if (type == typeof(double)) return 1.0;
            if (type == typeof(string)) return "value";
            if (type == typeof(DateTimeOffset)) return DateTimeOffset.UnixEpoch;

            var underlying = Nullable.GetUnderlyingType(type);
            if (underlying != null) return CreateValue(underlying);

            if (type.IsEnum) return Enum.GetValues(type).GetValue(0)!;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                var elementType = type.GetGenericArguments()[0];
                var list = (IList)Activator.CreateInstance(type)!;
                list.Add(CreateValue(elementType));
                return list;
            }

            if (type.IsClass) return Populate(Activator.CreateInstance(type)!);

            throw new NotSupportedException(
                $"ModelEqualityCoverageTests has no value strategy for {type} — teach CreateValue about it.");
        }

        // Produces a value guaranteed to differ from `current`, using the same repertoire of types
        // as CreateValue. Nullable/reference types simply flip null-ness; non-nullable value types
        // need a genuinely different value of the same type.
        private static object? CreateDifferentValue(Type type, object? current)
        {
            var isNullable = !type.IsValueType || Nullable.GetUnderlyingType(type) != null;
            if (isNullable) return current is null ? CreateValue(type) : null;

            if (type == typeof(bool)) return !(bool)current!;
            if (type == typeof(long)) return (long)current! + 1;
            if (type == typeof(double)) return (double)current! + 1;
            if (type == typeof(DateTimeOffset)) return ((DateTimeOffset)current!).AddDays(1);

            if (type.IsEnum)
            {
                foreach (var value in Enum.GetValues(type))
                {
                    if (!value.Equals(current)) return value;
                }
                throw new NotSupportedException($"Enum {type} has no alternate value to mutate to.");
            }

            throw new NotSupportedException(
                $"ModelEqualityCoverageTests has no alternate-value strategy for non-nullable {type} — teach CreateDifferentValue about it.");
        }
    }
}
