using System.Numerics;
using LiteNetLib.Utils;
using Team801.Tibia2.Common.Extensions;

namespace Team801.Tibia2.Common.Models
{
    public struct PlayerState : INetSerializable 
    {
        public int Pid;
        public Vector2 Position;

        public void Serialize(NetDataWriter writer) 
        {
            writer.Put(Pid);
            writer.Put(Position);
        }

        public void Deserialize(NetDataReader reader) 
        {
            Pid = reader.GetInt();
            Position = reader.GetVector2();
        }
    }
}