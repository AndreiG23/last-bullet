using Godot;
using System;

public partial class Bullet : CharacterBody2D
{
	[Export] public float Speed = 800f;
    [Export] public int MaxBounces = 5;

	private Vector2 _velocity;
    private int _bounces;

    [Signal]
    public delegate void OutOfBouncesEventHandler();

    public override void _Ready()
    {
        _bounces = 0;
    }
	// Called when the node enters the scene tree for the first time.
	public override void _PhysicsProcess(double delta)
    {
        KinematicCollision2D collision =
            MoveAndCollide(_velocity * (float)delta);

        if (collision == null)
            return;

        // Hit an enemy or hostage
        if (collision.GetCollider() is Actor actor)
        {
            actor.TakeDamage(1);
            return;
        }

        // Bounce off walls
        _bounces++;

        if (_bounces >= MaxBounces)
        {
            EmitSignal(SignalName.OutOfBounces);
            QueueFree();
            return;
        }

        _velocity = _velocity.Bounce(collision.GetNormal());
        Rotation = _velocity.Angle();
    }

	public void Fire(Vector2 direction)
    {
        _velocity = direction.Normalized() * Speed;
        Rotation = direction.Angle();
    }
}
