using System;
using LiteNetLib;
using LiteNetLib.Utils;
using Team801.Tibia2.Core;
using UnityEngine;

namespace Team801.Tibia2.Client
{
    public class PlayerMovementManager
    {
        private readonly NetPeer _server;

        public PlayerMovementManager(NetPeer server)
        {
            _server = server;
        }

        public void Move(Vector2 direction)
        {
            var writer = new NetDataWriter();
            writer.Put(new PlayerMovePacket{Direction = direction});
            _server?.Send(writer, DeliveryMethod.Unreliable);
        }
    }
}