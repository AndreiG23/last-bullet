using Godot;
using System;

public partial class Hud : CanvasLayer
{
	private Panel gameOverPanel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameOverPanel = GetNode<Panel>("Control/GameOverPanel");
		gameOverPanel.Visible = false;
	}

	public void ShowGameOver()
    {
        gameOverPanel.Visible = true;
    }

    public void HideGameOver()
    {
        gameOverPanel.Visible = false;
    }

	public void _on_try_again_button_pressed()
	{
		GetTree().Paused = false;
		GetTree().ReloadCurrentScene();
	}

	public void _on_exit_button_pressed()
	{
		GetTree().Quit();
	}
}
