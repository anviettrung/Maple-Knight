using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickTest : MonoBehaviour
{
	public Transform player;
	public Image circle;
	public Image outerCircle;

	public float speed;
	public float maxRange;
	private Rigidbody2D rb;

	private Vector2 pointA;
	private Vector2 pointB;

	private bool IsUsingJoystick = false;
	private int touchID;


	private void Start()
	{
		rb = player.GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (IsUsingJoystick == false) {
			FindTouchBegin();
		} 
		else {
			if (IsTouchEnd()) {
				EndJoystick();
			}
			else {
				MovingJoystick();
			}
		}
	}

	private void FixedUpdate()
	{
		if (IsUsingJoystick) {
			Vector2 offset = pointB - pointA;
			float delta = Mathf.Clamp(offset.magnitude, 0, maxRange);
			Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);

			moveCharacter(direction * delta / maxRange);

			circle.transform.position = new Vector2(pointA.x + delta * direction.x, pointA.y + delta * direction.y);
		} else {
			IsUsingJoystick = false;
		}
	}
	void moveCharacter(Vector2 direction)
	{
		rb.position += direction * speed * Time.deltaTime;

	}

	private void FindTouchBegin()
	{
		for (int i = 0; i < Input.touchCount; i++) 
		{
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
		pointA = new Vector3(
			Input.GetTouch(touchID).position.x,
			Input.GetTouch(touchID).position.y,
			Camera.main.transform.position.z
		);

		circle.transform.position = pointA;
		outerCircle.transform.position = pointA;
		circle.GetComponent<Image>().enabled = true;
		outerCircle.GetComponent<Image>().enabled = true;
	}

	private void MovingJoystick()
	{
		pointB = new Vector3(
			Input.GetTouch(touchID).position.x,
			Input.GetTouch(touchID).position.y,
			Camera.main.transform.position.z
		);

	}

}
