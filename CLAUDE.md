# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

* **Emmitter** is a NINA plugin that emits cloud-event wrapped data structures to multiple backends.
* Build with `dotnet build`
* Run tests with `dotnet test -v diagnostic`
* Run formatting and linting with `dotnet format -v diagnostic`
* Generate and Install the Models after a schema change with `./schema/build.sh all`
* Architecture decisions are documented as ADRs in `docs/adr/`. Design documents are in `docs/designs/`.

## Conventions
- **Commit style:** Conventional commits (`add:`, `fix:`, `chore:`),
- **Line length:** 100 characters max
- **Indentation:** 4 spaces for C#, 2 for YAML/Markdown
