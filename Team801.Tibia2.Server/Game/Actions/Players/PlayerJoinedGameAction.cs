using Autofac;
using Team801.Tibia2.Common.Models.Creature;
using Team801.Tibia2.Common.Packets.FromServer;
using Team801.Tibia2.Common.Packets.Models;
using Team801.Tibia2.Server.Configuration;
using Team801.Tibia2.Server.Game.Actions.Base;
using Team801.Tibia2.Server.Game.Actions.Creatures;
using Team801.Tibia2.Server.Game.Contracts;
using Team801.Tibia2.Server.Models;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.Game.Actions.Players
{
    public class PlayerJoinedGameAction : BaseGameAction
    {
        private readonly IGameActionsDispatcher _gameActionDispatcher;
        private readonly IServerPacketManager _serverPacketManager;

        private readonly Player _player;

        public PlayerJoinedGameAction(Player player, string charName) 
            : base(() => player.CurrentCharacter = new Character { Name = charName })
        {
            _player = player;

            _serverPacketManager = ServerConfig.IoC.Resolve<IServerPacketManager>();
            _gameActionDispatcher = ServerConfig.IoC.Resolve<IGameActionsDispatcher>();
        }

        protected override void OnCompleted()
        {
            var packetModel = new CreatureStatePacketModel
            {
                Id = _player.CurrentCharacter.Id,
                Name = _player.CurrentCharacter.Name,
                Position = _player.CurrentCharacter.Position,
                Direction = _player.CurrentCharacter.Direction
            };

            _serverPacketManager.Send(new JoinAcceptedPacket { CreatureState = packetModel }, _player.Peer);
            _gameActionDispatcher.AddAction(new CreatureSpawnedGameAction(_player.CurrentCharacter));
        }
    }
}