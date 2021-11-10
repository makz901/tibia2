using System.Collections.Generic;
using System.Linq;
using Godot;
using Team801.Tibia2.Common.Models.Player;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.Services
{
    public class PlayerManager : IPlayerManager
    {
        private static readonly object PlayersLock = new object();

        private readonly Dictionary<int, Player> _players = new Dictionary<int, Player>();


        public void Add(int peerId, Player newPlayer)
        {
            lock (PlayersLock)
            {
                if (_players.ContainsKey(peerId)) return;

                _players.Add(peerId, newPlayer);
            }
        }

        public void Remove(int id)
        {
            lock (PlayersLock)
            {
                if (!_players.TryGetValue(id, out var player)) return;

                _players.Remove(id);
            }
        }

        public Player Get(int id)
        {
            // lock (PlayersLock)
            {
                return _players.TryGetValue(id, out var player) ? player : null;
            }
        }

        public IEnumerable<Player> GetNearby(Vector2 position)
        {
            // lock (PlayersLock)
            {
                return _players.Values.Where(x => IsPlayerNearPosition(x, position));
            }
        }

        private bool IsPlayerNearPosition(Player player, Vector2 position)
        {
            return player.CurrentCharacter != null && (player.CurrentCharacter.Position - position).Length() < 20;
        }
    }
}