using System;
using System.Threading;
using UnityEngine;

namespace Team801.Tibia2.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client();
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

        private static void HandleKey(ConsoleKey key, Client client)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    client.Move(Vector2.up);
                    break;
                case ConsoleKey.DownArrow:
                    client.Move(Vector2.down);
                    break;
                case ConsoleKey.LeftArrow:
                    client.Move(Vector2.left);
                    break;
                case ConsoleKey.RightArrow:
                    client.Move(Vector2.right);
                    break;
            }
        }
    }
}
