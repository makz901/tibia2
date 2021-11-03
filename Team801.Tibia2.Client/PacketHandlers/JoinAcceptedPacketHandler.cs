using System;
using LiteNetLib;
using Team801.Tibia2.Client.Services.Contracts;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromServer;

namespace Team801.Tibia2.Client.PacketHandlers
{
    public class JoinAcceptedPacketHandler : BasePacketHandler<JoinAcceptedPacket>
    {
        private readonly IGameStateManager _gameStateManager;

        public JoinAcceptedPacketHandler(
            IGameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }

        public override void Handle(JoinAcceptedPacket packet, NetPeer peer = null)
        {
            Console.WriteLine($"Join accepted by server");
            _gameStateManager.CurrentPlayer.Position = packet.PlayerPosition;
        }
    }
}