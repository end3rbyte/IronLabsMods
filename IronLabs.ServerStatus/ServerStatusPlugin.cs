using System;
using BepInEx;
using BepInEx.Configuration;
using IronLabs.SharedLib;

namespace IronLabs.ServerStatus
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public sealed class ServerStatusPlugin : IronLabsPlugin
    {
        private const string PluginGuid = "IronLabs.ServerStatus";
        private const string PluginName = "IronLabs.ServerStatus";
        private const string PluginVersion = "1.0.0";
        private const int DefaultPort = 8765;

        private ConfigEntry<int> _port;
        private LocalStatusServer _server;
        private float _nextSnapshotTime;
        internal static ModLog Log { get; private set; }

        private void Awake()
        {
            Log = InitializePlugin(PluginGuid);
            _port = Config.Bind("RPC", "Port", DefaultPort,
                "Local HTTP port used by the server status RPC.");
            Log.LogInfo($"{PluginName} {PluginVersion} is loaded.");
        }

        private void Update()
        {
            if (ZNet.instance == null || !ZNet.instance.IsServer())
            {
                return;
            }

            EnsureServerStarted();
            if (UnityEngine.Time.unscaledTime < _nextSnapshotTime)
            {
                return;
            }

            _nextSnapshotTime = UnityEngine.Time.unscaledTime + 1f;
            _server.SetResponse(StatusSnapshot.CreateJson());
        }

        private void EnsureServerStarted()
        {
            if (_server != null)
            {
                return;
            }

            int port = _port.Value;
            if (port < 1 || port > 65535)
            {
                Log.LogWarning($"Invalid RPC port {port}; using {DefaultPort}.");
                port = DefaultPort;
            }

            _server = new LocalStatusServer(port, Log);
            _server.Start();
        }

        private void OnDestroy()
        {
            _server?.Dispose();
            _server = null;
            ShutdownPlugin();
            Log = null;
        }
    }
}
