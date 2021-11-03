using System.Collections.Generic;
using Team801.Tibia2.Common.Models.Creature;

namespace Team801.Tibia2.Client.Services.Contracts
{
    public interface IGameStateManager
    {
        Player CurrentPlayer { get; set; }
        List<Player> PlayerList { get; }
        List<Creature> CreatureList { get; }
    }
}