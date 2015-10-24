using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundary;
	private Rigidbody r;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	public bool bonusShot;
	public bool bonusShot2;
	public Transform bonusShotPort;
	public Transform bonusShotStarboard;
	public Transform bonusShotPortAngle;
	public Transform bonusShotStarboardAngle;

	void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play();
			if(bonusShot)
			{
				Instantiate(shot, bonusShotPort.position, bonusShotPort.rotation);
				GetComponent<AudioSource>().Play();
				Instantiate(shot, bonusShotStarboard.position, bonusShotStarboard.rotation);
				GetComponent<AudioSource>().Play();
			}
			if(bonusShot2)
			{
				Instantiate(shot, bonusShotPortAngle.position, bonusShotPortAngle.rotation);
				GetComponent<AudioSource>().Play();
				Instantiate(shot, bonusShotStarboardAngle.position, bonusShotStarboardAngle.rotation);
				GetComponent<AudioSource>().Play();
			}
		}
	}

	void FixedUpdate()
	{
		r = GetComponent<Rigidbody>();
		float moveHortizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3(moveHortizontal, 0.0f, moveVertical);
		r.velocity = movement * speed;

		r.position = new Vector3 
			(
			Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f,
			Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
			);

		r.rotation = Quaternion.Euler (0 , 0, r.velocity.x * -tilt);
	}
}
