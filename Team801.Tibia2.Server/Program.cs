﻿namespace Team801.Tibia2.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new GameServer();
            server.Start();
        }
    }
}
