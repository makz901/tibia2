using System;
using Godot;
using Team801.Tibia2.Client.Controllers.Callbacks;
using Team801.Tibia2.Client.Managers;
using Team801.Tibia2.Common.Packets.FromClient;

namespace Team801.Tibia2.Client.Controllers
{
    public class MovementController : BaseController, IMovementController
    {
        private readonly ClientManager _clientManager;

        public IMovementControllerCallbacks Callbacks { get; set; }

        public MovementController(
            ClientManager clientManager)
        {
            _clientManager = clientManager;
        }

        public void Move(Vector2 direction)
        {
            _clientManager.SendToServer(new MoveRequestPacket {Direction = direction});
        }

        public void MoveTo(Vector2 point)
        {
            //todo:
        }

        public void OnMoved()
        {
            Moved?.Invoke();
        }

        public event Action Moved;
    }
}