using System;
using LiteNetLib;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromClient;
using Team801.Tibia2.Server.Game;
using Team801.Tibia2.Server.Services;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.PacketHandlers
{
    public class MoveRequestPacketHandler : BasePacketHandler<MoveRequestPacket>
    {
        private readonly IPlayerManager _playerManager;
        private readonly IServerPacketManager _serverPacketManager;
        private readonly IGameActionsDispatcher _gameActionsDispatcher;

        public MoveRequestPacketHandler(
            IPlayerManager playerManager,
            IServerPacketManager serverPacketManager,
            IGameActionsDispatcher gameActionsDispatcher)
        {
            _playerManager = playerManager;
            _serverPacketManager = serverPacketManager;
            _gameActionsDispatcher = gameActionsDispatcher;
        }

        protected override void Handle(MoveRequestPacket packet, NetPeer peer = null)
        {
            if (peer == null) throw new ArgumentNullException(nameof(peer));

            var input = packet.Direction;

            Console.WriteLine($"Received movement input {input} (pid: {peer.Id})");

            var player = _playerManager.Get(peer.Id);
            if (player?.CurrentCharacter != null)
            {
                _gameActionsDispatcher.AddEvent(new GameAction(() => player.CurrentCharacter.Move(input)));
            }
        }
    }
}