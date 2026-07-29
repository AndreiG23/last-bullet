using Godot;
using System;

public partial class Game : Node2D
{
	private Timer timer;
	private Hud hud;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		hud = GetNode<Hud>("HUD");
		timer.Timeout += OnTimerTimeout;

		foreach (Hostage hostage in GetTree().GetNodesInGroup("Hostages"))
		{
			hostage.Shot += OnHostageShot;
		}
	}

	private void OnHostageShot(Hostage hostage)
    {
        GD.Print($"{hostage.Name} was shot!");

        if (timer.IsStopped())
        {
            timer.Start();
        }
    }

	public void OnBulletOutOfBounces()
	{
		// Are there any enemies left?
		bool enemiesRemaining = GetTree().GetNodesInGroup("Enemies").Count > 0;

		if (enemiesRemaining)
		{
			if (timer.IsStopped())
				timer.Start();
		}
	}

	private void OnTimerTimeout()
	{
		GetTree().Paused = true;
		hud.ShowGameOver();
	}
}
