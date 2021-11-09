using System.Collections.Generic;
using Team801.Tibia2.Common.Models.Creature;
using Team801.Tibia2.Common.Models.Player;

namespace Team801.Tibia2.Client.Managers
{
    public class GameStateManager
    {
        public Character MyCharacter { get; set; }
        public List<Creature> CreatureList { get; } = new List<Creature>();
    }
}