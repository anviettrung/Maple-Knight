using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventFloat : UnityEvent<float> {}

public class Attribute : MonoBehaviour {

	public enum Energy {
		HEALTH,
		MANA,
		STAMINA
	};

	[SerializeField] private float m_base;
	public float Base {
		get {
			return m_base;
		}
		set {
			float increase = value - m_base;
			currentValue = currentValue + increase * (currentValue / maxValue);
			m_base = value;

			if (OnMaxValueChanged != null)
				OnMaxValueChanged.Invoke(maxValue);
		}
	}

	[SerializeField] private float m_bonus;
	public float Bonus {
		get {
			return m_bonus;
		}
		set {
			float increase = value - m_bonus;
			currentValue = currentValue + increase * (currentValue / maxValue);
			m_bonus = value;

			if (OnMaxValueChanged != null)
				OnMaxValueChanged.Invoke(maxValue);
		}
	}

	[SerializeField] private float m_multi = 1;
	public float Multiple {
		get {
			return m_multi;
		}
		set {
			float increase = value - m_multi;
			currentValue = currentValue * (1 + increase);
			m_multi = value;

			if (OnMaxValueChanged != null)
				OnMaxValueChanged.Invoke(maxValue);
		}
	}

	public float maxValue { 
		get {
			return m_multi * (m_base + m_bonus);
		}
	}

	[SerializeField]
	private float currentValue;
	public float CurrentValue {
		get {
			return currentValue;
		}
		set {
			if (value > maxValue) {
				currentValue = maxValue;
			} else {
				currentValue = value;
			}

			if (OnCurrentValueChanged != null)
				OnCurrentValueChanged.Invoke(CurrentValue);
		}
	}

	public bool regenable;
	public float regenPerSec;

	public UnityEventFloat OnMaxValueChanged;
	public UnityEventFloat OnCurrentValueChanged;

	protected virtual void OnEnable()
	{
		CurrentValue = maxValue;
	}

	protected virtual void FixedUpdate()
	{
		if (regenable)
			CurrentValue += regenPerSec * Time.deltaTime;
	}

	protected virtual void OnDisable()
	{
		OnMaxValueChanged.RemoveAllListeners();
		OnCurrentValueChanged.RemoveAllListeners();
	}
}
