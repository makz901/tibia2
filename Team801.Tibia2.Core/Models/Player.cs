using LiteNetLib;
using UnityEngine;

namespace Team801.Tibia2.Core.Models
{
    public class Player
    {
        public NetPeer Peer;
        public string Username;
        public PlayerState State;
        public PlayerAttributes Attributes = new PlayerAttributes();

        public void Move(Vector2 input, float deltaTime)
        {
            State.Position += input * Attributes.Speed * deltaTime;
        }
    }
}