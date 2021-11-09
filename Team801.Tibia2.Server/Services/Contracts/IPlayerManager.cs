using System.Collections.Generic;
using Godot;
using Team801.Tibia2.Common.Models.Player;

namespace Team801.Tibia2.Server.Services.Contracts
{
    public interface IPlayerManager
    {
        void Add(int peerId, Player newPlayer);
        void Remove(int peerId);
        Player Get(int peerId);
        IEnumerable<Player> GetNearby(Vector2 position);
    }
}