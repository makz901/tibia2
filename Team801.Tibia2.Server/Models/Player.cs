using LiteNetLib;
using Team801.Tibia2.Common.Models.Creature;

namespace Team801.Tibia2.Server.Models
{
    public class Player
    {
        public NetPeer Peer { get; }
        public Character CurrentCharacter { get; set; }

        public Player(NetPeer peer)
        {
            Peer = peer;
        }
    }
}