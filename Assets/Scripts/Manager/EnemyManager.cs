using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
	public GameObject enemyPrefab;
	[HideInInspector]
	public List<Transform> enemyList;
	public int numberOfEnemy;
	public Transform holder;

	private void Start()
	{
		Gen(numberOfEnemy);
	}

	void Gen(int count)
	{
		enemyList = new List<Transform>(count);

		for (int i = 0; i < count; i++) {
			float xpos = Random.Range(-10, 10);
			float ypos = Random.Range(-10, 10);

			Vector2 pos = new Vector2(xpos, ypos);

		 	GameObject clone = SpawnMonster(enemyPrefab, pos);

			enemyList.Add(clone.transform);
		}
	}

	public int NumberOfObjectAlive()
	{
		int i = 0;
		int count = 0;
		while (i < enemyList.Count) {
			if (enemyList[i].gameObject.activeInHierarchy)
				count += 1;
			i++;
		}

		return count;
	}

	public Transform GetObjectNearestPoint(Vector2 point)
	{
		if (enemyList.Count == 0)
			return null;

		Transform result = null;
		float minDist = 0;
		for (int i = 0; i < enemyList.Count; i++) {
			if (enemyList[i].gameObject.activeInHierarchy) {
				result = enemyList[i];
				minDist = (point - (Vector2)enemyList[i].position).magnitude;
				break;
			}
		}

		// No enemy left
		if (result == null)
			return null;

		float dist = 0;
		for (int i = 1; i < enemyList.Count; i++) {
			if (enemyList[i].gameObject.activeInHierarchy) {
				dist = (point - (Vector2)enemyList[i].position).magnitude;

				if (dist < minDist) {
					minDist = dist;
					result = enemyList[i];
				}
			}
		}

		return result;
	}

	GameObject SpawnMonster(GameObject model, Vector2 pos)
	{
		// Clone Monster
		VoidEntity clone = ObjectPool.Instance.GetPooledObject(model).GetComponent<VoidEntity>();
		clone.transform.position = pos;
		clone.transform.SetParent(holder);


		// Remove old callback. This ensures that
		// the event only execute the callback once
		// If the event haven't received the callback yet, 
		// RemoveListener wouldn't have done anything
		//clone.healthAtt.OnHealthPointIsZero.RemoveListener(ReduceMonsterLeft);
		//clone.healthAtt.OnHealthPointIsZero.AddListener(ReduceMonsterLeft);

		// Clone Monster UIStatus
		UIManager.Instance.CreateNormalHealthBar(
			clone.uiStatusBarAnchor,
			clone.healthAtt
		);

		return clone.gameObject;
	}
}
