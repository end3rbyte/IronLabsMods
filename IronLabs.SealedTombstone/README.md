# Plugin SealedTombstone

IronLabs.SealedTombstone prevents players from looting another player's recent
tombstone without permission.

When a player interacts with a locked tombstone, its online owner receives a
Valheim vanilla Yes/No popup. Yes permanently unlocks that tombstone; No rejects
the request. Requests expire after 30 seconds, and requesters must wait two
minutes before sending another request.

Tombstones automatically become accessible after ten in-game days. Tombstones
created before the plugin recorded a lock day remain protected until their owner
accepts a request. Every participating client must have the plugin installed.

Any player who killed or injured the owner during the two minutes before their
death is recorded on that tombstone's permanent deny list. Those players cannot
send an access request for that tombstone, so its owner receives no popup from
them.

## Installation Sides

| Client required | Server required |
|---|---|
| Yes | Yes |

## Contact

Report bugs through [GitHub Issues](https://github.com/end3rbyte/IronLabsMods/issues).
For questions, feedback, and other discussions, use [GitHub Discussions](https://github.com/end3rbyte/IronLabsMods/discussions).
