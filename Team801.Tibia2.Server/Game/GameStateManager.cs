using System.Threading.Tasks;
using Team801.Tibia2.Common.Configuration;
using Team801.Tibia2.Server.Game.Contracts;
using Thread = System.Threading.Thread;

namespace Team801.Tibia2.Server.Game
{
    public class GameStateManager : IGameStateManager
    {
        private readonly IGameActionsDispatcher _gameActionsDispatcher;

        private bool _started;

        public GameStateManager(
            IGameActionsDispatcher gameActionsDispatcher)
        {
            _gameActionsDispatcher = gameActionsDispatcher;
        }

        public void Start()
        {
            if (_started) return;

            _started = true;
            Task.Run(() =>
            {
                while (_started)
                {
                    ProcessFrame();
                    Thread.Sleep(GameConstants.ServerTickInterval);
                }
            });
        }

        public void Stop()
        {
            if (!_started) return;

            _started = false;
        }

        private void ProcessFrame()
        {
        }
    }
}