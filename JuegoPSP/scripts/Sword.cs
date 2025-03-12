 using Godot;
using System;
using System.Reflection.Emit;

public partial class Sword : Area2D
{	
	[Export] int damage = 10;
	[Export] Player player;
	[Export] FacingCollisionShape2D facing_shape;

	public override void _Ready()
	{
		Monitoring = false;
		player.Connect("facing_direction_changed", new Callable(this, nameof(on_player_facing_direction_changed)));
	}
	public void _on_body_entered(Node body)
	{
		foreach (var child in body.GetChildren())
		{
			if (child is Damageable damageableChild)
			{
				Vector2 directionToDamageable = ((Node2D)body).GlobalPosition - ((Node2D)GetParent()).GlobalPosition;
				var direction_sign = MathF.Sign(directionToDamageable.X);

				if (direction_sign > 0)
				{
					damageableChild.hit(damage, Vector2.Right);
				}else if (direction_sign < 0)
				{
					damageableChild.hit(damage, Vector2.Left);
				}else damageableChild.hit(damage, Vector2.Zero);
			}
		}
	}

	public void on_player_facing_direction_changed(bool facing_right){
		if (facing_right)
		{
			facing_shape.Position = facing_shape.right_position;
		}else facing_shape.Position = facing_shape.left_position;
	}
}
