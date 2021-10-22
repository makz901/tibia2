using LiteNetLib.Utils;
using Team801.Tibia2.Core.Extensions;
using Team801.Tibia2.Core.Packets;

namespace Team801.Tibia2.Core.Configuration
{
    public class PacketProcessor : NetPacketProcessor
    {
        public PacketProcessor()
        {
            RegisterNestedType((w, v) => w.Put(v), reader => reader.GetVector2());
            RegisterNestedType<PlayerStatePacket>();
        }
    }
}