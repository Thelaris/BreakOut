using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorBrickClickEvent : MonoBehaviour {

	public int brickType;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		if (GetComponent<Bricks> ().levelEditor == true) {
			LevelEditor.instance.brickType = GetComponent<Bricks> ().brickTypeNum;
		}
	/*	GetComponent<Bricks> ().brickTypeNum = LevelEditor.instance.brickType;
		GetComponent<Bricks> ().SetColour ();
		GetComponent<Bricks> ().AttachSpriteSheet();
		*/
	}


}
