using LiteNetLib;
using Team801.Tibia2.Core.Packets;

namespace Team801.Tibia2.Core.Models
{
    public class Player
    {
        public NetPeer Peer;
        public string Username;
        public PlayerStatePacket State;
    }
}