using LiteNetLib;
using Team801.Tibia2.Common.Configuration;
using Team801.Tibia2.Common.Packets;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.Services
{
    public class ServerPacketManager : IServerPacketManager
    {
        private readonly PacketProcessor _packetProcessor;

        public ServerPacketManager(PacketProcessor packetProcessor)
        {
            _packetProcessor = packetProcessor;
        }

        public void Send<TPacket>(TPacket packet, NetPeer peer) where TPacket : BasePacket, new()
        {
            _packetProcessor.SendTo(peer, packet);
        }
    }
}