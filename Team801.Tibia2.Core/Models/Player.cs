using LiteNetLib;

namespace Team801.Tibia2.Core.Models
{
    public class Player
    {
        public NetPeer Peer;
        public string Username;
        public PlayerState State;
    }
}