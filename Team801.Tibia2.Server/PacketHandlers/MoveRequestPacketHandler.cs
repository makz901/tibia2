using System;
using LiteNetLib;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromClient;
using Team801.Tibia2.Common.Packets.FromServer;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.PacketHandlers
{
    public class MoveRequestPacketHandler : BasePacketHandler<MoveRequestPacket>
    {
        private readonly IPlayerManager _playerManager;
        private readonly IPacketManager _packetManager;

        public MoveRequestPacketHandler(
            IPlayerManager playerManager,
            IPacketManager packetManager)
        {
            _playerManager = playerManager;
            _packetManager = packetManager;
        }

        public override void Handle(MoveRequestPacket packet, NetPeer peer = null)
        {
            if (peer == null) throw new ArgumentNullException(nameof(peer));

            var input = packet.Direction;

            Console.WriteLine($"Received movement input {input} (pid: {peer.Id})");

            var player = _playerManager.Get(peer.Id);
            if (player != null)
            {
                player.Move(input.Normalized(), );

                var movedPacket = new PlayerMovedPacket {PlayerPosition = player.Position, PlayerName = player.Name};

                foreach (var nearbyPeer in _playerManager.GetNearbyPeers(player.Position))
                {
                    _packetManager.QueuePacket(movedPacket, nearbyPeer);
                }
            }
        }
    }
}