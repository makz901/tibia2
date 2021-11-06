using System;
using System.Net;
using System.Net.Sockets;
using Autofac;
using LiteNetLib;
using Team801.Tibia2.Client.Configuration;
using Team801.Tibia2.Client.Controllers;
using Team801.Tibia2.Client.Managers;
using Team801.Tibia2.Common.Configuration;
using Team801.Tibia2.Common.Packets.FromClient;

namespace Team801.Tibia2.Client
{
    public class Client : INetEventListener
    {
        private readonly NetManager _netManager;
        private readonly PacketProcessor _processor;
        private readonly ClientManager _clientManager;
        private string _requestedName;

        public IMovementController MovementController { get; }

        public Client()
        {
            _netManager = new NetManager(this) {AutoRecycle = true};

            var container = ClientConfig.Build();

            _processor = container.Resolve<PacketProcessor>();
            _clientManager = container.Resolve<ClientManager>();
            MovementController = container.Resolve<IMovementController>();
        }

        public void OnFrameUpdated() => _netManager.PollEvents();

        public void Connect(string name)
        {
            Console.WriteLine("Connecting to server...");
            _requestedName = name;

            _netManager.Start();
            _netManager.Connect("localhost", 12345, "");
        }

        public void OnPeerConnected(NetPeer peer)
        {
            _clientManager.Initialize(peer);
            _clientManager.SendToServer(new JoinRequestPacket { Username = _requestedName }, DeliveryMethod.ReliableOrdered);
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            Console.WriteLine("Disconnected");
            _requestedName = null;
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
        }
    }
}