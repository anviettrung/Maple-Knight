using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
	public Transform target;
	public SpriteRenderer rink;

	private float leftLimit = 0;
	private float rightLimit = 0;
	private float topLimit = 0;
	private float botLimit = 0;
	private Camera cam;


	private void Start()
	{
		cam = Camera.main;
		float camHeight = cam.orthographicSize * 2;
		float camWidth = cam.aspect * camHeight;
		leftLimit  = rink.transform.position.x - 0.5f * (rink.bounds.size.x - camWidth);
		rightLimit = rink.transform.position.x + 0.5f * (rink.bounds.size.x - camWidth);
		topLimit = rink.transform.position.y + 0.5f * (rink.bounds.size.y - camHeight);
		botLimit = rink.transform.position.y - 0.5f * (rink.bounds.size.y - camHeight);

	}

	void Update()
    {
		float newCamPositionX;
		float newCamPositionY;

		if (target.position.x <= leftLimit)
			newCamPositionX = leftLimit;
		else if (target.position.x >= rightLimit)
			newCamPositionX = rightLimit;
		else
			newCamPositionX = target.position.x;

		if (target.position.y <= botLimit)
			newCamPositionY = botLimit;
		else if (target.position.y >= topLimit)
			newCamPositionY = topLimit;
		else
			newCamPositionY = target.position.y;

		Camera.main.transform.position = new Vector3(
			newCamPositionX,
			newCamPositionY,
			cam.transform.position.z);
			
    }
}
