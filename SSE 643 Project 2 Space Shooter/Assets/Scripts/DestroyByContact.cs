using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) 
		{
			Debug.Log("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "boundary" || other.tag == "Enemy" || other.tag == "Power Up")
			return;

		if (explosion != null) 
		{
			Instantiate (explosion, GetComponent<Rigidbody> ().transform.position, GetComponent<Rigidbody> ().transform.rotation);
		}
		if (other.tag == "Player" && gameObject.tag == "Enemy") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.LoseLife ();
		} 
		else
		{
			gameController.AddScore (scoreValue);
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}
