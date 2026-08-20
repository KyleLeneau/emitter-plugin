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
* [x] deploy landing page & documentation to github pages
* [x] gitops release & CD automation
* [x] deploy plugin/manifests file to github pages
* [x] setup release process
* [x] create and deploy logo/image github pages
* [x] generate asyncapi website / docs
* [x] not sure device info skipping and equality is working
* [x] I need a new equality check pattern in handlers to avoid the duplicates (ugh)
* [x] Update asyncapi docs to match the events and topics being sent
* [ ] implement kafka backend
* [ ] re-look at image saved event and include more data
* [ ] re-look at sequence handler data and if/how to get more
* [ ] rust nats example (filter and re-publish?)
* [ ] full test and run through of the plugin with a setup

### Events
* [x] Device connection
* [x] Safety info
* [x] Safety is_safe change
* [x] weather info
* [x] camera info
* [x] camera download timeout
* [x] dome info
* [x] dome shutter event
* [x] filter wheel info
* [x] filter wheel changed
* [x] flat panel info
* [x] flat panel brightness change
* [x] flat panel led toggle
* [x] flat panel open/close
* [x] focuser info
* [x] focuser focus change, start, end
* [x] guider info
* [x] guider after dither
* [x] guider guide step
* [x] guider suiding start / stop
* [x] mount info
* [x] mount flip
* [x] rotator info
* [x] rotator moved
* [x] rotator moved mechanical
* [x] rotator synced
* [x] switch info
* [x] profile selected
* [x] profile locale change
* [x] profile location change
* [x] profile horizon change
* [x] profile collection change
* [ ] image saved
* [x] TS wait start
* [x] TS new target start
* [x] TS target start
* [x] TS container stop
* [x] TS target complete
* [ ] sequence starting
* [ ] sequence finish
* [ ] sequence custom (need a sequence action)

### Bugs


### Future

* [ ] Sequence item to emit a generic event defined by a user and still wrapped in a cloudevent
* [ ] Allow for configuring which events / domains to emit
* [ ] A dockable with current status of queue or connections on emitting
* [ ] Refactor project so Core of Emitter can not depend on NINA.Plugin and be testable

### Parking Lot


### Housekeeping

* [ ] Bump NINA.Plugin nuget to 3.3.0 to resolve ToastNotification and VVVV.FreeImage warnings, then remove `<NoWarn>$(NoWarn);NU1701</NoWarn>` from csproj.
