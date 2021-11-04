using Godot;
using LiteNetLib.Utils;
using Team801.Tibia2.Common.Extensions;
using Team801.Tibia2.Common.Models.Enums;

namespace Team801.Tibia2.Common.Packets.Models
{
    public struct PlayerStatePacketModel : INetSerializable
    {
        public string Name;
        public Vector2 Position;
        public WorldDirection Direction;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Name);
            writer.Put(Position);
            writer.Put((int) Direction);
        }

        public void Deserialize(NetDataReader reader)
        {
            Name = reader.GetString();
            Position = reader.GetVector2();
            Direction = (WorldDirection) reader.GetInt();
        }
    }
}