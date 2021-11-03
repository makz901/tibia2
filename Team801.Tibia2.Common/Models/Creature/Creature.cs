using Godot;

namespace Team801.Tibia2.Common.Models.Creature
{
    public abstract class Creature
    {
        public string CreatureId;
        public string Name;
        public Vector2 Position;
        public Vector2 Rotation;
        public Attributes Attributes = new Attributes();
    }
}