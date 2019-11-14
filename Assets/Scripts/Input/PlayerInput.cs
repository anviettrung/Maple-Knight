using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour {

	public string currentAbilityButtonAxisName;

	[HideInInspector] public Hero player;

	private bool validInput;

	void Update()
	{
		if (player != null) {
			validateInput();

			#if UNITY_STANDALONE || UNITY_EDITOR
			//DESKTOP COMPUTERS

			if (Input.GetButtonDown(currentAbilityButtonAxisName) && validInput)
			{
				player.attackAble.targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				player.CastCurrentAbility();
			}
			#endif

			#if UNITY_IOS || UNITY_ANDROID
			//MOBILE DEVICES

			//player.attackAble.targetPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && validInput)
			{
				player.attackAble.targetPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				player.CastCurrentAbility();
			}

			#endif
		}
	}

	public void ChangeAbility(int index)
	{
		player.ChangeAbility(index);
	}


	/// <summary>
	/// Check if pointer over UI element
	/// </summary>
	void validateInput()
	{
		#if UNITY_STANDALONE || UNITY_EDITOR
		//DESKTOP COMPUTERS

		if (Input.GetButtonDown(currentAbilityButtonAxisName))
		{
			if (EventSystem.current.IsPointerOverGameObject())
				validInput = false;
			else
				validInput = true;
		}

		#endif

		#if UNITY_IOS || UNITY_ANDROID
		//MOBILE DEVICES

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
				validInput = false;
			else
				validInput = true;
		}

		#endif
	}

}
