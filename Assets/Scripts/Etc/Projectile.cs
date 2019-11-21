using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {

	public bool canBeBroken;  // destroy when touch entity
	public bool canBeCounter; // destroy by other projectile

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
			if (canBeBroken) 
				TryDestroyProjectile();
			return;
		}

		Projectile oP = other.GetComponent<Projectile>();
		if (oP != null)
		if (TeamTag.Compare(other.gameObject, gameObject) == false) {
			if (canBeCounter) 
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
		ObjectPool.Instance.PushToPool(gameObject);
	}
}
