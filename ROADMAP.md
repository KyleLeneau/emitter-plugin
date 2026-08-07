## Roadmap

### Immediate

* [x] Update contributing
* [x] docs / adr directory
* [x] gitops CI / CD
* [x] create skeleton nina plugin
* [x] decide on linting and formatting
* [x] update CLAUDE.md with conventions
* [x] setup NINAPlugin.targets file for msbuild step
* [x] Pass CI build number into project
* [x] decide on directory layout
* [x] Update readme
* [x] create schema directory, scripts and CLAUDE.md convention
* [x] build asyncapi documentation html
* [x] architect enque to sink data flow for backends
* [x] implement nats io backend
* [x] implement webhook backend
* [x] Global options configuration for 1 or more sinks
* [x] Setup MVVM for Global Options
* [x] Allow nats subject prefix to be specified / changed
* [x] Need a better pattern to hold the handlers & nina interfaces and not have everything bloat in root plugin
* [x] Implement skeleton handlers
* [x] Build an example webhook endpoint that logs using python
* [x] setup example webhook servers (rust, ts, python) client uses
* [ ] deploy documentation and plugin/manifests file to github pages + logo
* [ ] gitops release & CD automation
* [ ] setup release process
* [ ] create images and artifacts for deployment
* [ ] implement kafka backend

### Bugs

* [ ] Update asyncapi docs to match the events and topics being sent

### Future

* [ ] generate asyncapi website / docs
* [ ] Sequence item to emit a generic event defined by a user and still wrapped in a cloudevent
* [ ] Allow for configuring which events / domains to emit
* [ ] A dockable with current status of queue or connections on emitting
* [ ] Refactor project so Core of Emitter can not depend on NINA.Plugin and be testable

### Parking Lot


### Housekeeping

* [ ] Bump NINA.Plugin nuget to 3.3.0 to resolve ToastNotification and VVVV.FreeImage warnings, then remove `<NoWarn>$(NoWarn);NU1701</NoWarn>` from csproj.
