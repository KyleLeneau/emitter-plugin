# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

* **Emmitter** is a NINA plugin that emits cloud-event wrapped data structures to multiple backends.
* Build with `dotnet build`
* Run tests with `dotnet test -v diagnostic`
* Run formatting and linting with `dotnet format -v diagnostic`
* Generate and Install the Models after a schema change with `./schema/build.sh all`
* Architecture decisions are documented as ADRs in `docs/adr/`. Design documents are in `docs/designs/`.
* Quicktype-generated models (`Generated/EmitterModels.cs`) have no C# record support, so device-info
  types used for polling dedupe (`data.Equals(lastData)` in Handlers) get hand-written `IEquatable<T>`
  partials in `src/Bortle.NINA.Emitter/Models/`. After a schema change touching one of these types,
  update its equality partial too — `ModelEqualityCoverageTests` will fail `dotnet test` if a field is
  missed. See [ADR-004](docs/adr/004-generated-model-value-equality.md).

## Conventions
- **Commit style:** Conventional commits (`add:`, `fix:`, `chore:`),
- **Line length:** 100 characters max
- **Indentation:** 4 spaces for C#, 2 for YAML/Markdown
