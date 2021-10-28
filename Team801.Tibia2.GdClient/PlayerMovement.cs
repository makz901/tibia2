using System;
using Godot;

namespace Team801.Tibia2.GdClient
{
	public class PlayerMovement : Area2D
	{
		private const int Speed = 100;

		private Client.Client _client;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_client = new Client.Client();
			_client.Connect("GD Makz");

			GD.Print("-> connecting to server...");
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(float delta)
		{
			var input = GetInputVector().Normalized();
			if (input.Length() > 0)
			{
				Position += input * delta * Speed;
				_client.Move(new UnityEngine.Vector2(input.x, input.y));

				GD.Print($"-> processing input: {input}");
			}
		}

		private Vector2 GetInputVector()
		{
			var velocity = new Vector2();

			if (Input.IsActionPressed("ui_right"))
				velocity.x += 1;

			if (Input.IsActionPressed("ui_left"))
				velocity.x -= 1;

			if (Input.IsActionPressed("ui_down"))
				velocity.y += 1;

			if (Input.IsActionPressed("ui_up"))
				velocity.y -= 1;

			return velocity.Normalized();
		}
	}
}
