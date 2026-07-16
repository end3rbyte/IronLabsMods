# Plugin ExpandedServer

IronLabs.ExpandedServer raises Valheim's server player limit from 10 to a
configurable value that defaults to 20.
It updates server admission checks and the capacities advertised through Steam
and PlayFab.

Every connecting client must use a compatible mod setup. Values above 100 are
limited to 100. Missing, non-numeric, zero, and negative values use the default
of 20. PlayFab Party can impose a lower backend capacity than the configured
limit.

Only the dedicated server or the player hosting the world configures the
limit. Joining clients do not need the `--maxplayer` argument; they use the
capacity advertised by the server through Steam or PlayFab.

## Valheim.exe Command Arguments Added

| Argument | Value | Behavior |
|---|---|---|
| `--maxplayer` | Integer from 1 to 100 | Sets the server or host player limit. Defaults to 20 when absent or invalid; values above 100 become 100. Clients do not use this argument. |

Example:

```text
Valheim.exe --maxplayer 50
```

For a dedicated server, add the same argument to the server launch command.
Do not add it to joining clients.

## Installation Sides

| Client required | Server required |
|---|---|
| Yes | Yes |

## Contact

Report bugs through [GitHub Issues](https://github.com/end3rbyte/IronLabsMods/issues).
For questions, feedback, and other discussions, use [GitHub Discussions](https://github.com/end3rbyte/IronLabsMods/discussions).
