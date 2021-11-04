using LiteNetLib;
using Team801.Tibia2.Common.Configuration;
using Team801.Tibia2.Common.Packets;

namespace Team801.Tibia2.Client.Managers
{
    public class ClientManager
    {
        private readonly PacketProcessor _processor;

        public ClientManager(
            PacketProcessor processor)
        {
            _processor = processor;
        }

        public bool IsConnected => ServerPeer?.ConnectionState == ConnectionState.Connected;
        public NetPeer ServerPeer { get; private set; }

        public void SendToServer<TPacket>(TPacket packet) where TPacket : BasePacket, new()
        {
            _processor.SendTo(ServerPeer, packet);
        }

        public void Initialize(NetPeer peer)
        {
            ServerPeer = peer;
        }
    }
}