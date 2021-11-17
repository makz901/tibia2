using System;
using Team801.Tibia2.Server.Game.Actions.Base;

namespace Team801.Tibia2.Server.Game.Actions.Creatures
{
    public class CreatureSpawnedGameAction : BaseGameAction
    {
        public CreatureSpawnedGameAction(Action action, int? expirationMs = null) 
            : base(action, expirationMs)
        {
        }
    }
}