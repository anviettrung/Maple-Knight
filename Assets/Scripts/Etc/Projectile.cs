using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {

	public bool canBeBroken;

	[HideInInspector] public int teamIndex;
	[HideInInspector] public float damage;

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.tag == "Ground") {
			TryDestroyProjectile();
			return;
		}
		
		Entity oC = other.gameObject.GetComponent<Entity>();
		if (oC != null && oC.TeamNumber != teamIndex) {
			oC.healthAtt.IncreaseCurrentHealth(-damage);
			TryDestroyProjectile();
			return;
		}

		Projectile oP = other.GetComponent<Projectile>();
		if (oP != null && oP.teamIndex != teamIndex) {
			TryDestroyProjectile();
			return;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "GameArea") {
			ObjectPool.Instance.PushToPool(gameObject);
		}
	}

	void TryDestroyProjectile()
	{
		if (canBeBroken)
			ObjectPool.Instance.PushToPool(gameObject);
	}
}
