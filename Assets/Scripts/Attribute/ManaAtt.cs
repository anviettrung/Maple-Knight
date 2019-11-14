using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaAtt : Attribute {

	/// <summary>
	/// Use "amount" of mana
	/// If it has enough mana for using then return true
	/// </summary>
	/// <param name="amount">Mana will be used</param>
	public bool Use(float amount)
	{
		if (amount > CurrentValue) {
			return false;
		}
		CurrentValue -= amount;
		return true;
	}

}
