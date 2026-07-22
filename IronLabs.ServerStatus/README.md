# ServerStatus

Exposes live Valheim server information through a local read-only JSON endpoint.

## Features

- Reports the server and world names, player list and current player count.
- Reports the active maximum player count and current in-game day.
- Listens only on `127.0.0.1` so an on-machine website backend can query it safely.
- Returns `worldCreatedAt` as `null` because Valheim does not store a creation date in its world save.
- Supports dedicated servers and peer-hosted worlds.

## Installation

| Client required | Server required |
|---|---|
| No | Yes |

## Configuration

| BepInEx setting | Default | Purpose |
|---|---:|---|
| `RPC.Port` | `8765` | Local TCP port for the HTTP endpoint. |

Restart the Valheim server after changing the port.

## RPC

Request the current snapshot from the same machine as Valheim:

```http
GET http://127.0.0.1:8765/status
```

Example response:

```json
{
  "ready": true,
  "serverName": "My server",
  "worldName": "Dedicated",
  "playerCount": 2,
  "maxPlayers": 20,
  "day": 148,
  "worldCreatedAt": null,
  "players": ["Astrid", "Eirik"]
}
```

| Field | Type | Meaning |
|---|---|---|
| `ready` | Boolean | Whether a server world is currently available. |
| `serverName` | String or null | Value supplied through Valheim's `-name` argument. |
| `worldName` | String | Loaded world name. |
| `playerCount` | Number | Number of connected players. |
| `maxPlayers` | Number | Vanilla, ExpandedServer default, or `--maxplayer` capacity. |
| `day` | Number | Current in-game day. |
| `worldCreatedAt` | String or null | Always null until Valheim stores this value in the world save. |
| `players` | String array | Connected character names. |

The endpoint is intentionally unavailable from other machines. Expose it through the website backend, with the backend's own authentication and HTTPS controls.

## Contact

Report bugs through [GitHub Issues](https://github.com/end3rbyte/IronLabsMods/issues).
For questions, feedback, and other discussions, use [GitHub Discussions](https://github.com/end3rbyte/IronLabsMods/discussions).
