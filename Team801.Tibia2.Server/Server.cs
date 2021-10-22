using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using LiteNetLib.Utils;
using Team801.Tibia2.Core.Configuration;
using Team801.Tibia2.Core.Models;
using Team801.Tibia2.Core.Packets;
using UnityEngine;

namespace Team801.Tibia2.Server
{
    public class Server : INetEventListener
    {
        private readonly Dictionary<int, Player> _players = new Dictionary<int, Player>();

        private readonly NetManager _instance;
        private readonly NetDataWriter _writer;
        private readonly NetPacketProcessor _packetProcessor;

        public Server()
        {
            _instance = new NetManager(this) {AutoRecycle = true};
            _writer = new NetDataWriter();
            _packetProcessor = new PacketProcessor();
        }

        public void Start()
        {
            Console.WriteLine("Starting server");

            _packetProcessor.SubscribeReusable<JoinPacket, NetPeer>(OnJoinReceived);

            _instance.Start(12345);
        }

        private void SendPacket<T>(T packet, NetPeer peer, DeliveryMethod deliveryMethod) where T : class, new()
        {
            if (peer == null) return;

            _writer.Reset();
            _packetProcessor.Write(_writer, packet);
            peer.Send(_writer, deliveryMethod);
        }

        private void OnJoinReceived(JoinPacket packet, NetPeer peer)
        {
            Console.WriteLine($"Received join from {packet.Username} (pid: {peer.Id})");

            var newPlayer = new Player 
            {
                Peer = peer,
                State = new PlayerStatePacket
                {
                    Pid = peer.Id,
                    Position = Vector2.zero
                },
                Username = packet.Username
            };

            _players[peer.Id] = newPlayer;

            SendPacket(new JoinAcceptedPacket { PlayerStatePacket = newPlayer.State }, peer, DeliveryMethod.ReliableOrdered);
        }

        public void OnFrameUpdated() => _instance?.PollEvents();

        public void OnPeerConnected(NetPeer peer)
        {
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {
        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            _packetProcessor.ReadAllPackets(reader);
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