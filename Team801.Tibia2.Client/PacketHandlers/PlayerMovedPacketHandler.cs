using System;
using LiteNetLib;
using Team801.Tibia2.Client.Services.Contracts;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromServer;

namespace Team801.Tibia2.Client.PacketHandlers
{
    public class PlayerMovedPacketHandler : BasePacketHandler<PlayerMovedPacket>
    {
        private readonly IPlayerManager _playerManager;

        public PlayerMovedPacketHandler(
            IPlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        public override void Handle(PlayerMovedPacket packet, NetPeer peer = null)
        {
            Console.WriteLine($"Player [{packet.PlayerName}] moved to a new position {packet.PlayerState.Position}");
            _playerManager.Player.State = packet.PlayerState;
        }
    }
}