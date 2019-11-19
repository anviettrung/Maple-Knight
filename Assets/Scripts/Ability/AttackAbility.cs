using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Ability", menuName = "Ability/Attack")]
public class AttackAbility : Ability {

	public bool rotateToFlyDirection;
	public float projectileSpeed;
	public Rigidbody2D projectile;

	public override bool OnSpellStart (Entity caster)
	{
		if (!base.OnSpellStart(caster))
			return false;

		// Check if ProjectileShootTriggerable component is available
		ProjectileShootTriggerable weaponCapability;
		if (SetComponent<ProjectileShootTriggerable>(caster.weapon, out weaponCapability) == false) return false;
		if (!weaponCapability.canAttack) return false;

		// else if can attack

		Vector2 direction = (weaponCapability.targetPosition - weaponCapability.spawnPosition).normalized;

		GameObject clone = ObjectPool.Instance.GetPooledObject(projectile.gameObject);

		clone.transform.position = weaponCapability.spawnPosition;
		clone.transform.rotation = projectile.transform.rotation;

		if (rotateToFlyDirection) {
			float zRotation = clone.transform.eulerAngles.z + ExdMath.SignedAngle(Vector2.right, direction);
			clone.transform.eulerAngles = new Vector3(0, 0, zRotation);
		}

		clone.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed);

		Projectile projectileClone = clone.GetComponent<Projectile> ();
		projectileClone.GetComponent<TeamTag>().SetTeam(caster.gameObject);
		projectileClone.damage     = caster.damageAtt.maxValue;

		// Spell execute successfully
		return true;
	}

}
