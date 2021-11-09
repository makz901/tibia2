using System;

namespace Team801.Tibia2.Server.Tasks
{
    public interface IGameEvent
    {
        Action Action { get; }

        bool HasExpired { get; }
        bool HasNoTimeout { get; }

        void SetToNotExpire();
    }
}