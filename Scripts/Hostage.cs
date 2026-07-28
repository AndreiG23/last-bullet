using Godot;
using System;

public partial class Hostage : Actor
{
	protected override void Die()
		{
			GD.Print("Hostage died");
			base.Die();
		}
}
