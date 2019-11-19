using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidEntity : Entity
{
	public Transform uiStatusBarAnchor;

	protected override void OnDying()
	{
		ObjectPool.Instance.PushToPool(gameObject);
	}
}
