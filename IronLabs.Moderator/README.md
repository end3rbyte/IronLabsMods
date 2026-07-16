# Plugin Moderator

Plugin logging is written through the shared `ModLog` wrapper with local
timestamps and BepInEx log levels.

IronLabs.Moderator provides server-authorized moderation commands for Valheim.

## Commands

| Command | Purpose | Access |
|---|---|---|
| `moderator` | Enables or disables moderator mode for the current session. | Administrator |
| `exploremap` | Reveals the complete local map and continuously shows connected players. | Administrator |
| `goto <player>` | Teleports the local administrator to another player. | Administrator |
| `itemset <biome>` | Replaces current items with a Vanilla item set for `meadows`, `blackforest`, `swamp`, `mountain`, or `plains`. | Administrator |
| `playerlist` | Lists online players with `[Moderator]` after administrator names. | Administrator |
| `summon <player>` | Teleports a named player to the local administrator. | Administrator |
| `resetmap` | Clears local map exploration. | Administrator |
| `spawn` | Spawns a named Valheim prefab. | Administrator |

Moderator mode is disabled by default when a session starts. The `moderator`
command requires administrator access confirmed by the server and toggles the
mode. Every other Moderator command requires both confirmed administrator
access and enabled moderator mode. Commands are hidden from `help` when their
requirements are not met. Every invocation is recorded at Info level with the
administrator name and the complete command line.

The vanilla `devcommands` command is not registered while Moderator is
installed. Vanilla cheat commands are hidden from help and autocomplete, and
their execution is blocked.

## Valheim Vanilla Administrator Commands

These commands are provided by Valheim Vanilla, not by Moderator. The server
only accepts them from players listed in its `adminlist.txt`.

| Command | Purpose |
|---|---|
| `kick <name/ip/userID>` | Disconnects a player from the server. |
| `ban <name/ip/userID>` | Adds a player to the server ban list. |
| `unban <ip/userID>` | Removes a player from the server ban list. |
| `banned` | Lists banned players. |
| `save` | Forces the server to save the world and player profiles. |
| `optterrain` | Converts old terrain modifications to the current format. |

The `itemset` command uses Valheim's item sets for its first five biomes.
Existing items are placed in a tombstone; there is no option to keep the
current inventory.

God mode and ghost mode are enabled with moderator mode. They are disabled when
the mode is toggled off, the session ends, or administrator validation fails.

After `exploremap`, connected players appear through Valheim's standard player
markers even when they do not publicly share their positions. Positions refresh
every 30 seconds. Tracking remains active until `resetmap`, moderator mode is
disabled, or the session ends.

Administrators with moderator mode enabled can hold Left Shift and left-click
the large map to teleport to the selected terrain position. A
normal left-click keeps its vanilla behavior when the modifier is not held.

`[Moderator]` is appended in green to an administrator's name while
moderator mode is enabled. This display state is replicated to other players
without changing the character profile name.

## Installation Sides

| Client required | Server required |
|---|---|
| Yes | Yes |

The server validates administrator status against its Valheim `adminlist.txt`
and sends the result to participating clients. Matching Moderator versions
must be installed on the server and clients.

## Contact

Report bugs through [GitHub Issues](https://github.com/end3rbyte/IronLabsMods/issues).
For questions, feedback, and other discussions, use [GitHub Discussions](https://github.com/end3rbyte/IronLabsMods/discussions).
