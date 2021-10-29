using System.Collections.Generic;
using System.Numerics;
using Team801.Tibia2.Common.Models;

namespace Team801.Tibia2.Server.Services.Contracts
{
    public interface IPlayerManager
    {
        void Add(Player newPlayer);
        void Remove(int id);
        Player Get(int id);
        IEnumerable<Player> GetNearby(Vector2 position);
    }
}