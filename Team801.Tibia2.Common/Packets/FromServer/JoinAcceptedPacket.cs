using Team801.Tibia2.Common.Models;

namespace Team801.Tibia2.Common.Packets.FromServer
{
    public class JoinAcceptedPacket : BasePacket
    {
        public PlayerState PlayerState { get; set; }
    }
}