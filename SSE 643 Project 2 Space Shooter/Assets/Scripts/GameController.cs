using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public GameObject[] enemies;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public float respawnFlicker;
	public float lives;

	public float powerProb;
	public float weaponProb;
	public float lifeProb;

	public GameObject weaponUp;
	public GameObject scoreUp;
	public GameObject lifeUp;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText waveText;
	public GUIText lifeText;

	private bool gameOver;
	private bool restart;
	private int score;
	private bool respawn;
	private GameObject player;
	private PlayerController playerController;

	void Start()
	{
		gameOver = false;
		restart = false;
		respawn = false;
		gameOverText.text = "";
		restartText.text = "";
		waveText.text = "";
		UpdatesLives();
		player = GameObject.FindWithTag("Player");
		playerController = player.GetComponent<PlayerController> ();
		score = 0;
		UpdateScore ();
		StartCoroutine(SpawnWaves ());
	}

	void Update()
	{
		if (restart) 
		{
			if(Input.GetKeyDown(KeyCode.R))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves()
	{
		int waveNum = 1;
		float enemyProb = 1f;
		yield return new WaitForSeconds (startWait);
		while(true)
		{
			Vector3 spawnPosition;
			Quaternion spawnRotation;

			waveText.text = "Wave " + waveNum;
			for (int i = 0; i < hazardCount && !respawn; i++) 
			{
				GameObject hazard;
				if(Random.value > enemyProb)
					hazard = enemies[Random.Range(0, enemies.Length)];
				else
					hazard = hazards[Random.Range(0,hazards.Length)];
				if(Random.value > powerProb)
				{
					spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					spawnRotation = Quaternion.identity;
					float x = Random.value;
					if(x > weaponProb)
					{
						Instantiate (weaponUp, spawnPosition, spawnRotation);
					}
					else if(x > lifeProb && x <= weaponProb)
					{
						Instantiate (lifeUp, spawnPosition, spawnRotation);
					}
					else
					{
						Instantiate (scoreUp, spawnPosition, spawnRotation);
					}
				}
				spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
				Debug.Log ("Loop Run " + i + " finished.");
			}
			Debug.Log ("Exited Loop");
			while( GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
			{
				Debug.Log ("Remaining Objects: " + GameObject.FindGameObjectsWithTag("Enemy").Length );
				yield return new WaitForSeconds(2);
			}

			if(gameOver)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
			else if( respawn)
			{
				Debug.Log ("Got to respawn.");
				gameOverText.text = "Respawning...";
				player.transform.position =  new Vector3(0f, 0f, 0f);
				player.SetActive(true);
				float respawnFlickerVar = respawnFlicker;
				for(int i = 0; i < 10; i++)
				{
					yield return new WaitForSeconds(respawnFlickerVar);
					player.SetActive(false);
					yield return new WaitForSeconds(respawnFlickerVar);
					player.SetActive(true);
					respawnFlickerVar /= 2f;
				}
				respawn = false;
				gameOverText.text = "";
			}
			else
			{	
				hazardCount += waveNum*waveNum;
				spawnWait /= .95f;
				enemyProb -= .05f*waveNum;
				waveNum++;
			}
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void addLife()
	{
		lives += 1;
		UpdatesLives ();
	}

	void UpdatesLives()
	{
		lifeText.text = "Lives: " + lives;
	}

	public void LoseLife ()
	{
		lives--;
		if (lives < 1) 
		{
			lifeText.text = "Lives: dead";
			GameOver ();
		} 
		else 
		{
			UpdatesLives();
			player.SetActive(false);
			playerController.bonusShot = false;
			playerController.bonusShot2 = false;
			respawn = true;
		}
	}

	public void GameOver()
	{
		Destroy (player);
		gameOverText.text = "GAME OVER!";
		gameOver = true;
	}
}
