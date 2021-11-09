using System;
using Godot;
using Team801.Tibia2.Client.Controllers.Callbacks;

namespace Team801.Tibia2.ConsoleClient.Callbacks
{
    public class MovementControllerCallbacks : IMovementControllerCallbacks
    {
        public void OnMoved(Vector2 serverPosition)
        {
            Console.WriteLine($"Moved to a new position: {serverPosition}");
        }
    }
}