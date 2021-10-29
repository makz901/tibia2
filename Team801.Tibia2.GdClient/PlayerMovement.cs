using Godot;

namespace Team801.Tibia2.GdClient
{
	public class PlayerMovement : Area2D
	{
		private const int Speed = 100;

		private Client.Client _client;
		private Vector2 _serverPosition;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_client = new Client.Client();
			_client.Connect("GD Makz");

			_client.PlayerManager.PositionChanged += PlayerManagerOnPositionChanged;

			GD.Print("-> connecting to server...");
		}

		private void PlayerManagerOnPositionChanged(System.Numerics.Vector2 pos)
		{
			_serverPosition = new Vector2(pos.X, pos.Y);
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(float delta)
		{
			_client.OnFrameUpdated();

			var input = GetInputVector().Normalized();
			if (input.Length() > 0)
			{
				//simple prediction
				_client.PlayerManager.Player.Move(new System.Numerics.Vector2(input.x, input.y), delta);
				var calcPos = _client.PlayerManager.Player.State.Position;
				Position = new Vector2(calcPos.X, calcPos.Y);
				// end

				_client.Move(new System.Numerics.Vector2(input.x, input.y));

				GD.Print($"-> processing input: {input}");
			}

			// server position update
			Position = _serverPosition;
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
