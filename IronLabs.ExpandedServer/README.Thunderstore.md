# Plugin ExpandedServer

Raise your Valheim server capacity from 10 to a configurable player limit that
defaults to 20.

The mod updates connection admission and the player capacity advertised through
Steam and PlayFab, making it suitable for larger communities and events.
Only the dedicated server or the player hosting the world configures the limit.
Joining clients need the mod but do not need the command-line argument.

## Valheim.exe Command Arguments Added

| Argument | Value | Behavior |
|---|---|---|
| `--maxplayer` | Integer from 1 to 100 | Sets the server or host limit. Invalid or absent values use 20; values above 100 become 100. Joining clients do not use it. |

## Important

- Every connecting player needs a compatible mod setup.
- Larger populations increase server, network, and world-simulation load.
- PlayFab Party can impose a lower backend capacity than the configured limit.

For complete behavior, installation details, and limitations, see the [full README](https://github.com/end3rbyte/IronLabsMods/blob/main/IronLabs.ExpandedServer/README.md).

## Installation Sides

| Client required | Server required |
|---|---|
| Yes | Yes |

## Contact

Report bugs through [GitHub Issues](https://github.com/end3rbyte/IronLabsMods/issues).
For questions, feedback, and other discussions, use [GitHub Discussions](https://github.com/end3rbyte/IronLabsMods/discussions).
