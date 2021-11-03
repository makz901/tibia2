using System.Collections.Generic;
using Godot;
using LiteNetLib;
using Team801.Tibia2.Common.Models.Creature;

namespace Team801.Tibia2.Server.Services.Contracts
{
    public interface IPlayerManager
    {
        void Add(NetPeer peer, Player newPlayer);
        void Remove(int peerId);
        Player Get(int peerId);
        IEnumerable<Player> GetNearby(Vector2 position);
        IEnumerable<NetPeer> GetNearbyPeers(Vector2 position);
    }
}