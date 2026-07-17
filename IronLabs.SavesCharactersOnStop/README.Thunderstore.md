# Plugin SavesCharactersOnStop

Saves every connected ServerCharacters profile before a dedicated Valheim
server stops or restarts.

## Installation Sides

| Client required | Server required |
|---|---|
| Yes | Yes |

Install this plugin and ServerCharacters on every client and on the dedicated
server. The server host must use a compatible process supervisor. On a
controlled stop, clients save their current profiles, ServerCharacters writes
them on the server, and Valheim then performs its normal world save and exit.

The plugin is automatic, has no configuration, and is inactive in peer-hosted
worlds. It cannot recover progress after a process or machine crash.

## Contact

Report bugs through [GitHub Issues](https://github.com/end3rbyte/IronLabsMods/issues).
For questions, feedback, and other discussions, use [GitHub Discussions](https://github.com/end3rbyte/IronLabsMods/discussions).
