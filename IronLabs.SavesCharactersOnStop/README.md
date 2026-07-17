# Plugin SavesCharactersOnStop

Saves every connected ServerCharacters profile before a dedicated Valheim
server stops or restarts. It complements ServerCharacters and prevents a
controlled shutdown from restoring players to an older character state.

## Installation Sides

| Client required | Server required |
|---|---|
| Yes | Yes |

Install the plugin together with ServerCharacters on every client and on the
dedicated server. Peer-hosted worlds are unaffected.

## Behavior

| Stage | Behavior |
|---|---|
| Stop request | The process supervisor asks the plugin to save connected characters. |
| Client save | Each client writes its profile and sends it through ServerCharacters. |
| Confirmation | The server confirms each ordered profile transfer and disconnects that player. |
| Completion | Valheim performs its normal application shutdown and world save. |
| Timeout | After 90 seconds, unconfirmed players are logged and shutdown continues. |

The plugin is automatic and has no configuration. It cannot recover data after
Valheim or the operating system has already crashed. A compatible process
supervisor is required on the dedicated-server host.

## Coordination Protocol

Runtime files are stored under `BepInEx/run/SavesCharactersOnStop/`.

| File | Purpose |
|---|---|
| `stop.supported` | Identifies the active Valheim process. |
| `stop.mode` | Optional one-shot `world` request that skips character saves. |
| `stop.request` | Contains the unique `quit:` or `world:` request. |
| `stop.ready` | Confirms the exact request after completion or timeout. |

| Request | Result |
|---|---|
| `save:<unique-value>` | Saves and disconnects all players, then keeps Valheim running. |
| `quit:<unique-value>` | Saves and disconnects all players, then performs the vanilla shutdown. |
| `world:<unique-value>` | Skips character saves and performs the vanilla shutdown. |

The supervisor must allow at least 100 seconds and must not forward the
original stop signal after receiving the matching confirmation.

The first supervisor installation cannot protect the already running process.
Install it while no players are connected; subsequent service stops and
restarts are coordinated automatically.

## Contact

Report bugs through [GitHub Issues](https://github.com/end3rbyte/IronLabsMods/issues).
For questions, feedback, and other discussions, use [GitHub Discussions](https://github.com/end3rbyte/IronLabsMods/discussions).
