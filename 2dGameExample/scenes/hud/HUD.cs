using Godot;
using System;

public partial class HUD : CanvasLayer
{
  [Signal] public delegate void StartGameEventHandler();

	public void ShowMessage(string text)
	{
		var message = GetNode<Label>("Message");
		message.Text = text;
		message.Show();

		GetNode<Timer>("MessageTimer").Start();
	}

	async public void ShowGameOver()
	{
		ShowMessage("Game Over");

		var messageTimer = GetNode<Timer>("MessageTimer");
		await ToSignal(messageTimer, Timer.SignalName.Timeout);

		var message = GetNode<Label>("Message");
		message.Text = "Dodge the Creeps!";
		message.Show();

		await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
		GetNode<Button>("StartButton").Show();
	}

	public void UpdateScore(int score)
	{
		var scoreLabel = GetNode<Label>("ScoreLabel");
		scoreLabel.Text = score.ToString();

		if (Int32.Parse(scoreLabel.Text) <= 5) {
			scoreLabel.Set("theme_override_colors/font_color", new Color(0,1,0,1));
		} else if (Int32.Parse(scoreLabel.Text) <= 10) {
			scoreLabel.Set("theme_override_colors/font_color", new Color(1,1,0,1));
		} else if (Int32.Parse(scoreLabel.Text) <= 15) {
			scoreLabel.Set("theme_override_colors/font_color", new Color(1,0,0,1));
		} else {
			scoreLabel.Set("theme_override_colors/font_color", new Color(1,1,1,1));
		}
	}

	private void OnStartButtonPressed()
	{
		GetNode<Button>("StartButton").Hide();
		EmitSignal(SignalName.StartGame);
	}

	private void OnMessageTimerTimeout()
	{
		GetNode<Label>("Message").Hide();
	}
}
