using System.Linq;
using LiteNetLib;
using Team801.Tibia2.Client.Controllers;
using Team801.Tibia2.Client.Managers;
using Team801.Tibia2.Common.Models.Creature;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromServer;

namespace Team801.Tibia2.Client.PacketHandlers
{
    public class CreatureAppearedPacketHandler : BasePacketHandler<CreatureAppearedPacket>
    {
        private readonly GameStateManager _gameStateManager;
        private readonly ICharacterController _characterController;

        public CreatureAppearedPacketHandler(
            GameStateManager gameStateManager,
            ICharacterController characterController)
        {
            _gameStateManager = gameStateManager;
            _characterController = characterController;
        }

        protected override void Handle(CreatureAppearedPacket packet, NetPeer peer = null)
        {
            var existing = _gameStateManager.CreatureList.FirstOrDefault(x => x.Id == packet.State.Id);
            if (existing == null)
            {
                var creature = new Character
                {
                    Id = packet.State.Id,
                    Name = packet.State.Name,
                    Direction = packet.State.Direction,
                    Position = packet.State.Position,
                };

                _gameStateManager.CreatureList.Add(creature);
                _characterController.Callbacks.OnAppeared(creature);
            }
        }
    }
}