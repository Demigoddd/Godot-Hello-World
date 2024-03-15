using Godot;
using System;

public partial class Hero : CharacterBody2D
{
	private int _speed = 400;
	private float _angularSpeed = Mathf.Pi;
	private Vector2 heroIconNewScale = new Vector2((float)0.2, (float)0.2);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Hello World");
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

		var heroTimer = GetNode<Timer>("HeroTimer");
		if (IsProcessing())
		{
			heroTimer.Start();
		}
		else
		{
			heroTimer.Stop();
		}
	}

	private void onTimerTimeout()
	{
		var heroIcon = GetNode<Node2D>("HeroIcon");
		var heroCollision = GetNode<CollisionShape2D>("HeroCollisionShape");

		if (heroIcon.Scale >= new Vector2(2,2))
		{
			heroIcon.Scale -= new Vector2(1, 1);
			heroCollision.Scale -= new Vector2(1, 1);
		}
		else
		{
			heroIcon.Scale += new Vector2(1, 1);
			heroCollision.Scale += new Vector2(1, 1);
		}

		GD.Print(heroIcon.Scale);
		GD.Print(heroCollision.Scale);
	}
}
