using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using LiteNetLib;
using Team801.Tibia2.Common.Configuration;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets;

namespace Team801.Tibia2.Common.Extensions
{
    public static class ContainerExtensions
    {
        private static readonly Dictionary<Type, BasePacketHandler> PacketHandlers = new Dictionary<Type, BasePacketHandler>();
        private static PacketProcessor _processor;

        public static void RegisterHandler<TPacket, THandler>(this ContainerBuilder containerBuilder)
            where THandler : BasePacketHandler<TPacket>
            where TPacket : BasePacket, new()
        {
            containerBuilder.RegisterType<THandler>().SingleInstance();
            containerBuilder.RegisterBuildCallback(scope =>
            {
                _processor = _processor ?? scope.Resolve<PacketProcessor>();
                _processor.SubscribeReusable<TPacket, NetPeer>((packet, peer) =>
                {
                    THandler packetHandler;
                    var type = typeof(THandler);

                    var alreadyRegistered = PacketHandlers.TryGetValue(type, out var handler);
                    if (alreadyRegistered)
                    {
                        packetHandler = handler as THandler;
                    }
                    else
                    {
                        packetHandler = scope.Resolve<THandler>();
                        PacketHandlers.Add(type, packetHandler);
                    }

                    packetHandler?.HandleIfPacketValid(packet, peer);
                });
            });
        }
    }
}