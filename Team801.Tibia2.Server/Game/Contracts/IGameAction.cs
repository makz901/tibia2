using System;

namespace Team801.Tibia2.Server.Game.Contracts
{
    public interface IGameAction
    {
        void Invoke();

        bool HasExpired { get; }
        bool HasNoTimeout { get; }

        event Action Completed;
    }
}