using System;
using Autofac;
using Team801.Tibia2.Server.Configuration;
using Team801.Tibia2.Server.Game.Actions.Base;
using Team801.Tibia2.Server.Game.Actions.Creatures;
using Team801.Tibia2.Server.Game.Contracts;

namespace Team801.Tibia2.Server.Game.Actions.Character
{
    public class CharacterJoinedGameAction : BaseGameAction
    {
        private readonly IGameActionsDispatcher _gameActionDispatcher;

        public CharacterJoinedGameAction(Action action, int? expirationMs = null) 
            : base(action, expirationMs)
        {
            _gameActionDispatcher = ServerConfig.IoC.Resolve<IGameActionsDispatcher>();

            Completed += OnCompleted;
        }

        private void OnCompleted()
        {
            _gameActionDispatcher.AddAction(new CreatureSpawnedGameAction());
        }
    }
}