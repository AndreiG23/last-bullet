using Godot;
using System;

public partial class Enemy : Actor
{
    public override void _Ready()
    {
        AddToGroup("Enemies");
    }

    protected override void Die()
    {
        GD.Print("Enemy died");
        RemoveFromGroup("Enemies");
        base.Die();
    }
}
