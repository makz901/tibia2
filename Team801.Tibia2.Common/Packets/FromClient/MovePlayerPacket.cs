using UnityEngine;

namespace Team801.Tibia2.Common.Packets.FromClient
{
    public class MovePlayerPacket : BasePacket
    {
        public Vector2 Direction { get; set; }

        public override string ToString() => $"[{Direction.x}, {Direction.y}]";
    }
}
