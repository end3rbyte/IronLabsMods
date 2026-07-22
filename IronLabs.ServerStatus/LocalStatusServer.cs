using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using IronLabs.SharedLib;

namespace IronLabs.ServerStatus
{
    internal sealed class LocalStatusServer : IDisposable
    {
        private const int MaximumRequestLineLength = 2048;
        private readonly int _port;
        private readonly ModLog _log;
        private TcpListener _listener;
        private Thread _thread;
        private volatile bool _running;
        private string _response = StatusSnapshot.NotReadyJson;

        internal LocalStatusServer(int port, ModLog log)
        {
            _port = port;
            _log = log;
        }

        internal void Start()
        {
            try
            {
                _listener = new TcpListener(IPAddress.Loopback, _port);
                _listener.Start();
                _running = true;
                _thread = new Thread(Listen) { IsBackground = true, Name = "ServerStatus RPC" };
                _thread.Start();
                _log.LogInfo($"Server status RPC is listening on http://127.0.0.1:{_port}/status.");
            }
            catch (Exception exception)
            {
                _log.LogError($"Could not start the server status RPC: {exception}");
                Dispose();
            }
        }

        internal void SetResponse(string response)
        {
            Interlocked.Exchange(ref _response, response);
        }

        private void Listen()
        {
            while (_running)
            {
                try
                {
                    using (TcpClient client = _listener.AcceptTcpClient())
                    {
                        Handle(client);
                    }
                }
                catch (SocketException) when (!_running)
                {
                    return;
                }
                catch (Exception exception)
                {
                    _log.LogWarning($"Server status RPC request failed: {exception.Message}");
                }
            }
        }

        private void Handle(TcpClient client)
        {
            client.ReceiveTimeout = 2000;
            client.SendTimeout = 2000;
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream, Encoding.ASCII, false, 1024, true))
            {
                string requestLine = reader.ReadLine();
                bool valid = requestLine != null && requestLine.Length <= MaximumRequestLineLength &&
                             (requestLine.StartsWith("GET /status ") || requestLine.StartsWith("GET /status?"));
                WriteResponse(stream, valid ? "200 OK" : "404 Not Found",
                    valid ? Interlocked.CompareExchange(ref _response, null, null) : "{\"error\":\"not_found\"}");
            }
        }

        private static void WriteResponse(Stream stream, string status, string json)
        {
            byte[] body = Encoding.UTF8.GetBytes(json);
            string headers = $"HTTP/1.1 {status}\r\nContent-Type: application/json; charset=utf-8\r\n" +
                             $"Content-Length: {body.Length}\r\nCache-Control: no-store\r\nConnection: close\r\n\r\n";
            byte[] headerBytes = Encoding.ASCII.GetBytes(headers);
            stream.Write(headerBytes, 0, headerBytes.Length);
            stream.Write(body, 0, body.Length);
        }

        public void Dispose()
        {
            _running = false;
            _listener?.Stop();
            _listener = null;
            if (_thread != null && _thread != Thread.CurrentThread)
            {
                _thread.Join(2000);
            }
            _thread = null;
        }
    }
}
