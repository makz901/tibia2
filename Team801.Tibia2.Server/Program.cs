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
            var server = new Server();
            server.Start();

            //Simulating game updates
            while (!Console.KeyAvailable)
            {
                server.OnFrameUpdated();
                Thread.Sleep(15);
            }
        }
    }
}
