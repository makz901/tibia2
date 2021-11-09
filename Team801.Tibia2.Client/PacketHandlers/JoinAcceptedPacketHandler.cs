using System;
using System.Linq;
using LiteNetLib;
using Team801.Tibia2.Client.Managers;
using Team801.Tibia2.Common.Models.Creature;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromServer;

namespace Team801.Tibia2.Client.PacketHandlers
{
    public class JoinAcceptedPacketHandler : BasePacketHandler<JoinAcceptedPacket>
    {
        private readonly GameStateManager _gameStateManager;

        public JoinAcceptedPacketHandler(
            GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }

        protected override void Handle(JoinAcceptedPacket packet, NetPeer peer = null)
        {
            // Console.WriteLine($"Join accepted by server");
            var myChar = new Character
            {
                Id = packet.CreatureState.Id,
                Name = packet.CreatureState.Name,
                Position = packet.CreatureState.Position,
                Direction = packet.CreatureState.Direction
            };

            _gameStateManager.MyCharacter = myChar;

            if (_gameStateManager.CreatureList.Any(x => x.Id == myChar.Id) == false)
            {
                _gameStateManager.CreatureList.Add(myChar);
            }
        }
    }
}