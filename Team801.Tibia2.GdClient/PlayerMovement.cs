using Godot;

namespace Team801.Tibia2.GdClient
{
	public class PlayerMovement : Area2D
	{
		private Client.Client _client;
		private Vector2 _serverPosition;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_client = new Client.Client();
			_client.Connect("GD Makz");

			_client.MovementController.AddListener(new PlayerMovementListener());
			_client.PlayerManager.PositionChanged += PlayerManagerOnPositionChanged;

			GD.Print("-> connecting to server...");
		}

		private void PlayerManagerOnPositionChanged(Vector2 pos)
		{
			_serverPosition = pos;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(float delta)
		{
			_client.OnFrameUpdated();

			var input = GetInputVector().Normalized();
			if (input.Length() > 0)
			{
				//simple prediction
				_client.PlayerManager.Player.Move(input, delta);
				var calcPos = _client.PlayerManager.Player.State.Position;
				Position = calcPos;
				// end

				// _client.MovementController.MoveToLocation(mouseClick);
				_client.MovementController.MoveToDirection(input);
				_client.Move(input);

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
