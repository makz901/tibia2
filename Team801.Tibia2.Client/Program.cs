using System;
using System.Threading;
using LiteNetLib;

namespace Team801.Tibia2.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client();
            client.Connect("makz");

            //Simulating game updates
            while (!Console.KeyAvailable)
            {
                client.OnFrameUpdated();
                Thread.Sleep(15);
            }
        }
    }
}
