using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour {

	public Image image;
	public Image darkMask;
	public Text  cdTextDisplay;

	private Entity.AbilityUsing ability_;
	public  Entity.AbilityUsing ability {
		get {
			return ability_;
		}
		set {
			ability_ = value;
			if (ability_ != null)
				Init();
		}
	}

	void LateUpdate()
	{
		if (ability_ != null && ability_.cdTimeLeft > 0) {
			darkMask.enabled = true;
			darkMask.fillAmount = (ability_.cdTimeLeft / ability_.data.cooldownDuration);

			cdTextDisplay.enabled = true;
			cdTextDisplay.text = Mathf.Round(ability_.cdTimeLeft).ToString();
		} else {
			darkMask.enabled = false;
			cdTextDisplay.enabled = false;
		}
	}

	void Init()
	{
		image.sprite = ability_.data.iconSprite;
	}
}
