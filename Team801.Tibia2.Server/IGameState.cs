using System.Collections.Generic;
using Team801.Tibia2.Common.Models.Creature;
using Team801.Tibia2.Common.Models.Item;

namespace Team801.Tibia2.Server
{
    public interface IGameState
    {
        // Map Map { get; set; }
        List<Item> ItemList { get; }
        List<Player> PlayerList { get; }
        List<Creature> CreatureList { get; }
    }
}