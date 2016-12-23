using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour {
	public object[][] bricksList;
	public Texture[] bricksTex;
	public GameObject brick;
	public float spawnX = -9.5f;
	public float spawnY = -16f;
	public float paddingX = 2f;
	public float paddingY = 1.5f;
	public int brickType = 99;

	private GameObject cloneBrick;

	public static LevelEditor instance = null;

	// Use this for initialization
	void OnEnable () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

	//	gameObject.AddComponent<SpawnBricks> ();

		SpawnBricks.instance.levelNum = -1;
		SpawnBricks.instance.bricksPrefab = brick;

		SpawnBricks.instance.InstantiateBricks ();

		//int i = 1;
		for (int i = 1; i < 8; i++) {
			Vector3 position = new Vector3 (spawnX, spawnY, 0f);
			cloneBrick = Instantiate (brick, position, Quaternion.identity);
			cloneBrick.gameObject.tag = "Untagged";
			cloneBrick.GetComponent<Bricks> ().brickTypeNum = i;
			cloneBrick.GetComponent<Bricks> ().SetColour ();
			cloneBrick.GetComponent<Bricks> ().levelEditor = true;
			cloneBrick.AddComponent<LevelEditorBrickClickEvent> ();
			spawnX += paddingX;
		}
		spawnY -= paddingY;
		spawnX = -9.5f;
			for (int i = 1; i < 7; i++) {
			Vector3 position = new Vector3 (spawnX, spawnY, 0f);
			cloneBrick = Instantiate (brick, position, Quaternion.identity);
			cloneBrick.gameObject.tag = "Untagged";
			cloneBrick.GetComponent<Bricks> ().brickTypeNum = i + 7;
			if (cloneBrick.GetComponent<Bricks> ().brickTypeNum == 13) {
				cloneBrick.GetComponent<Bricks> ().brickTypeNum = 98;
			}
			cloneBrick.GetComponent<Bricks> ().SetColour ();
			cloneBrick.GetComponent<Bricks> ().levelEditor = true;
			cloneBrick.AddComponent<LevelEditorBrickClickEvent> ();
			spawnX += paddingX;
			//i++;
			}
		
		spawnX = -9.5f;
		spawnY = -16f;
	}

	void OnDisable() {
		instance = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
}
