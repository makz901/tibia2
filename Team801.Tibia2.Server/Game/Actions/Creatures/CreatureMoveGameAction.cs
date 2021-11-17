using System.Linq;
using Autofac;
using Godot;
using Team801.Tibia2.Common.Models.Creature;
using Team801.Tibia2.Common.Packets.FromServer;
using Team801.Tibia2.Server.Configuration;
using Team801.Tibia2.Server.Game.Actions.Base;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.Game.Actions.Creatures
{
    public class CreatureMoveGameAction : BaseGameAction
    {
        private readonly Creature _creature;
        private readonly IServerPacketManager _serverPacketManager;
        private readonly IPlayerManager _playerManager;

        public CreatureMoveGameAction(Creature creature, Vector2 direction)
            : base(() => creature.Move(direction))
        {
            _creature = creature;
            _serverPacketManager = ServerConfig.IoC.Resolve<IServerPacketManager>();
            _playerManager = ServerConfig.IoC.Resolve<IPlayerManager>();

            Completed += OnCompleted;
        }

        private void OnCompleted()
        {
            var nearby = _playerManager.GetNearbyPeers(_creature.Position).ToList();
            if (nearby.Any())
            {
                var movedPacket = new CreatureMovedPacket {NewPosition = _creature.Position, CreatureId = _creature.Id};
                foreach (var nearbyPeer in nearby)
                {
                    _serverPacketManager.Send(movedPacket, nearbyPeer);
                }
            }
        }
    }
}