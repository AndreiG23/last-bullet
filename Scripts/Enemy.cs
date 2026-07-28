using Godot;
using System;

public partial class Enemy : Actor
{
    protected override void Die()
    {
        GD.Print("Enemy died");
        base.Die();
    }
}
