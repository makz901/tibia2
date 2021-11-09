using Autofac;
using Team801.Tibia2.Client.Controllers;
using Team801.Tibia2.Client.Managers;
using Team801.Tibia2.Client.PacketHandlers;
using Team801.Tibia2.Common.Configuration;
using Team801.Tibia2.Common.Extensions;
using Team801.Tibia2.Common.Packets.FromServer;

namespace Team801.Tibia2.Client.Configuration
{
    public class ClientConfig
    {
        // public static IContainer IoC { get; private set; }

        public static IContainer Build()
        {
            var containerBuilder = new ContainerBuilder();

            RegisterControllers(containerBuilder);
            Services(containerBuilder);
            RegisterPacketHandlers(containerBuilder);

            return containerBuilder.Build();
        }

        private static void RegisterControllers(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<MovementController>().As<IMovementController>().SingleInstance();
        }

        private static void Services(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<PacketProcessor>().SingleInstance();
            containerBuilder.RegisterType<ClientManager>().SingleInstance();
            containerBuilder.RegisterType<GameStateManager>().SingleInstance();
            containerBuilder.RegisterType<GameClientListener>().SingleInstance();
        }

        private static void RegisterPacketHandlers(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterHandler<JoinAcceptedPacket, JoinAcceptedPacketHandler>();
            containerBuilder.RegisterHandler<CreatureMovedPacket, PlayerMovedPacketHandler>();
        }
    }
}