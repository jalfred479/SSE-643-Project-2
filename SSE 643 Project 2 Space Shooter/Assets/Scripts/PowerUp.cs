using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public int scoreValue;
	public bool scoreUp;
	public bool lifeUp;

	private GameController gameController;
	private PlayerController player;

	void Start () 
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		GameObject playerController = GameObject.FindWithTag ("Player");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) 
		{
			Debug.Log("Cannot find 'GameController' script");
		}
		if (playerController != null) 
		{
			player = playerController.GetComponent<PlayerController>();
		}
		if (playerController == null) 
		{
			Debug.Log("Cannot find 'PlayerController' script");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			gameController.AddScore(scoreValue);
			if(!scoreUp && !lifeUp)
			{
				modWeapon();
			}
			if(lifeUp)
			{
				gameController.addLife();
			}
			Destroy (gameObject);
		}
	}

	void modWeapon()
	{
		if (!player.bonusShot) {
			player.bonusShot = true;
		} else if (!player.bonusShot2) {
			player.bonusShot2 = true;
		} else {

		}
	}


}
