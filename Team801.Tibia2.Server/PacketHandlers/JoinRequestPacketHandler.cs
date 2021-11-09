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
            if (existingPlayer != null)
            {
                Console.WriteLine($"Character with name {requestPacket.Username} already connected (pid: {peer.Id})");
                return;
            }

            Console.WriteLine($"Received join from {requestPacket.Username} (pid: {peer.Id})");
            
            var newPlayer = new Player(peer)
            {
                CurrentCharacter = new Character 
                {
                    Name = requestPacket.Username,
                    Position = Vector2.Zero
                }
            };

            _playerManager.Add(peer.Id, newPlayer);
            _characterEvents.Add(newPlayer.CurrentCharacter);

            // foreach (var nearByPlayer in _playerManager.GetNearby(newPlayer.CurrentCharacter.Position))
            // {
            // }
        }
    }
}