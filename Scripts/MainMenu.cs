using Godot;
using System;

public partial class MainMenu : Control
{
	public void _on_start_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/game.tscn");
	}

	public void _on_exit_pressed()
	{
		GetTree().Quit();
	}
}
