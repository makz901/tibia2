using Autofac;
using Team801.Tibia2.Common.Configuration;
using Team801.Tibia2.Common.Extensions;
using Team801.Tibia2.Common.Packets.FromClient;
using Team801.Tibia2.Server.Events;
using Team801.Tibia2.Server.Game;
using Team801.Tibia2.Server.Game.Contracts;
using Team801.Tibia2.Server.PacketHandlers;
using Team801.Tibia2.Server.Services;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.Configuration
{
    public static class ServerConfig
    {
        public static IContainer Build()
        {
            var containerBuilder = new ContainerBuilder();

            RegisterServices(containerBuilder);
            RegisterHandlers(containerBuilder);
            RegisterEvents(containerBuilder);

            return containerBuilder.Build();
        }

        private static void RegisterEvents(ContainerBuilder containerBuilder)
        {
            //todo: remove single instance?
            containerBuilder.RegisterType<CreatureEvents>().SingleInstance();
        }

        private static void RegisterServices(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<PacketProcessor>().SingleInstance();

            containerBuilder.RegisterType<PlayerManager>().As<IPlayerManager>().SingleInstance();
            containerBuilder.RegisterType<ServerPacketManager>().As<IServerPacketManager>().SingleInstance();
            containerBuilder.RegisterType<GameActionsDispatcher>().As<IGameActionsDispatcher>().SingleInstance();
            containerBuilder.RegisterType<ServerGameCalculations>().As<IServerGameCalculations>().SingleInstance();
        }

        private static void RegisterHandlers(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterHandler<JoinRequestPacket, JoinRequestPacketHandler>();
            containerBuilder.RegisterHandler<MoveRequestPacket, MoveRequestPacketHandler>();
        }
    }
}