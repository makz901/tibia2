using System;
using Godot;
using LiteNetLib;
using Team801.Tibia2.Common.Models.Creature;
using Team801.Tibia2.Common.Models.Player;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromClient;
using Team801.Tibia2.Common.Packets.FromServer;
using Team801.Tibia2.Common.Packets.Models;
using Team801.Tibia2.Server.Events;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.PacketHandlers
{
    public class JoinRequestPacketHandler : BasePacketHandler<JoinRequestPacket>
    {
        private readonly IPlayerManager _playerManager;
        private readonly CharacterEvents _characterEvents;
        private readonly IServerPacketManager _serverPacketManager;

        public JoinRequestPacketHandler(
            IPlayerManager playerManager,
            CharacterEvents characterEvents,
            IServerPacketManager serverPacketManager)
        {
            _playerManager = playerManager;
            _characterEvents = characterEvents;
            _serverPacketManager = serverPacketManager;
        }

        protected override void Handle(JoinRequestPacket requestPacket, NetPeer peer = null)
        {
            if (peer == null) throw new ArgumentNullException(nameof(peer));

            var existingPlayer = _playerManager.Get(peer.Id);
            if (existingPlayer == null)
            {
                Console.WriteLine($"Player with ID:{peer.Id} is not connected");
                return;
            }

            Console.WriteLine($"Received join from character [{requestPacket.Username}] (pid: {peer.Id})");

            var player = _playerManager.Get(peer.Id);
            var newChar = new Character
            {
                Name = requestPacket.Username,
                Position = Vector2.Zero
            };

            if (player == null)
            {
                player = new Player(peer) {CurrentCharacter = newChar};
                _playerManager.Add(peer.Id, player);
            }
            else
            {
                player.CurrentCharacter = newChar;
            }

            var packetModel = new CreatureStatePacketModel
            {
                Id = newChar.Id,
                Name = newChar.Name,
                Position = newChar.Position,
                Direction = newChar.Direction
            };

            _serverPacketManager.Send(new JoinAcceptedPacket { CreatureState = packetModel }, peer);
            // _characterEvents.Add(player.CurrentCharacter);

            foreach (var nearByPlayer in _playerManager.GetNearby(newChar.Position))
            {
                _serverPacketManager.Send(new CreatureAppearedPacket { State = packetModel }, nearByPlayer.Peer);
            }
        }
    }
}