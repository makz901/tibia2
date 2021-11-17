using System;
using Godot;
using Team801.Tibia2.Client.Controllers.Callbacks;
using Team801.Tibia2.Client.Managers;
using Team801.Tibia2.Common.Models.Creature;
using Team801.Tibia2.Common.Packets.FromClient;

namespace Team801.Tibia2.Client.Controllers
{
    public class MovementController : BaseController, IMovementController
    {
        private readonly ClientManager _clientManager;
        private readonly GameStateManager _gameStateManager;

        public IMovementControllerCallbacks Callbacks { get; set; }

        public MovementController(
            ClientManager clientManager,
            GameStateManager gameStateManager)
        {
            _clientManager = clientManager;
            _gameStateManager = gameStateManager;
        }

        public void Move(Vector2 direction)
        {
            _gameStateManager.MyCharacter.Move(direction);

            Console.WriteLine($"Expected position: {_gameStateManager.MyCharacter.Position}");

            _clientManager.SendToServer(new MoveRequestPacket {Direction = direction});
        }

        public void MoveTo(Vector2 point)
        {
            //todo:
        }
    }
}