using System;
using Team801.Tibia2.Client.Controllers.Callbacks;
using Team801.Tibia2.Common.Models.Creature;

namespace Team801.Tibia2.ConsoleClient.Callbacks
{
    public class CharacterControllerCallbacks : ICharacterControllerCallbacks
    {
        public void OnJoined(DateTime timestamp, Character character)
        {
            Console.WriteLine($"Your character {character} joined the world");
        }

        public void OnAppeared(DateTime timestamp, Creature creature)
        {
            Console.WriteLine($"Creature {creature} appeared. (Position: {creature.Position})");
        }

        public void OnDisappeared(DateTime timestamp, Creature creature)
        {
            Console.WriteLine($"Creature {creature} disappeared.");
        }
    }
}