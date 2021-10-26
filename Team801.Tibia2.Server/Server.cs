using System;
using System.Net;
using System.Net.Sockets;
using Autofac;
using LiteNetLib;
using Team801.Tibia2.Core.Configuration;
using Team801.Tibia2.Server.Services.Contracts;
using Container = Team801.Tibia2.Server.Configuration.Container;

namespace Team801.Tibia2.Server
{
    public class Server : INetEventListener
    {
        private readonly NetManager _instance;

        private PacketProcessor _processor;
        private IPlayerManager _playerManager;

        public Server()
        {
            _instance = new NetManager(this) {AutoRecycle = true};

            Container.Build();
            Container.SetupHandlers();
        }

        public void Start()
        {
            Console.WriteLine("Starting server");

            _processor = Container.Instance.Resolve<PacketProcessor>();
            _playerManager = Container.Instance.Resolve<IPlayerManager>();

            _instance.Start(12345);
        }

        public void OnFrameUpdated() => _instance?.PollEvents();

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