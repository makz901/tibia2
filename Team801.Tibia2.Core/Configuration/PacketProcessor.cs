using LiteNetLib;
using LiteNetLib.Utils;
using Team801.Tibia2.Core.Extensions;
using Team801.Tibia2.Core.Models;

namespace Team801.Tibia2.Core.Configuration
{
    public class PacketProcessor : NetPacketProcessor
    {
        private readonly NetDataWriter _writer = new NetDataWriter();

        public PacketProcessor()
        {
            RegisterNestedType((w, v) => w.Put(v), reader => reader.GetVector2());
            RegisterNestedType<PlayerState>();
        }

        public void SendTo<T>(NetPeer peer, T packet, DeliveryMethod deliveryMethod = DeliveryMethod.Unreliable) where T : class, new()
        {
            if (peer == null) return;

            Send(peer, packet, deliveryMethod);
        }
    }
}