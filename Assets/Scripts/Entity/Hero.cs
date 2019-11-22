using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Entity {

	[HideInInspector]
	public ProjectileShootTriggerable attackAble;

	protected Animator anim;
	[HideInInspector]
	public Movable mover;

	protected void OnEnable()
	{
		attackAble = weapon.GetComponent<ProjectileShootTriggerable>();

		ChangeAbility(0);
	}

	public override void Init()
	{
		base.Init();

		anim = gameObject.GetComponent<Animator>();
		mover = gameObject.GetComponent<Movable>();
	}

	private void LateUpdate()
	{
		// animation
		if (mover.MoveDirection == Vector2.zero) {
			anim.SetBool("isMoving", false);
		} else {
			anim.SetBool("isMoving", true);
			anim.SetFloat("velX", mover.MoveDirection.x);
			anim.SetFloat("velY", mover.MoveDirection.y);
		}
	}

	protected override void OnDying() 
	{
		Debug.Log("Hero has fallen");
		ObjectPool.Instance.PushToPool(gameObject);
	}
}

