using LiteNetLib;
using Team801.Tibia2.Core.PacketHandlers;
using Team801.Tibia2.Core.Packets;

namespace Team801.Tibia2.Core.Configuration
{
    public static class PacketProcessorExtensions
    {
        public static void SetupHandler<TPacket>(this PacketProcessor processor, BasePacketHandler<TPacket>  handler) 
            where TPacket : BasePacket, new()
        {
            processor.SubscribeReusable<TPacket, NetPeer>(handler.Handle);
        }
    }
}