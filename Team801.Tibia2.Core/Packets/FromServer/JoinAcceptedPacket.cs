using Team801.Tibia2.Core.Models;

namespace Team801.Tibia2.Core.Packets.FromServer
{
    public class JoinAcceptedPacket : BasePacket
    {
        public PlayerState PlayerState { get; set; }
    }
}