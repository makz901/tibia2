using System;
using LiteNetLib;
using Team801.Tibia2.Client.Managers;
using Team801.Tibia2.Common.Models.Creature;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromServer;

namespace Team801.Tibia2.Client.PacketHandlers
{
    public class JoinAcceptedPacketHandler : BasePacketHandler<JoinAcceptedPacket>
    {
        private readonly GameStateManager _gameStateManager;

        public JoinAcceptedPacketHandler(
            GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }

        public override void Handle(JoinAcceptedPacket packet, NetPeer peer = null)
        {
            Console.WriteLine($"Join accepted by server");
            _gameStateManager.CurrentPlayer = new Player()
            {
                Name = packet.PlayerState.Name,
                Position = packet.PlayerState.Position,
                Direction = packet.PlayerState.Direction
            };
        }
    }
}