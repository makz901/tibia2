using System;
using System.Numerics;
using System.Threading;

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
                    Thread.Sleep(15);
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
                    client.Move(Vector2.UnitY);
                    break;
                case ConsoleKey.DownArrow:
                    client.Move(-Vector2.UnitY);
                    break;
                case ConsoleKey.LeftArrow:
                    client.Move(-Vector2.UnitX);
                    break;
                case ConsoleKey.RightArrow:
                    client.Move(Vector2.UnitX);
                    break;
            }
        }
    }
}
