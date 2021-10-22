using System;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using LiteNetLib.Utils;
using Team801.Tibia2.Core.Configuration;
using Team801.Tibia2.Core.Models;
using Team801.Tibia2.Core.Packets.FromClient;
using Team801.Tibia2.Core.Packets.FromServer;

namespace Team801.Tibia2.Client
{
    public class Client : INetEventListener
    {
        private readonly Player _player = new Player();

        private readonly NetManager _instance;
        private readonly NetDataWriter _writer;
        private readonly NetPacketProcessor _packetProcessor;

        private NetPeer _server;

        public Client()
        {
            _instance = new NetManager(this) {AutoRecycle = true};
            _writer = new NetDataWriter();
            _packetProcessor = new PacketProcessor();
        }

        public void OnFrameUpdated() => _instance.PollEvents();

        public void Connect(string username)
        {
            Console.WriteLine("Connecting to server...");

            _player.Username = username;

            _packetProcessor.SubscribeReusable<JoinAcceptedPacket>(OnJoinAccept);

            _instance.Start();
            _instance.Connect("localhost", 12345, "");
        }

        private void SendPacket<T>(T packet, DeliveryMethod deliveryMethod) where T : class, new()
        {
            if (_server == null) return;

            _writer.Reset();
            _packetProcessor.Write(_writer, packet);
            _server.Send(_writer, deliveryMethod);
        }

        private void OnJoinAccept(JoinAcceptedPacket packet)
        {
            Console.WriteLine($"Join accepted by server (pid: {packet.PlayerState.Pid})");
            _player.State = packet.PlayerState;
        }

        public void OnPeerConnected(NetPeer peer)
        {
            _server = peer;

            SendPacket(new JoinPacket { Username = _player.Username }, DeliveryMethod.ReliableOrdered);
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