using System.Collections.Generic;
using System.Linq;
using Godot;
using Team801.Tibia2.Common.Models.Player;
using Team801.Tibia2.Common.Packets.FromServer;
using Team801.Tibia2.Common.Packets.Models;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.Services
{
    public class PlayerManager : IPlayerManager
    {
        private static readonly object PlayersLock = new object();

        private readonly Dictionary<int, Player> _players = new Dictionary<int, Player>();
        private readonly IServerPacketManager _serverPacketManager;

        public PlayerManager(
            IServerPacketManager serverPacketManager)
        {
            _serverPacketManager = serverPacketManager;
        }

        public void Add(int peerId, Player newPlayer)
        {
            lock (PlayersLock)
            {
                if (_players.ContainsKey(peerId)) return;

                _players.Add(peerId, newPlayer);
                OnPlayerAdded(newPlayer);
            }
        }

        public void Remove(int id)
        {
            lock (PlayersLock)
            {
                if (!_players.TryGetValue(id, out var player)) return;

                _players.Remove(id);
                OnPlayerRemoved(player);
            }
        }

        public Player Get(int id)
        {
            lock (PlayersLock)
            {
                return _players.TryGetValue(id, out var player) ? player : null;
            }
        }

        public IEnumerable<Player> GetNearby(Vector2 position)
        {
            lock (PlayersLock)
            {
                return _players.Values.Where(x => IsPlayerNearPosition(x, position));
            }
        }

        private void OnPlayerAdded(Player newPlayer)
        {
            var packetModel = new CreatureStatePacketModel
            {
                Id = newPlayer.CurrentCharacter.Id,
                Name = newPlayer.CurrentCharacter.Name,
                Position = newPlayer.CurrentCharacter.Position,
                Direction = newPlayer.CurrentCharacter.Direction
            };

            _serverPacketManager.Send(new JoinAcceptedPacket { CreatureState = packetModel }, newPlayer.Peer);
        }

        private void OnPlayerRemoved(Player player)
        {
            //todo:
        }

        private bool IsPlayerNearPosition(Player player, Vector2 position)
        {
            return player.CurrentCharacter != null && (player.CurrentCharacter.Position - position).Length() < 20;
        }
    }
}