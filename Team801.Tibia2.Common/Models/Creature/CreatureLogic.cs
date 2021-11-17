using Godot;

namespace Team801.Tibia2.Common.Models.Creature
{
    public static class CreatureLogic
    {
        public static void Move(this Creature creature, Vector2 input)
        {
            creature.Position += input.Normalized() * creature.Speed;
        }
    }
}