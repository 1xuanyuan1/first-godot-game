using Godot;
using System;

public partial class Player : CharacterBody2D
{
  public const float Speed = 150.0f;
  public const float ACCELERATE = 5.0f;

  public override void _PhysicsProcess(double delta)
  {
    Vector2 velocity = Velocity;

    // Add the gravity.
    //if (!IsOnFloor())
    //{
    //velocity += GetGravity() * (float)delta;
    //}

    //// Handle Jump.
    //if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
    //{
    //velocity.Y = JumpVelocity;
    //}

    // Get the input direction and handle the movement/deceleration.
    // As good practice, you should replace UI actions with custom gameplay actions.
    Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");
    Velocity = velocity.Lerp(direction * Speed, (float)(ACCELERATE * delta));
    MoveAndSlide();
  }
}
