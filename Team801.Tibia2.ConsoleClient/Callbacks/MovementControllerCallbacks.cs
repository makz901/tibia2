using System;
using Godot;
using Team801.Tibia2.Client.Controllers.Callbacks;
using Team801.Tibia2.Common.Models.Creature;

namespace Team801.Tibia2.ConsoleClient.Callbacks
{
    public class MovementControllerCallbacks : IMovementControllerCallbacks
    {
        public void OnMoved(Creature creature)
        {
            Console.WriteLine($"{creature} moved to a new position: {creature.Position}");
        }
    }
}