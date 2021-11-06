using System;

namespace Team801.Tibia2.Common.Packets
{
    public class BasePacket
    {
        public long Timestamp { get; set; } = DateTime.Now.Ticks;
    }
}