using Godot;

namespace Team801.Tibia2.Client.Controllers.Callbacks
{
    public interface IMovementControllerCallbacks
    {
        void OnMoved(Vector2 serverPosition);
    }
}