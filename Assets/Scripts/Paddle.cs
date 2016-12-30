using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

	public float paddleSpeed = 0f;
	public bool grabPaddle;
	public GameObject paddleGuns;
	public bool shootingPaddle = false;

	private Vector3 playerPos = new Vector3 (0, -13.75f, 0);
	private Vector3 pos = new Vector3 (0, -13.75f, 0);
	private Vector3 worldPos;
	private Vector3 localPos;
	private float rotOffset;
	private Vector3 paddleRot;
	private Vector3 rotationVector;
	private GameObject paddleGunsClone;

	void Start() {
		transform.position = playerPos;
	}

	// Update is called once per frame
	void Update () {
		if (!GM.instance.gamePaused) {
			if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
			
				float xPos = transform.position.x + (Input.GetAxis ("Mouse X"));
				playerPos = new Vector3 (Mathf.Clamp (xPos, -9.25f, 9.25f), -13.75f, 0f);
				transform.position = playerPos;
			}

			if (Application.isMobilePlatform) {
				if (Input.touchCount > 0) {
					pos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.GetTouch (0).position.x, -13.75f, 0f));
					playerPos = new Vector3 (pos.x, -13.75f, 0);
					transform.position = playerPos;
				}
			}
		}
	}

	public void SpawnShootingPaddle() {
		Vector3 gunSpawnLocation = new Vector3 (transform.position.x, transform.position.y + 0.7f, transform.position.z);
		paddleGunsClone = Instantiate (paddleGuns, gunSpawnLocation, Quaternion.identity);
		paddleGunsClone.transform.localScale = transform.localScale;
		paddleGunsClone.transform.parent = transform;
	}

	/*
	void OnCollisionEnter(Collision target) {
		GameObject ball = GameObject.FindGameObjectWithTag ("Ball");
		if (target.gameObject.tag.Equals ("Ball") == true) {
			
			print ("Points colliding: " + target.contacts.Length);
			print ("First point that collided: " + target.contacts [0].point);
			worldPos = target.contacts [0].point;
			localPos = gameObject.transform.InverseTransformPoint (worldPos);
			print (localPos);

			print (ball.GetComponent<Rigidbody> ().velocity);

			rotOffset = localPos.x * -160;

			paddleRot = gameObject.transform.rotation.eulerAngles;

			rotationVector = ball.transform.rotation.eulerAngles;

			rotationVector.z = paddleRot.z + rotOffset;

			ball.transform.rotation = Quaternion.Euler (rotationVector);

			ball.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);

			ball.GetComponent<Rigidbody> ().AddForce (ball.transform.up * 800.0f);
		} else {
			rotationVector = ball.transform.rotation.eulerAngles;

			rotationVector.z += 1;

			ball.transform.rotation = Quaternion.Euler (rotationVector);

			ball.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);

			ball.GetComponent<Rigidbody> ().AddForce (ball.transform.up * 800.0f);
		}
	}
	*/
}
