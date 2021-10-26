using Autofac;
using LiteNetLib;
using Team801.Tibia2.Core.Configuration;
using Team801.Tibia2.Core.PacketHandlers;
using Team801.Tibia2.Core.Packets;

namespace Team801.Tibia2.Core.Extensions
{
    public static class ContainerExtensions
    {
        public static void RegisterHandler<TPacket, THandler>(this ContainerBuilder containerBuilder)
            where THandler : BasePacketHandler<TPacket>
            where TPacket : BasePacket, new()
        {
            containerBuilder.RegisterType<THandler>();
            containerBuilder.RegisterBuildCallback(scope =>
            {
                var processor = scope.Resolve<PacketProcessor>();
                processor.SubscribeReusable<TPacket, NetPeer>(scope.Resolve<THandler>().Handle);
            });
        }
    }
}