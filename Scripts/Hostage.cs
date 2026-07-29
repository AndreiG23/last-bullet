using Godot;
using System;

public partial class Hostage : Actor
{
	[Signal]
    public delegate void ShotEventHandler(Hostage hostage);

	public override void _Ready()
	{
		AddToGroup("Hostages");
	}

	protected override void Die()
	{
		EmitSignal(SignalName.Shot, this);
		GD.Print("Emitting Shot signal");
		RemoveFromGroup("Enemies");
		base.Die();
	}
}
