using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hero Data", menuName = "Entity/Hero Data")]
public class HeroData : ScriptableObject {

	public string name = "";
	public float moveSpeed;
	public float attackRange;
	public float baseCrit;
	public List<Ability> abilities;
	public List<HeroDataEachLevel> levels;


	public int MaxLevel {
		get {
			return levels.Count;
		}
	}

	[System.Serializable]
	public class HeroDataEachLevel
	{
		public int heathPoint;
		public int manaPoint;
		public float regenManaPoint;
		public int damage;
		public int attackSpeed;
	};

	public HeroDataEachLevel GetDataAtLevel(int x)
	{
		if (x < 1 || x > levels.Count)
			return null;

		return levels[x - 1];
	}
}
