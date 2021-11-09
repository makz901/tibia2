using System;
using System.Threading.Tasks;
using Godot;
using Team801.Tibia2.Common.Models.Enums;

namespace Team801.Tibia2.Common.Models.Creature
{
    public abstract class Creature
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public WorldDirection Direction { get; set; }

        //tiles per seconds
        public int Speed { get; set; } = 1;

        //Actions
        public void Move(Vector2 input)
        {
            Position += input.Normalized() * Speed;
        }

        public event Action<Creature> Moved;
        public void OnMoved()
        {
            Task.Run(() => Moved?.Invoke(this));
        }
    }
}