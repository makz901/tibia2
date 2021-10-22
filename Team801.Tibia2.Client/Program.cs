using System;
using System.Threading;
using System.Threading.Tasks;
using LiteNetLib;
using LiteNetLib.Utils;
using Team801.Tibia2.Core;
using UnityEngine;

namespace Team801.Tibia2.Client
{
    class Program
    {
        private static NetPeer _server;

        static void Main(string[] args)
        {
            Console.WriteLine("Client started");

            var listener = new EventBasedNetListener();
            listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
            {
                Console.WriteLine($"We got: '{dataReader.GetString()}' from {fromPeer.Id}");
                dataReader.Recycle();
            };

            listener.PeerConnectedEvent += peer =>
            {
                _server = peer;
            };

            var client = new NetManager(listener);
            client.Start();
            client.Connect("localhost" /* host ip or name */, 9050 /* port */, "SomeConnectionKey" /* text key or NetDataWriter */);

            //Simulating game updates
            while (!Console.KeyAvailable)
            {
                client.PollEvents();
                Thread.Sleep(15);
            }

            client.Stop();
        }
        public static void Move(Vector2 direction)
        {
            var writer = new NetDataWriter();
            writer.Put(new PlayerMovePacket{Direction = direction});
            _server?.Send(writer, DeliveryMethod.Unreliable);
        }
    }
}
