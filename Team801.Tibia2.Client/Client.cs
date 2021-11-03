using System;
using System.Net;
using System.Net.Sockets;
using Autofac;
using LiteNetLib;
using Team801.Tibia2.Client.Configuration;
using Team801.Tibia2.Client.Controllers;
using Team801.Tibia2.Client.Services;
using Team801.Tibia2.Common.Packets.FromClient;

namespace Team801.Tibia2.Client
{
    public class Client : INetEventListener
    {
        private readonly ClientNetManager _netManager;

        public IMovementController MovementController { get; }

        public Client()
        {
            ClientConfig.Build(this);

            _netManager = ClientConfig.IoC.Resolve<ClientNetManager>();
            MovementController = ClientConfig.IoC.Resolve<IMovementController>();
        }

        public void OnFrameUpdated() => _netManager.PollEvents();

        public void Connect()
        {
            Console.WriteLine("Connecting to server...");

            _netManager.Start();
            _netManager.Connect("localhost", 12345, "");
        }

        public void OnPeerConnected(NetPeer peer)
        {
            _netManager.Send(new JoinPacket { Username =  });
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {
        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod) 
        {
            _netManager.ReadAllPackets(reader, peer);
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