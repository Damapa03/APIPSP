using Godot;
using System;

public partial class SnailDamageArea : Area2D
{
    [Export] int damage = 10;
    
    public void _on_body_entered(Node body)
	{
		foreach (var child in body.GetChildren())
		{
			

			if (child is PlayerDamageable damageableChild)
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
}
