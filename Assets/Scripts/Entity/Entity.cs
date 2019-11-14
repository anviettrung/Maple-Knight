using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for living objects:
// - Heroes
// - Monsters
[RequireComponent(typeof(HealthAtt))]
[RequireComponent(typeof(DamageAtt))]
public abstract class Entity : MonoBehaviour {

	public int TeamNumber;

	[System.Serializable]
	public class AbilityUsing {
		
		public Ability data;

		[HideInInspector]
		public float   cdTimeLeft = 0;
	};

	[SerializeField]
	protected List<AbilityUsing> abilities;
	public List<AbilityUsing> Abilities {
		get {
			return abilities;
		}
	}
	protected int currentAbilityIndex;

	public GameObject weapon;

	[HideInInspector]
	public HealthAtt  healthAtt;
	[HideInInspector]
	public ManaAtt    manaAtt;
	[HideInInspector]
	public StaminaAtt staminaAtt;
	[HideInInspector]
	public DamageAtt  damageAtt;

	protected virtual void Awake()
	{
		
		healthAtt  = GetComponent<HealthAtt> ();
		manaAtt    = GetComponent<ManaAtt> ();
		staminaAtt = GetComponent<StaminaAtt> ();
		damageAtt  = GetComponent<DamageAtt> ();
		healthAtt.OnHealthPointIsZero.AddListener(OnDying);

	}

	void Update()
	{
		for (int i = 0; i < abilities.Count; i++) {
			Cooldown(abilities[i], Time.deltaTime);
		}
		
	}

	void Cooldown(AbilityUsing abil, float timePassed)
	{
		if (abil.cdTimeLeft > 0) {
			abil.cdTimeLeft -= timePassed;
			if (abil.cdTimeLeft < 0)
				abil.cdTimeLeft = 0;
		}
	}

	public void CastCurrentAbility()
	{
		if (abilities[currentAbilityIndex].cdTimeLeft == 0) {
			abilities[currentAbilityIndex].data.OnSpellStart(this);
			abilities[currentAbilityIndex].cdTimeLeft += abilities[currentAbilityIndex].data.cooldownDuration;
		}
	}

	public void ChangeAbility(int index) 
	{
		if (0 <= index && index < abilities.Count)
		{
			currentAbilityIndex = index;
			abilities[currentAbilityIndex].data.OnSelected(this);
		} else {
			Debug.LogWarning(abilities[index]);
		}
	}

	protected abstract void OnDying();
}
