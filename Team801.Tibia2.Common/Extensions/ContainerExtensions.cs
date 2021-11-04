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
        private static readonly List<BasePacketHandler> PacketHandlers = new List<BasePacketHandler>();
        private static PacketProcessor _processor;

        public static void RegisterHandler<TPacket, THandler>(this ContainerBuilder containerBuilder)
            where THandler : BasePacketHandler<TPacket>
            where TPacket : BasePacket, new()
        {
            containerBuilder.RegisterType<THandler>();
            containerBuilder.RegisterBuildCallback(scope =>
            {
                _processor = _processor ?? scope.Resolve<PacketProcessor>();
                _processor.SubscribeReusable<TPacket, NetPeer>((packet, peer) =>
                {
                    var handler = PacketHandlers.OfType<THandler>().FirstOrDefault();
                    if (handler == null)
                    {
                        handler = scope.Resolve<THandler>();
                        PacketHandlers.Add(handler);
                    }

                    handler.Handle(packet, peer);
                });
            });
        }
    }
}