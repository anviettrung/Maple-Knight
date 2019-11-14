using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour {

	public Transform waveHolder;
	public LevelData levelData;
	public UnityEvent OnFinishLevel;
	private int countMonsterLeft;

	public IEnumerator Call()
	{
		int k = 0;

		while (k < levelData.waveListOrder.Length) {
			// Call the first one and TMonster have wait time difference 0
			StartCoroutine( CallWave(k) );
			k += 1;
			//----------------------------------

			while (k < levelData.waveListOrder.Length && levelData.waveListOrder[k].afterPreviousTime == 0.0f) {
				StartCoroutine( CallWave(k) );
				k += 1;
			}

			if (k < levelData.waveListOrder.Length)
				yield return new WaitForSeconds (levelData.waveListOrder[k].afterPreviousTime);
		}

		while (countMonsterLeft > 0)
			yield return null;

		OnFinishLevel.Invoke();
	}

	// k stand for WaveIndex in WaveListOrder
	// i stand for MonsterIndex in MonsterListOrder
	void Spawn(int k, int i)
	{
		SpawnMonster (levelData.waveListOrder[k].data.monsterListOrder[i].model, waveHolder.position);
		countMonsterLeft += 1;
	}
		
	Monster SpawnMonster(Monster model, Vector2 pos)
	{
		// Clone Monster
		Monster clone = ObjectPool.Instance.GetPooledObject(model.gameObject).GetComponent<Monster>();
		clone.transform.position = pos;
		clone.transform.SetParent(waveHolder);


		// Remove old callback. This ensures that
		// the event only execute the callback once
		// If the event haven't received the callback yet, 
		// RemoveListener wouldn't have done anything
		clone.healthAtt.OnHealthPointIsZero.RemoveListener( ReduceMonsterLeft ); 
		clone.healthAtt.OnHealthPointIsZero.AddListener( ReduceMonsterLeft );

		// Clone Monster UIStatus
		UIManager.Instance.CreateNormalHealthBar( 
			clone.uiStatusBarAnchor, 
			clone.healthAtt
		);

		return clone;
	}

	// k stand for WaveIndex in WaveListOrder
	IEnumerator CallWave(int k)
	{
		int i = 0;

		int length = levelData.waveListOrder[k].data.monsterListOrder.Length;
		while (i < length) {
			// Call the first one and TMonster have wait time difference 0
			Spawn(k, i);
			i += 1;
			//----------------------------------

			while (i < length && levelData.waveListOrder[k].data.monsterListOrder[i].afterPreviousTime == 0.0f) {
				Spawn(k, i);
				i += 1;
			}

			if (i < length)
				yield return new WaitForSeconds (levelData.waveListOrder[k].data.monsterListOrder[i].afterPreviousTime);
		}
	}


	// From Wave Script
	void ReduceMonsterLeft()
	{
		countMonsterLeft -= 1;
		Debug.Log(countMonsterLeft);
		if (countMonsterLeft == 0) {
			waveHolder.DetachChildren();
		}
	}

	public void ForceKill()
	{
		for (int i = 0; i < waveHolder.childCount; i++)
			ObjectPool.Instance.PushToPool(waveHolder.GetChild(i).gameObject);
		waveHolder.DetachChildren();
		countMonsterLeft = 0;
	}
}
