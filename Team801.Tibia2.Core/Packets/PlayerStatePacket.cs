using LiteNetLib.Utils;
using Team801.Tibia2.Core.Extensions;
using UnityEngine;

namespace Team801.Tibia2.Core.Packets
{
    public struct PlayerStatePacket : INetSerializable 
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