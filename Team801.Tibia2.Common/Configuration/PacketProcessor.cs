using System;
using Godot;
using LiteNetLib;
using LiteNetLib.Utils;
using Team801.Tibia2.Common.Extensions;
using Team801.Tibia2.Common.Packets.Models;

namespace Team801.Tibia2.Common.Configuration
{
    public class PacketProcessor : NetPacketProcessor
    {
        public PacketProcessor()
        {
            //register classes to be processed in packets, here:

            RegisterNestedType<DateTime>((writer, value) => writer.Put(value.Ticks), reader => new DateTime(reader.GetLong()));
            RegisterNestedType<Vector2>((writer, value) => writer.Put(value), reader => reader.GetVector2());
            RegisterNestedType<PlayerStatePacketModel>();
        }

        public void SendTo<T>(NetPeer peer, T packet, DeliveryMethod deliveryMethod = DeliveryMethod.Unreliable) where T : class, new()
        {
            if (peer == null) return;

            Send(peer, packet, deliveryMethod);
        }
    }
}