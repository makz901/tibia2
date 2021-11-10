using System.Collections.Generic;
using System.Linq;
using Godot;
using Team801.Tibia2.Common.Models.Creature;
using Team801.Tibia2.Common.Packets.FromServer;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.Events
{
    public class CreatureEvents
    {
        private readonly Godot.Collections.Dictionary<string, List<Creature>> _nearbyDictionary = new Godot.Collections.Dictionary<string, List<Creature>>();

        private readonly IPlayerManager _playerManager;
        private readonly IServerPacketManager _serverPacketManager;

        public CreatureEvents(
            IPlayerManager playerManager,
            IServerPacketManager serverPacketManager)
        {
            _playerManager = playerManager;
            _serverPacketManager = serverPacketManager;
        }

        public void Move(Creature creature, Vector2 direction)
        {
            var newPosition = creature.Move(direction);
            var playersToNotify = _playerManager.GetNearby(newPosition).ToList();
            if (!_nearbyDictionary.ContainsKey(creature.Id))
            {
                _nearbyDictionary.Add(creature.Id, new List<Creature>());
            }

            var currentList = playersToNotify.Select(x => x.CurrentCharacter);
            var oldList = _nearbyDictionary[creature.Id];
            var newCreaturesArray = currentList.Except(oldList);
            var oldCreaturesArray = oldList.Except(currentList);

            if (playersToNotify.Any())
            {
                var movedPacket = new CreatureMovedPacket {NewPosition = newPosition, CreatureId = creature.Id};
                foreach (var nearbyPlayer in playersToNotify)
                {
                    _serverPacketManager.Send(movedPacket, nearbyPlayer.Peer);
                }
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
        }
    }
}