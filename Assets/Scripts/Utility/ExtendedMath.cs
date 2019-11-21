using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Extended Math Functions
public static class ExdMath {

	public static float SignedAngle(Vector2 vec1, Vector2 vec2) {
		int signed = 1;
		if (Vector3.Cross(vec1, vec2).z < 0) signed = -1;
		return signed * Vector3.Angle(vec1, vec2);
	}

	public static Vector2 Rotate(this Vector2 v, float degrees)
	{
		float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
		float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

		float tx = v.x;
		float ty = v.y;
		v.x = (cos * tx) - (sin * ty);
		v.y = (sin * tx) + (cos * ty);
		return v;
	}
}
