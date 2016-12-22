using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

	private GameObject[] balls;

	void OnTriggerEnter(Collider other) {
		Destroy (other.gameObject, 0.2f);
		if (other.gameObject.tag.Equals ("Ball")) {
			balls = GameObject.FindGameObjectsWithTag ("Ball");
			if (balls.Length == 1) {
				GM.instance.LoseLife ();
			}
		}
	}
}
