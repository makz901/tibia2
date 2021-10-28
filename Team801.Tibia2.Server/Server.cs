using System;
using System.Net;
using System.Net.Sockets;
using Autofac;
using LiteNetLib;
using Team801.Tibia2.Common.Configuration;
using Team801.Tibia2.Server.Configuration;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server
{
    public class Server : INetEventListener
    {
        private const int Port = 12345;

        private readonly NetManager _instance;

        private PacketProcessor _processor;
        private IPlayerManager _playerManager;
        private IGameTimer _gameTimer;

        private DateTime _lastUpdateTime = DateTime.Now;

        public Server()
        {
            ServerConfig.Build();

            _instance = new NetManager(this) {AutoRecycle = true};
        }

        public void Start()
        {
            Console.WriteLine("Starting server...");

            _processor = ServerConfig.IoC.Resolve<PacketProcessor>();
            _playerManager = ServerConfig.IoC.Resolve<IPlayerManager>();
            _gameTimer = ServerConfig.IoC.Resolve<IGameTimer>();

            _instance.Start(Port);

            Console.WriteLine($"Listening on port: {Port}");
        }

        public void OnFrameUpdated()
        {
            _gameTimer.FrameDeltaTime = DateTime.Now - _lastUpdateTime;
            _instance?.PollEvents();
            _lastUpdateTime = DateTime.Now;
        }

        public void OnPeerConnected(NetPeer peer)
        {
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            var player = _playerManager.Get(peer.Id);
            if (player == null)
            {
                return;
            }

            _playerManager.Remove(peer.Id);
            Console.WriteLine($"Player {player.Username} left the game");
        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {
        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            _processor.ReadAllPackets(reader, peer);
        }

        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {
        }

        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {
        }

        public void OnConnectionRequest(ConnectionRequest request)
        {
            if(_instance.ConnectedPeersCount < 10)
                request.Accept();
            else
                request.Reject();
        }
    }
}