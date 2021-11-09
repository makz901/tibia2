using System;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using Team801.Tibia2.Common.Configuration;

namespace Team801.Tibia2.Client
{
    public class GameClientListener : INetEventListener
    {
        private readonly PacketProcessor _packetProcessor;

        public event Action<NetPeer> OnConnected; 
        public event Action<DisconnectInfo> OnDisconnected;

        public GameClientListener(
            PacketProcessor packetProcessor)
        {
            _packetProcessor = packetProcessor;
        }

        public void OnPeerConnected(NetPeer peer)
        {
            OnConnected?.Invoke(peer);
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            OnDisconnected?.Invoke(disconnectInfo);
        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {
        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod) 
        {
            _packetProcessor.ReadAllPackets(reader, peer);
        }

        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {
        }

        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {
        }

        public void OnConnectionRequest(ConnectionRequest request)
        {
        }
    }
}