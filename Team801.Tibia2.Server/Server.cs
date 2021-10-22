using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using Team801.Tibia2.Core.Configuration;
using Team801.Tibia2.Core.Models;
using Team801.Tibia2.Core.Packets.FromClient;
using Team801.Tibia2.Core.Packets.FromServer;
using UnityEngine;

namespace Team801.Tibia2.Server
{
    public class Server : INetEventListener
    {
        private readonly NetManager _instance;
        private readonly Dictionary<int, Player> _players = new Dictionary<int, Player>();
        private readonly PacketProcessor _packetProcessor = new PacketProcessor();

        public Server()
        {
            _instance = new NetManager(this) {AutoRecycle = true};
        }

        public void Start()
        {
            Console.WriteLine("Starting server");

            _packetProcessor.SubscribeReusable<JoinPacket, NetPeer>(OnJoinReceived);
            _packetProcessor.SubscribeReusable<MovePlayerPacket, NetPeer>(OnPlayerMovementReceived);

            _instance.Start(12345);
        }

        private void OnPlayerMovementReceived(MovePlayerPacket input, NetPeer peer)
        {
            Console.WriteLine($"Received movement input {input.Direction} (pid: {peer.Id})");

            if (_players.TryGetValue(peer.Id, out var player))
            {
                input.Direction.Normalize();
                player.State.Position += input.Direction;

                _packetProcessor.SendTo(peer, new PlayerMovedPacket { PositionState = player.State });
            }
        }

        private void OnJoinReceived(JoinPacket packet, NetPeer peer)
        {
            Console.WriteLine($"Received join from {packet.Username} (pid: {peer.Id})");

            var newPlayer = new Player 
            {
                Peer = peer,
                State = new PositionState
                {
                    Pid = peer.Id,
                    Position = Vector2.zero
                },
                Username = packet.Username
            };

            _players[peer.Id] = newPlayer;

            _packetProcessor.SendTo(peer, new JoinAcceptedPacket { PositionState = newPlayer.State });
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
            if(_instance.ConnectedPeersCount < 10)
                request.Accept();
            else
                request.Reject();
        }
    }
}