using Autofac;
using Godot;
using Team801.Tibia2.Common.Models.Creature;
using Team801.Tibia2.Common.Packets.FromServer;
using Team801.Tibia2.Common.Packets.Models;
using Team801.Tibia2.Server.Configuration;
using Team801.Tibia2.Server.Game.Actions.Base;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.Game.Actions.Creatures
{
    public class CreatureSpawnedGameAction : BaseGameAction
    {
        private readonly IPlayerManager _playerManager;
        private readonly IServerPacketManager _serverPacketManager;

        private readonly Creature _creature;

        public CreatureSpawnedGameAction(Creature creature) 
            : base(() => creature.Position = Vector2.Zero)
        {
            _creature = creature;

            _playerManager = ServerConfig.IoC.Resolve<IPlayerManager>();
            _serverPacketManager = ServerConfig.IoC.Resolve<IServerPacketManager>();
        }

        protected override void OnCompleted()
        {
            var packetModel = new CreatureStatePacketModel
            {
                Id = _creature.Id,
                Name = _creature.Name,
                Position = _creature.Position,
                Direction = _creature.Direction
            };

            foreach (var nearByPlayer in _playerManager.GetNearby(_creature.Position))
            {
                _serverPacketManager.Send(new CreatureAppearedPacket { State = packetModel }, nearByPlayer.Peer);
            }
        }
    }
}