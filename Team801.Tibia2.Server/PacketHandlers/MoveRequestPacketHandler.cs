using System;
using LiteNetLib;
using Team801.Tibia2.Common.Configuration;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromClient;
using Team801.Tibia2.Common.Packets.FromServer;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.PacketHandlers
{
    public class MoveRequestPacketHandler : BasePacketHandler<MoveRequestPacket>
    {
        private readonly IPlayerManager _playerManager;
        private readonly IServerManager _serverManager;
        private long _lastPacketTick;

        public MoveRequestPacketHandler(
            IPlayerManager playerManager,
            IServerManager serverManager)
        {
            _playerManager = playerManager;
            _serverManager = serverManager;
        }

        protected override void Handle(MoveRequestPacket packet, NetPeer peer = null)
        {
            if (peer == null) throw new ArgumentNullException(nameof(peer));

            var input = packet.Direction;

            Console.WriteLine($"Received movement input {input} (pid: {peer.Id})");

            var player = _playerManager.Get(peer.Id);
            if (player != null)
            {
                _lastPacketTick = packet.Timestamp;
                player.Move(input, (float) Constants.SyncInterval/1000);

                var movedPacket = new PlayerMovedPacket {PlayerPosition = player.Position, PlayerName = player.Name};

                foreach (var nearbyPeer in _playerManager.GetNearbyPeers(player.Position))
                {
                    _serverManager.QueuePacket(movedPacket, nearbyPeer);
                }
            }
        }
    }
}