using System;
using System.Threading.Tasks;
using Team801.Tibia2.Server.Game.Contracts;

namespace Team801.Tibia2.Server.Game.Actions.Base
{
    public abstract class BaseGameAction : IGameAction
    {
        private readonly Action _action;

        public BaseGameAction(Action action, int? expirationMs = null)
        {
            _action = action;

            if (expirationMs != null)
            {
                ExpirationTime = DateTime.Now.AddMilliseconds(expirationMs.Value).TimeOfDay;
            }
        }

        public TimeSpan? ExpirationTime { get; }

        public bool HasNoTimeout => ExpirationTime == null;

        public bool HasExpired => DateTime.Now.TimeOfDay > ExpirationTime;

        public event Action Completed;

        public void Invoke()
        {
            _action?.Invoke();

            Task.Run(() => Completed?.Invoke());
        }
    }
}