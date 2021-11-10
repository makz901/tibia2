using System;
using System.Linq;
using LiteNetLib;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromClient;
using Team801.Tibia2.Common.Packets.FromServer;
using Team801.Tibia2.Common.Packets.Models;
using Team801.Tibia2.Server.Game;
using Team801.Tibia2.Server.Services;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.PacketHandlers
{
    public class MoveRequestPacketHandler : BasePacketHandler<MoveRequestPacket>
    {
        private readonly IPlayerManager _playerManager;
        private readonly IServerPacketManager _serverPacketManager;
        private readonly IGameActionsDispatcher _gameActionsDispatcher;

        public MoveRequestPacketHandler(
            IPlayerManager playerManager,
            IServerPacketManager serverPacketManager,
            IGameActionsDispatcher gameActionsDispatcher)
        {
            _playerManager = playerManager;
            _serverPacketManager = serverPacketManager;
            _gameActionsDispatcher = gameActionsDispatcher;
        }

        protected override void Handle(MoveRequestPacket packet, NetPeer peer = null)
        {
            if (peer == null) throw new ArgumentNullException(nameof(peer));

            var input = packet.Direction;

            Console.WriteLine($"Received movement input {input} (pid: {peer.Id})");

            var player = _playerManager.Get(peer.Id);
            if (player?.CurrentCharacter != null)
            {
                _gameActionsDispatcher.AddEvent(new GameAction(() =>
                {
                    player.CurrentCharacter.Move(input);

                    var movedPacket = new CreatureMovedPacket {NewPosition = player.CurrentCharacter.Position, CreatureId = player.CurrentCharacter.Id};

                    var actualNearbyList = _playerManager.GetNearby(player.CurrentCharacter.Position).ToList();
                    var appearedList = actualNearbyList.Except(player.NearbyPlayerList).ToList();
                    var disappearedList = player.NearbyPlayerList.Except(actualNearbyList).ToList();
                    player.NearbyPlayerList = actualNearbyList;

                    foreach (var nearbyPlayer in actualNearbyList)
                    {
                        _serverPacketManager.Send(movedPacket, nearbyPlayer.Peer);
                    }

                    // if (disappearedList.Any())
                    // {
                    //     foreach (var disappearedPlayer in disappearedList)
                    //     {
                    //         var disappearedPacket = new CreatureDisappearedPacket {CreatureId = disappearedPlayer.CurrentCharacter.Id};
                    //         _serverPacketManager.Send(disappearedPacket, player.Peer);
                    //     }
                    // }
                    //
                    // if (appearedList.Any())
                    // {
                    //     foreach (var appearedPlayer in appearedList)
                    //     {
                    //         var appearedPacket = new CreatureAppearedPacket {State = new CreatureStatePacketModel
                    //         {
                    //             Id = appearedPlayer.CurrentCharacter.Id,
                    //             Name = appearedPlayer.CurrentCharacter.Name,
                    //             Position = appearedPlayer.CurrentCharacter.Position,
                    //             Direction = appearedPlayer.CurrentCharacter.Direction,
                    //         }};
                    //
                    //         _serverPacketManager.Send(appearedPacket, player.Peer);
                    //     }
                    // }
                }));
            }
        }
    }
}