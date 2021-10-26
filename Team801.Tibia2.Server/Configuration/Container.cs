using Autofac;
using Team801.Tibia2.Core.Configuration;
using Team801.Tibia2.Server.PacketHandlers;
using Team801.Tibia2.Server.Services;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.Configuration
{
    public static class Container
    {
        public static IContainer Instance { get; private set; } = new ContainerBuilder().Build();

        public static void Build()
        {
            var containerBuilder = new ContainerBuilder();

            RegisterServices(containerBuilder);
            RegisterHandlers(containerBuilder);

            Instance = containerBuilder.Build();
        }

        public static void SetupHandlers()
        {
            var processor = Instance.Resolve<PacketProcessor>();

            processor.SetupHandler(Instance.Resolve<JoinPacketHandler>());
            processor.SetupHandler(Instance.Resolve<MovePlayerPacketHandler>());
        }

        private static void RegisterServices(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<PacketProcessor>().SingleInstance();

            containerBuilder.RegisterType<PlayerManager>().As<IPlayerManager>().SingleInstance();
            containerBuilder.RegisterType<PacketManager>().As<IPacketManager>().SingleInstance();
        }

        private static void RegisterHandlers(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<JoinPacketHandler>();
            containerBuilder.RegisterType<MovePlayerPacketHandler>();
        }
    }
}