using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoArrow : MonoBehaviour
{	
	public Hero player;

	private void Start()
	{
		player = gameObject.GetComponent<Hero>();
	}

	private void Update()
	{
		if (TriggerAttack()) {
			player.attackAble.targetPosition = EnemyManager.Instance.GetObjectNearestPoint((Vector2)player.transform.position).position;

			player.CastCurrentAbility();
		}
	}

	private bool TriggerAttack()
	{
		if (EnemyManager.Instance.NumberOfObjectAlive() > 0 && player.mover.MoveDirection == Vector2.zero)
			return true;

		return false;
	}
}
