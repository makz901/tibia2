using System;
using System.Collections.Generic;
using LiteNetLib;
using Team801.Tibia2.Common.Packets;

namespace Team801.Tibia2.Common.PacketHandlers
{
    public abstract class BasePacketHandler
    {
        protected readonly Dictionary<int, long> PeerTimestampDictionary = new Dictionary<int, long>();
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

            if (!PeerTimestampDictionary.TryGetValue(peer.Id, out var lastPacketTimestamp))
            {
                PeerTimestampDictionary.Add(peer.Id, packet.Timestamp);
                Handle(packet, peer);
                return true;
            }

            if (packet.Timestamp >= lastPacketTimestamp)
            {
                PeerTimestampDictionary[peer.Id] = packet.Timestamp;
                Handle(packet, peer);
                return true;
            }

            return false;
        }

        protected abstract void Handle(TPacket packet, NetPeer peer = null);
    }
}