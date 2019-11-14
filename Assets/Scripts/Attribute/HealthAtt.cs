using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthAtt : Attribute {

	public UnityEvent OnHealthPointIsZero;

	public void SetCurrentHealth(float hp)
	{
		if (hp < 1) {
			CurrentValue = 0;
			OnHealthPointIsZero.Invoke();
		} else {
			CurrentValue = hp;
		}
	}

	public void IncreaseCurrentHealth(float hp)
	{
		float new_hp = CurrentValue + hp;
		if (new_hp < 1) {
			CurrentValue = 0;
			if (OnHealthPointIsZero != null)
				OnHealthPointIsZero.Invoke();
		} else {
			CurrentValue = new_hp;
		}
	}
		 
}
