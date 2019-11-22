using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TeamTag))]
public class Projectile : MonoBehaviour {

	public bool canBeBroken;  // will be destroy if collide entity
	public bool canBeCounter; // will be destroy if collide projectile

	public float damage;
	protected TeamTag teamTag;

	private bool beingDestroy;

	private void Awake()
	{
		teamTag = GetComponent<TeamTag>();
	}

	private void OnEnable()
	{
		beingDestroy = false;
	}



	void OnTriggerEnter2D(Collider2D other) 
	{
		if (beingDestroy)
			return;

		TeamTag otherTeam = other.GetComponent<TeamTag>();

		if (otherTeam == null)
			return;

		Entity oC = other.GetComponent<Entity>();
		if (oC != null)
		if (TeamTag.CompareTeam(otherTeam, teamTag) == false) {
			oC.healthAtt.IncreaseCurrentHealth(-damage);
			if (canBeBroken) 
				TryDestroyProjectile();
			return;
		}

		Projectile oP = other.GetComponent<Projectile>();
		if (oP != null)
		if (TeamTag.CompareTeam(otherTeam, teamTag) == false) {
			if (canBeCounter)
				TryDestroyProjectile();
			return;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Game Area")
			TryDestroyProjectile();
	}

	void TryDestroyProjectile()
	{
		beingDestroy = true;
		ObjectPool.Instance.PushToPool(gameObject);
	}
}
