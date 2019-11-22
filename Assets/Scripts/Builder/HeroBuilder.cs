using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroBuilder : MonoBehaviour
{
	public HeroData blueprint;
	public Hero     baseModel;
	public Text message;

	private Hero currentInstance;

	public void Build()
	{
		if (baseModel == null) {
			Message("Base model not found");
			return;
		}

		Hero clone = Instantiate(baseModel.gameObject).GetComponent<Hero>();
		if (currentInstance != null)
			Destroy(currentInstance.gameObject);
		currentInstance = clone;

		if (blueprint == null) {
			Message("Blueprint not found!\n" +
				"Please create a new Hero data and drag it to blueprint in Inspector.");
			return;
		}

		// Copy from blueprint
		clone.gameObject.name = blueprint.name;
		clone.mover.moveSpeed = blueprint.moveSpeed;
		clone.GetComponent<AttackRange>().Base = blueprint.attackRange;
		clone.healthAtt.Base = blueprint.GetDataAtLevel(1).heathPoint;
		clone.manaAtt.Base = blueprint.GetDataAtLevel(1).manaPoint;
		clone.manaAtt.regenPerSec = blueprint.GetDataAtLevel(1).regenManaPoint;
		clone.damageAtt.Base = blueprint.GetDataAtLevel(1).damage;
		// Attack speed
		// Abilitty

		Message("Built successfully!\n" +
			"If you want it to be a prefab, drag GameObject name [" + blueprint.name + "] " +
			"in hierarchy onto Prefabs/Entities folder in project window.");
	}

	public void Export()
	{ 
	
	}

	public void Message(string msg)
	{
		message.text = msg;
	}
}
