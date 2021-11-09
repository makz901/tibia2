using System.Threading.Tasks;
using Team801.Tibia2.Common.Configuration;
using Team801.Tibia2.Server.Game.Contracts;
using Team801.Tibia2.Server.Services;
using Thread = System.Threading.Thread;

namespace Team801.Tibia2.Server.Game
{
    public class ServerGameCalculations : IServerGameCalculations
    {
        private readonly IGameEventsDispatcher _gameEventsDispatcher;

        private bool _started;

        public ServerGameCalculations(
            IGameEventsDispatcher gameEventsDispatcher)
        {
            _gameEventsDispatcher = gameEventsDispatcher;
        }

        public void Start()
        {
            if (_started) return;

            _started = true;
            Task.Run(() =>
            {
                while (_started)
                {
                    ProcessGameFrame();
                    Thread.Sleep(GameConstants.ServerTickInterval);
                }
            });
        }

        public void Stop()
        {
            if (!_started) return;

            _started = false;
        }

        private void ProcessGameFrame()
        {
        }
    }
}