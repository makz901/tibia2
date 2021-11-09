using System;
using System.Threading.Tasks;
using Godot;
using Team801.Tibia2.Client;
using Team801.Tibia2.ConsoleClient.Callbacks;
using Thread = System.Threading.Thread;

namespace Team801.Tibia2.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What's your name:");

            var client = new GameClient();
            client.MovementController.Callbacks = new MovementControllerCallbacks();

            client.Connect(Console.ReadLine());

            ConsoleKeyInfo info;
            do {
                while (!Console.KeyAvailable)
                {
                    client.OnFrameUpdated();
                    Thread.Sleep(20);
                }

                info = Console.ReadKey(true);
                HandleKey(info.Key, client);

            } while(info.Key != ConsoleKey.Backspace);
        }

        private static void HandleKey(ConsoleKey key, GameClient gameClient)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    gameClient.MovementController.Move(Vector2.Down);
                    break;
                case ConsoleKey.DownArrow:
                    gameClient.MovementController.Move(Vector2.Up);
                    break;
                case ConsoleKey.LeftArrow:
                    gameClient.MovementController.Move(Vector2.Left);
                    break;
                case ConsoleKey.RightArrow:
                    gameClient.MovementController.Move(Vector2.Right);
                    break;
            }
        }
    }
}
