# Plugin NoServerPassword

IronLabs.NoServerPassword allows a public or crossplay Valheim server to be
started with an empty password from the in-game server creation screen.

The plugin only relaxes validation for an empty password. Non-empty passwords
continue to use Valheim's vanilla minimum-length and world-name validation.
Worlds with a save-data error are not allowed to bypass the normal start checks.

Install the standalone `IronLabs.NoServerPassword.dll` plugin. The server then
appears without password protection, so anyone who
can discover or reach it can attempt to connect.

Joining players do not need the mod. A player hosting from the in-game menu must
install it because their game process acts as the server. Dedicated servers must
also install it.

## Installation Sides

| Client required | Server required |
|---|---|
| No | Yes |

## Contact

Report bugs through [GitHub Issues](https://github.com/end3rbyte/IronLabsMods/issues).
For questions, feedback, and other discussions, use [GitHub Discussions](https://github.com/end3rbyte/IronLabsMods/discussions).
