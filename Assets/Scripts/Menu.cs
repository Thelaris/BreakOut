using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadGame() {
		SceneManager.LoadScene("Game");
	}

	public void LoadLevelEditor() {
		SceneManager.LoadScene("LevelEditor");
	}

	public void LoadMenu() {
		SceneManager.LoadScene("Menu");
	}

	public void Quit() {

	}
}
