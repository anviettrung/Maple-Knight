using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaAtt : Attribute {

	/// <summary>
	/// Use "amount" of stamina
	/// If it has enough stamina for using then return true
	/// </summary>
	/// <param name="amount">Stamina will be used</param>
	public bool Use(float amount)
	{
		if (amount > CurrentValue) {
			return false;
		}
		CurrentValue -= amount;
		return true;
	}

}
