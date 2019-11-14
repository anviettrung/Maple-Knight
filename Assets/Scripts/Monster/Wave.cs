using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wave : MonoBehaviour {

	private WaveData data;
	private Vector2 spawnPosition;
	public UnityEvent OnCallNextWave = new UnityEvent();
	public int countMonsterLeft;

	public static Wave Create(WaveData dat, Vector2 spawnPos)
	{
		Wave ins = new GameObject().AddComponent<Wave>();
		ins.data = dat;
		ins.spawnPosition = spawnPos;

		ins.gameObject.name = "Wave";

		return ins;
	}


	Monster SpawnMonster(Monster model, Vector2 pos)
	{
		// Clone Monster
		Monster clone = ObjectPool.Instance.GetPooledObject(model.gameObject).GetComponent<Monster>();
		clone.transform.position = pos;

		clone.healthAtt.OnHealthPointIsZero.AddListener(ReduceMonsterLeft);
		clone.transform.SetParent(this.transform);

		// Clone Monster UIStatus
		UIManager.Instance.CreateNormalHealthBar( 
			clone.uiStatusBarAnchor, 
			clone.healthAtt
		);

		return clone;
	}

//	public void Call()
//	{
//		gameObject.SetActive(true);
//
//		countMonsterLeft = 0;
//		WaveData.TMonster mons;
//		for (int i = 0; i < data.monsters.Length; i++) {
//
//			mons = data.monsters[i];
//			for (int j = 0; j < mons.num; j++) {
//				SpawnMonster(mons.model, spawnPosition - new Vector2(2*j, 0));
//			}
//			countMonsterLeft += mons.num;
//
//		}
//			
//	}

	void ReduceMonsterLeft()
	{
		countMonsterLeft -= 1;
		if (countMonsterLeft == 0) {
			OnCallNextWave.Invoke();
			transform.DetachChildren();
			Destroy(gameObject);
		}
	}

	public void ForceKill()
	{
		for (int i = 0; i < transform.childCount; i++)
			ObjectPool.Instance.PushToPool(transform.GetChild(i).gameObject);
		transform.DetachChildren();
		Destroy(gameObject);
	}

	void OnDisable()
	{
		OnCallNextWave.RemoveAllListeners();
	}
}
