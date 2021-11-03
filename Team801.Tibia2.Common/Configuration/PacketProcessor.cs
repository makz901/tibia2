using LiteNetLib;
using LiteNetLib.Utils;
using Team801.Tibia2.Common.Extensions;
using Team801.Tibia2.Common.Models;

namespace Team801.Tibia2.Common.Configuration
{
    public class PacketProcessor : NetPacketProcessor
    {
        public PacketProcessor()
        {
            RegisterNestedType((w, v) => w.Put(v), reader => reader.GetVector2());
        }

        public void SendTo<T>(NetPeer peer, T packet, DeliveryMethod deliveryMethod = DeliveryMethod.Unreliable) where T : class, new()
        {
            if (peer == null) return;

            Send(peer, packet, deliveryMethod);
        }
    }
}