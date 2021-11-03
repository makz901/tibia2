using Autofac;
using LiteNetLib;
using LiteNetLib.Layers;
using Team801.Tibia2.Client.Configuration;
using Team801.Tibia2.Common.Configuration;
using Team801.Tibia2.Common.Packets;

namespace Team801.Tibia2.Client.Services
{
    public class ClientNetManager : NetManager
    {
        private readonly PacketProcessor _packetProcessor;

        public ClientNetManager(
            INetEventListener listener,
            PacketLayerBase extraPacketLayer = null)
            : base(listener, extraPacketLayer)
        {
            _packetProcessor = ClientConfig.IoC.Resolve<PacketProcessor>();
        }

        public void Send(BasePacket packet)
        {
            _packetProcessor.SendTo(FirstPeer, packet);
        }

        public void ReadAllPackets(NetPacketReader reader, NetPeer peer)
        {
            _packetProcessor.ReadAllPackets(reader, peer);
        }
    }
}