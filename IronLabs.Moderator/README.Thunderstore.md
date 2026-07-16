# Plugin Moderator

Give server administrators practical Valheim commands for moderation, testing,
teleportation, spawning, map control, and player management.

Moderator mode starts disabled. A server-authorized administrator can toggle it
with `moderator`; while enabled, `[Moderator]` appears in green above the
character and the moderation features become available.

The vanilla `devcommands` command is removed while Moderator is installed.
Vanilla cheat commands are hidden and blocked.

Administrators with moderator mode enabled can hold Left Shift and left-click
the large map to teleport to the selected terrain position.

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

The `moderator` toggle requires administrator access confirmed by the server.
Every other Moderator command requires both administrator access and enabled
moderator mode. Every invocation is logged at Info level with the administrator
name and command line.

After `exploremap`, connected players appear through Valheim's standard player
markers even when they do not publicly share their positions. Positions refresh
every 30 seconds. Tracking stops with `resetmap`, when moderator mode is
disabled, or when the session ends.

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

God mode and ghost mode follow moderator mode and are disabled when it is
toggled off or the session ends.

This is an administration and testing mod, not a balanced survival feature.
Install matching versions on the server and participating administrator clients.
The server must recognize each user in Valheim's `adminlist.txt`.

For complete behavior, installation details, and limitations, see the [full README](https://github.com/end3rbyte/IronLabsMods/blob/main/IronLabs.Moderator/README.md).

## Installation Sides

| Client required | Server required |
|---|---|
| Yes | Yes |

The server authoritatively validates administrator access.
## Contact

Report bugs through [GitHub Issues](https://github.com/end3rbyte/IronLabsMods/issues).
For questions, feedback, and other discussions, use [GitHub Discussions](https://github.com/end3rbyte/IronLabsMods/discussions).
