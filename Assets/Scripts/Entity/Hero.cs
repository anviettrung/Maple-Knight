using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Entity {

	[HideInInspector]
	public ProjectileShootTriggerable attackAble;

	protected void OnEnable()
	{
		attackAble = weapon.GetComponent<ProjectileShootTriggerable>();

		ChangeAbility(0);
	}

	protected override void OnDying() 
	{
		Debug.Log("Hero has fallen");
		ObjectPool.Instance.PushToPool(gameObject);
	}
}

