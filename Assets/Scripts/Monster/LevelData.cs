using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "Level Data")]
public class LevelData : ScriptableObject {

	[System.Serializable]
	public struct TWave {
		public WaveData data;
		public float afterPreviousTime;
	}

	public TWave[] waveListOrder;
}
