using Team801.Tibia2.Common.Models;

namespace Team801.Tibia2.Common.Packets.FromServer
{
    public class PlayerMovedPacket : BasePacket
    {
        public string PlayerName { get; set; }
        public PlayerState PlayerState { get; set; }
    }
}