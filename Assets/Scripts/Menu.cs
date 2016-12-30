using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	bool shouldQuit = false;


	// Use this for initialization
	void Start () {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;



	}


	
	// Update is called once per frame
	void Update () {
		if (shouldQuit == true) {
			if (!Application.isEditor) {
				System.Diagnostics.Process.GetCurrentProcess ().Kill ();
			}
			//Application.Quit ();
		}
			
	}


	public void LoadGame() {
		LevelManager.instance.levelEditor = false;
		SceneManager.LoadScene("Game");
	}

	public void LoadLevelEditor() {
		if (LevelManager.instance.levelEditor == true) {
			LevelManager.instance.levelNum = -99;
		} else {
			LevelManager.instance.levelNum = -1;
		}
		LevelManager.instance.levelEditor = true;
		SceneManager.LoadScene("LevelEditor");
	}

	public void LoadMenu() {
		LevelManager.instance.levelEditor = false;
		SceneManager.LoadScene("Menu");
	}

	public void Quit() {
		shouldQuit = true;
	}

	public void TestLevel() {
		GameObject[] bricks = GameObject.FindGameObjectsWithTag ("Brick");

		foreach (GameObject brick in bricks) {
			LevelManager.instance.GetComponent<LevelManager>().tempBricksArray [brick.GetComponent<Bricks> ().arrayY, brick.GetComponent<Bricks> ().arrayX] = brick.GetComponent<Bricks> ().brickTypeNum;
		}



		LevelManager.instance.levelNum = -99;
		SceneManager.LoadScene("Game");
	}
		

	public void SetLevel(int i) {
	//	if (i == 0) {
			LevelManager.instance.levelNum = i;
	//	} else {
			//int i = System.Convert.ToInt32(level);
	//		LevelManager.instance.levelNum = i + 1;
	//	}
	}
}
