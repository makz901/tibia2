using System.Collections.Generic;
using System.Linq;
using Godot;
using LiteNetLib;
using Team801.Tibia2.Common.Models.Creature;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.Services
{
    public class PlayerManager : IPlayerManager
    {
        private readonly Dictionary<NetPeer, Player> _players = new Dictionary<NetPeer, Player>();

        public void Add(NetPeer peer, Player newPlayer)
        {
            _players.Add(peer, newPlayer);
        }

        public void Remove(int id)
        {
            var toRemove = _players.Keys.FirstOrDefault(x => x.Id == id);
            if (toRemove != null)
            {
                _players.Remove(toRemove);
            }
        }

        public Player Get(int id)
        {
            var peer = _players.Keys.FirstOrDefault(x => x.Id == id);
            if (peer != null)
            {
                return _players.TryGetValue(peer, out var player) ? player : null;
            }

            return null;
        }

        public IEnumerable<Player> GetNearby(Vector2 position)
        {
            return _players.Values.Where(x => (x.Position - position).Length() < 10);
        }

        public IEnumerable<NetPeer> GetNearbyPeers(Vector2 position)
        {
            return _players.Where(x => (x.Value.Position - position).Length() < 10).Select(x => x.Key);
        }
    }
}