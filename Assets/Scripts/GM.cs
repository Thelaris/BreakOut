using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour {

	public int lives = 5;
	public int bricks = 0;
	public float resetDelay = 1f;
	public Text livesText;
	public GameObject gameOver;
	public GameObject youWon;
	public GameObject bricksPrefab;
	public GameObject bricksPrefab2;
	public GameObject paddle;
	public GameObject deathParticles;
	public static GM instance = null;
	public bool moveToNextLevel;
	public int cols;
	public int rows;
	public float brickSpawnX = -10.125f;
	public float brickSpawnY = 8f;
	public Vector3 brickSpawnLocation = new Vector3 (-10.125f, 8f, 0f);
	public int levelNum = 1;
	public GameObject cloneBall;


	private GameObject clonePaddle;
	private GameObject cloneBrick;
	private GameObject currentBrick;
	private Bricks myBrick;
	private int brickType;

	public int[,] bricksArray = new int[,] {
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, }, 
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },

	};

	// Use this for initialization
	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);


		Setup ();

		print (bricksArray [0,0]);
		print (bricksArray.Length);

		
	}


	public void Setup() {
		clonePaddle = Instantiate (paddle, transform.position, Quaternion.identity) as GameObject;

		//Instantiate (bricksPrefab, transform.position, Quaternion.identity);
		SpawnBricks();


	}

	void SpawnBricks() {
		if (levelNum == 0) {

			bricksArray = new int[,] {
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, }, 
				{ 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, },
				{ 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },

			};
		}

		if (levelNum == 1) {

			bricksArray = new int[,] {
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, }, 
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },

			};
		}



		if (levelNum == 2) {
			bricksArray = new int[,] {
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, }, 
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
				{ 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, },
				{ 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, },
				{ 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },

			};
		}



		for (int i = 0; i < cols; i++)
		{
			for (int j = 0; j < rows; j++)
			{
				//bricksArray [i, j] = 1;
				if (bricksArray [i, j] == 1) {
					brickType = bricksArray [i, j];
					cloneBrick = Instantiate (bricksPrefab, brickSpawnLocation, Quaternion.identity);
					SetBrickType ();
					bricks++;
					brickSpawnX += 2.25f;

					brickSpawnLocation = new Vector3 (brickSpawnX, brickSpawnY, 0f);
				} else if (bricksArray [i, j] == 2) {
					brickType = bricksArray [i, j];
					cloneBrick = Instantiate (bricksPrefab, brickSpawnLocation, Quaternion.identity);
					SetBrickType ();
					bricks++;
					brickSpawnX += 2.25f;

					brickSpawnLocation = new Vector3 (brickSpawnX, brickSpawnY, 0f);
				} else if (bricksArray [i, j] == 3) {
					brickType = bricksArray [i, j];
					cloneBrick = Instantiate (bricksPrefab, brickSpawnLocation, Quaternion.identity);
					SetBrickType ();
					bricks++;
					brickSpawnX += 2.25f;

					brickSpawnLocation = new Vector3 (brickSpawnX, brickSpawnY, 0f);
				} else {
					brickSpawnX += 2.25f;

					brickSpawnLocation = new Vector3 (brickSpawnX, brickSpawnY, 0f);
				}
			}
			brickSpawnY -= 1.25f;
			brickSpawnX = -10.125f;
			brickSpawnLocation = new Vector3 (brickSpawnX, brickSpawnY, 0f);
		}
	}

	void SetBrickType() {
		myBrick = cloneBrick.GetComponent<Bricks> ();
		myBrick.brickTypeNum = brickType;
	}

	void CheckGameOver() {
		if (bricks < 1) {
			youWon.SetActive (true);
			Time.timeScale = .25f;
			cloneBall =  GameObject.FindGameObjectWithTag ("Ball");
			Invoke ("Won", resetDelay);

		}

		if (lives < 1) {
			gameOver.SetActive (true);
			Time.timeScale = .25f;
			Invoke ("Reset", resetDelay);
		}
	}

	void Won () {
		Time.timeScale = 1f;
		levelNum++;
		//Instantiate (bricksPrefab2, transform.position, Quaternion.identity);
		bricks = 0;
		brickSpawnY = 8f;
		brickSpawnX = -10.125f;
		brickSpawnLocation = new Vector3 (brickSpawnX, brickSpawnY, 0f);
		Destroy (clonePaddle);
		Destroy (cloneBall);
		youWon.SetActive (false);
		SetupPaddle ();
		SpawnBricks ();
		//moveToNextLevel = true;
	}

	void Reset() {
		Time.timeScale = 1f;
		SceneManager.LoadScene("Level01");
	}

	public void LoseLife() {
		lives--;
		livesText.text = "Lives: " + lives;
		Instantiate (deathParticles, clonePaddle.transform.position, Quaternion.identity);
		cloneBall =  GameObject.FindGameObjectWithTag ("Ball");
		Destroy (cloneBall);
		Destroy (clonePaddle);
		Invoke ("SetupPaddle", resetDelay);
		CheckGameOver ();
	}

	void SetupPaddle() {
		clonePaddle = Instantiate (paddle, transform.position, Quaternion.identity) as GameObject;
	}

	public void DestroyBrick() {
		bricks--;
		CheckGameOver ();
	}
}
