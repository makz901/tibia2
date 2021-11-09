using Autofac;
using LiteNetLib;
using Team801.Tibia2.Client.Configuration;
using Team801.Tibia2.Client.Controllers;
using Team801.Tibia2.Client.Managers;
using Team801.Tibia2.Common.Packets.FromClient;

namespace Team801.Tibia2.Client
{
    public class GameClient
    {
        private readonly GameClientListener _gameClientListener;
        private readonly NetManager _netManager;
        private readonly ClientManager _clientManager;

        private string _requestedName;

        public IMovementController MovementController { get; }

        public GameClient()
        {
            var container = ClientConfig.Build();

            _clientManager = container.Resolve<ClientManager>();
            _gameClientListener = container.Resolve<GameClientListener>();
            MovementController = container.Resolve<IMovementController>();

            _netManager = new NetManager(_gameClientListener) {AutoRecycle = true};
        }

        public void OnFrameUpdated() => _netManager.PollEvents();

        public void Connect(string name)
        {
            // Console.WriteLine("Connecting to server...");
            _requestedName = name;

            _netManager.Start();
            _netManager.Connect("localhost", 12345, "");

            _gameClientListener.OnConnected += OnClientConnected;
            _gameClientListener.OnDisconnected += OnClientDisconnected;
        }

        public void Disconnect()
        {
            _netManager.DisconnectAll();
        }

        private void OnClientConnected(NetPeer peer)
        {
            _clientManager.Initialize(peer);
            _clientManager.SendToServer(new JoinRequestPacket { Username = _requestedName }, DeliveryMethod.ReliableOrdered);
        }

        private void OnClientDisconnected(DisconnectInfo obj)
        {
            _requestedName = null;

            _gameClientListener.OnConnected -= OnClientConnected;
            _gameClientListener.OnDisconnected -= OnClientDisconnected;
        }
    }
}