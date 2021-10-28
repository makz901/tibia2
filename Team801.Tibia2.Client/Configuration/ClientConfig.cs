using Autofac;
using Team801.Tibia2.Client.PacketHandlers;
using Team801.Tibia2.Client.Services.Contracts;
using Team801.Tibia2.Common.Configuration;
using Team801.Tibia2.Common.Extensions;
using Team801.Tibia2.Common.Packets.FromServer;

namespace Team801.Tibia2.Client.Configuration
{
    public class ClientConfig
    {
        public static IContainer IoC { get; private set; }

        public static void Build()
        {
            var containerBuilder = new ContainerBuilder();

            RegisterServices(containerBuilder);
            RegisterHandlers(containerBuilder);

            IoC = containerBuilder.Build();
        }

        private static void RegisterServices(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<PacketProcessor>().SingleInstance();

            containerBuilder.RegisterType<PlayerManager>().As<IPlayerManager>().SingleInstance();
        }

        private static void RegisterHandlers(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterHandler<JoinAcceptedPacket, JoinAcceptedPacketHandler>();
            containerBuilder.RegisterHandler<PlayerMovedPacket, PlayerMovedPacketHandler>();
        }
    }
}