using Team801.Tibia2.Common.Packets.Models;

namespace Team801.Tibia2.Common.Packets.FromServer
{
    public class CreatureAppearedPacket : BasePacket
    {
        public CreatureStatePacketModel State { get; set; }
    }
}