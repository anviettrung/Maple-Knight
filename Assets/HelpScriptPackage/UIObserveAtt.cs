using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class UIObserveAtt : MonoBehaviour {

	private Slider slider;
	private Attribute observedAttribute;
	public Attribute ObservedAttribute {
		get {
			return observedAttribute;
		}
		set {
			if (observedAttribute != null) {
				// Remove the link to old attribute
				observedAttribute.OnMaxValueChanged.RemoveListener( changeMaxValue );
				observedAttribute.OnCurrentValueChanged.RemoveListener( changeCurrentValue );
			}

			observedAttribute = value;
			if (observedAttribute != null) {
				// Call first time
				changeMaxValue(observedAttribute.maxValue);
				changeCurrentValue(observedAttribute.CurrentValue);

				// Observe
				observedAttribute.OnMaxValueChanged.AddListener( changeMaxValue );
				observedAttribute.OnCurrentValueChanged.AddListener( changeCurrentValue );
			}
		}
	}
	[SerializeField] private Text textCurrentValue;

	void Awake()
	{
		slider = GetComponent<Slider>();
	}

	void OnEnable()
	{
		if (observedAttribute != null) {
			// Setting
			ObservedAttribute = observedAttribute;
		}

	}


	/// <summary>
	/// Changes the max value of slider
	/// </summary>
	/// <param name="val">Max value</param>
	void changeMaxValue(float val)
	{
		if (slider != null) {
			slider.maxValue = val;
		}
	}

	/// <summary>
	/// Changes the current value of slider
	/// </summary>
	/// <param name="val">Current value</param>
	void changeCurrentValue(float val)
	{
		if (slider != null) {
			slider.value = val;
			if (textCurrentValue != null)
				textCurrentValue.text = val.ToString("0.");
		}
	}
}
