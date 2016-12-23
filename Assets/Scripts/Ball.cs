using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float ballInitialVelocity = 800f;


	private Rigidbody rb;
	private bool ballInPlay;
	private Vector3 worldPos;
	private Vector3 localPos;
	private float rotOffset;
	private Vector3 paddleRot;
	private Vector3 rotationVector;
	public float currentSpeed;
	private GameObject emptyObject;
//	private bool fireBall = false;
	private GameObject[] bricks;
	private GameObject[] balls;
	public bool fireBall;
	private Ball cloneBall;
	private GameObject ballToSpawn;
	public bool brickOnBottom;
	public GameObject ballPrefab;
	public float rotationOffset = 45f;


	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody> ();
		currentSpeed = ballInitialVelocity + 200f;

	}
	
	// Update is called once per frame
	void Update () {
		GameObject paddle = GameObject.FindGameObjectWithTag ("Paddle");

		if (rb.isKinematic == true) {
			ballInPlay = false;
		}

		if (Input.GetButtonUp ("Fire1") && ballInPlay == false && paddle.GetComponent<Paddle>().grabPaddle == false) {
			transform.parent = null;
			ballInPlay = true;
			rb.isKinematic = false;
			transform.rotation = Quaternion.Euler (new Vector3 (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 45f));
			//rb.AddForce(new Vector3(ballInitialVelocity, ballInitialVelocity, 0));
			GetComponent<Rigidbody> ().AddForce (transform.up * currentSpeed);
		} 
	//	print ("Vel = " + rb.velocity);

		/*
		if (GM.instance.moveToNextLevel == true) {
			//Destroy (gameObject);
			GM.instance.moveToNextLevel = false;
		} 
		*/
		transform.up = rb.velocity;

		if (rb.velocity.y < 3 && rb.velocity.y >= 0) {
			rb.AddForce (0f, 5f, 0f);
		} else if (rb.velocity.y > -3 && rb.velocity.y <= 0) {
			rb.AddForce (0f, -5f, 0f);
		}



		if (paddle.GetComponent<Paddle> ().grabPaddle == true && rb.isKinematic == true) {
			transform.position = new Vector3 (paddle.transform.position.x + localPos.x * paddle.transform.localScale.x, paddle.transform.position.y + paddle.transform.localScale.y + (GetComponent<Collider>().bounds.size.y / 2) - 0.35f, transform.position.z);
		}

		if (Input.GetButtonUp ("Fire1") && ballInPlay == false && paddle.GetComponent<Paddle>().grabPaddle == true) {
			//transform.parent = null;
			ballInPlay = true;
			rb.isKinematic = false;
			transform.rotation = Quaternion.Euler (rotationVector);
			GetComponent<Rigidbody> ().AddForce (transform.up * currentSpeed);
			//Destroy (emptyObject);
		}

	/*	if (GM.instance.splitBall == true) {
			cloneBall = Instantiate (this, transform.position, Quaternion.Euler (new Vector3 (-transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z)));
			cloneBall.ballInPlay = true;
		//	GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
			cloneBall.GetComponent<Rigidbody> ().AddForce (-transform.up * currentSpeed);

			GM.instance.splitBall = false;
		} */


	}


	void OnCollisionEnter(Collision target) {
		GameObject paddle = GameObject.FindGameObjectWithTag ("Paddle");
		if (target.gameObject.tag.Equals ("Paddle") == true) {
			if (paddle.GetComponent<Paddle>().grabPaddle == true) {
				worldPos = target.contacts [0].point;
				localPos = paddle.transform.InverseTransformPoint (worldPos);
				rotOffset = localPos.x * -160;

				GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);

				paddleRot = paddle.transform.rotation.eulerAngles;

				rotationVector = transform.rotation.eulerAngles;

				rotationVector.z = paddleRot.z + rotOffset;
				rb.isKinematic = true;
				ballInPlay = false;
				//transform.rotation = Quaternion.Euler (rotationVector);



				//emptyObject.transform.rotation = transform.rotation;

				//emptyObject.transform.parent = paddle.transform;
				//emptyObject.transform.localScale = transform.localScale;
				//transform.parent = emptyObject.transform;
				//transform.localRotation = Quaternion.identity;
				//transform.rotation = new Vector3 (0f, 0f, 0f); //emptyObject.transform.rotation;
			} else {
				//print ("Points colliding: " + target.contacts.Length);
				//	print ("First point that collided: " + target.contacts [0].point);
				worldPos = target.contacts [0].point;
				localPos = paddle.transform.InverseTransformPoint (worldPos);
				//	print (localPos);

				//	print (paddle.GetComponent<Rigidbody> ().velocity);

				rotOffset = localPos.x * -160;

				paddleRot = paddle.transform.rotation.eulerAngles;

				rotationVector = transform.rotation.eulerAngles;

				rotationVector.z = paddleRot.z + rotOffset;

				transform.rotation = Quaternion.Euler (rotationVector);

				GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);

				GetComponent<Rigidbody> ().AddForce (transform.up * currentSpeed);
				bricks = GameObject.FindGameObjectsWithTag ("Brick");
				foreach (GameObject currentBrick in bricks) {
					if (currentBrick.transform.position.y == SpawnBricks.instance.brickSpawnYDefault - SpawnBricks.instance.rows - 1) {
						brickOnBottom = true;						
					}

			}
				if (brickOnBottom == false && GM.instance.fallingBricks == true) {
					foreach (GameObject currentBrick in bricks) {
						currentBrick.transform.position = new Vector3 (currentBrick.transform.position.x, currentBrick.transform.position.y - SpawnBricks.instance.brickYSpawnGap, currentBrick.transform.position.z);
					}
				} else {
					brickOnBottom = false;
				}
		} /* else {
			rotationVector = transform.rotation.eulerAngles;

			//rotationVector.z += -1f;

			transform.rotation = Quaternion.Euler (rotationVector);

		//	GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);

			GetComponent<Rigidbody> ().AddForce (0, 1, 0);
		} */
		}
	}

	public void SetBallSpeed(float ballSpeed) {
		currentSpeed += ballSpeed;
		rb.AddForce (transform.up * ballSpeed);
	}

	public void SplitBall() {
		cloneBall = Instantiate (this, transform.position, Quaternion.Euler (new Vector3 (transform.eulerAngles.x, -transform.eulerAngles.y, transform.eulerAngles.z)));
		cloneBall.ballInPlay = true;
		//	GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
		cloneBall.GetComponent<Rigidbody> ().AddForce (-transform.up * currentSpeed);

	//	GM.instance.splitBall = false;
	}

	public void EightBall() {
		

	//	rb.isKinematic = true;
		for (int i = 0; i < 8; i++) {
		//	rotationVector = transform.rotation.eulerAngles;

		//	rotationVector.z = rotationOffset;
			ballToSpawn = Instantiate (ballPrefab, transform.position, Quaternion.identity) as GameObject;
			balls = GameObject.FindGameObjectsWithTag ("Ball");
			foreach (GameObject eightBall in balls) {
				eightBall.transform.parent = null;
				eightBall.GetComponent<Ball> ().LaunchBall ();
			}

			//Destroy (ballToSpawn);
			//ballToSpawn.transform.localScale = transform.localScale;
	//		cloneBall.GetComponent<Rigidbody> ().isKinematic = true;

		//	GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
	//	cloneBall.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
			//Quaternion rot = transform.rotation;
			//rot.z = transform.rotation.z + rotationOffset;
	//		cloneBall.transform.Rotate (0f, 0f, rotationOffset);
		//	cloneBall.GetComponent<Rigidbody> ().isKinematic = false;

			//rotationOffset += 45f;
		//	GM.instance.splitBall = false;
			//print (rotationOffset);
		}
	//	rb.isKinematic = false;
	}

	public void LaunchBall() {
		//ballToSpawn.transform.Rotate (0f, 0f, rotationOffset);
		if (currentSpeed < 1) {
			rb.isKinematic = false;
			ballInPlay = true;
			GetComponent<Rigidbody> ().AddForce (transform.up * (ballInitialVelocity + 700f));
		}
	}

}
