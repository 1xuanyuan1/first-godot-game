using System;
using Godot;

public partial class Player : CharacterBody2D
{
  public const float Speed = 150.0f;
  public const float ACCELERATE = 5.0f;
  public string facingDirection = "Down";
  AnimatedSprite2D animatedSprite2D;

  public override void _Ready()
  {
	animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
  }

  public override void _PhysicsProcess(double delta)
  {
	Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");

	Velocity = Velocity.Lerp(direction * Speed, (float)(ACCELERATE * delta));
	MoveAndSlide();
	facingDirection = GetDirection(direction);
	if (Velocity.Length() > 20)
	  animatedSprite2D.Play("Run_" + facingDirection);
	else
	  animatedSprite2D.Play("Idle_" + facingDirection);
  }
  public string GetDirection(Vector2 direction)
  {
	if (direction == Vector2.Zero)
	  return facingDirection;
	if (direction.Y > 0)
	  facingDirection = "Down";
	else if (direction.Y < 0)
	  facingDirection = "Up";
	else if (direction.X > 0)
	  facingDirection = "Right";
	else if (direction.X < 0)
	  facingDirection = "Left";
	return facingDirection;
  }
}
