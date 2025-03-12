using Godot;
using System;

public partial class Snail : CharacterBody2D
{
	[Export]public float Speed = 30.0f;
	[Export] State hit_state;
	[Export] Sprite2D sprite2D;
	[Export] RayCast2D raycastRight;
	[Export] RayCast2D raycastDownRight;
	[Export] RayCast2D raycastLeft;
	[Export] RayCast2D raycastDownLeft;
	public Vector2 starting_move_direction = Vector2.Left;
	CharacterStateMachine stateMachine;
	AnimationTree animationTree;
    private int facint = 1;


    public override void _Ready()
    {
		stateMachine = GetNode<CharacterStateMachine>("CharacterStateMachine");
		animationTree = GetNode<AnimationTree>("AnimationTree");
		animationTree.Active = true;
    }

    public override void _PhysicsProcess(double delta)
	{

		Vector2 velocity = Velocity;
		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.

		Vector2 direction = starting_move_direction;
		if (direction != Vector2.Zero && stateMachine.check_if_can_move())
		{
			velocity.X = direction.X * facint * Speed;
		}
		else if(stateMachine.currentState != hit_state)
			{	
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			}


		if (raycastRight.IsColliding())
		{
			sprite2D.FlipH = true;
			facint = -1;
		}

		if (raycastLeft.IsColliding())
		{
			sprite2D.FlipH = false;
			facint = 1;
		}
		if (!raycastDownRight.IsColliding())
		{
			sprite2D.FlipH = true;
			facint = -1;
		}

		if (!raycastDownLeft.IsColliding())
		{
			sprite2D.FlipH = false;
			facint = 1;
		}

		
		Velocity = velocity;
		MoveAndSlide();
	}
}
