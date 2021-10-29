using System;
using System.Numerics;
using Team801.Tibia2.Common.Models;

namespace Team801.Tibia2.Client.Services.Contracts
{
    public class PlayerManager : IPlayerManager
    {
        public Player Player { get; } = new Player();
        public event Action<Vector2> PositionChanged;
        public void OnPositionChanged(Vector2 position) => PositionChanged?.Invoke(position);
    }
}