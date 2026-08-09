# 002 — Automated Github release process based on tags

## Context

I would like to have an automated github release process (CD) based on the pushing of semantic version tags. This process is inspired by photonyx process (https://github.com/KyleLeneau/photonyx/blob/main/.github/workflows/release.yml) which was setup with cargo dist. The CD process for the plugin should do the following things:

* Kicked off with pushing a tag that matches a semantic version defined in .github/workflows/release.yml
* Automatic parsing of the changelog.md file for this matching version. The contents returned will be part of the github release notes.
* An assembly version should be constructed using the tag version plus the build number like: `0.1.0.{build-number}
* The computed version + build should be passed to dotnet build with property name `VersionNumber`
* The build that is done should be in release mode
* The build should be stagged so as to not create a github release until all assets are ready
* A github release with the version number for the title should be created
* The zip and sha256 of the build should be uploaded to the github release
* The body of the release should be combined with the template in .github/workflows/release_template.md, using the change log contents parsed earlier
* If the build fails or the release note creation fails then the github release shoul not be created

---

## Decision

Implemented in `.github/workflows/release.yml` as three jobs:

1. **prepare** (ubuntu-latest) — resolves the triggering tag (from the push, or
   from a `workflow_dispatch` `tag` input for re-running a failed release
   without re-tagging) into a `version_core` (`major.minor.patch`, with an
   optional leading `v` stripped) and a `version_number`
   (`{version_core}.{github.run_number}`). It then extracts the matching
   `## <version_core>` section from `CHANGELOG.md` and splices it into
   `.github/release_template.md` in place of the `<!-- CHANGELOG -->` marker
   to produce the release body. Failure to find/parse the changelog section
   fails this job, so no release is created (`build`/`release` never run).
2. **build** (windows-latest, matching `ci.yml`) — runs
   `dotnet build -c Release -p:VersionNumber={version_number}`. The existing
   `NINAPlugin.targets` `SetPluginVersion`/`Assemble` targets stamp the
   assembly version and package `build/{version_number}-Emitter-Release.zip`
   + `.sha256` as part of the build itself — no separate zipping step is
   needed in the workflow.
3. **release** (ubuntu-latest, needs both) — downloads the rendered notes and
   the zip/sha256, then creates the GitHub release **as a draft** with those
   assets attached (`softprops/action-gh-release`), and only as a final step
   publishes it (`gh release edit --draft=false`). This staging means a
   partially-assembled release is never visible, and if `prepare` or `build`
   fails, the `release` job — and the draft — never happen at all.

Tags are matched as bare (`0.1.0`) or `v`-prefixed (`v0.1.0`)
`major.minor.patch` only; pre-release suffixes (`-rc.1`) are not currently
supported, since neither the changelog format nor the ADR's example called
for them — can be added later if needed. The release title and git tag both
use the tag as pushed (no re-formatting).

The `pull_request:` trigger present in the original skeleton (copied from a
cargo-dist-based workflow) was dropped — it doesn't apply here and PR
validation is already covered by `ci.yml`. A `workflow_dispatch` input was
added instead, so a failed release run can be retried against the same tag
without deleting/re-pushing it.
