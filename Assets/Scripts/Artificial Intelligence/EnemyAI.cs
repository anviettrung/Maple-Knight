using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movable))]
[RequireComponent(typeof(Entity))]
public class EnemyAI : MonoBehaviour
{
	private Movable mover;

	private void Awake()
	{
		mover = gameObject.GetComponent<Movable>();
		mover.OnComputeVelocity.AddListener(SetDirection);
	}

	private void Update()
	{

	}

	private void FindTarget()
	{ 
	}

	private void GoToTarget()
	{

	}

	private void SetNewDirection()
	{ 
	}

	private void Walking()
	{
	}

	private void SetDirection()
	{
		mover.MoveDirection = Vector2.zero;
	}
}
