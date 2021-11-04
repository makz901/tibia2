using Godot;

namespace Team801.Tibia2.Common.Packets.FromClient
{
    public class MoveRequestPacket : BasePacket
    {
        public Vector2 Direction { get; set; }
    }
}
