using System;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using Team801.Tibia2.Core.Configuration;
using Team801.Tibia2.Core.Models;
using Team801.Tibia2.Core.Packets.FromClient;
using Team801.Tibia2.Core.Packets.FromServer;
using UnityEngine;

namespace Team801.Tibia2.Client
{
    public class Client : INetEventListener
    {
        private readonly NetManager _instance;
        private readonly PacketProcessor _packetProcessor = new PacketProcessor();

        private NetPeer _server;

        public Player Player { get; } = new Player();

        public Client()
        {
            _instance = new NetManager(this) {AutoRecycle = true};
        }

        public void OnFrameUpdated() => _instance.PollEvents();

        public void Connect(string username)
        {
            Console.WriteLine("Connecting to server...");

            Player.Username = username;

            _packetProcessor.SubscribeReusable<JoinAcceptedPacket>(OnJoinAccepted);
            _packetProcessor.SubscribeReusable<PlayerMovedPacket>(OnPlayerMoved);

            _instance.Start();
            _instance.Connect("localhost", 12345, "");
        }

        public void Move(Vector2 direction)
        {
            _packetProcessor.SendTo(_server, new MovePlayerPacket { Direction = direction });
        }

        private void OnPlayerMoved(PlayerMovedPacket packet)
        {
            Console.WriteLine($"You moved to a new position {packet.PositionState.Position}");
            Player.State = packet.PositionState;
        }

        private void OnJoinAccepted(JoinAcceptedPacket packet)
        {
            Console.WriteLine($"Join accepted by server");
            Player.State = packet.PositionState;
        }

        public void OnPeerConnected(NetPeer peer)
        {
            _server = peer;

            _packetProcessor.SendTo(_server, new JoinPacket { Username = Player.Username });
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