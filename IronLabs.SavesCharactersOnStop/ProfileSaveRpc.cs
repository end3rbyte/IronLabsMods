using System;
using System.Collections;
using System.Text.RegularExpressions;
using HarmonyLib;

namespace IronLabs.SavesCharactersOnStop
{
    [HarmonyPatch(typeof(ZNet), "OnNewConnection")]
    internal static class ProfileSaveRequestRpc
    {
        private static void Postfix(ZNet __instance, ZNetPeer peer)
        {
            if (!__instance.IsServer())
            {
                peer.m_rpc.Register<string>(
                    GracefulShutdownCoordinator.SaveRequestRpc, SaveProfile);
                return;
            }

            peer.m_rpc.Register<string>(GracefulShutdownCoordinator.SaveStartedRpc,
                SavesCharactersOnStopPlugin.Coordinator.RecordSaveStarted);
        }

        private static void SaveProfile(ZRpc serverRpc, string requestId)
        {
            if (Game.instance?.GetPlayerProfile() == null)
            {
                return;
            }

            SavesCharactersOnStopPlugin.Instance.Run(SaveWhenReady(serverRpc, requestId));
        }

        private static IEnumerator SaveWhenReady(ZRpc serverRpc, string requestId)
        {
            while (ServerCharactersTransferTracker.IsSendingProfile)
            {
                yield return null;
            }

            SavesCharactersOnStopPlugin.Log.LogMessage(
                $"Saving the character for graceful shutdown request {requestId}.");
            serverRpc.Invoke(GracefulShutdownCoordinator.SaveStartedRpc, requestId);
            Game.instance.SavePlayerProfile(true);
        }
    }

    [HarmonyPatch(typeof(ZRpc), nameof(ZRpc.Invoke))]
    internal static class ServerCharactersTransferTracker
    {
        private const string ProfileRpc = "ServerCharacters PlayerProfile";
        internal static bool IsSendingProfile { get; private set; }

        private static void Prefix(string method, object[] parameters)
        {
            if (method != ProfileRpc || parameters?.Length != 1 || !(parameters[0] is ZPackage package))
            {
                return;
            }

            int position = package.GetPos();
            try
            {
                if (package.Size() < sizeof(long) + sizeof(int) + sizeof(int))
                {
                    return;
                }

                package.SetPos(0);
                package.ReadLong();
                int fragment = package.ReadInt();
                int fragments = package.ReadInt();
                if (fragments > 0 && fragment >= 0 && fragment < fragments)
                {
                    IsSendingProfile = fragment < fragments - 1;
                }
            }
            finally
            {
                package.SetPos(position);
            }
        }
    }

    [HarmonyPatch(typeof(PlayerProfile), "SavePlayerToDisk")]
    internal static class SavedProfilePatch
    {
        private static void Postfix(PlayerProfile __instance, bool __result)
        {
            if (__result && ZNet.instance?.IsServer() == true)
            {
                SavesCharactersOnStopPlugin.Coordinator.RecordSavedProfile(__instance);
            }
        }
    }

    internal static class ServerCharactersIdentity
    {
        internal static bool Matches(PlayerProfile profile, ZNetPeer peer)
        {
            string host = peer.m_socket.GetHostName();
            string id = Regex.IsMatch(host, @"^\d+$") ? "Steam_" + host : host;
            string expected = id + "_" + peer.m_playerName.ToLower();
            return string.Equals(profile.m_filename, expected, StringComparison.Ordinal);
        }
    }
}
