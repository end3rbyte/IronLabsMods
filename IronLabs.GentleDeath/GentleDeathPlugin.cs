using BepInEx;
using IronLabs.SharedLib;

namespace IronLabs.GentleDeath
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public sealed class GentleDeathPlugin : IronLabsPlugin
    {
        private const string PluginGuid = "IronLabs.GentleDeath";
        private const string PluginName = "IronLabs.GentleDeath";
        private const string PluginVersion = "1.0.4";

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
