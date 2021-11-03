using Godot;
using Team801.Tibia2.Client.Services;
using Team801.Tibia2.Common.Packets.FromClient;

namespace Team801.Tibia2.Client.Controllers
{
    public class MovementController : BaseController, IMovementController
    {
        private readonly ClientNetManager _clientNetManager;

        public MovementController(
            ClientNetManager clientNetManager)
        {
            _clientNetManager = clientNetManager;
        }

        public void Move(Vector2 direction)
        {
            _clientNetManager.Send(new MovePlayerPacket {Direction = direction});
        }

        public void MoveTo(Vector2 point)
        {
            //todo:
        }
    }
}