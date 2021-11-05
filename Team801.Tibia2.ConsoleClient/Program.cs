using System;
using Godot;
using Team801.Tibia2.Common.Configuration;
using Thread = System.Threading.Thread;

namespace Team801.Tibia2.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client.Client();
            Console.WriteLine("What's your name:");
            client.Connect(Console.ReadLine());

            ConsoleKeyInfo info;

            do {
                while (!Console.KeyAvailable)
                {
                    client.OnFrameUpdated();
                    Thread.Sleep(Constants.SyncInterval);
                }

                info = Console.ReadKey(true);
                HandleKey(info.Key, client);

            } while(info.Key != ConsoleKey.Backspace);
        }

        private static void HandleKey(ConsoleKey key, Client.Client client)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    client.MovementController.Move(Vector2.Down);
                    break;
                case ConsoleKey.DownArrow:
                    client.MovementController.Move(Vector2.Up);
                    break;
                case ConsoleKey.LeftArrow:
                    client.MovementController.Move(Vector2.Left);
                    break;
                case ConsoleKey.RightArrow:
                    client.MovementController.Move(Vector2.Right);
                    break;
            }
        }
    }
}
