using LiteNetLib.Utils;
using UnityEngine;

namespace Team801.Tibia2.Core
{
    public class PlayerMovePacket : INetSerializable
    {
        public Vector2 Direction { get; set; }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Direction.x);
            writer.Put(Direction.y);
        }

        public void Deserialize(NetDataReader reader)
        {
            var x = reader.GetFloat();
            var y = reader.GetFloat();
            Direction = new Vector2(x, y);
        }

        public override string ToString() => $"[{Direction.x}, {Direction.y}]";
    }
}
