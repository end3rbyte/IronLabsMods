using BepInEx;
using IronLabs.SharedLib;

namespace IronLabs.NoServerPassword
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public sealed class NoServerPasswordPlugin : IronLabsPlugin
    {
        private const string PluginGuid = "IronLabs.NoServerPassword";
        private const string PluginName = "IronLabs.NoServerPassword";
        private const string PluginVersion = "1.0.6";

        internal static ModLog Log { get; private set; }

        private void Awake()
        {
            Log = InitializePlugin(PluginGuid);
            Log.LogInfo($"{PluginName} {PluginVersion} is loaded.");
        }

        private void OnDestroy()
        {
            ShutdownPlugin();
            Log = null;
        }
    }
}
