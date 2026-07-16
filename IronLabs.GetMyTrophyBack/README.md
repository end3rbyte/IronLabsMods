# Plugin GetMyTrophyBack

Recover boss trophies after using them to unlock powers at Valheim's
Sacrificial Stones.

## Behavior

| Event | Result |
|---|---|
| A trophy is mounted | The associated power remains available normally |
| A player selects that power | A five-second trophy return timer starts |
| The timer finishes | The mounted trophy drops as a recoverable world item |
| Another request reaches an empty stone | No additional trophy is created |

The drop preserves the trophy's stored item data. Removal is performed by the
network owner of the stone and synchronized to all peers, so simultaneous power
selections cannot duplicate the trophy.

## Installation Sides

| Client required | Server required |
|---|---|
| Yes | Yes |

Every player and the server must install the plugin. The activating client
starts the timer, while the peer owning the Sacrificial Stone performs the
authoritative network drop.

## Compatibility

| Area | Behavior |
|---|---|
| Vanilla powers and trophies | Supported |
| Modded guardian stones using vanilla `ItemStand` powers | Supported |
| Trophy already removed before the timer finishes | Safely ignored |

## Contact

Report bugs through [GitHub Issues](https://github.com/end3rbyte/IronLabsMods/issues).
For questions, feedback, and other discussions, use [GitHub Discussions](https://github.com/end3rbyte/IronLabsMods/discussions).
