using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Movable : PhysicsObject
{
	public UnityEvent OnComputeVelocity;

	public float moveSpeed;
	private Vector2 move;
	private Direction4 direction;
	public Vector2 MoveDirection {
		get {
			return move;
		}
		set {
			move = value;
		}
	}

	protected override void ComputeVelocity()
	{
		base.ComputeVelocity();

		OnComputeVelocity.Invoke();

		if (move != Vector2.zero)
			UpdateDirection4();

		targetVelocity = move * moveSpeed;
	}

	public Direction4 GetDirection4()
	{
		return direction;
	}

	private void UpdateDirection4() 
	{
		if (Mathf.Abs(move.y) > Mathf.Abs(move.x))
			direction = move.y > 0 ? Direction4.BACKWARD : Direction4.FORWARD;
		else
			direction = move.x > 0 ? Direction4.RIGHT : Direction4.LEFT;
	}
}

public enum Direction4
{
	FORWARD,
	BACKWARD,
	RIGHT,
	LEFT
};

