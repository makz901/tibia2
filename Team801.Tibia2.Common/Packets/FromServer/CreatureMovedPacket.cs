using Godot;

namespace Team801.Tibia2.Common.Packets.FromServer
{
    public class CreatureMovedPacket : BasePacket
    {
        public string CreatureId { get; set; }
        public Vector2 NewPosition { get; set; }
    }
}