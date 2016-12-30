using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour {

	public int lives = 5;
	//public int bricks = 0;
	public float resetDelay = 1f;
	public Text livesText;
	public GameObject gameOver;
	public GameObject youWon;
	public GameObject bricksPrefab;
	public GameObject bricksPrefab2;
	public GameObject paddle;
	public GameObject deathParticles;
	public static GM instance = null;
	//public bool moveToNextLevel;
/*	public int cols;
	public int rows;
	public float brickSpawnXDefault = -10.125f;
	public float brickSpawnYDefault = 8f;
	public float brickSpawnX = -10.125f;
	public float brickSpawnY = 8f;
	public Vector3 brickSpawnLocation = new Vector3 (-10.125f, 8f, 0f);
	public float brickXSpawnGap = 2.25f;
	public float brickYSpawnGap = 1.25f; */
	static public int levelNum = 1;

	public GameObject ball;
	public GameObject cloneBall;
	public GameObject powerUpHolder;
	public bool thruBrick = false;
	public bool fireBall = false;
	public bool splitBall = false;
	public bool fallingBricks = false;
	public float ballRotationOffset;
	public GameObject LevelEditorButton;

	public bool gamePaused = false;
	float stopTimeScale = 1;

	private GameObject clonePaddle;
	private GameObject cloneBrick;
	private GameObject currentBrick;
	private GameObject clonePowerUp;
	private GameObject[] balls;
	private GameObject[] spawnedBricks;
	private GameObject[] spawnedPowerUps;
	private GameObject[] gunLasers;
	private GameObject[] paddleGuns;
	private GameObject[] bricksToChange;
	private int brickChangeCounter;
//	private Bricks myBrick;
//	private int brickType;


/*	public int[,] bricksArray = new int[,] {
		//0  1  2  3  4  5  6  7  8  9  10 11 12
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //0
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //1
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //2
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //3
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //4
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //5
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //6
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //7
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //8
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //9
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //10
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //11
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //12
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //13
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //14
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //15
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //16
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //17
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //18
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }  //19

	};
*/


	void Start () {
		// Check for only 1 instance of this script, otherwise destroy parent object
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		GameObject scene = GameObject.FindGameObjectWithTag ("Scene");
		scene.SetActive (true);

		if (LevelManager.instance.levelEditor == true) {
			LevelEditorButton.SetActive (true);
		} else {
			LevelEditorButton.SetActive (false);
		}

		Setup ();
	}

	void Update() {
		if (Input.GetButtonDown ("Cancel")) {
			gamePaused = !gamePaused;
			PauseGame ();
			if (Time.timeScale == 0) {
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
			} else {
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
			}
		}
	}

	void PauseGame() {
		if (gamePaused) {
			stopTimeScale = 0;
		} else if (!gamePaused) {
			stopTimeScale = 1;
		}
		Time.timeScale = stopTimeScale;
	}

	// Initial game setup - Paddle and Bricks
	public void Setup() {
		clonePaddle = Instantiate (paddle, transform.position, Quaternion.identity) as GameObject;

	//	gameObject.AddComponent<SpawnBricks> ();

		if(SpawnBricks.instance.bricks == 0) {
			SpawnBricks.instance.levelNum = LevelManager.instance.levelNum;

		SpawnBricks.instance.bricksPrefab = bricksPrefab;

		SpawnBricks.instance.InstantiateBricks ();
		//SpawnBricks();
		}
		GameObject[] allBricks = GameObject.FindGameObjectsWithTag ("Brick");
		foreach (GameObject brick in allBricks) {
			if (brick.GetComponent<Bricks> ().brickTypeNum == 99) {
				brick.GetComponent<Bricks> ().brickTypeNum = 0;
			}
		}
	}

/*	void SpawnBricks() {
		SetLevel ();

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				//bricksArray [i, j] = 1;
				if (bricksArray [i, j] > 0) {
					brickType = bricksArray [i, j];
					cloneBrick = Instantiate (bricksPrefab, brickSpawnLocation, Quaternion.identity);
					SetBrickType (); // Based on array number
					bricks++; //Add to bricks to ensure all bricks are counted for
					brickSpawnX += brickXSpawnGap; //Add to brick X location for next spawn
					brickSpawnLocation = new Vector3 (brickSpawnX, brickSpawnY, 0f);

					cloneBrick.GetComponent<Bricks> ().arrayY = i;
					cloneBrick.GetComponent<Bricks> ().arrayX = j;

				} else {
					brickSpawnX += brickXSpawnGap;

					brickSpawnLocation = new Vector3 (brickSpawnX, brickSpawnY, 0f);

				}
			}
			brickSpawnY -= brickYSpawnGap; // Drop a row
			brickSpawnX = brickSpawnXDefault; // Reset X location to first postion LHS
			brickSpawnLocation = new Vector3 (brickSpawnX, brickSpawnY, 0f);
		}
	}


	void SetLevel() {
		//TESTING LEVEL
		if (levelNum == 0) {

			bricksArray = new int[,] {
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //0
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //1
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //2
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, //3
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, //4
				{ 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1 }, //5
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, //6
				{ 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1 }, //7
				{ 1, 1, 1, 4, 1, 1, 1, 1, 1, 4, 1, 1, 1 }, //8
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, //9
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, //10
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //11
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //12
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //13
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //14
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //15
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //16
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //17
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //18
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }  //19

			};
		}

		// LEVEL 1
		if (levelNum == 1) {

			bricksArray = new int[,] {
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
				{ 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
				{ 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
				{ 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
				{ 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0 }

			};
		}


		// LEVEL 2
		if (levelNum == 2) {
			bricksArray = new int[,] {
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 },
				{ 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }

			};
		}

		// LEVEL 3
		if (levelNum == 3) {
			bricksArray = new int[,] {
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
				{ 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 },
				{ 0, 0, 1, 1, 1, 2, 2, 2, 1, 1, 1, 0, 0 },
				{ 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0 },
				{ 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2 },
				{ 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3 },
				{ 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 1, 2, 2, 2, 1, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 1, 2, 2, 2, 1, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }

			};
		}

			// LEVEL 
			if (levelNum == 4) {
				bricksArray = new int[,] {
					{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
					{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
					{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
					{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
					{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
					{ 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 0, 0, 0 }, 
					{ 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 }, 
					{ 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 }, 
					{ 2, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 2 }, 
					{ 2, 1, 1, 1, 2, 3, 3, 3, 2, 1, 1, 1, 2 }, 
					{ 2, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 2 }, 
					{ 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
					{ 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 },
					{ 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 0, 0, 0 },
					{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
					{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
					{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
					{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
					{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
					{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }

				};
		}

		if (levelNum == 5) {
			bricksArray = new int[,] {
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
				{ 0, 0, 0, 0, 0, 1, 1, 4, 0, 0, 0, 0, 0 }, 
				{ 0, 0, 0, 0, 1, 1, 4, 1, 1, 0, 0, 0, 0 }, 
				{ 0, 0, 0, 1, 1, 4, 1, 1, 1, 4, 0, 0, 0 }, 
				{ 0, 0, 1, 1, 4, 1, 1, 1, 4, 1, 1, 0, 0 }, 
				{ 0, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 0 }, 
				{ 0, 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 0 }, 
				{ 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1 }, 
				{ 4, 1, 1, 1, 4, 2, 2, 2, 4, 1, 1, 1, 4 }, 
				{ 1, 1, 1, 4, 3, 3, 3, 4, 3, 1, 1, 4, 1 }, 
				{ 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1 }, 
				{ 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 4 },
				{ 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 4, 1 },
				{ 1, 1, 1, 4, 2, 3, 3, 4, 2, 2, 4, 1, 1 },
				{ 1, 1, 4, 1, 0, 0, 0, 0, 0, 4, 1, 1, 1 },
				{ 0, 4, 2, 2, 0, 0, 0, 0, 0, 2, 2, 2, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }

			};
		}
	}

	// Send brick type to brick script
	void SetBrickType() {
		myBrick = cloneBrick.GetComponent<Bricks> ();
		myBrick.brickTypeNum = brickType;
		myBrick.SetColour ();
	} */


	void CheckGameOver() {

		// Level Complete
		if (SpawnBricks.instance.bricks < 1) {
			if (LevelManager.instance.levelNum == -99 && LevelManager.instance.levelEditor == true) {
				LevelEditorButton.GetComponent<Menu> ().LoadLevelEditor ();
			} else {
			youWon.SetActive (true);
			Time.timeScale = .25f;
			balls = GameObject.FindGameObjectsWithTag ("Ball");
			spawnedPowerUps = GameObject.FindGameObjectsWithTag ("PowerUp");
			//cloneBall =  GameObject.FindGameObjectWithTag ("Ball");

			foreach (Object cloneBall in balls) {
				Destroy (cloneBall);
			}

			paddleGuns = GameObject.FindGameObjectsWithTag ("PaddleGun");
			foreach (Object clonePaddleGuns in paddleGuns) {
				Destroy (clonePaddleGuns);
			}

			gunLasers = GameObject.FindGameObjectsWithTag ("GunLaserParent");
			foreach (Object cloneLasers in gunLasers) {
				Destroy (cloneLasers);
			}

			foreach (Object clonePowerUp in spawnedPowerUps) {
				Destroy (clonePowerUp);
			}

			paddleGuns = GameObject.FindGameObjectsWithTag ("PaddleGun");
			foreach (Object clonePaddleGuns in paddleGuns) {
				Destroy (clonePaddleGuns);
			}

			gunLasers = GameObject.FindGameObjectsWithTag ("GunLaser");
			foreach (Object cloneLasers in gunLasers) {
				Destroy (cloneLasers);
			}


				Invoke ("Won", resetDelay);
		//	Won();
			}

		}

		// PLayer Died
		if (lives < 1) {
			gameOver.SetActive (true);
			Time.timeScale = .25f;
			Invoke ("Reset", resetDelay);
		}
	}

	void Won () {
		Time.timeScale = 1f;
		SpawnBricks.instance.levelNum++;
		//Instantiate (bricksPrefab2, transform.position, Quaternion.identity);
		SpawnBricks.instance.bricks = 0; // Reset Bricks
		SpawnBricks.instance.brickSpawnY = SpawnBricks.instance.brickSpawnYDefault; // Reset brick spawning locations
		SpawnBricks.instance.brickSpawnX = SpawnBricks.instance.brickSpawnXDefault; // Reset brick spawning locations
		SpawnBricks.instance.brickSpawnLocation = new Vector3 (SpawnBricks.instance.brickSpawnX, SpawnBricks.instance.brickSpawnY, 0f);

		Destroy (clonePaddle);

		youWon.SetActive (false); // Reset winning text

		// Set up next level
		SetupPaddle ();

		if (SpawnBricks.instance.levelNum == LevelManager.instance.levelsArray.Length) {
			SceneManager.LoadScene ("Menu");	
		} else {
			SpawnBricks.instance.InstantiateBricks ();
		}
		//moveToNextLevel = true;
	}

	void Reset() {
		Time.timeScale = 1f;
		SceneManager.LoadScene("Game");
	}

	public void LoseLife() {
		lives--;
		livesText.text = "Lives: " + lives;
		Instantiate (deathParticles, clonePaddle.transform.position, Quaternion.identity);
		balls = GameObject.FindGameObjectsWithTag ("Ball");
		spawnedPowerUps = GameObject.FindGameObjectsWithTag ("PowerUp");
	//	cloneBall =  GameObject.FindGameObjectWithTag ("Ball");
		foreach (Object cloneBall in balls) {
			Destroy (cloneBall);
		}

		foreach (Object clonePowerUp in spawnedPowerUps) {
			Destroy (clonePowerUp);
		}

		Destroy (clonePaddle);
		Invoke ("SetupPaddle", resetDelay);

		CheckGameOver ();
	}

	public void AddLife() {
		lives++;
		livesText.text = "Lives: " + lives;
	}

	void SetupPaddle() {
		clonePaddle = Instantiate (paddle, transform.position, Quaternion.identity) as GameObject;
		thruBrick = false;
		fireBall = false;
		splitBall = false;
		fallingBricks = false;
	}

	public void DestroyBrick(Vector3 brickLocation, int brickType) {
		SpawnBricks.instance.bricks--;
		if (brickType != 12) {
			SpawnPowerUp (brickLocation);
		}
		CheckGameOver ();
	}

	public void SpawnPowerUp(Vector3 powerUpSpawnLocation) {
		int range = Random.Range (0, 1000);
		//print (range);
		//range = 2;
		if (range < 50) {
			clonePowerUp = Instantiate (powerUpHolder, powerUpSpawnLocation, Quaternion.Euler (new Vector3 (0f, 180f, 0f))) as GameObject;
		} else if (range >= 231 && range <= 232) {
			clonePowerUp = Instantiate (powerUpHolder, powerUpSpawnLocation, Quaternion.Euler (new Vector3 (0f, 180f, 0f))) as GameObject;
			clonePowerUp.GetComponent<PowerUp> ().powerUpType = 1;
			clonePowerUp.GetComponent<PowerUp> ().powerUpNum = 9;
			clonePowerUp.GetComponent<Renderer> ().material.SetTexture("_MainTex", clonePowerUp.GetComponent<PowerUp>().goodPowerUpTex[clonePowerUp.GetComponent<PowerUp>().powerUpNum - 1]);
		} else if (range == 622) {
			clonePowerUp = Instantiate (powerUpHolder, powerUpSpawnLocation, Quaternion.Euler (new Vector3 (0f, 180f, 0f))) as GameObject;
			clonePowerUp.GetComponent<PowerUp> ().powerUpType = 1;
			clonePowerUp.GetComponent<PowerUp> ().powerUpNum = 10;
			clonePowerUp.GetComponent<Renderer> ().material.SetTexture("_MainTex", clonePowerUp.GetComponent<PowerUp>().goodPowerUpTex[clonePowerUp.GetComponent<PowerUp>().powerUpNum - 1]);
		}
	}

	public void EightBall() {
		balls = GameObject.FindGameObjectsWithTag ("Ball");
		ballRotationOffset = -45f;
		for (int i = 0; i < 8; i++) {
			cloneBall = Instantiate (ball, balls[0].transform.position, Quaternion.Euler(0f, 0f, ballRotationOffset));
			cloneBall.GetComponent<Rigidbody> ().isKinematic = false;
			cloneBall.transform.localScale = balls [0].transform.localScale;
			cloneBall.GetComponent<Rigidbody> ().AddForce (cloneBall.transform.up * 1500f);
			cloneBall.GetComponent<Ball> ().currentSpeed = 1500f;

			ballRotationOffset += -45f;
		}
		if (balls [0].GetComponent<Ball> ().currentSpeed < 1500f) {
			
			float forceOffset;
			forceOffset = 1500f - balls [0].GetComponent<Ball> ().currentSpeed;

			balls [0].GetComponent<Ball> ().currentSpeed = 1500f;
			balls [0].GetComponent<Rigidbody> ().AddForce (balls [0].transform.up * forceOffset);
		}
	}

	public void AddBricksToChange(GameObject brickToAdd) {
		brickChangeCounter++;

		System.Array.Resize (ref bricksToChange, brickChangeCounter);

		bricksToChange [brickChangeCounter - 1] = brickToAdd;
	}

	public void SetExplodingBricks() {
		if (bricksToChange != null) {
			foreach (GameObject brick in bricksToChange) {
				brick.GetComponent<Bricks> ().brickTypeNum = 12;
				brick.GetComponent<Bricks> ().SetColour ();
				brick.GetComponent<Bricks> ().AttachSpriteSheet ();
			}
		}
		brickChangeCounter = 0;
		System.Array.Resize (ref bricksToChange, brickChangeCounter);
	}

/*	public void FireBall() {
		if (fireBall == true) {
			spawnedBricks = GameObject.FindGameObjectsWithTag ("Brick");
			foreach (GameObject cloneBrick in spawnedBricks) {
				cloneBrick.GetComponent<Collider> ().isTrigger = true;
			}
		}
	} */
}
