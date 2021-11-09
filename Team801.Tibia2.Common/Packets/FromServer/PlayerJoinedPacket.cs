using Team801.Tibia2.Common.Packets.Models;

namespace Team801.Tibia2.Common.Packets.FromServer
{
    public class JoinAcceptedPacket : BasePacket
    {
        public CreatureStatePacketModel CreatureState { get; set; }
    }
}