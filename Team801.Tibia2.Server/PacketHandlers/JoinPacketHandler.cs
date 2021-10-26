using System;
using LiteNetLib;
using Team801.Tibia2.Core.Models;
using Team801.Tibia2.Core.PacketHandlers;
using Team801.Tibia2.Core.Packets.FromClient;
using Team801.Tibia2.Core.Packets.FromServer;
using Team801.Tibia2.Server.Services.Contracts;
using UnityEngine;

namespace Team801.Tibia2.Server.PacketHandlers
{
    public class JoinPacketHandler : BasePacketHandler<JoinPacket>
    {
        private readonly IPacketManager _packetManager;
        private readonly IPlayerManager _playerManager;

        public JoinPacketHandler(
            IPacketManager packetManager,
            IPlayerManager playerManager)
        {
            _packetManager = packetManager;
            _playerManager = playerManager;
        }

        public override void Handle(JoinPacket packet, NetPeer peer)
        {
            Console.WriteLine($"Received join from {packet.Username} (pid: {peer.Id})");

            var newPlayer = new Player 
            {
                Peer = peer,
                State = new PlayerState
                {
                    Pid = peer.Id,
                    Position = Vector2.zero
                },
                Username = packet.Username
            };

            _playerManager.Add(newPlayer);

            _packetManager.QueuePacket(new JoinAcceptedPacket { PlayerState = newPlayer.State }, peer);
        }
    }
}