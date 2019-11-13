using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Joystick : MonoBehaviour
{
	public Transform player;
	private Rigidbody2D rb;
	public float speed = 5.0f;
	private bool touchStart = false;
	private Vector2 pointA;
	private Vector2 pointB;

	public Transform circle;
	public Transform outerCircle;

	private bool validInput = true;
	private int fingerID;

	private void Start()
	{
		rb = player.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if (player != null) {
			//validateInput();

			#if UNITY_STANDALONE || UNITY_EDITOR
			//DESKTOP COMPUTERS
			if (Input.GetMouseButtonDown(0) && validInput) {
				StartJoystick(0);
			}

			if (Input.GetMouseButton(0)) {
				OnMovingJoystick(0);
			} else {
				touchStart = false;
			}
			#endif

			#if UNITY_IOS || UNITY_ANDROID
			//MOBILE DEVICES

			if (Input.touchCount > 0) {
				Touch touch = Input.GetTouch(0);

				if (touch.phase == TouchPhase.Began && validInput) {
					StartJoystick(1);
					fingerID = touch.fingerId;
				}

				if (touch.phase == TouchPhase.Moved && touch.fingerId == fingerID) {
					OnMovingJoystick(1);
				} else {
					touchStart = false;
				}
			}

			#endif
		}
	}


	void StartJoystick(int i)
	{
		if (i == 0) {
			pointA = Camera.main.ScreenToWorldPoint(new Vector3(
				Input.mousePosition.x,
				Input.mousePosition.y,
				Camera.main.transform.position.z)
			);
		}

		if (i == 1) {
			pointA = Camera.main.ScreenToWorldPoint(new Vector3(
				Input.GetTouch(0).position.x,
				Input.GetTouch(0).position.y,
				Camera.main.transform.position.z)
			);
		}

		circle.transform.position = pointA;
		outerCircle.transform.position = pointA;
		circle.GetComponent<SpriteRenderer>().enabled = true;
		outerCircle.GetComponent<SpriteRenderer>().enabled = true;
	}

	void OnMovingJoystick(int i)
	{
		touchStart = true;
		if (i == 0) {
			pointB = Camera.main.ScreenToWorldPoint(new Vector3(
				Input.mousePosition.x,
				Input.mousePosition.y,
				Camera.main.transform.position.z)
			);
		}

		if (i == 1) {
			pointB = Camera.main.ScreenToWorldPoint(new Vector3(
				Input.GetTouch(0).position.x,
				Input.GetTouch(0).position.y,
				Camera.main.transform.position.z)
			);
		}
	}



	private void FixedUpdate()
	{
		if (touchStart) {
			Vector2 offset = pointB - pointA;
			Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
			moveCharacter(direction);

			circle.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);
		} else {
			circle.GetComponent<SpriteRenderer>().enabled = false;
			outerCircle.GetComponent<SpriteRenderer>().enabled = false;
		}

	}
	void moveCharacter(Vector2 direction)
	{
		rb.position += direction * speed * Time.deltaTime;

	}

	/// <summary>
	/// Check if pointer over UI element
	/// </summary>
	void validateInput()
	{
		#if UNITY_STANDALONE || UNITY_EDITOR
		//DESKTOP COMPUTERS

		if (Input.GetMouseButtonDown(0))
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
