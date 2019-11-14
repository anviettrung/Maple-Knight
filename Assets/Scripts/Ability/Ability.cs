using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject {

	public Sprite iconSprite;
	public float cooldownDuration;
	public float stCost;
	public float mpCost;

	// Call when the caster choose the ability to be casted
	/// <summary>
	/// Call when the caster chooses this ability to be casted
	/// </summary>
	/// <param name="caster">Caster who selected this spell
	public virtual void OnSelected(Entity caster) {}

	/// <summary>
	/// Call when the caster casts this ability
	/// </summary>
	/// <param name="caster">Caster who casted this spell</param>
	public virtual bool OnSpellStart(Entity caster) 
	{
		if (stCost > 0) {
			StaminaAtt staminaAtt;
			if (SetComponent<StaminaAtt>(caster.gameObject, out staminaAtt) == false) return false;
			if (staminaAtt.Use(stCost) == false) return false;
		}

		if (mpCost > 0) {
			ManaAtt manaAtt;
			if (SetComponent<ManaAtt>(caster.gameObject, out manaAtt) == false) return false;
			if (manaAtt.Use(mpCost) == false) return false;
		}
		return true;
	}

	protected bool SetComponent<T> (GameObject sender, out T receive) where T: MonoBehaviour
	{
		receive = sender.GetComponent<T>();
		if (receive == null) {
			Debug.LogError("Required component " + typeof(T) + " on GameObject " + sender);
		} else {
			return true;
		}

		return false;
	}

}
