using Team801.Tibia2.Common.Models.Creature;

namespace Team801.Tibia2.Client.Controllers.Callbacks
{
    public interface ICharacterControllerCallbacks
    {
        void OnJoined(Character character);
        void OnAppeared(Creature creature);
        void OnDisappeared(Creature creature);
    }
}