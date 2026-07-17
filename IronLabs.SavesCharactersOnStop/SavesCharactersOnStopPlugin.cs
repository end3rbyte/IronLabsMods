using System.Collections;
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
        private const string PluginVersion = "1.0.0";
        private float _elapsed;

        internal static ModLog Log { get; private set; }
        internal static GracefulShutdownCoordinator Coordinator { get; private set; }
        internal static SavesCharactersOnStopPlugin Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            Log = InitializePlugin(PluginGuid);
            Coordinator = new GracefulShutdownCoordinator();
            Log.LogInfo($"{PluginName} {PluginVersion} is loaded.");
        }

        internal void Run(IEnumerator routine)
        {
            StartCoroutine(routine);
        }

        private void FixedUpdate()
        {
            _elapsed += Time.fixedDeltaTime;
            if (_elapsed < 1f)
            {
                return;
            }

            _elapsed -= 1f;
            Coordinator.Tick();
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
