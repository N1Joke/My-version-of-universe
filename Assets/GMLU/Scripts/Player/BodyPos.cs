using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BodyPos : MonoBehaviour
{
    [SerializeField] private PlanetGrav _planet;
	private Rigidbody _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();

		_rigidbody.useGravity = false;
		//_rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
	}

	private void FixedUpdate()
	{
		_planet.GetGravityUp(_rigidbody);
	}
}
