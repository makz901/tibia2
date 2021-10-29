using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Team801.Tibia2.Common.Models;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.Services
{
    public class PlayerManager : IPlayerManager
    {
        private readonly Dictionary<int, Player> _players = new Dictionary<int, Player>();

        public void Add(Player newPlayer)
        {
            _players.Add(newPlayer.Peer.Id, newPlayer);
        }

        public void Remove(int id)
        {
            _players.Remove(id);
        }

        public Player Get(int id)
        {
            return _players.TryGetValue(id, out var player) ? player : null;
        }

        public IEnumerable<Player> GetNearby(Vector2 position)
        {
            return _players.Values.Where(x => (x.State.Position - position).Length() < 10);
        }
    }
}