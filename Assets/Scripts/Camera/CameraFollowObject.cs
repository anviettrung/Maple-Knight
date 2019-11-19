using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
	public Transform target;
	public SpriteRenderer rink;

	private float leftLimit = 0;
	private float rightLimit = 0;
	private Camera cam;


	private void Start()
	{
		cam = Camera.main;
		float camWidth = rink.bounds.size.y * cam.aspect;
		leftLimit  = rink.transform.position.x - 0.5f * (rink.bounds.size.x - camWidth);
		rightLimit = rink.transform.position.x + 0.5f * (rink.bounds.size.x - camWidth);
	}

	void Update()
    {
		float newCamPositionX;

		if (target.position.x <= leftLimit)
			newCamPositionX = leftLimit;
		else if (target.position.x >= rightLimit)
			newCamPositionX = rightLimit;
		else
			newCamPositionX = target.position.x;

		Camera.main.transform.position = new Vector3(
			newCamPositionX,
			target.transform.position.y,
			cam.transform.position.z);
			
    }
}
