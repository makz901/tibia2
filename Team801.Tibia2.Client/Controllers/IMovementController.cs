using System;
using Godot;
using Team801.Tibia2.Client.Controllers.Callbacks;

namespace Team801.Tibia2.Client.Controllers
{
    public interface IMovementController
    {
        IMovementControllerCallbacks Callbacks { get; set; }

        void Move(Vector2 direction);
        void MoveTo(Vector2 point);
    }
}