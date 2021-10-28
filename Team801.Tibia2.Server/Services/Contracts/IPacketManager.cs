using LiteNetLib;
using Team801.Tibia2.Common.Packets;

namespace Team801.Tibia2.Server.Services.Contracts
{
    public interface IPacketManager
    {
        void QueuePacket<TPacket>(TPacket packet, NetPeer peer) where TPacket : BasePacket, new();
    }
}