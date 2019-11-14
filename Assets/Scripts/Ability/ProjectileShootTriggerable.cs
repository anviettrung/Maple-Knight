using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShootTriggerable : MonoBehaviour {

//	private AttackAbility data;
	public bool canAttack;
	[HideInInspector]
	public Vector2 targetPosition;

	public Vector2 spawnPosition { 
		get {
			return (Vector2)transform.position;
		}
	}

}
