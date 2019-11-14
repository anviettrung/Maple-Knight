using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

	protected UIManager() {}

	public Canvas mainCanvas;
	public Canvas dynamicStatusBarCanvas;
	public GameObject restartButton;

	[SerializeField] private GameObject normalHealthBarModel;
	[SerializeField] private GameObject normalStaminaBarModel;
	[SerializeField] private UIObserveAtt playerHealthBar;
	[SerializeField] private UIObserveAtt playerManaBar;
	[SerializeField] private UIObserveAtt playerStaminaBar;
	[SerializeField] private List<AbilityCooldown> abilityCDs;

	/// <summary>
	/// Create a health bar and set it's transform always
	/// equal to objToFollow's transform
	/// </summary>
	/// <returns>The health bar reference</returns>
	/// <param name="objToFollow">Transform of object that the bar will follow</param>
	/// <param name="a">Attribute that the bar will present</param>
	public GameObject CreateNormalHealthBar(Transform objToFollow, Attribute a)
	{
		GameObject ui_clone = ObjectPool.Instance.GetPooledObject(normalHealthBarModel);

		ui_clone.transform.SetParent(dynamicStatusBarCanvas.transform, false);

		ui_clone.GetComponent<UIAnchor>().objectToFollow = objToFollow;
		ui_clone.GetComponentInChildren<UIObserveAtt>().ObservedAttribute = a;

		return ui_clone;
	}

	/// <summary>
	/// Sets the player's attribute links to the bar
	/// so it can present the attribute
	/// There are 3 types it can present, each one set
	/// to difference bar on the game UI
	/// </summary>
	/// <param name="e">One type of Attribute.Energy</param>
	/// <param name="a">Attribute that the bar will present.</param>
	public void SetPlayerStatusBar(Attribute.Energy e, Attribute a) {
		switch (e)
		{

		case Attribute.Energy.HEALTH:
			if (playerHealthBar != null)
				playerHealthBar.ObservedAttribute  = a;
			else
				Debug.LogWarning("Player Health Bar doesn't exist");
			break;
		case Attribute.Energy.MANA:
			if (playerManaBar != null)
				playerManaBar.ObservedAttribute    = a;
			else
				Debug.LogWarning("Player Mana Bar doesn't exist");
			break;
		case Attribute.Energy.STAMINA:
			if (playerStaminaBar != null)
				playerStaminaBar.ObservedAttribute = a;
			else
				Debug.LogWarning("Player Stamina Bar doesn't exist");
			break;
		
		}
	}

	public void SetPlayerAbilityIcon(int abilityIconIndex, Entity.AbilityUsing a)
	{
		if (abilityCDs[abilityIconIndex] != null)
			abilityCDs[abilityIconIndex].ability = a;
	}

}
