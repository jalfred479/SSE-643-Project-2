using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{
	public float speed;
	public bool looker;

	private Rigidbody rb;
	private GameObject player;

	void Start ()
	{
		player = GameObject.FindWithTag ("Player");
		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * speed;
	}

	void FixedUpdate()
	{
		if (looker && gameObject.transform.position.z < 12 && player.activeInHierarchy) 
		{
			rb.velocity = transform.forward * 0f;
			gameObject.transform.LookAt (player.transform);
			gameObject.transform.rotation *= Quaternion.Euler (0.0f, 180f, 0f);
		} 
		else if (looker && (player == null || !player.activeInHierarchy))
		{
			gameObject.transform.rotation = Quaternion.identity;
			rb.velocity = transform.forward * speed;
		}

	}
}
