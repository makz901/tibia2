using System;
using Godot;
using LiteNetLib;
using Team801.Tibia2.Common.Models.Creature;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromClient;
using Team801.Tibia2.Common.Packets.FromServer;
using Team801.Tibia2.Common.Packets.Models;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.PacketHandlers
{
    public class JoinRequestPacketHandler : BasePacketHandler<JoinRequestPacket>
    {
        private readonly IServerManager _serverManager;
        private readonly IPlayerManager _playerManager;

        public JoinRequestPacketHandler(
            IServerManager serverManager,
            IPlayerManager playerManager)
        {
            _serverManager = serverManager;
            _playerManager = playerManager;
        }

        protected override void Handle(JoinRequestPacket requestPacket, NetPeer peer = null)
        {
            if (peer == null) throw new ArgumentNullException(nameof(peer));

            Console.WriteLine($"Received join from {requestPacket.Username} (pid: {peer.Id})");

            var newPlayer = new Player 
            {
                Name = requestPacket.Username,
                Position = Vector2.Zero
            };

            _playerManager.Add(peer, newPlayer);

            var packetModel = new PlayerStatePacketModel
            {
                Name = newPlayer.Name,
                Position = newPlayer.Position,
                Direction = newPlayer.Direction
            };

            _serverManager.QueuePacket(new JoinAcceptedPacket { PlayerState = packetModel }, peer);
        }
    }
}