using Godot;
using LiteNetLib;

namespace Team801.Tibia2.Common.Models
{
    public class Player
    {
        public NetPeer Peer;
        public string Username;
        public PlayerState State;
        public PlayerAttributes Attributes = new PlayerAttributes();

        public void Move(Vector2 input, double deltaTime)
        {
            State.Position += input * Attributes.Speed * (float) deltaTime;
        }
    }
}