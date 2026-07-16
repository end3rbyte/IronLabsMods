# Plugin QuickLaunch

Plugin logging is written to both BepInEx and `System.Diagnostics.Debug` through
the `ModLog` wrapper.

IronLabs.QuickLaunch automatically resumes the last Valheim session by default.
Launch the game with `--quicklaunch false` to disable automatic session resume.

## Valheim.exe Command Arguments Added

| Argument | Value | Behavior |
|---|---|---|
| `--quicklaunch` | <code>true\|false</code> | Automatic session resume is enabled by default; set this argument to `false` to disable it. |

For a local session, it selects the remembered character and world. For a Join
multi-player session, it selects the remembered character and reconnects to the first
valid entry in Valheim's recent-server list.

The plugin records whether a manually started session is local or multiplayer.
Before that preference has been recorded, local play remains the fallback.

QuickLaunch stops at the main menu if the remembered character, local world, or
recent multiplayer server is unavailable. It does not store or manage server
passwords.

## Installation Sides

| Client required | Server required |
|---|---|
| Yes | No |

## Contact

Report bugs through [GitHub Issues](https://github.com/end3rbyte/IronLabsMods/issues).
For questions, feedback, and other discussions, use [GitHub Discussions](https://github.com/end3rbyte/IronLabsMods/discussions).
