# SavesCharactersOnStop

With this mod [ServerCharacters](https://thunderstore.io/c/valheim/p/Smoothbrain/ServerCharacters/) will automatically save all connected characters when the server stops or restarts.

## Features

- Requests a profile save from every connected client.
- Waits for ServerCharacters to confirm each transfer.
- Processes up to four profile saves at a time.
- Disconnects saved players before shutdown.
- Performs the normal Valheim world save and exit afterward.
- Logs unconfirmed profiles and continues after a 90-second timeout.
- Intercepts a private dedicated-server exit request without a separate supervisor.
- Detects its private exit request immediately through filesystem notifications.
- Lets administrators restart a compatible systemd-managed server gracefully.

## Commands

| Command | Access | Description |
|---|---|---|
| `restartserver` | Administrator | Saves characters and the world, then restarts the dedicated server. |

## Valheim.exe Command Arguments Added

| Argument | Default | Description |
|---|---|---|
| `--disable-restart-command` | `false` | Disables the `restartserver` command. |

## Installation

Install [ServerCharacters](https://thunderstore.io/c/valheim/p/Smoothbrain/ServerCharacters/) and this mod on the dedicated server and every client.

For `restartserver` to relaunch the dedicated server, configure its systemd service with:

```ini
[Service]
Restart=always
RestartSec=10
```

An explicit `systemctl stop valheim.service` still leaves the service stopped. See the full documentation for the complete service configuration.

| Client required | Server required |
|---|---|
| Yes | Yes |

Read the [full documentation](https://github.com/end3rbyte/IronLabsMods/blob/main/IronLabs.SavesCharactersOnStop/README.md) on GitHub.

## Contact

Report bugs through [GitHub Issues](https://github.com/end3rbyte/IronLabsMods/issues).
For questions, feedback, and other discussions, use [GitHub Discussions](https://github.com/end3rbyte/IronLabsMods/discussions).
