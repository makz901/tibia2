using System;
using Team801.Tibia2.Common.Models.Creature;

namespace Team801.Tibia2.Client.Controllers.Callbacks
{
    public interface IMovementControllerCallbacks
    {
        void OnMoved(DateTime timestamp, Creature creature);
    }
}