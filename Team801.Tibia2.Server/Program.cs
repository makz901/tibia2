using System;
using System.Threading;
using LiteNetLib;
using LiteNetLib.Utils;
using Team801.Tibia2.Core;

namespace Team801.Tibia2.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server started");

            var listener = new EventBasedNetListener();
            var server = new NetManager(listener);

            listener.ConnectionRequestEvent += request =>
            {
                if(server.ConnectedPeersCount < 10 /* max connections */)
                    request.AcceptIfKey("SomeConnectionKey");
                else
                    request.Reject();
            };

            listener.PeerConnectedEvent += peer =>
            {
                Console.WriteLine("We got connection: {0}", peer.EndPoint); // Show peer ip
                NetDataWriter writer = new NetDataWriter();                 // Create writer class
                writer.Put($"Hello client {peer.Id}!");                                // Put some string
                peer.Send(writer, DeliveryMethod.Unreliable);             // Send with reliability
            };

            listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
            {
                var move = dataReader.Get<PlayerMovePacket>();
                Console.WriteLine($"Received move request: {move}");
                dataReader.Recycle();

                NetDataWriter writer = new NetDataWriter();                 // Create writer class
                writer.Put($"You moved to {move}!");                                // Put some string
                fromPeer.Send(writer, DeliveryMethod.Unreliable);   
            };

            server.Start(9050 /* port */);

            while (!Console.KeyAvailable)
            {
                server.PollEvents();
                Thread.Sleep(15);
            }

            server.Stop();
        }
    }
}
