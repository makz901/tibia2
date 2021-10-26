using Autofac;
using Team801.Tibia2.Client.PacketHandlers;
using Team801.Tibia2.Client.Services.Contracts;
using Team801.Tibia2.Core.Configuration;

namespace Team801.Tibia2.Client.Configuration
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

            processor.SetupHandler(Instance.Resolve<JoinAcceptedPacketHandler>());
            processor.SetupHandler(Instance.Resolve<PlayerMovedPacketHandler>());
        }

        private static void RegisterServices(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<PacketProcessor>().SingleInstance();

            containerBuilder.RegisterType<PlayerManager>().As<IPlayerManager>().SingleInstance();
        }

        private static void RegisterHandlers(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<JoinAcceptedPacketHandler>();
            containerBuilder.RegisterType<PlayerMovedPacketHandler>();
        }
    }
}