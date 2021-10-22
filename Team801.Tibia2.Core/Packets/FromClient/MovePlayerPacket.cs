using UnityEngine;

namespace Team801.Tibia2.Core.Packets.FromClient
{
    public class MovePlayerPacket
    {
        public Vector2 Direction { get; set; }

        public override string ToString() => $"[{Direction.x}, {Direction.y}]";
    }
}
