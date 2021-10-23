using Team801.Tibia2.Core.Models;

namespace Team801.Tibia2.Core.Packets.FromServer
{
    public class PlayerMovedPacket
    {
        public string PlayerName { get; set; }
        public PlayerState PlayerState { get; set; }
    }
}