using System.Collections.Generic;
using Team801.Tibia2.Core.Models;
using UnityEngine;

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