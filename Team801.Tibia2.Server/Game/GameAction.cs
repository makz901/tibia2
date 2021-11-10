using System;
using Team801.Tibia2.Server.Game.Contracts;

namespace Team801.Tibia2.Server.Game
{
    public class GameAction : IGameAction
    {
        private readonly Action _action;

        public GameAction(Action action)
        {
            _action = action;
            HasNoTimeout = true;
        }

        public GameAction(int expirationMs, Action action)
        {
            _action = action;
            ExpirationTime = DateTime.Now.AddMilliseconds(expirationMs).TimeOfDay;
        }

        public TimeSpan ExpirationTime { get; }

        public bool HasNoTimeout { get; private set; }

        public bool HasExpired => DateTime.Now.TimeOfDay > ExpirationTime;

        public void SetToNotExpire()
        {
            HasNoTimeout = true;
        }

        public void Invoke() => _action?.Invoke();
    }
}