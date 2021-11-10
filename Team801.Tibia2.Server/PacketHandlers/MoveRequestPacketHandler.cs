using System;
using LiteNetLib;
using Team801.Tibia2.Common.Models.Creature;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromClient;
using Team801.Tibia2.Server.Configuration;
using Team801.Tibia2.Server.Events;
using Team801.Tibia2.Server.Game;
using Team801.Tibia2.Server.Services;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.PacketHandlers
{
    public class MoveRequestPacketHandler : BasePacketHandler<MoveRequestPacket>
    {
        private readonly IPlayerManager _playerManager;
        private readonly IGameActionsDispatcher _gameActionsDispatcher;
        private readonly CreatureEvents _creatureEvents;

        public MoveRequestPacketHandler(
            IPlayerManager playerManager,
            IGameActionsDispatcher gameActionsDispatcher,
            CreatureEvents creatureEvents)
        {
            _playerManager = playerManager;
            _gameActionsDispatcher = gameActionsDispatcher;
            _creatureEvents = creatureEvents;
        }

        protected override void Handle(MoveRequestPacket packet, NetPeer peer = null)
        {
            if (peer == null) throw new ArgumentNullException(nameof(peer));

            var direction = packet.Direction;

            Console.WriteLine($"Received movement input {direction} (pid: {peer.Id})");

            var character = _playerManager.Get(peer.Id)?.CurrentCharacter;
            if (character != null)
            {
                _gameActionsDispatcher.AddEvent(new GameAction(() => character.Position = character.Move(direction)));
                _creatureEvents.Move(character, direction);
            }
        }
    }
}