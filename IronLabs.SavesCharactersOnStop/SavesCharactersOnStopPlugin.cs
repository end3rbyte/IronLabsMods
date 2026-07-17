using System;
using System.Collections;
using System.Linq;
using System.Threading;
using BepInEx;
using IronLabs.SharedLib;
using UnityEngine;

namespace IronLabs.SavesCharactersOnStop
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("org.bepinex.plugins.servercharacters", BepInDependency.DependencyFlags.HardDependency)]
    public sealed class SavesCharactersOnStopPlugin : IronLabsPlugin
    {
        private const string PluginGuid = "IronLabs.SavesCharactersOnStop";
        private const string PluginName = "IronLabs.SavesCharactersOnStop";
        private const string PluginVersion = "1.0.1";
        private const string DisableRestartCommandSwitch = "--disable-restart-command";
        internal static ModLog Log { get; private set; }
        internal static GracefulShutdownCoordinator Coordinator { get; private set; }
        internal static SavesCharactersOnStopPlugin Instance { get; private set; }
        internal static bool RestartCommandEnabled { get; private set; }

        private void Awake()
        {
            Instance = this;
            Log = InitializePlugin(PluginGuid);
            RestartCommandEnabled = !Environment.GetCommandLineArgs().Contains(DisableRestartCommandSwitch);
            Coordinator = new GracefulShutdownCoordinator(SynchronizationContext.Current);
            RestartServerCommand.Register();
            Log.LogDebug($"The restartserver command is " +
                $"{(RestartCommandEnabled ? "enabled" : "disabled")}.");
            Log.LogInfo($"{PluginName} {PluginVersion} is loaded.");
        }

        internal void Run(IEnumerator routine)
        {
            StartCoroutine(routine);
        }

        internal void QuitNextFrame()
        {
            StartCoroutine(QuitAfterCurrentFrame());
        }

        private static IEnumerator QuitAfterCurrentFrame()
        {
            yield return null;
            Application.Quit();
        }

        private void OnDestroy()
        {
            Coordinator?.Dispose();
            Coordinator = null;
            Instance = null;
            RestartCommandEnabled = false;
            ShutdownPlugin();
            Log = null;
        }
    }
}
