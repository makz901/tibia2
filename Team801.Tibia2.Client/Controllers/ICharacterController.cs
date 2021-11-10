using Team801.Tibia2.Client.Controllers.Callbacks;

namespace Team801.Tibia2.Client.Controllers
{
    public interface ICharacterController
    {
        ICharacterControllerCallbacks Callbacks { get; set; }

        void JoinGame(string name);
    }
}