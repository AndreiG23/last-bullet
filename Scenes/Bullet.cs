using Godot;
using System;

public partial class Bullet : CharacterBody2D
{
	[Export] public float Speed = 800f;
    [Export] public int MaxBounces = 5;

	private Vector2 _velocity;
    private int _bounces;

	// Called when the node enters the scene tree for the first time.
	public override void _PhysicsProcess(double delta)
    {
        Velocity = _velocity;

        KinematicCollision2D collision = MoveAndCollide(_velocity * (float)delta);

        if (collision != null)
        {
            _velocity = _velocity.Bounce(collision.GetNormal());

            Rotation = _velocity.Angle();

            _bounces++;

            if (_bounces >= MaxBounces)
                QueueFree();
        }
    }

	public void Fire(Vector2 direction)
    {
        _velocity = direction.Normalized() * Speed;
        Rotation = direction.Angle();
    }
}
