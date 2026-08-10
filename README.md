# Emitter

[![Actions status](https://github.com/KyleLeNeau/emitter-plugin/actions/workflows/ci.yml/badge.svg)](https://github.com/KyleLeNeau/emitter-plugin/actions)

Emitter is a [NINA](https://nighttime-imaging.eu/) plugin that sends events to a variety of backends including nats.io, webhook &amp; kafka

## Featues

TBD

## Requirements

* [dotnet](https://github.com/dotnet/sdk) cli or visual studio (cross platform buildable, I'm on a mac)
* [quicktype](https://quicktype.io/) to generate schema `npm install -g quicktype`

## Installation

### Manual

Download the zip file from the [releases](https://github.com/KyleLeneau/emitter-plugin/releases) and extract the contents to `%APPDATA%\Local\NINA\Plugins\3.0.0`

### Automatic

Add `https://kyleleneau.github.io/emitter-plugin/` as a NINA plugin repository and check for updates.

## Uninstallation

You can uninstall in NINA or delete the plugin in `%APPDATA%\Local\NINA\Plugins\3.0.0`.

## Usage

TBD

## Roadmap

Please see [ROADMAP.md](./ROADMAP.md) for more details.

## Contributing

PRs are welcome & appreciated! See the [contributing guide](./CONTRIBUTING.md) to get started.

## FAQ

TBD

## License

Emitter is licensed under:

- BSD-3-Clause license ([LICENSE](LICENSE) or <https://opensource.org/licenses/BSD-3-Clause>)

Unless you explicitly state otherwise, any contribution intentionally submitted for inclusion in Emitter by you, as defined in the BSD-3-Clause license, shall be dually licensed as above, without any additional terms or conditions.
