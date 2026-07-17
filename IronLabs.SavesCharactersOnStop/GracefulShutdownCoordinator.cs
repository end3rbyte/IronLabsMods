using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using BepInEx;
using UnityEngine;

namespace IronLabs.SavesCharactersOnStop
{
    internal sealed class GracefulShutdownCoordinator : IDisposable
    {
        internal const string SaveRequestRpc = "SavesCharactersOnStop SaveRequest";
        internal const string SaveStartedRpc = "SavesCharactersOnStop SaveStarted";
        private const string RequestFileName = "stop.request";
        private const string ReadyFileName = "stop.ready";
        private const string SupportedFileName = "stop.supported";
        private readonly HashSet<ZNetPeer> _pendingPeers = new HashSet<ZNetPeer>();
        private readonly Dictionary<ZNetPeer, string> _startedRequests = new Dictionary<ZNetPeer, string>();
        private DateTime _deadlineUtc;
        private bool _quitAfterSave;
        private string _requestId;

        internal void Tick()
        {
            ZNet network = ZNet.instance;
            if (network == null || !network.IsServer() || !Application.isBatchMode)
            {
                return;
            }

            EnsureSupportedMarker();
            if (_requestId == null)
            {
                TryStart(network);
            }
            else if (DateTime.UtcNow >= _deadlineUtc)
            {
                CompleteAfterTimeout();
            }
        }

        internal void RecordSavedProfile(PlayerProfile profile)
        {
            if (_requestId == null)
            {
                return;
            }

            ZNetPeer peer = _pendingPeers.FirstOrDefault(candidate =>
                _startedRequests.TryGetValue(candidate, out string startedRequest) &&
                startedRequest == _requestId && ServerCharactersIdentity.Matches(profile, candidate));
            if (peer == null || !_pendingPeers.Remove(peer))
            {
                return;
            }

            _startedRequests.Remove(peer);
            SavesCharactersOnStopPlugin.Log.LogMessage(
                $"Confirmed graceful character save for {peer.m_playerName} ({_pendingPeers.Count} remaining).");
            ZNet.instance.Disconnect(peer);
            if (_pendingPeers.Count == 0)
            {
                Complete();
            }
        }

        internal void RecordSaveStarted(ZRpc peerRpc, string startedRequestId)
        {
            ZNetPeer peer = ZNet.instance?.GetPeers().FirstOrDefault(candidate => candidate.m_rpc == peerRpc);
            if (peer != null && _pendingPeers.Contains(peer) && startedRequestId == _requestId)
            {
                _startedRequests[peer] = startedRequestId;
            }
        }

        public void Dispose()
        {
            DeleteOwnedMarker(SupportedFileName);
        }

        private void TryStart(ZNet network)
        {
            string path = GetPath(RequestFileName);
            if (!File.Exists(path))
            {
                return;
            }

            _requestId = File.ReadAllText(path).Trim();
            if (string.IsNullOrWhiteSpace(_requestId))
            {
                _requestId = null;
                return;
            }

            StartRequest(network, path);
        }

        private void StartRequest(ZNet network, string path)
        {
            _quitAfterSave = _requestId.StartsWith("quit:", StringComparison.Ordinal) ||
                _requestId.StartsWith("world:", StringComparison.Ordinal);
            File.Delete(path);
            DeleteIfPresent(GetPath(ReadyFileName));
            if (_requestId.StartsWith("world:", StringComparison.Ordinal))
            {
                CompleteWorldOnlyRequest();
                return;
            }

            _deadlineUtc = DateTime.UtcNow.AddSeconds(90);
            _pendingPeers.UnionWith(network.GetPeers().Where(IsConnected));
            SavesCharactersOnStopPlugin.Log.LogMessage(
                $"Graceful shutdown requested; saving {_pendingPeers.Count} connected character(s).");
            RequestProfiles();
        }

        private void RequestProfiles()
        {
            foreach (ZNetPeer peer in _pendingPeers)
            {
                peer.m_rpc.Invoke(SaveRequestRpc, _requestId);
            }
            if (_pendingPeers.Count == 0)
            {
                Complete();
            }
        }

        private void CompleteWorldOnlyRequest()
        {
            File.WriteAllText(GetPath(ReadyFileName), _requestId);
            SavesCharactersOnStopPlugin.Log.LogMessage(
                "World-only shutdown requested; skipping connected character saves.");
            FinishRequest();
        }

        private void Complete()
        {
            File.WriteAllText(GetPath(ReadyFileName), _requestId);
            SavesCharactersOnStopPlugin.Log.LogMessage(
                "All connected character profiles were written to disk; the server is ready to stop.");
            FinishRequest();
        }

        private void CompleteAfterTimeout()
        {
            string players = string.Join(", ", _pendingPeers.Select(peer => peer.m_playerName).ToArray());
            SavesCharactersOnStopPlugin.Log.LogWarning(
                $"The 90-second shutdown save timeout expired with {_pendingPeers.Count} unsaved character(s): {players}.");
            foreach (ZNetPeer peer in _pendingPeers)
            {
                ZNet.instance.Disconnect(peer);
            }
            _pendingPeers.Clear();
            _startedRequests.Clear();
            File.WriteAllText(GetPath(ReadyFileName), _requestId);
            SavesCharactersOnStopPlugin.Log.LogWarning(
                "Continuing the vanilla server shutdown after the character save timeout.");
            FinishRequest();
        }

        private void FinishRequest()
        {
            _requestId = null;
            _startedRequests.Clear();
            if (!_quitAfterSave)
            {
                return;
            }

            _quitAfterSave = false;
            SavesCharactersOnStopPlugin.Log.LogMessage("Starting the vanilla application shutdown.");
            Application.Quit();
        }

        private static bool IsConnected(ZNetPeer peer)
        {
            return peer?.m_socket?.IsConnected() == true;
        }

        private static string GetPath(string name)
        {
            return Path.Combine(GetRuntimeDirectory(), name);
        }

        private static string GetRuntimeDirectory()
        {
            return Path.Combine(Paths.BepInExRootPath, "run", "SavesCharactersOnStop");
        }

        private static void EnsureSupportedMarker()
        {
            Directory.CreateDirectory(GetRuntimeDirectory());
            string path = GetPath(SupportedFileName);
            string processId = Process.GetCurrentProcess().Id.ToString();
            if (!File.Exists(path) || File.ReadAllText(path).Trim() != processId)
            {
                File.WriteAllText(path, processId);
            }
        }

        private static void DeleteOwnedMarker(string name)
        {
            string path = GetPath(name);
            string processId = Process.GetCurrentProcess().Id.ToString();
            if (File.Exists(path) && File.ReadAllText(path).Trim() == processId)
            {
                File.Delete(path);
            }
        }

        private static void DeleteIfPresent(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
