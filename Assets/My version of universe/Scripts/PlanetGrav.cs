using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGrav : MonoBehaviour
{
	public Vector3 GetGravityUp(Rigidbody body)
	{
		Vector3 gravityUp = (body.position - transform.position).normalized;
		
		return gravityUp;
	}
}
