using BepInEx;
using IronLabs.SharedLib;

namespace IronLabs.SealedTombstone
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public sealed class SealedTombstonePlugin : IronLabsPlugin
    {
        private const string PluginGuid = "IronLabs.SealedTombstone";
        private const string PluginName = "IronLabs.SealedTombstone";
        private const string PluginVersion = "1.0.0";


        internal static ModLog Log { get; private set; }

        private void Awake()
        {
            Log = InitializePlugin(PluginGuid);
            Log.LogInfo($"{PluginName} {PluginVersion} is loaded.");
        }

        private void Update()
        {
            TombstoneAccess.Tick();
        }

        private void OnDestroy()
        {
            TombstoneAccess.ResetSession();
            ShutdownPlugin();
            Log = null;
        }
    }
}
