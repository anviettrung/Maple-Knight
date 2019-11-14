using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movable : PhysicsObject
{
	public float moveSpeed;

	protected override void ComputeVelocity()
	{
		base.ComputeVelocity();

		Vector2 move = Vector2.zero;

		move = Joystick.Instance.GetDirection() * Joystick.Instance.GetRange();

		targetVelocity = move * moveSpeed;
	}
}

