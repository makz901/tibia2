using System;
using LiteNetLib;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromClient;
using Team801.Tibia2.Server.Game.Actions.Creatures;
using Team801.Tibia2.Server.Game.Contracts;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.PacketHandlers
{
    public class MoveRequestPacketHandler : BasePacketHandler<MoveRequestPacket>
    {
        private readonly IPlayerManager _playerManager;
        private readonly IGameActionsDispatcher _gameActionsDispatcher;

        public MoveRequestPacketHandler(
            IPlayerManager playerManager,
            IGameActionsDispatcher gameActionsDispatcher)
        {
            _playerManager = playerManager;
            _gameActionsDispatcher = gameActionsDispatcher;
        }

        protected override void Handle(MoveRequestPacket packet, NetPeer peer = null)
        {
            if (peer == null) throw new ArgumentNullException(nameof(peer));

            Console.WriteLine($"Received movement input {packet.Direction} (pid: {peer.Id})");

            var character = _playerManager.Get(peer.Id)?.CurrentCharacter;
            if (character != null)
            {
                _gameActionsDispatcher.AddAction(new CreatureMoveGameAction(character, packet.Direction));
            }
        }
    }
}
