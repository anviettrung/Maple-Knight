using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {

	public bool canBeBroken;

	[HideInInspector] public float damage;

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.tag == "Ground") {
			TryDestroyProjectile();
			return;
		}
		
		Entity oC = other.gameObject.GetComponent<Entity>();
		if (oC != null)
		if (TeamTag.Compare(other.gameObject, gameObject) == false) {
			oC.healthAtt.IncreaseCurrentHealth(-damage);
			TryDestroyProjectile();
			return;
		}

		Projectile oP = other.GetComponent<Projectile>();
		if (oP != null)
		if (TeamTag.Compare(other.gameObject, gameObject)) {
			TryDestroyProjectile();
			return;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Game Bound") {
			ObjectPool.Instance.PushToPool(gameObject);
		}
	}

	void TryDestroyProjectile()
	{
		if (canBeBroken)
			ObjectPool.Instance.PushToPool(gameObject);
	}
}
