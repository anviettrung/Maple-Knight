using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Supernova Ability", menuName = "Ability/Supernova")]
public class SupernovaAbility : Ability
{
	public int numberOfProjectiles;
	public bool rotateToFlyDirection;
	public float projectileSpeed;
	public Rigidbody2D projectile;


	public override bool OnSpellStart(Entity caster)
	{
		if (!base.OnSpellStart(caster))
			return false;

		// Check if ProjectileShootTriggerable component is available
		ProjectileShootTriggerable weaponCapability;
		if (SetComponent<ProjectileShootTriggerable>(caster.weapon, out weaponCapability) == false) return false;
		if (!weaponCapability.canAttack) return false;

		// else if can attack
		float deltaAngle = 360.0f / numberOfProjectiles;
		Vector2 directionToTarget = (weaponCapability.targetPosition - weaponCapability.spawnPosition).normalized;
		for (int i = 0; i < numberOfProjectiles; i++) {
			Vector2 direction = ExdMath.Rotate(directionToTarget, i * deltaAngle);

			GameObject clone = ObjectPool.Instance.GetPooledObject(projectile.gameObject);

			clone.transform.position = weaponCapability.spawnPosition;
			clone.transform.rotation = projectile.transform.rotation;

			if (rotateToFlyDirection) {
				float zRotation = clone.transform.eulerAngles.z + ExdMath.SignedAngle(Vector2.right, direction);
				clone.transform.eulerAngles = new Vector3(0, 0, zRotation);
			}

			clone.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed);

			Projectile projectileClone = clone.GetComponent<Projectile>();
			projectileClone.GetComponent<TeamTag>().SetTeam(caster.gameObject);
			projectileClone.damage = caster.damageAtt.maxValue;
		}

		// Spell execute successfully
		return true;
	}

}
