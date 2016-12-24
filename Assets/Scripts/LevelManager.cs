using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public int levelNum;

	public static LevelManager instance = null;

	// Use this for initialization


	void Start () {
		DontDestroyOnLoad (transform.gameObject);

		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetLevel(string level) {
		int i = System.Convert.ToInt32(level);
		levelNum = i;
	}
}
