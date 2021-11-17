using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Autofac;
using LiteNetLib;
using Team801.Tibia2.Common.Configuration;
using Team801.Tibia2.Server.Configuration;
using Team801.Tibia2.Server.Game.Contracts;
using Team801.Tibia2.Server.Models;
using Team801.Tibia2.Server.Services;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server
{
    public class GameServer : INetEventListener
    {
        public static IContainer IoC { get; private set; }

        private const int Port = 12345;

        private readonly NetManager _instance;
        private readonly PacketProcessor _processor;
        private readonly IPlayerManager _playerManager;
        private readonly IGameActionsDispatcher _gameActionsDispatcher;

        private bool _running;

        public GameServer()
        {
            var container = ServerConfig.Build();
            IoC = container;

            _instance = new NetManager(this) {AutoRecycle = true};
            _processor = container.Resolve<PacketProcessor>();
            _playerManager = container.Resolve<IPlayerManager>();
            _gameActionsDispatcher = container.Resolve<IGameActionsDispatcher>();
        }


        public void Start()
        {
            Console.WriteLine("Starting server...");

            _instance.Start(Port);
            _gameActionsDispatcher.Start(new CancellationToken());

            Console.WriteLine($"Listening on port: {Port}");

            _running = true;
            while (_running)
            {
                _instance?.PollEvents();
            }
        }

        public void Stop()
        {
            _instance.DisconnectAll();
            _running = false;
        }

        public void OnPeerConnected(NetPeer peer)
        {
            var existingPlayer = _playerManager.Get(peer.Id);
            if (existingPlayer == null)
            {
                _playerManager.Add(peer.Id, new Player(peer));
            }

            Console.WriteLine($"Player with ID:{peer.Id} is now connected.");
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            var player = _playerManager.Get(peer.Id);
            if (player == null)
            {
                return;
            }

            _playerManager.Remove(peer.Id);
            Console.WriteLine($"Player with ID:{peer.Id} disconnected.");
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