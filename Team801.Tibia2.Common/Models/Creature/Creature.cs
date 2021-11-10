using System;
using Godot;
using Team801.Tibia2.Common.Models.Enums;

namespace Team801.Tibia2.Common.Models.Creature
{
    public abstract class Creature
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public WorldDirection Direction { get; set; }

        //units per second
        public int Speed { get; set; } = 1;

        public override string ToString() => $"[{Name}]";
    }
}