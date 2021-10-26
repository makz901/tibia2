using System;
using System.Net;
using System.Net.Sockets;
using Autofac;
using LiteNetLib;
using Team801.Tibia2.Client.Services.Contracts;
using Team801.Tibia2.Core.Configuration;
using Team801.Tibia2.Core.Packets.FromClient;
using UnityEngine;
using Container = Team801.Tibia2.Client.Configuration.Container;

namespace Team801.Tibia2.Client
{
    public class Client : INetEventListener
    {
        private readonly NetManager _instance;
        private readonly PacketProcessor _packetProcessor;

        private NetPeer _server;

        public IPlayerManager PlayerManager { get; }

        public Client()
        {
            _instance = new NetManager(this) {AutoRecycle = true};

            Container.Build();
            Container.SetupHandlers();

            PlayerManager = Container.Instance.Resolve<IPlayerManager>();
            _packetProcessor = Container.Instance.Resolve<PacketProcessor>();
        }

        public void OnFrameUpdated() => _instance.PollEvents();

        public void Connect(string username)
        {
            Console.WriteLine("Connecting to server...");

            PlayerManager.Player.Username = username;

            _instance.Start();
            _instance.Connect("localhost", 12345, "");
        }

        public void Move(Vector2 direction)
        {
            _packetProcessor.SendTo(_server, new MovePlayerPacket { Direction = direction });
        }

        public void OnPeerConnected(NetPeer peer)
        {
            _server = peer;

            _packetProcessor.SendTo(_server, new JoinPacket { Username = PlayerManager.Player.Username });
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {
        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod) 
        {
            _packetProcessor.ReadAllPackets(reader, peer);
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