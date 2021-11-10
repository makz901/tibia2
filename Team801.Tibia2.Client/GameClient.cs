using System.Threading.Tasks;
using Autofac;
using LiteNetLib;
using Team801.Tibia2.Client.Configuration;
using Team801.Tibia2.Client.Controllers;
using Team801.Tibia2.Client.Managers;

namespace Team801.Tibia2.Client
{
    public class GameClient
    {
        private readonly TaskCompletionSource<bool> _connectionTcs = new TaskCompletionSource<bool>();
        private readonly GameClientListener _gameClientListener;
        private readonly NetManager _netManager;
        private readonly ClientManager _clientManager;

        public ICharacterController CharacterController { get; }
        public IMovementController MovementController { get; }

        public GameClient()
        {
            var container = ClientConfig.Build();

            _clientManager = container.Resolve<ClientManager>();
            _gameClientListener = container.Resolve<GameClientListener>();
            MovementController = container.Resolve<IMovementController>();
            CharacterController = container.Resolve<ICharacterController>();

            _netManager = new NetManager(_gameClientListener) {AutoRecycle = true};

            _gameClientListener.OnConnected += OnClientConnected;
            _gameClientListener.OnDisconnected += OnClientDisconnected;
        }

        public void OnFrameUpdated() => _netManager.PollEvents();

        public async Task<bool> Connect()
        {
            // Console.WriteLine("Connecting to server...");

            _netManager.Start();
            _netManager.Connect("localhost", 12345, "");

            return await _connectionTcs.Task;
        }

        public void Disconnect()
        {
            _netManager.DisconnectAll();
        }

        private void OnClientConnected(NetPeer peer)
        {
            _clientManager.Initialize(peer);
            _connectionTcs.TrySetResult(true);
        }

        private void OnClientDisconnected(DisconnectInfo obj)
        {
            _connectionTcs.TrySetResult(false);
        }
    }
}