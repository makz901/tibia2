using System;
using Team801.Tibia2.Common.Models.Creature;

namespace Team801.Tibia2.Client.Controllers.Callbacks
{
    public interface ICharacterControllerCallbacks
    {
        void OnJoined(DateTime timestamp, Character character);
        void OnAppeared(DateTime timestamp, Creature creature);
        void OnDisappeared(DateTime timestamp, Creature creature);
    }
}