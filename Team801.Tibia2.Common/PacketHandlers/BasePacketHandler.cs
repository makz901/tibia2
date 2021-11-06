using System;
using System.Collections.Generic;
using LiteNetLib;
using Team801.Tibia2.Common.Packets;

namespace Team801.Tibia2.Common.PacketHandlers
{
    public abstract class BasePacketHandler
    {
        protected readonly int HandlerId = new Random().Next();
        protected readonly Dictionary<int, BasePacket> PacketsDictionary = new Dictionary<int, BasePacket>();
    }

    public abstract class BasePacketHandler<TPacket> : BasePacketHandler where TPacket : BasePacket, new()
    {
        public bool HandleIfPacketValid(TPacket packet, NetPeer peer = null)
        {
            Console.WriteLine($"Handling [{typeof(TPacket).Name}]\t handler: {HandlerId} - packet time [{packet.Timestamp.Ticks}]");

            if (peer == null)
            {
                Handle(packet);
                return true;
            }

            if (!PacketsDictionary.TryGetValue(peer.Id, out var lastPacket))
            {
                PacketsDictionary.Add(peer.Id, packet);
                Handle(packet, peer);
                return true;
            }

            if (packet.Timestamp >= lastPacket.Timestamp)
            {
                PacketsDictionary[peer.Id] = packet;
                Handle(packet, peer);
                return true;
            }

            return false;
        }

        protected abstract void Handle(TPacket packet, NetPeer peer = null);
    }
}