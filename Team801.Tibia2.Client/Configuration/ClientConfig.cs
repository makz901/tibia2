using Autofac;
using Team801.Tibia2.Client.PacketHandlers;
using Team801.Tibia2.Client.Services;
using Team801.Tibia2.Client.Services.Contracts;
using Team801.Tibia2.Common.Configuration;
using Team801.Tibia2.Common.Extensions;
using Team801.Tibia2.Common.Packets.FromServer;

namespace Team801.Tibia2.Client.Configuration
{
    public class ClientConfig
    {
        public static IContainer IoC { get; private set; }

        public static void Build(Client client)
        {
            var containerBuilder = new ContainerBuilder();
            var clientNetManager = new ClientNetManager(client) {AutoRecycle = true};

            containerBuilder.RegisterInstance(clientNetManager).As<ClientNetManager>().SingleInstance();

            RegisterServices(containerBuilder);
            RegisterHandlers(containerBuilder);

            IoC = containerBuilder.Build();
        }

        private static void RegisterServices(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<GameStateManager>().As<IGameStateManager>().SingleInstance();

            containerBuilder.RegisterType<PacketProcessor>().SingleInstance();
        }

        private static void RegisterHandlers(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterHandler<JoinAcceptedPacket, JoinAcceptedPacketHandler>();
            containerBuilder.RegisterHandler<PlayerMovedPacket, PlayerMovedPacketHandler>();
        }
    }
}