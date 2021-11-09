using LiteNetLib;
using Team801.Tibia2.Common.Models.Creature;

namespace Team801.Tibia2.Common.Models.Player
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