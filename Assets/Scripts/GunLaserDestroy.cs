using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLaserDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag.Equals ("Brick")) {
			other.GetComponent<Bricks> ().CheckShouldDestroy ();
			Destroy (gameObject);
		} else if (other.gameObject.tag.Equals ("Wall")) {
			Destroy (gameObject);
		}
	}
}
