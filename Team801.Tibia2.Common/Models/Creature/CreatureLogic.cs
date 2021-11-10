using Godot;

namespace Team801.Tibia2.Common.Models.Creature
{
    public static class CreatureLogic
    {
        public static Vector2 Move(this Creature creature, Vector2 input)
        {
            return creature.Position + input.Normalized() * creature.Speed;
        }
    }
}