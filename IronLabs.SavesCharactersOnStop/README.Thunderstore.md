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

## Installation

Install [ServerCharacters](https://thunderstore.io/c/valheim/p/Smoothbrain/ServerCharacters/) and this mod on the dedicated server and every client.

| Client required | Server required |
|---|---|
| Yes | Yes |

The server service must write its Valheim PID to `saves_characters_on_stop.drp` when stopping and wait at least 100 seconds before forcing termination. Copy the stop script and systemd configuration from the [GitHub README](https://github.com/end3rbyte/IronLabsMods/tree/main/IronLabs.SavesCharactersOnStop#service-configuration).

Read the [full documentation](https://github.com/end3rbyte/IronLabsMods/blob/main/IronLabs.SavesCharactersOnStop/README.md) on GitHub.

## Contact

Report bugs through [GitHub Issues](https://github.com/end3rbyte/IronLabsMods/issues).
For questions, feedback, and other discussions, use [GitHub Discussions](https://github.com/end3rbyte/IronLabsMods/discussions).
