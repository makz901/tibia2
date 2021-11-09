using Godot;
using LiteNetLib.Utils;
using Team801.Tibia2.Common.Extensions;
using Team801.Tibia2.Common.Models.Enums;

namespace Team801.Tibia2.Common.Packets.Models
{
    public struct CreatureStatePacketModel : INetSerializable
    {
        public string Id;
        public string Name;
        public Vector2 Position;
        public WorldDirection Direction;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Id);
            writer.Put(Name);
            writer.Put(Position);
            writer.Put((int) Direction);
        }

        public void Deserialize(NetDataReader reader)
        {
            Id = reader.GetString();
            Name = reader.GetString();
            Position = reader.GetVector2();
            Direction = (WorldDirection) reader.GetInt();
        }
    }
}