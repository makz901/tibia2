using System.Linq;
using LiteNetLib;
using Team801.Tibia2.Client.Controllers;
using Team801.Tibia2.Client.Managers;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromServer;

namespace Team801.Tibia2.Client.PacketHandlers
{
    public class PlayerMovedPacketHandler : BasePacketHandler<CreatureMovedPacket>
    {
        private readonly GameStateManager _gameStateManager;
        private readonly IMovementController _movementController;

        public PlayerMovedPacketHandler(
            GameStateManager gameStateManager,
            IMovementController movementController)
        {
            _gameStateManager = gameStateManager;
            _movementController = movementController;
        }

        protected override void Handle(CreatureMovedPacket packet, NetPeer peer = null)
        {
            var creature = _gameStateManager.CreatureList.FirstOrDefault(x => x.Id == packet.CreatureId);
            if (creature != null)
            {
                creature.Position = packet.NewPosition;

                _movementController.Callbacks?.OnMoved(creature);
            }
        }
    }
}