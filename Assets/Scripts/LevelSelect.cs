using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {

	public Dropdown levelSelectDropdown;

	// Use this for initialization
	void Start () {
		//StartCoroutine (LateStart (1f));
	}
	
	// Update is called once per frame
	void Update () {
		if (LevelManager.instance != null) {
			GetComponent<Dropdown> ().value = LevelManager.instance.levelNum;
			GetComponent<Dropdown> ().RefreshShownValue ();
		}
	}

	IEnumerator LateStart(float waitTime) {
		yield return new WaitForSeconds (waitTime);

	}
}
