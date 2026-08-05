using NINA.Equipment.Equipment.MyWeatherData;
using NINA.Equipment.Interfaces;
using NINA.Equipment.Interfaces.Mediator;
using NINA.Equipment.Interfaces.ViewModel;

namespace Bortle.NINA.Emitter.Tests.Fakes
{

    /// <summary>
    /// Minimal fake covering only the RegisterConsumer/RemoveConsumer members WeatherHandler
    /// actually uses; the rest of the wide IDeviceMediator surface is unused in these tests.
    /// </summary>
    public class FakeWeatherDataMediator : IWeatherDataMediator
    {
        public List<IWeatherDataConsumer> RegisteredConsumers { get; } = new List<IWeatherDataConsumer>();

        public void RegisterConsumer(IWeatherDataConsumer consumer) => this.RegisteredConsumers.Add(consumer);

        public void RemoveConsumer(IWeatherDataConsumer consumer) => this.RegisteredConsumers.Remove(consumer);

        public void RegisterHandler(IWeatherDataVM handler) => throw new NotImplementedException();

        public Task<IList<string>> Rescan() => throw new NotImplementedException();

        public Task<bool> Connect() => throw new NotImplementedException();

        public Task Disconnect() => throw new NotImplementedException();

        public void Broadcast(WeatherDataInfo deviceInfo) => throw new NotImplementedException();

        public WeatherDataInfo GetInfo() => throw new NotImplementedException();

        public string Action(string actionName, string actionParameters) => throw new NotImplementedException();

        public string SendCommandString(string command, bool raw) => throw new NotImplementedException();

        public bool SendCommandBool(string command, bool raw) => throw new NotImplementedException();

        public void SendCommandBlind(string command, bool raw) => throw new NotImplementedException();

        public IDevice GetDevice() => throw new NotImplementedException();

#pragma warning disable CS0067 // unused: WeatherHandler doesn't subscribe to these
        public event Func<object, EventArgs, Task>? Connected;

        public event Func<object, EventArgs, Task>? Disconnected;
#pragma warning restore CS0067
    }
}
