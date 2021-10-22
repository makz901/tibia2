using LiteNetLib.Utils;
using Team801.Tibia2.Core.Extensions;
using UnityEngine;

namespace Team801.Tibia2.Core.Packets
{
    public class MovementInputPacket : INetSerializable
    {
        public Vector2 Direction { get; set; }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Direction);
        }

        public void Deserialize(NetDataReader reader)
        {
            Direction = reader.GetVector2();
        }

        public override string ToString() => $"[{Direction.x}, {Direction.y}]";
    }
}
