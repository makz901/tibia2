using Autofac;
using Team801.Tibia2.Core.Configuration;
using Team801.Tibia2.Core.Extensions;
using Team801.Tibia2.Core.Packets.FromClient;
using Team801.Tibia2.Server.PacketHandlers;
using Team801.Tibia2.Server.Services;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.Configuration
{
    public static class ServerConfig
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
            containerBuilder.RegisterType<PacketManager>().As<IPacketManager>().SingleInstance();
            containerBuilder.RegisterType<GameTimer>().As<IGameTimer>().SingleInstance();
        }

        private static void RegisterHandlers(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterHandler<JoinPacket, JoinPacketHandler>();
            containerBuilder.RegisterHandler<MovePlayerPacket, MovePlayerPacketHandler>();
        }
    }
}