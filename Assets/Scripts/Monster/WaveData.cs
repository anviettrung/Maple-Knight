using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Data", menuName = "Monster/Wave Data")]
public class WaveData : ScriptableObject {

	[System.Serializable]
	public struct TMonster {
		public Monster model;
		public float afterPreviousTime;
	}
		
	public TMonster[] monsterListOrder;
//	public bool              visibleSignalOnWaveLine;

}
