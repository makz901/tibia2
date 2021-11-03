using Godot;

namespace Team801.Tibia2.Common.Models.Creature
{
    public class Player : Creature
    {
        public int Level;

        public void Move(Vector2 input, double deltaTime)
        {
            Position += input * Attributes.Speed * (float) deltaTime;
        }
    }
}