using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsObject : MonoBehaviour
{
	protected Rigidbody2D rb2d;
	protected Vector2 targetVelocity;
	protected Vector2 velocity;

	protected ContactFilter2D contactFilter;
	protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
	protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

	protected const float minMoveDistance = 0.001f;
	protected const float shellRadius = 0.01f;

	private void OnEnable()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		contactFilter.useTriggers = false;
		contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
		contactFilter.useLayerMask = true;
	}

	private void Update()
	{
		targetVelocity = Vector2.zero;
		ComputeVelocity();
	}

	protected virtual void ComputeVelocity()
	{

	}

	private void FixedUpdate()
	{
		velocity = targetVelocity;

		Vector2 deltaPosition = velocity * Time.deltaTime;

		Movement(Vector2.right * deltaPosition.x);
		Movement(Vector2.up * deltaPosition.y);
	}

	void Movement (Vector2 move)
	{
		float distance = move.magnitude;

		// Recalculate move distance base on collisions
		if (distance > minMoveDistance) {

			int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
			hitBufferList.Clear();
			for (int i = 0; i < count; i++) {
				hitBufferList.Add(hitBuffer[i]);
			}

			for (int i = 0; i < hitBufferList.Count; i++) {

				Vector2 currentNormal = hitBufferList[i].normal;

				float projection = Vector2.Dot(velocity, currentNormal);
				if (projection < 0) {
					velocity = velocity - projection * currentNormal;
				}

				float modifiedDistance = hitBufferList[i].distance - shellRadius;
				distance = modifiedDistance < distance ? modifiedDistance : distance;

			}

		}

		rb2d.position = rb2d.position + move.normalized * distance;
	}
}
