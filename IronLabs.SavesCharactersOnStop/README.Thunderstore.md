# SavesCharactersOnStop

Saves connected ServerCharacters profiles before a dedicated server stops or restarts.

## Features

- Requests a profile save from every connected client.
- Waits for ServerCharacters to confirm each transfer.
- Disconnects saved players before shutdown.
- Performs the normal Valheim world save and exit afterward.
- Logs unconfirmed profiles and continues after a 90-second timeout.

## Requirements

| Dependency | Purpose |
|---|---|
| [ServerCharacters](https://thunderstore.io/c/valheim/p/Smoothbrain/ServerCharacters/) | Stores authoritative server profiles. |
| Compatible process supervisor | Coordinates dedicated-server service stops. |

- The plugin is automatic and has no configuration.
- Peer-hosted worlds are unaffected.
- Process or machine crashes cannot be recovered.
- The first supervisor installation must occur with no players connected.

## Installation

| Client required | Server required |
|---|---|
| Yes | Yes |

Install matching versions with ServerCharacters on every client and the dedicated server.

## Contact

Report bugs through [GitHub Issues](https://github.com/end3rbyte/IronLabsMods/issues).
For questions, feedback, and other discussions, use [GitHub Discussions](https://github.com/end3rbyte/IronLabsMods/discussions).
