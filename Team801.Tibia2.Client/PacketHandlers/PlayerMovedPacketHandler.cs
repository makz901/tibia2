using System;
using LiteNetLib;
using Team801.Tibia2.Client.Managers;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromServer;

namespace Team801.Tibia2.Client.PacketHandlers
{
    public class PlayerMovedPacketHandler : BasePacketHandler<PlayerMovedPacket>
    {
        private readonly GameStateManager _gameStateManager;

        public PlayerMovedPacketHandler(
            GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }

        public override void Handle(PlayerMovedPacket packet, NetPeer peer = null)
        {
            Console.WriteLine($"Player [{packet.PlayerName}] moved to a new position {packet.PlayerPosition}");
            _gameStateManager.CurrentPlayer.Position = packet.PlayerPosition;
        }
    }
}