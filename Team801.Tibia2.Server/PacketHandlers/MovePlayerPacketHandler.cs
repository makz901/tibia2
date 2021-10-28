using System;
using LiteNetLib;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromClient;
using Team801.Tibia2.Common.Packets.FromServer;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.PacketHandlers
{
    public class MovePlayerPacketHandler : BasePacketHandler<MovePlayerPacket>
    {
        private readonly IPlayerManager _playerManager;
        private readonly IPacketManager _packetManager;
        private readonly IGameTimer _gameTimer;

        public MovePlayerPacketHandler(
            IPlayerManager playerManager,
            IPacketManager packetManager,
            IGameTimer gameTimer)
        {
            _playerManager = playerManager;
            _packetManager = packetManager;
            _gameTimer = gameTimer;
        }

        public override void Handle(MovePlayerPacket packet, NetPeer peer)
        {
            var input = packet.Direction;

            Console.WriteLine($"Received movement input {input} (pid: {peer.Id})");

            var player = _playerManager.Get(peer.Id);
            if (player != null)
            {
                input.Normalize();
                player.Move(input, (float) _gameTimer.FrameDeltaTime.TotalSeconds);

                var movedPacket = new PlayerMovedPacket {PlayerState = player.State, PlayerName = player.Username};

                foreach (var loggedPlayer in _playerManager.GetNearby(player.State.Position))
                {
                    _packetManager.QueuePacket(movedPacket, loggedPlayer.Peer);
                }
            }
        }
    }
}