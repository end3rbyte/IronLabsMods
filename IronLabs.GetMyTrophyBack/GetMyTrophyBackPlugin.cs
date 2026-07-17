using BepInEx;
using IronLabs.SharedLib;

namespace IronLabs.GetMyTrophyBack
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public sealed class GetMyTrophyBackPlugin : IronLabsPlugin
    {
        private const string PluginGuid = "IronLabs.GetMyTrophyBack";
        private const string PluginName = "IronLabs.GetMyTrophyBack";
        private const string PluginVersion = "1.0.3";

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
