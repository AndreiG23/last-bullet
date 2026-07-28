using Godot;
using System;

public partial class Actor : CharacterBody2D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
    public int Health = 1;

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
            Die();
    }

    protected virtual void Die()
    {
        QueueFree();
    }
}
