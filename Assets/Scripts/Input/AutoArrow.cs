using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoArrow : MonoBehaviour
{	
	[HideInInspector]
	public Hero player;
	protected AttackRange attackRange;

	private void Start()
	{
		player      = gameObject.GetComponent<Hero>();
		attackRange = player.GetComponent<AttackRange>();
	}

	private void Update()
	{
		if (TriggerAttack()) {
			player.attackAble.targetPosition = EnemyManager.Instance.GetObjectNearestPoint((Vector2)player.transform.position).position;

			// Check if enemy in attack range
			if (((Vector2)transform.position-player.attackAble.targetPosition).magnitude <= attackRange.maxValue)
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
