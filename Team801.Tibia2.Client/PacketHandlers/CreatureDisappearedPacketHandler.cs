using System;
using System.Linq;
using LiteNetLib;
using Team801.Tibia2.Client.Controllers;
using Team801.Tibia2.Client.Managers;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromServer;

namespace Team801.Tibia2.Client.PacketHandlers
{
    public class CreatureDisappearedPacketHandler : BasePacketHandler<CreatureDisappearedPacket>
    {
        private readonly GameStateManager _gameStateManager;
        private readonly ICharacterController _characterController;

        public CreatureDisappearedPacketHandler(
            GameStateManager gameStateManager,
            ICharacterController characterController)
        {
            _gameStateManager = gameStateManager;
            _characterController = characterController;
        }

        protected override void Handle(CreatureDisappearedPacket packet, NetPeer peer = null)
        {
            var existing = _gameStateManager.CreatureList.FirstOrDefault(x => x.Id == packet.CreatureId);
            if (existing != null)
            {
                _gameStateManager.CreatureList.Remove(existing);
                _characterController.Callbacks.OnDisappeared(new DateTime(packet.Timestamp), existing);
            }
        }
    }
}