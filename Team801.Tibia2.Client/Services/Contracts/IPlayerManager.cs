using System;
using System.Numerics;
using Team801.Tibia2.Common.Models;

namespace Team801.Tibia2.Client.Services.Contracts
{
    public interface IPlayerManager
    {
        Player Player { get; }
        event Action<Vector2> PositionChanged;
        void OnPositionChanged(Vector2 position);
    }
}