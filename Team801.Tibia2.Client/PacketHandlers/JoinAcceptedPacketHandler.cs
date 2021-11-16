using System;
using LiteNetLib;
using Team801.Tibia2.Client.Controllers;
using Team801.Tibia2.Client.Managers;
using Team801.Tibia2.Common.Models.Creature;
using Team801.Tibia2.Common.PacketHandlers;
using Team801.Tibia2.Common.Packets.FromServer;

namespace Team801.Tibia2.Client.PacketHandlers
{
    public class JoinAcceptedPacketHandler : BasePacketHandler<JoinAcceptedPacket>
    {
        private readonly GameStateManager _gameStateManager;
        private readonly ICharacterController _characterController;

        public JoinAcceptedPacketHandler(
            GameStateManager gameStateManager,
            ICharacterController characterController)
        {
            _gameStateManager = gameStateManager;
            _characterController = characterController;
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

            // if (_gameStateManager.CreatureList.Any(x => x.Id == myChar.Id) == false)
            // {
            //     _gameStateManager.CreatureList.Add(myChar);
            // }

            _characterController.Callbacks.OnJoined(new DateTime(packet.Timestamp), myChar);
        }
    }
}