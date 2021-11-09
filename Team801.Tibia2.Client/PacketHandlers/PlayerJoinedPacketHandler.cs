using System;
using LiteNetLib;
using Team801.Tibia2.Client.Managers;
using Team801.Tibia2.Common.Models.Creature;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromServer;

namespace Team801.Tibia2.Client.PacketHandlers
{
    public class PlayerJoinedPacketHandler : BasePacketHandler<PlayerJoinedPacket>
    {
        private readonly GameStateManager _gameStateManager;

        public PlayerJoinedPacketHandler(
            GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }

        protected override void Handle(PlayerJoinedPacket packet, NetPeer peer = null)
        {
            Console.WriteLine($"Join accepted by server");
            _gameStateManager.MyCharacter = new Character()
            {
                Name = packet.PlayerState.Name,
                Position = packet.PlayerState.Position,
                Direction = packet.PlayerState.Direction
            };
        }
    }
}