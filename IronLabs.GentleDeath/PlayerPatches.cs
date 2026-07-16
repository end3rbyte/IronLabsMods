using HarmonyLib;

namespace IronLabs.GentleDeath
{
    [HarmonyPatch(typeof(Player), "CreateTombStone")]
    internal static class CreateTombstonePatch
    {
        private static bool Prefix(Player __instance)
        {
            DeathInventory.CreateTombstone(__instance);
            return false;
        }
    }
}
