using System.Collections;
using System.Threading;
using BepInEx;
using IronLabs.SharedLib;
using UnityEngine;

namespace IronLabs.CharacterVault
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("org.bepinex.plugins.servercharacters", BepInDependency.DependencyFlags.HardDependency)]
    public sealed class CharacterVaultPlugin : IronLabsPlugin
    {
        private const string PluginGuid = "IronLabs.CharacterVault";
        private const string PluginName = "IronLabs.CharacterVault";
        private const string PluginVersion = "1.0.2";
        internal static ModLog Log { get; private set; }
        internal static GracefulShutdownCoordinator Coordinator { get; private set; }
        internal static CharacterVaultPlugin Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            Log = InitializePlugin(PluginGuid);
            Coordinator = new GracefulShutdownCoordinator(SynchronizationContext.Current);
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
            ShutdownPlugin();
            Log = null;
        }
    }
}
