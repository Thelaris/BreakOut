using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleGun : MonoBehaviour {
	public GameObject gunLasers;
	public float laserSpeed;
	private GameObject gunLasersClone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp ("Fire1")) {
			StartCoroutine(Shoot ());
		}
	}

	IEnumerator Shoot() {
		GameObject paddle = GameObject.FindWithTag ("Paddle");
		Vector3 spawnLocation = new Vector3 (paddle.transform.position.x, paddle.transform.position.y + 0.9f, paddle.transform.position.z);
		gunLasersClone = Instantiate (gunLasers, spawnLocation, Quaternion.identity);
		gunLasersClone.GetComponent<Rigidbody>().AddForce(transform.up * laserSpeed);
		yield return new WaitForSeconds (2f);
	}
}
