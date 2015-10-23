using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

	public float tumble;
	Rigidbody r;

	void Start()
	{
		r = GetComponent<Rigidbody> ();
		r.angularVelocity = Random.insideUnitCircle * tumble;
	}
}
