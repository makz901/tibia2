using LiteNetLib;
using Team801.Tibia2.Client.Controllers.Callbacks;
using Team801.Tibia2.Client.Managers;
using Team801.Tibia2.Common.Packets.FromClient;

namespace Team801.Tibia2.Client.Controllers
{
    public class CharacterController : BaseController, ICharacterController
    {
        private readonly ClientManager _clientManager;
        public ICharacterControllerCallbacks Callbacks { get; set; }

        public CharacterController(
            ClientManager clientManager)
        {
            _clientManager = clientManager;
        }

        public void JoinGame(string name)
        {
            _clientManager.SendToServer(new JoinRequestPacket { Username = name }, DeliveryMethod.ReliableOrdered);
        }
    }
}