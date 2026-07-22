# ServerStatus

Exposes live Valheim server information through a local read-only JSON endpoint.

## Features

- Reports server and world names, connected players and player capacity.
- Reports the current in-game day.
- Listens only on `127.0.0.1` for an on-machine website backend.
- Supports dedicated servers and peer-hosted worlds.

## Installation

| Client required | Server required |
|---|---|
| No | Yes |

## Configuration

| BepInEx setting | Default | Purpose |
|---|---:|---|
| `RPC.Port` | `8765` | Local port for `GET /status`. |

The JSON response contains `serverName`, `worldName`, `playerCount`, `maxPlayers`, `day`, `players`, and `worldCreatedAt`. The creation date is null because Valheim does not store it in the world save.

Read the [full documentation](https://github.com/end3rbyte/IronLabsMods/blob/main/IronLabs.ServerStatus/README.md) on GitHub.

## Contact

Report bugs through [GitHub Issues](https://github.com/end3rbyte/IronLabsMods/issues).
For questions, feedback, and other discussions, use [GitHub Discussions](https://github.com/end3rbyte/IronLabsMods/discussions).
