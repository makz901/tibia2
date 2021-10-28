using LiteNetLib;
using Team801.Tibia2.Common.Packets;

namespace Team801.Tibia2.Common.PacketHandlers
{
    public abstract class BasePacketHandler
    {
    }

    public abstract class BasePacketHandler<TPacket> : BasePacketHandler where TPacket : BasePacket, new()
    {
        public abstract void Handle(TPacket packet, NetPeer peer = null);
    }
}