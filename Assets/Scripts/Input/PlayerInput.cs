using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour {

	//public string currentAbilityButtonAxisName;

	private Hero player;
	public Hero Player {
		get {
			return player;
		}
		set {
			player = value;
			playerMover = player.GetComponent<Movable>();
			playerMover.OnComputeVelocity.AddListener(PlayerMove);
		}
	}
	public Movable playerMover;

	public void PlayerMove()
	{
		playerMover.MoveDirection = Joystick.Instance.GetDirection();
	}

	public void ChangeAbility(int index)
	{
		player.ChangeAbility(index);
	}

}
