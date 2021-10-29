using System.Numerics;

namespace Team801.Tibia2.Common.Packets.FromClient
{
    public class MovePlayerPacket : BasePacket
    {
        public Vector2 Direction { get; set; }
    }
}
