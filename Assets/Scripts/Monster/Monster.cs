using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity {

	public Transform uiStatusBarAnchor;

	public Vector2 movingPosition;
	public float moveSpeed;
	public float maxSpeed;

	private Vector2 targetVelocity;

	private Transform FuckingPlayer;
	private Rigidbody2D rb;
	private ProjectileShootTriggerable attackAble;

	protected void OnEnable()
	{
		Find_Player();
		moveSpeed = maxSpeed;
		ChangeAbility(0);
		InvokeRepeating("kill_player", 1.5f, 2.0f);
	}

	protected void Start()
	{
		attackAble = weapon.GetComponent<ProjectileShootTriggerable>();
		rb = GetComponent<Rigidbody2D>();

		Find_Player();
	}

	void Update()
	{
		targetVelocity = Vector2.zero;
		ComputeVelocity();
	}

	void FixedUpdate()
	{	
		if (targetVelocity != Vector2.zero)
			rb.position += 0.01f * targetVelocity * Time.deltaTime;
	}

	void Find_Player()
	{
		Hero obj = Object.FindObjectOfType<Hero>(); 
		if (obj != null)
			FuckingPlayer = obj.transform;	
	}

	//SIMPLE AI
	void kill_player()
	{
		if (FuckingPlayer != null) {
			attackAble.targetPosition = FuckingPlayer.position;
			CastCurrentAbility();
		}
	}

	void ComputeVelocity()
	{
		Vector2 distance2D = movingPosition - rb.position;
		Vector2 direction = Vector2.zero;
		if (distance2D.magnitude > 0.01f)
			direction = distance2D.normalized;
		targetVelocity = direction * moveSpeed;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "GameArea") {
			// Instant kill
			if (gameObject.activeInHierarchy)
				this.healthAtt.SetCurrentHealth(0);
		}
	}

	void OnDisable()
	{
		CancelInvoke("kill_player");
	}

	protected override void OnDying ()
	{
		ObjectPool.Instance.PushToPool(gameObject);
	}

}
