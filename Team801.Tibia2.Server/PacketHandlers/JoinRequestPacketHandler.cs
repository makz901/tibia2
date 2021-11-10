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
        private readonly IPlayerManager _playerManager;
        private readonly IServerPacketManager _serverPacketManager;

        public JoinRequestPacketHandler(
            IPlayerManager playerManager,
            IServerPacketManager serverPacketManager)
        {
            _playerManager = playerManager;
            _serverPacketManager = serverPacketManager;
        }

        protected override void Handle(JoinRequestPacket requestPacket, NetPeer peer = null)
        {
            if (peer == null) throw new ArgumentNullException(nameof(peer));

            var player = _playerManager.Get(peer.Id);
            if (player == null)
            {
                Console.WriteLine($"Player with ID:{peer.Id} is not connected");
                return;
            }

            Console.WriteLine($"Received join from character [{requestPacket.Username}] (pid: {peer.Id})");

            player.CurrentCharacter = new Character
            {
                Name = requestPacket.Username,
                Position = Vector2.Zero
            };

            var packetModel = new CreatureStatePacketModel
            {
                Id = player.CurrentCharacter.Id,
                Name = player.CurrentCharacter.Name,
                Position = player.CurrentCharacter.Position,
                Direction = player.CurrentCharacter.Direction
            };

            _serverPacketManager.Send(new JoinAcceptedPacket { CreatureState = packetModel }, peer);

            foreach (var nearByPlayer in _playerManager.GetNearby(player.CurrentCharacter.Position))
            {
                _serverPacketManager.Send(new CreatureAppearedPacket { State = packetModel }, nearByPlayer.Peer);
            }
        }
    }
}