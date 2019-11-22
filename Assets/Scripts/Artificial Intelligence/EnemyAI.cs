using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movable))]
[RequireComponent(typeof(Entity))]
[RequireComponent(typeof(TeamTag))]
public class EnemyAI : MonoBehaviour
{
	protected Movable mover;
	protected Entity  entity; // this gameobject
	protected ProjectileShootTriggerable weapon;
	protected TeamTag teamTag;
	public float chaseRange; // prevent chasing too close to target
	public float attackRange;

	public Collider2D searchingArea;
	protected ContactFilter2D contactFilter;
	protected Collider2D[] resultBuffer = new Collider2D[16];
	protected List<Collider2D> resultBufferList = new List<Collider2D>(16);

	protected Vector2 currentDirection = Vector2.zero;


	private void Awake()
	{
		mover = gameObject.GetComponent<Movable>();
		mover.OnComputeVelocity.AddListener(SetDirection);
		entity = gameObject.GetComponent<Entity>();
		teamTag = gameObject.GetComponent<TeamTag>();
		weapon = entity.weapon.GetComponent<ProjectileShootTriggerable>();
	}

	private void Start()
	{
		contactFilter.useTriggers = true;
		contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(searchingArea.gameObject.layer));
		contactFilter.useLayerMask = true;
	}

	private void FixedUpdate()
	{
		float distToTarget;
		Entity target = SearchingNearestTarget(out distToTarget);

		if (target != null) {
			if (entity.CurrentAbility.cdTimeLeft == 0) {
				weapon.targetPosition = target.transform.position;
				entity.CastCurrentAbility();
			}
			if (distToTarget > chaseRange)
				currentDirection = (target.transform.position - entity.transform.position).normalized;
			else
				currentDirection = Vector2.zero;
		} else {
			currentDirection = Vector2.zero;
		}
	}


	private Entity SearchingNearestTarget(out float distToTarget)
	{
		int count = searchingArea.OverlapCollider(contactFilter, resultBuffer);

		resultBufferList.Clear();
		for (int i = 0; i < count; i++) {
			resultBufferList.Add(resultBuffer[i]);
		}

		Entity target = null;
		float minDist = Mathf.Infinity;
		distToTarget = -1;
		for (int i = 0; i < resultBufferList.Count; i++) {
			Entity targetTmp = resultBufferList[i].GetComponent<Entity>();
			if (targetTmp != null && targetTmp != entity) {
				TeamTag targetTeamTag = resultBufferList[i].GetComponent<TeamTag>();

				if (targetTeamTag != null) 
					if (TeamTag.CompareTeam(teamTag, targetTeamTag) == false) {
						distToTarget = (entity.transform.position - targetTeamTag.transform.position).magnitude;
						if (distToTarget < minDist)
							target = targetTmp;
					}
			}
		}

		return target;
	}

	private void WalkAround()
	{

	}

	private void SetDirection()
	{
		mover.MoveDirection = currentDirection;
	}
}
