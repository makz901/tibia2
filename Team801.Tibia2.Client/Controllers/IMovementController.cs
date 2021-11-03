using Godot;

namespace Team801.Tibia2.Client.Controllers
{
    public interface IMovementController
    {
        void Move(Vector2 direction);
        void MoveTo(Vector2 point);
    }
}