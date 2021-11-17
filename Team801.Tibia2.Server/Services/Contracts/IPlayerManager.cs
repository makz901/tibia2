using System.Collections.Generic;
using Godot;
using LiteNetLib;
using Team801.Tibia2.Server.Models;

namespace Team801.Tibia2.Server.Services.Contracts
{
    public interface IPlayerManager
    {
        void Add(int peerId, Player newPlayer);
        void Remove(int peerId);
        Player Get(int peerId);
        IEnumerable<Player> GetNearby(Vector2 position);
        IEnumerable<NetPeer> GetNearbyPeers(Vector2 position);
    }
}