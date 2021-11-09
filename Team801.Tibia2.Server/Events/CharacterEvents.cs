using Team801.Tibia2.Common.Models.Creature;
using Team801.Tibia2.Common.Packets.FromServer;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.Events
{
    public class CharacterEvents : ServerEvents<Character>
    {
        private readonly IPlayerManager _playerManager;
        private readonly IServerPacketManager _serverPacketManager;

        public CharacterEvents(
            IPlayerManager playerManager,
            IServerPacketManager serverPacketManager)
        {
            _playerManager = playerManager;
            _serverPacketManager = serverPacketManager;
        }

        public override void Add(Character obj)
        {
            obj.Moved += OnCharacterMoved;
        }

        public override void Remove(Character obj)
        {
            obj.Moved -= OnCharacterMoved;
        }

        private void OnCharacterMoved(Creature creature)
        {
            var movedPacket = new CreatureMovedPacket {NewPosition = creature.Position, CreatureId = creature.Id};
            foreach (var nearbyPlayer in _playerManager.GetNearby(creature.Position))
            {
                _serverPacketManager.Send(movedPacket, nearbyPlayer.Peer);
            }
        }
    }
}