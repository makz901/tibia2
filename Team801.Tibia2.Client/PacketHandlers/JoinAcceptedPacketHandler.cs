using System;
using LiteNetLib;
using Team801.Tibia2.Client.Services.Contracts;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromServer;

namespace Team801.Tibia2.Client.PacketHandlers
{
    public class JoinAcceptedPacketHandler : BasePacketHandler<JoinAcceptedPacket>
    {
        private readonly IPlayerManager _playerManager;

        public JoinAcceptedPacketHandler(
            IPlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        public override void Handle(JoinAcceptedPacket packet, NetPeer peer = null)
        {
            Console.WriteLine($"Join accepted by server");
            _playerManager.Player.State = packet.PlayerState;
        }
    }
}