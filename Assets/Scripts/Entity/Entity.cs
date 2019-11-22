using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for living objects:
// - Heroes
// - Monsters
[RequireComponent(typeof(HealthAtt))] // Living objects must have heath
[RequireComponent(typeof(TeamTag))]   // For communication with others
public abstract class Entity : MonoBehaviour {

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

	public AbilityUsing CurrentAbility {
		get {
			return abilities[currentAbilityIndex];
		}
	}

	public GameObject weapon;

	[HideInInspector]
	public HealthAtt  healthAtt;
	[HideInInspector]
	public ManaAtt    manaAtt;
	[HideInInspector]
	public StaminaAtt staminaAtt;
	[HideInInspector]
	public DamageAtt  damageAtt;
	[HideInInspector]
	public TeamTag teamTag;

	protected virtual void Awake()
	{
		Init();
	}

	public virtual void Init()
	{
		
		healthAtt  = GetComponent<HealthAtt>();
		manaAtt    = GetComponent<ManaAtt>();
		staminaAtt = GetComponent<StaminaAtt>();
		damageAtt  = GetComponent<DamageAtt>();
		teamTag    = GetComponent<TeamTag>();
		teamTag.Owner = this;
		healthAtt.OnHealthPointIsZero.AddListener(OnDying);

	}

	void Update()
	{
		for (int i = 0; i < abilities.Count; i++) {
			ReduceCooldown(abilities[i], Time.deltaTime);
		}
		
	}

	void ReduceCooldown(AbilityUsing abil, float timePassed)
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
			abilities[currentAbilityIndex].cdTimeLeft = abilities[currentAbilityIndex].data.cooldownDuration;
		}
	}

	public void ChangeAbility(int index) 
	{
		if (0 <= index && index < abilities.Count)
		{
			currentAbilityIndex = index;
			abilities[currentAbilityIndex].data.OnSelected(this);
		} else {
			Debug.LogWarning("[" + gameObject.name + "]" + "doesn't have " + (index+1) + "th ability.");
		}
	}

	protected abstract void OnDying();
}
