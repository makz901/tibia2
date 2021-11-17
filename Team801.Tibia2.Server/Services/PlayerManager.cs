using System.Collections.Generic;
using System.Linq;
using Godot;
using LiteNetLib;
using Team801.Tibia2.Server.Models;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.Services
{
    public class PlayerManager : IPlayerManager
    {
        private readonly Dictionary<int, Player> _players = new Dictionary<int, Player>();


        public void Add(int peerId, Player newPlayer)
        {
            if (_players.ContainsKey(peerId)) return;

            _players.Add(peerId, newPlayer);
        }

        public void Remove(int id)
        {
            if (!_players.ContainsKey(id)) return;

            _players.Remove(id);
        }

        public Player Get(int id)
        {
            return _players.TryGetValue(id, out var player) ? player : null;
        }

        public IEnumerable<Player> GetNearby(Vector2 position)
        {
            return _players.Values.Where(x => IsPlayerNearPosition(x, position));
        }

        public IEnumerable<NetPeer> GetNearbyPeers(Vector2 position)
        {
            return GetNearby(position).Select(x => x.Peer);
        }

        private bool IsPlayerNearPosition(Player player, Vector2 position)
        {
            return player.CurrentCharacter != null && (player.CurrentCharacter.Position - position).Length() < 20;
        }
    }
}