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
	protected TeamTag teamTag;
	public float attackRange; // prevent chasing too close to target

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
	}

	private void Start()
	{
		contactFilter.useTriggers = true;
		contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(searchingArea.gameObject.layer));
		contactFilter.useLayerMask = true;
	}

	private void FixedUpdate()
	{
		Entity target = SearchingNearestTarget(); 
		if (target != null) {
			currentDirection = (target.transform.position - entity.transform.position).normalized;
		} else {
			currentDirection = Vector2.zero;
		}
	}


	private Entity SearchingNearestTarget()
	{
		int count = searchingArea.OverlapCollider(contactFilter, resultBuffer);

		resultBufferList.Clear();
		for (int i = 0; i < count; i++) {
			resultBufferList.Add(resultBuffer[i]);
		}

		Entity target = null;
		float minDist = Mathf.Infinity;
		float dist = 0;
		for (int i = 0; i < resultBufferList.Count; i++) {
			Entity targetTmp = resultBufferList[i].GetComponent<Entity>();
			if (targetTmp != null && targetTmp != entity) {
				TeamTag targetTeamTag = resultBufferList[i].GetComponent<TeamTag>();

				if (targetTeamTag != null) 
					if (TeamTag.Compare(teamTag, targetTeamTag) == false) {
						dist = (entity.transform.position - targetTeamTag.transform.position).magnitude;
						if (dist < minDist && dist > attackRange)
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
