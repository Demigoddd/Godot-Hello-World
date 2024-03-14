using Godot;
using System;

public partial class Hero : CharacterBody2D
{
	private int _speed = 400;
	private float _angularSpeed = Mathf.Pi;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Hello World");

		var timer = GetNode<Timer>("Timer");
		timer.Timeout += OnTimerTimeout;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Rotation += _angularSpeed * (float)delta;
		// var velocity = Vector2.Up.Rotated(Rotation) * _speed;
		// Position += velocity * (float)delta;

		var direction = 0;
		if (Input.IsActionPressed("ui_left"))
		{
			direction = -1;
		}
		if (Input.IsActionPressed("ui_right"))
		{
			direction = 1;
		}

		Rotation += _angularSpeed * direction * (float)delta;

		var velocity = Vector2.Zero;
		if (Input.IsActionPressed("ui_up"))
		{
			velocity = Vector2.Up.Rotated(Rotation) * _speed;
		}
		if (Input.IsActionPressed("ui_down"))
		{
			velocity = Vector2.Down.Rotated(Rotation) * _speed;
		}

		Position += velocity * (float)delta;
	}

	private void OnButtonPressed()
	{
		SetProcess(!IsProcessing());
	}

	private void OnTimerTimeout()
	{
		// Visible = !Visible;

		var sprite = GetNode<Sprite2D>("Hero");
		// sprite.Transform.Scale = Vector2(1.5, 1.5);
	}
}
