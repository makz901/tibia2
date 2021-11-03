using Godot;

namespace Team801.Tibia2.Common.Packets.FromServer
{
    public class JoinAcceptedPacket : BasePacket
    {
        public Vector2 PlayerPosition { get; set; }
    }
}