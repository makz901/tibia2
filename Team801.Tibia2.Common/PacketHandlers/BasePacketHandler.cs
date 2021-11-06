using System.Collections.Generic;
using LiteNetLib;
using Team801.Tibia2.Common.Packets;

namespace Team801.Tibia2.Common.PacketHandlers
{
    public abstract class BasePacketHandler
    {
        protected readonly Dictionary<int, BasePacket> PacketsDictionary = new Dictionary<int, BasePacket>();
    }

    public abstract class BasePacketHandler<TPacket> : BasePacketHandler where TPacket : BasePacket, new()
    {
        public bool HandleIfPacketValid(TPacket packet, NetPeer peer = null)
        {
            if (peer == null)
            {
                Handle(packet);
                return true;
            }

            if (PacketsDictionary.TryGetValue(peer.Id, out var lastPacket))
            {
                if (packet.Timestamp < lastPacket.Timestamp)
                    return false;

                PacketsDictionary[peer.Id] = packet;
                Handle(packet, peer);
                return true;

            }

            PacketsDictionary.Add(peer.Id, packet);
            Handle(packet, peer);
            return true;
        }

        protected abstract void Handle(TPacket packet, NetPeer peer = null);
    }
}