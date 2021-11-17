using System;
using LiteNetLib;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromClient;
using Team801.Tibia2.Server.Game.Actions.Players;
using Team801.Tibia2.Server.Game.Contracts;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.PacketHandlers
{
    public class JoinRequestPacketHandler : BasePacketHandler<JoinRequestPacket>
    {
        private readonly IPlayerManager _playerManager;
        private readonly IGameActionsDispatcher _gameActionsDispatcher;

        public JoinRequestPacketHandler(
            IPlayerManager playerManager,
            IGameActionsDispatcher gameActionsDispatcher)
        {
            _playerManager = playerManager;
            _gameActionsDispatcher = gameActionsDispatcher;
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

            Console.WriteLine($"Received join request from character [{requestPacket.Username}] (pid: {peer.Id})");

            _gameActionsDispatcher.AddAction(new PlayerJoinedGameAction(player, requestPacket.Username));
        }
    }
}