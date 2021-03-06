﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBricks : MonoBehaviour {

	public int cols = 13;
	public int rows = 20;
	public float brickSpawnXDefault = -10.5f;
	public float brickSpawnYDefault = 12f;
	public float brickSpawnX = -10.5f;
	public float brickSpawnY = 12f;
	public Vector3 brickSpawnLocation = new Vector3 (-10.5f, 12f, 0f);
	public float brickXSpawnGap = 1.75f;
	public float brickYSpawnGap = 1f;
	public int levelNum = 1;
	//public GameObject cloneBall;
	public int bricks = 0;
	private int brickType;
	private GameObject cloneBrick;
	public GameObject bricksPrefab;
	private Bricks myBrick;

	public List<Level> levelList;

	public static SpawnBricks instance = null;

	public int[,] tempBricksArray; /* = new int[,] {
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

	}; */

	public int[] Levels;


	public int[,] bricksArray = new int[,] {
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



	// Use this for initialization
	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InstantiateBricks() {
		

		if (levelNum == -99) {
			bricksArray = LevelManager.instance.tempBricksArray;
		} else if (levelNum == -1) {
			bricksArray = LevelManager.instance.blankLevelEditor;
		} else {
			LevelManager.instance.LoadLevel (levelNum);
			bricksArray = LevelManager.instance.bricksArray;
		}
		//SetLevel ();

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				//bricksArray [i, j] = 1;
				if (bricksArray [i, j] > 0) {
					brickType = bricksArray [i, j];
					cloneBrick = Instantiate (bricksPrefab, brickSpawnLocation, Quaternion.identity);
					SetBrickType (); // Based on array number
					if (brickType != 99) {
					bricks++; //Add to bricks to ensure all bricks are counted for
					}
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

	// Send brick type to brick script
	void SetBrickType() {
		myBrick = cloneBrick.GetComponent<Bricks> ();
		myBrick.brickTypeNum = brickType;
		myBrick.SetColour ();
	}

	void SetLevel() {

		if (levelNum == -99) {

			bricksArray = LevelManager.instance.tempBricksArray;
		}

		if (levelNum == -1) {

			bricksArray = new int[,] {
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }, //99
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }, //1
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }, //2
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }, //3
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }, //4
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }, //5
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }, //6
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }, //7
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }, //8
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }, //9
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }, //199
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }, //11
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }, //12
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }, //13
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }, //14
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }, //15
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }, //16
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }, //17
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }, //18
				{ 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }  //19

			};
		}



		//TESTING LEVEL
		if (levelNum == 0) {

			bricksArray = new int[,] {
				
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //0
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //1
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //2
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, //3
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, //12
				{ 1, 1, 12, 1, 1, 1, 1, 1, 1, 1, 12, 1, 1 }, //5
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, //6
				{ 1, 1, 1, 1, 1, 1, 12, 1, 1, 1, 1, 1, 1 }, //7
				{ 1, 1, 1, 12, 1, 1, 1, 1, 1, 12, 1, 1, 1 }, //8
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, //9
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, //10
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //11
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //12
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //13
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //112
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //15
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //16
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //17
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //18
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }  //19
			};
		}

		// LEVEL 1
		if (levelNum == 1) {
			//print (levelList [0]);
			//bricksArray = levelList [7].bricksArray;

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
				{ 0, 0, 0, 0, 0, 1, 1, 12, 0, 0, 0, 0, 0 }, 
				{ 0, 0, 0, 0, 1, 1, 12, 1, 1, 0, 0, 0, 0 }, 
				{ 0, 0, 0, 1, 1, 12, 1, 1, 1, 12, 0, 0, 0 }, 
				{ 0, 0, 1, 1, 12, 1, 1, 1, 12, 1, 1, 0, 0 }, 
				{ 0, 1, 1, 12, 1, 1, 1, 12, 1, 1, 1, 12, 0 }, 
				{ 0, 1, 12, 1, 1, 1, 12, 1, 1, 1, 12, 1, 0 }, 
				{ 1, 12, 1, 1, 1, 12, 1, 1, 1, 12, 1, 1, 1 }, 
				{ 12, 1, 1, 1, 12, 2, 2, 2, 12, 1, 1, 1, 12 }, 
				{ 1, 1, 1, 12, 3, 3, 3, 12, 3, 1, 1, 12, 1 }, 
				{ 1, 1, 12, 1, 1, 1, 12, 1, 1, 1, 12, 1, 1 }, 
				{ 1, 12, 1, 1, 1, 12, 1, 1, 1, 12, 1, 1, 12 },
				{ 12, 1, 1, 1, 12, 1, 1, 1, 12, 1, 1, 12, 1 },
				{ 1, 1, 1, 12, 2, 3, 3, 12, 2, 2, 12, 1, 1 },
				{ 1, 1, 12, 1, 0, 0, 0, 0, 0, 12, 1, 1, 1 },
				{ 0, 12, 2, 2, 0, 0, 0, 0, 0, 2, 2, 2, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }

			};
		}
			if (levelNum == 6) {
			bricksArray = new int[,] {
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
				{ 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, },
				{ 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, },
				{ 1, 2, 2, 13, 13, 7, 7, 7, 13, 13, 2, 2, 1, },
				{ 1, 2, 2, 13, 12, 4, 4, 4, 12, 13, 2, 2, 1, },
				{ 1, 2, 2, 7, 4, 3, 10, 3, 4, 7, 2, 2, 1, },
				{ 1, 2, 2, 7, 4, 10, 8, 10, 4, 7, 2, 2, 1, },
				{ 1, 2, 2, 7, 4, 10, 8, 10, 4, 7, 2, 2, 1, },
				{ 1, 2, 2, 7, 4, 3, 10, 3, 4, 7, 2, 2, 1, },
				{ 1, 2, 2, 13, 12, 4, 4, 4, 12, 13, 2, 2, 1, },
				{ 1, 2, 2, 13, 13, 7, 7, 7, 13, 13, 2, 2, 1, },
				{ 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, },
				{ 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, },
				{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, }
			};
		}

		if (levelNum == 7) {
			bricksArray = new int[,] {
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  },
				{ 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13,  },
				{ 0, 13, 0, 13, 0, 13, 0, 13, 0, 13, 0, 13, 0,  },
				{ 3, 0, 3, 0, 3, 0, 3, 0, 3, 0, 3, 0, 3,  },
				{ 10, 10, 10, 10, 10, 10, 12, 4, 4, 4, 4, 4, 4,  },
				{ 10, 10, 10, 10, 10, 10, 12, 4, 4, 4, 4, 4, 4,  },
				{ 10, 10, 10, 10, 10, 10, 12, 4, 4, 4, 4, 4, 4,  },
				{ 10, 10, 10, 10, 10, 10, 12, 4, 4, 4, 4, 4, 4,  },
				{ 10, 10, 10, 10, 10, 10, 3, 4, 4, 4, 4, 4, 4,  },
				{ 3, 12, 12, 12, 12, 3, 12, 3, 12, 12, 12, 12, 3,  },
				{ 6, 6, 6, 6, 6, 6, 3, 2, 2, 2, 2, 2, 2,  },
				{ 6, 6, 6, 6, 6, 6, 12, 2, 2, 2, 2, 2, 2,  },
				{ 6, 6, 6, 6, 6, 6, 12, 2, 2, 2, 2, 2, 2,  },
				{ 6, 6, 6, 6, 6, 6, 12, 2, 2, 2, 2, 2, 2,  },
				{ 6, 6, 6, 6, 6, 6, 12, 2, 2, 2, 2, 2, 2,  },
				{ 3, 0, 3, 0, 3, 0, 3, 0, 3, 0, 3, 0, 3,  },
				{ 0, 13, 0, 13, 0, 13, 0, 13, 0, 13, 0, 13, 0,  },
				{ 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13,  },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  },
			};
		}
		if (levelNum == 8) {
			bricksArray = new int[,] {
				{ 2, 2, 0, 0, 13, 13, 0, 13, 13, 0, 0, 2, 2, },
				{ 2, 2, 0, 0, 13, 13, 0, 13, 13, 0, 0, 2, 2, },
				{ 2, 2, 0, 0, 13, 13, 0, 13, 13, 0, 0, 2, 2, },
				{ 2, 2, 0, 0, 13, 13, 0, 13, 13, 0, 0, 2, 2, },
				{ 2, 2, 0, 0, 13, 13, 0, 13, 13, 0, 0, 2, 2, },
				{ 2, 2, 0, 0, 13, 13, 0, 13, 13, 0, 0, 2, 2, },
				{ 2, 2, 0, 0, 13, 13, 0, 13, 13, 0, 0, 2, 2, },
				{ 2, 2, 0, 0, 13, 13, 0, 13, 13, 0, 0, 2, 2, },
				{ 2, 2, 0, 0, 13, 13, 0, 13, 13, 0, 0, 2, 2, },
				{ 2, 2, 0, 0, 13, 13, 12, 13, 13, 0, 0, 2, 2, },
				{ 2, 2, 0, 0, 13, 13, 0, 13, 13, 0, 0, 2, 2, },
				{ 2, 2, 0, 0, 13, 13, 0, 13, 13, 0, 0, 2, 2, },
				{ 2, 2, 0, 0, 13, 13, 0, 13, 13, 0, 0, 2, 2, },
				{ 2, 2, 0, 0, 13, 13, 0, 13, 13, 0, 0, 2, 2, },
				{ 2, 2, 0, 0, 13, 13, 0, 13, 13, 0, 0, 2, 2, },
				{ 2, 2, 0, 0, 13, 13, 0, 13, 13, 0, 0, 2, 2, },
				{ 2, 2, 0, 0, 13, 13, 0, 13, 13, 0, 0, 2, 2, },
				{ 2, 2, 0, 0, 13, 13, 0, 13, 13, 0, 0, 2, 2, },
				{ 2, 2, 0, 0, 13, 13, 0, 13, 13, 0, 0, 2, 2, },
				{ 2, 2, 13, 13, 13, 13, 0, 13, 13, 13, 13, 2, 2, }
			};

		}
	}
}
