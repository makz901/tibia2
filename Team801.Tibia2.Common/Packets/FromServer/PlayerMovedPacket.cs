using Godot;

namespace Team801.Tibia2.Common.Packets.FromServer
{
    public class PlayerMovedPacket : BasePacket
    {
        public string PlayerName { get; set; }
        public Vector2 PlayerPosition { get; set; }
    }
}