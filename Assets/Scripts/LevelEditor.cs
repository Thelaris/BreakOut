using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour {
	public object[][] bricksList;
	public Texture[] bricksTex;
	public GameObject brick;
	public float spawnX = -9.5f;
	public float spawnY = -16f;
	public float paddingX = 2f;
	public float paddingY = 1.5f;
	public int brickType = 99;
	public Text bricksText;
	public GameObject selectorFrame;

	private GameObject cloneBrick;

	public static LevelEditor instance = null;

	// Use this for initialization
	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);



		GameObject scene = GameObject.FindGameObjectWithTag ("Scene");
		scene.SetActive (true);

	//	gameObject.AddComponent<SpawnBricks> ();

		SpawnBricks.instance.levelNum = -1;
		SpawnBricks.instance.bricksPrefab = brick;

		SpawnBricks.instance.InstantiateBricks ();

		//int i = 1;
		for (int i = 1; i < 8; i++) {
			Vector3 position = new Vector3 (spawnX, spawnY, 0f);
			cloneBrick = Instantiate (brick, position, Quaternion.identity);
			cloneBrick.gameObject.tag = "LEBrick";
			cloneBrick.GetComponent<Bricks> ().brickTypeNum = i;
			cloneBrick.GetComponent<Bricks> ().SetColour ();
			cloneBrick.GetComponent<Bricks> ().levelEditor = true;
			cloneBrick.AddComponent<LevelEditorBrickClickEvent> ();
			cloneBrick.GetComponent<LevelEditorBrickClickEvent> ().selectorFrame = selectorFrame;
			spawnX += paddingX;
		}
		spawnY -= paddingY;
		spawnX = -9.5f;
			for (int i = 1; i < 8; i++) {
			Vector3 position = new Vector3 (spawnX, spawnY, 0f);
			cloneBrick = Instantiate (brick, position, Quaternion.identity);
			cloneBrick.gameObject.tag = "LEBrick";
			cloneBrick.GetComponent<Bricks> ().brickTypeNum = i + 7;
			if (cloneBrick.GetComponent<Bricks> ().brickTypeNum == 13) {
				cloneBrick.GetComponent<Bricks> ().brickTypeNum = 98;
			}
			if (cloneBrick.GetComponent<Bricks> ().brickTypeNum == 14) {
				cloneBrick.GetComponent<Bricks> ().brickTypeNum = 99;
			}
			cloneBrick.GetComponent<Bricks> ().SetColour ();
			cloneBrick.GetComponent<Bricks> ().levelEditor = true;
			cloneBrick.AddComponent<LevelEditorBrickClickEvent> ();
			cloneBrick.GetComponent<LevelEditorBrickClickEvent> ().selectorFrame = selectorFrame;
			spawnX += paddingX;
			//i++;
			}
		
		spawnX = -9.5f;
		spawnY = -16f;
	}

	void OnDisable() {
		instance = null;
	}

	public void Reset() {
		SceneManager.LoadScene ("LevelEditor");
	}

	public void SetBricks() {
		SpawnBricks.instance.bricks = 0;
		GameObject[] bricks = GameObject.FindGameObjectsWithTag ("Brick");
		foreach (GameObject brick in bricks) {
			if (brick.GetComponent<Bricks> ().brickTypeNum > 0) {
				SpawnBricks.instance.bricks++;
			}
			if (brick.GetComponent<Bricks> ().brickTypeNum == 99) {
				SpawnBricks.instance.bricks--;
			}
		}
		bricksText.text = "Bricks: " + SpawnBricks.instance.bricks;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
