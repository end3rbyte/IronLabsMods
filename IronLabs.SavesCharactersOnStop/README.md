# SavesCharactersOnStop

Saves connected ServerCharacters profiles before a dedicated server stops or restarts.

## Features

- Requests a synchronous profile save from every connected client.
- Waits for ServerCharacters to confirm each profile transfer.
- Disconnects saved players before the application exits.
- Performs Valheim's normal world save and shutdown afterward.
- Continues shutdown after a 90-second timeout and logs unconfirmed profiles.
- Supports save-only, graceful quit, and world-only supervisor requests.

## Requirements

| Dependency | Purpose |
|---|---|
| [ServerCharacters](https://github.com/blaxxun-boop/ServerCharacters/) | Stores authoritative character profiles on the server. |
| Compatible process supervisor | Coordinates service stops with the plugin. |

- The mod is automatic and has no configuration.
- It affects dedicated servers only; peer-hosted worlds are unaffected.
- It cannot recover data after a process or operating-system crash.
- Install the supervisor for the first time while no players are connected.

## Coordination Protocol

Runtime files are stored under `BepInEx/run/SavesCharactersOnStop/`.

| File | Purpose |
|---|---|
| `stop.supported` | Identifies the active Valheim process. |
| `stop.mode` | Selects an optional one-shot world-only stop. |
| `stop.request` | Contains the unique supervisor request. |
| `stop.ready` | Confirms completion or timeout for that request. |

| Request | Result |
|---|---|
| `save:<id>` | Saves and disconnects players while keeping Valheim running. |
| `quit:<id>` | Saves and disconnects players, then shuts Valheim down. |
| `world:<id>` | Skips character requests and performs the vanilla shutdown. |

The supervisor must allow at least 100 seconds and must not forward the original stop signal after receiving the matching confirmation.

## Installation

| Client required | Server required |
|---|---|
| Yes | Yes |

Install matching versions together with ServerCharacters on every client and the dedicated server.

## Contact

Report bugs through [GitHub Issues](https://github.com/end3rbyte/IronLabsMods/issues).
For questions, feedback, and other discussions, use [GitHub Discussions](https://github.com/end3rbyte/IronLabsMods/discussions).
