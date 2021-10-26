using LiteNetLib;
using Team801.Tibia2.Core.Configuration;
using Team801.Tibia2.Core.Packets;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.Services
{
    public class PacketManager : IPacketManager
    {
        private readonly PacketProcessor _packetProcessor;

        public PacketManager(PacketProcessor packetProcessor)
        {
            _packetProcessor = packetProcessor;
        }

        public void QueuePacket<TPacket>(TPacket packet, NetPeer peer) where TPacket : BasePacket, new()
        {
            _packetProcessor.SendTo(peer, packet);
        }
    }
}