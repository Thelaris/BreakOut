using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBricksCapsule : MonoBehaviour {
	private Collider brick;
	private bool isDone = false;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		if (isDone == true) {
			Destroy (gameObject);
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag.Equals ("Brick")) {
		//	print ("Is brick");
			brick = other;
			if (other.GetComponent<Bricks> ().brickTypeNum == 4) {
				other.GetComponent<Bricks> ().ExplodeBricks ();
				other.GetComponent<Bricks> ().DestroyBrick ();
			} else {
				other.GetComponent<Bricks> ().DestroyBrick ();
			}
		}
	}

	IEnumerator Explode () {
		

		yield return new WaitForSeconds (10f);
	}
}
