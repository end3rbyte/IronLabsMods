# Plugin GentleDeath

Keep your weapons, armor, and tools with you after death. Only the treasures
and raw materials gathered along your journey are moved to your tombstone.

IronLabs.GentleDeath changes the Valheim death inventory split. Equipable items
remain in the player's inventory and retain their equipped state, while
non-equipable items are moved to the tombstone.

Equipable items are determined by Valheim's vanilla `ItemData.IsEquipable()`
classification and include weapons, tools, armor, shields, bows, ammunition,
torches, utility items, and trinkets.

This behavior replaces the configured vanilla world death-penalty inventory
rules. If a tombstone unexpectedly lacks space, an item is safely retained in
the player's inventory and a warning is logged.

GentleDeath works client-side by itself. A game-mode plugin may nevertheless
declare it as a mandatory dependency on both sides; in that case, install the
DLL on the server as required by that game mode.

## Installation Sides

| Client required | Server required |
|---|---|
| Yes | No |

## Contact

Report bugs through [GitHub Issues](https://github.com/end3rbyte/IronLabsMods/issues).
For questions, feedback, and other discussions, use [GitHub Discussions](https://github.com/end3rbyte/IronLabsMods/discussions).
