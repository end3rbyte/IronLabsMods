# IronLabs Mods

This repository contains independently built Valheim plugins. See each
plugin's `README.md` for technical details and `README.Thunderstore.md` for a
concise player-oriented overview.

| Mod | Installation side | Description |
|---|---|---|
| [GentleDeath](IronLabs.GentleDeath/) | Client; server when required by a game mode | Keeps equipable gear on the player after death and moves other items to the tombstone. |
| [GetMyTrophyBack](IronLabs.GetMyTrophyBack/) | Both | Drops a mounted boss trophy five seconds after its guardian power is selected. |
| [Moderator](IronLabs.Moderator/) | Both | Adds multiplayer moderation commands gated by server-validated administrator access. |
| [QuickLaunch](IronLabs.QuickLaunch/) | Client-only | Automatically resumes the last local or multiplayer session by default. |
| [SavesCharactersOnStop](IronLabs.SavesCharactersOnStop/) | Both | Saves connected ServerCharacters profiles before a dedicated server stops. |
| [ExpandedServer](IronLabs.ExpandedServer/) | Both | Raises the server player limit. |
| [NoServerPassword](IronLabs.NoServerPassword/) | Server-only | Allows public and crossplay servers to start without a password. |
| [SealedTombstone](IronLabs.SealedTombstone/) | Both | Protects tombstones and lets their owners approve access. |

## Shared library

Every plugin references `IronLabs.SharedLib`, which provides the common plugin
base, Harmony registration, and logging. It is embedded into every standalone
plugin DLL and is never installed separately.

## Contact

Report bugs through [GitHub Issues](https://github.com/end3rbyte/IronLabsMods/issues).
For questions, feedback, and other discussions, use [GitHub Discussions](https://github.com/end3rbyte/IronLabsMods/discussions).
