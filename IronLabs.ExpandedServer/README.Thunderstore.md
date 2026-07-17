# ExpandedServer

Raises Valheim's server capacity to a configurable player limit.

## Features

- Raises the default server limit from 10 to 20 players by default.
- Updates admission checks and advertised Steam or PlayFab capacity.
- Supports dedicated servers and peer-hosted worlds.
- Caps the configured limit at 100 players.

## Valheim.exe Command Switches

| Switch | Default | Purpose |
|---|---:|---|
| `--maxplayer <1-100>` | `20` | Sets the server or host capacity. Joining clients do not use it. |

- Every connecting player needs a compatible mod setup.
- Larger populations increase server, network, and simulation load.
- PlayFab Party may impose a lower backend capacity.

## Installation

| Client required | Server required |
|---|---|
| Yes | Yes |

## Contact

Report bugs through [GitHub Issues](https://github.com/end3rbyte/IronLabsMods/issues).
For questions, feedback, and other discussions, use [GitHub Discussions](https://github.com/end3rbyte/IronLabsMods/discussions).
