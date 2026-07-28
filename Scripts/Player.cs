using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;

	[Export] PackedScene BulletScene;

	public Marker2D _muzzle;
	private Line2D _trajectory;

	public override void _Ready()
	{
    	_muzzle = GetNode<Marker2D>("Marker2D");
		_trajectory = GetNode<Line2D>("Line2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

		Velocity = direction* Speed;

		MoveAndSlide();
		
		LookAt(GetGlobalMousePosition());

		Shoot();

		DrawTrajectory();
	}

	private void Shoot()
	{
		if (!Input.IsActionJustPressed("shoot"))
        return;

		GD.Print("shoot");

    	var bullet = BulletScene.Instantiate<Bullet>();

		bullet.GlobalPosition = _muzzle.GlobalPosition;

		Vector2 direction =
    		(GetGlobalMousePosition() - _muzzle.GlobalPosition).Normalized();

		bullet.Fire(direction);

		GetTree().CurrentScene
    		.GetNode<Node>("Bullets")
    		.AddChild(bullet);
	}

	private void DrawTrajectory()
	{
		_trajectory.ClearPoints();

		Vector2 origin = _muzzle.GlobalPosition;
		Vector2 direction =
			(GetGlobalMousePosition() - origin).Normalized();

		_trajectory.AddPoint(ToLocal(origin));

		PhysicsDirectSpaceState2D space =
			GetWorld2D().DirectSpaceState;

		for (int i = 0; i < 5; i++)
		{
			var query = PhysicsRayQueryParameters2D.Create(
				origin,
				origin + direction * 1000);

			query.CollisionMask = 1; // World only

			var result = space.IntersectRay(query);

			if (result.Count == 0)
			{
				AddDashedSegment(origin, origin + direction * 1000);

				break;
			}

			Vector2 hit = (Vector2)result["position"];
			Vector2 normal = (Vector2)result["normal"];

			AddDashedSegment(origin, hit);

			direction = direction.Bounce(normal);

			origin = hit + direction * 2;
		}
	}

	private void AddDashedSegment(Vector2 start, Vector2 end)
	{
		Vector2 dir = (end - start).Normalized();
		float distance = start.DistanceTo(end);

		for (float t = 0; t < distance; t += 16)
		{
			_trajectory.AddPoint(ToLocal(start + dir * t));

			float endDash = Mathf.Min(t + 8, distance);
			_trajectory.AddPoint(ToLocal(start + dir * endDash));
		}
	}
}
