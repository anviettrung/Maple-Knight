using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : Singleton<Joystick>
{
	public Image circle;
	public Image outerCircle;
	public float maxRange;

	private Vector2 direction;
	private float delta; // value from 0 to 1
	private Vector2 pointA;
	private Vector2 pointB;

	private bool IsUsingJoystick = false;
	private int touchID;

	public Vector2 GetDirection()
	{
		if (IsUsingJoystick)
			return direction;

		return Vector2.zero;
	}

	public float GetRange()
	{
		if (IsUsingJoystick)
			return delta / maxRange;

		return 0;
	}


	private void Update()
	{
		if (IsUsingJoystick == false) {
			FindTouchBegin();
		} else {
			if (IsTouchEnd()) {
				EndJoystick();
			} else {
				MovingJoystick();
			}
		}
	}

	private void FindTouchBegin()
	{
		for (int i = 0; i < Input.touchCount; i++) {
			// valid (touch(i)); // Check if this touch UI or not
			Touch t = Input.GetTouch(i);
			if (t.phase == TouchPhase.Began) {
				Debug.Log("Saved Point A & Create Joystick using " + t.fingerId);

				StartJoystick();

				touchID = t.fingerId;
				IsUsingJoystick = true;
				break;
			}
		}
	}

	private bool IsTouchEnd()
	{
		if (Input.GetTouch(touchID).phase == TouchPhase.Ended)
			return true;

		return false;
	}


	private void EndJoystick()
	{
		Debug.Log("Touch End");
		IsUsingJoystick = false;
		circle.GetComponent<Image>().enabled = false;
		outerCircle.GetComponent<Image>().enabled = false;
	}

	void StartJoystick()
	{
		pointA = new Vector2(
			Input.GetTouch(touchID).position.x,
			Input.GetTouch(touchID).position.y
		);

		circle.transform.position = pointA;
		outerCircle.transform.position = pointA;
		circle.GetComponent<Image>().enabled = true;
		outerCircle.GetComponent<Image>().enabled = true;
	}

	private void MovingJoystick()
	{
		pointB = new Vector2(
			Input.GetTouch(touchID).position.x,
			Input.GetTouch(touchID).position.y
		);

		Vector2 offset = pointB - pointA;
		direction = Vector2.ClampMagnitude(offset, 1.0f);

		delta = Mathf.Clamp(offset.magnitude, 0, maxRange);
		circle.transform.position = new Vector2(
			pointA.x + delta * direction.x,
			pointA.y + delta * direction.y
		);
	}

}
