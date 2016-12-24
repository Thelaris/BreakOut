using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorBrickClickEvent : MonoBehaviour {

	public int brickType;
	public GameObject selectorFrame;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		if (GetComponent<Bricks> ().levelEditor == true) {
			LevelEditor.instance.brickType = GetComponent<Bricks> ().brickTypeNum;

			GameObject[] selectors = GameObject.FindGameObjectsWithTag ("Selector");
			foreach (GameObject selector in selectors) {
				Destroy (selector);
			}

			Vector3 pos = new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1);

			Instantiate (selectorFrame, pos, Quaternion.identity);
		}
	/*	GetComponent<Bricks> ().brickTypeNum = LevelEditor.instance.brickType;
		GetComponent<Bricks> ().SetColour ();
		GetComponent<Bricks> ().AttachSpriteSheet();
		*/
	}


}
