using System.Collections.Generic;
using Team801.Tibia2.Common.Models.Creature;

namespace Team801.Tibia2.Client.Managers
{
    public class GameStateManager
    {
        public Player CurrentPlayer { get; set; }
        public List<Player> PlayerList { get; } = new List<Player>();
        public List<Creature> CreatureList { get; } = new List<Creature>();
    }
}