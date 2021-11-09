using System;
using Team801.Tibia2.Server.Game.Contracts;

namespace Team801.Tibia2.Server.Game
{
    public class GameAction : IGameAction
    {
        public GameAction(Action action)
        {
            Action = action;
            HasNoTimeout = true;
        }

        public GameAction(int expirationMs, Action action)
        {
            Action = action;
            ExpirationTime = DateTime.Now.AddMilliseconds(expirationMs).TimeOfDay;
        }

        public TimeSpan ExpirationTime { get; }

        /// <summary>
        ///     Action to be added on event
        /// </summary>
        public Action Action { get; }

        /// <summary>
        ///     Set this property when event has no timeout
        /// </summary>
        public bool HasNoTimeout { get; private set; }

        /// <summary>
        ///     Indicates whether event has expired
        /// </summary>
        public bool HasExpired => DateTime.Now.TimeOfDay > ExpirationTime;

        /// <summary>
        ///     Sets event to not expire
        /// </summary>
        public void SetToNotExpire()
        {
            HasNoTimeout = true;
        }
    }
}