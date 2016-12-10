using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour {
	public GameObject brickParticle;
	public int brickTypeNum;
	public int easyBrickBreaks;
	public int mediumBrickBreaks;
	public int hardBrickBreaks;

	private int numOfHits;

	void OnCollisionEnter() {
		numOfHits++;

		if (brickTypeNum == 1) {
			Instantiate (brickParticle, transform.position, Quaternion.identity);
			GM.instance.DestroyBrick ();
			Destroy (gameObject);
		}

		if (brickTypeNum == 2) {
			if (numOfHits == mediumBrickBreaks) {
				Instantiate (brickParticle, transform.position, Quaternion.identity);
				GM.instance.DestroyBrick ();
				Destroy (gameObject);
			}
		}

		if (brickTypeNum == 3) {
			if (numOfHits == hardBrickBreaks) {
				Instantiate (brickParticle, transform.position, Quaternion.identity);
				GM.instance.DestroyBrick ();
				Destroy (gameObject);
			}
		}
	}
}
