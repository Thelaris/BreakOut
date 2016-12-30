using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ResetPlayerPrefs : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[MenuItem("Edit/Reset Playerprefs")] 
	public static void DeletePlayerPrefs() { 
		PlayerPrefs.DeleteAll(); 
	}
}
