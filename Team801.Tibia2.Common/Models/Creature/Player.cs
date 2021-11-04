using Godot;

namespace Team801.Tibia2.Common.Models.Creature
{
    public class Player : Creature
    {
        public int Level { get; set; }

        public void Move(Vector2 input, double deltaTime)
        {
            Position += input.Normalized() * Speed * (float) deltaTime;
        }
    }
}