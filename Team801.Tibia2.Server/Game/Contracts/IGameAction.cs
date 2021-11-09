using System;

namespace Team801.Tibia2.Server.Game.Contracts
{
    public interface IGameAction
    {
        Action Action { get; }

        bool HasExpired { get; }
        bool HasNoTimeout { get; }

        void SetToNotExpire();
    }
}