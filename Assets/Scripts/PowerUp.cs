using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	public float ballSpeedOffset;
	public float paddleScaleOffset;
	public Texture[] goodPowerUpTex;
	public Texture[] badPowerUpTex;
	public Texture[] neutralPowerUpTex;
	//public bool splitBall;

	private int randInt;
	public int powerUpType;
	public int powerUpNum;

	private GameObject[] balls;
	private GameObject[] bricks;
	private GameObject paddle;


	// Use this for initialization
	void Awake () {

		powerUpType = Random.Range (1, 4);
		SetPowerUp ();



		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetPowerUp() {
		// Good / 10
		if (powerUpType == 1) {
			powerUpNum = Random.Range (1, 9);
			print (powerUpType + " " + powerUpNum);

			GetComponent<Renderer> ().material.SetTexture("_MainTex", goodPowerUpTex[powerUpNum - 1]);

			//bad / 5
		} else if (powerUpType == 2) {
			powerUpNum = Random.Range (1, 6);
			print (powerUpType + " " + powerUpNum);

			GetComponent<Renderer> ().material.SetTexture("_MainTex", badPowerUpTex[powerUpNum - 1]);


			//neutral / 3
		} else if (powerUpType == 3) {
			powerUpNum = Random.Range (1, 6);
			print (powerUpType + " " + powerUpNum);

			GetComponent<Renderer> ().material.SetTexture("_MainTex", neutralPowerUpTex[powerUpNum - 1]);

		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Paddle")) {
			ApplyPowerUp (powerUpType, powerUpNum);
			Destroy (gameObject);
		}
	}

	void ApplyPowerUp (int type, int num) {
		balls = GameObject.FindGameObjectsWithTag ("Ball");
		paddle = GameObject.FindWithTag ("Paddle");

		if (type == 1) {
			if (num == 1) {
				SlowBall ();
			} else if (num == 2) {
				GrabPaddle ();
			} else if (num == 3) {
				ThruBrick ();
			} else if (num == 4) {
				FireBall ();
			} else if (num == 5) {
				ShootingPaddle ();
			} else if (num == 6) {
				SetOffExploding ();
			} else if (num == 7) {
				ExpandExploding();
			} else if (num == 8) {
				ZapBricks();
			} else if (num == 9) {
				ExtraLife ();
			} else if (num == 10) {
				LevelWarp ();
			}
		} else if (type == 2) {
			if (num == 1) {
				SpeedUpBall ();
			} else if (num == 2) {
				ShrinkBall ();
			} else if (num == 3) {
				KillPaddle ();
			} else if (num == 4) {
				FallingBricks ();
			} else if (num == 5) {
				SuperShrink ();
			}

			/*	foreach (GameObject cloneBall in balls) {
					Ball currentBall = cloneBall.GetComponent<Ball> ();

					currentBall.SetBallSpeed (200.0f);
					*/
		} else if (type == 3) {
			if (num == 1) {
				GrowPaddle ();
			} else if (num == 2) {
				ShrinkPaddle ();
			} else if (num == 3) {
				MegaBall();
			} else if (num == 4) {
				SplitBall();
			} else if (num == 5) {
				EightBall();
			}
			//paddle.transform.localScale += new Vector3 (1f, 0f, 0f);
		}
	}



	void SpeedUpBall() {
		foreach (GameObject cloneBall in balls) {
			Ball currentBall = cloneBall.GetComponent<Ball> ();

			currentBall.SetBallSpeed (ballSpeedOffset);
		}
	}
	

	void SlowDownBall() {
		foreach (GameObject cloneBall in balls) {
			Ball currentBall = cloneBall.GetComponent<Ball> ();

			currentBall.SetBallSpeed (-ballSpeedOffset);
		}
	}

	void ResetBallSpeed() {
		foreach (GameObject cloneBall in balls) {
			Ball currentBall = cloneBall.GetComponent<Ball> ();

			if (currentBall.currentSpeed < 1000f) {
				currentBall.SetBallSpeed (1000f - currentBall.currentSpeed);
			} else if (currentBall.currentSpeed > 1000f) {
				currentBall.SetBallSpeed ((-1000f + currentBall.currentSpeed) * -1f);
			}

		}
	}

	void SlowBall() {
		foreach (GameObject cloneBall in balls) {
			Ball currentBall = cloneBall.GetComponent<Ball> ();

			if (currentBall.currentSpeed < 1000f) {
				currentBall.SetBallSpeed (1000f - currentBall.currentSpeed - 200f);
			} else if (currentBall.currentSpeed > 1000f) {
				currentBall.SetBallSpeed ((-1000f + currentBall.currentSpeed - 200f) * -1f);
			}

		}
	}

	void GrowPaddle() {
		paddle.transform.localScale += new Vector3 (paddleScaleOffset, 0f, 0f);
	}

	void ShrinkPaddle() {
		if (paddle.transform.localScale.x > 1f) {
			paddle.transform.localScale += new Vector3 (-paddleScaleOffset, 0f, 0f);
		}
	}

	void ExtraLife() {
		GM.instance.AddLife();
	}

	void LevelWarp() {
		bricks = GameObject.FindGameObjectsWithTag ("Brick");
		foreach (GameObject cloneBrick in bricks) {
			cloneBrick.GetComponent<Bricks> ().DestroyBrick ();
		}
	}

	void GrabPaddle() {
		paddle.GetComponent<Paddle> ().grabPaddle = true;
	}

	void ThruBrick() {
		GM.instance.thruBrick = true;
	//	GM.instance.FireBall ();
	}

	void FireBall() {
		GM.instance.fireBall = true;
	}

	void SplitBall() {
		//GM.instance.splitBall = true; 
		foreach (GameObject cloneBall in balls) {
			Ball currentBall = cloneBall.GetComponent<Ball> ();
			currentBall.SplitBall ();
		}

	}

	void ShootingPaddle() {
		if (paddle.GetComponent<Paddle> ().shootingPaddle == false) {
			paddle.GetComponent<Paddle> ().shootingPaddle = true;
			paddle.GetComponent<Paddle> ().SpawnShootingPaddle ();
		}
	}

	void SetOffExploding() {
		bricks = GameObject.FindGameObjectsWithTag ("Brick");
		foreach (GameObject cloneBrick in bricks) {
			if (cloneBrick.GetComponent<Bricks> ().brickTypeNum == 12) {
				cloneBrick.GetComponent<Bricks> ().DestroyBrick ();
			}
		}
	}

	void ExpandExploding() {
		bricks = GameObject.FindGameObjectsWithTag ("Brick");
		foreach (GameObject originBrick in bricks) {
			if (originBrick.GetComponent<Bricks> ().brickTypeNum == 12) {
				foreach (GameObject cloneBrick in bricks) {
					if (cloneBrick.GetComponent<Bricks> ().arrayY == originBrick.GetComponent<Bricks> ().arrayY - 1 && cloneBrick.GetComponent<Bricks> ().arrayX == originBrick.GetComponent<Bricks> ().arrayX) {
						GM.instance.AddBricksToChange (cloneBrick);
					} else if (cloneBrick.GetComponent<Bricks> ().arrayY == originBrick.GetComponent<Bricks> ().arrayY + 1 && cloneBrick.GetComponent<Bricks> ().arrayX == originBrick.GetComponent<Bricks> ().arrayX) {
						GM.instance.AddBricksToChange (cloneBrick);
					} else if (cloneBrick.GetComponent<Bricks> ().arrayY == originBrick.GetComponent<Bricks> ().arrayY && cloneBrick.GetComponent<Bricks> ().arrayX == originBrick.GetComponent<Bricks> ().arrayX + 1) {
						GM.instance.AddBricksToChange (cloneBrick);
					} else if (cloneBrick.GetComponent<Bricks> ().arrayY == originBrick.GetComponent<Bricks> ().arrayY && cloneBrick.GetComponent<Bricks> ().arrayX == originBrick.GetComponent<Bricks> ().arrayX - 1) {
						GM.instance.AddBricksToChange (cloneBrick);
					}
				}
			}
		}

		GM.instance.SetExplodingBricks ();



		/* int i = 0;
		GameObject[] explodingBricks = new GameObject[0];
		bricks = GameObject.FindGameObjectsWithTag ("Brick");
		foreach (GameObject originBrick in bricks) {
			if (originBrick.GetComponent<Bricks> ().brickTypeNum == 4) {
				
				i++;
				System.Array.Resize (ref explodingBricks, i);
				explodingBricks [i - 1] = originBrick;

			}
		}



		foreach (GameObject originBrick in bricks) {
		foreach (GameObject cloneBrick in explodingBricks) {
			
				
				// Need array of current exploding bricks to apply to
					 
				/*	if (cloneBrick.GetComponent<Bricks> ().arrayY == originBrick.GetComponent<Bricks> ().arrayY - 1 && ameObject.GetComponent<Bricks> ().arrayX == originBrick.GetComponent<Bricks> ().arrayX - 1) {
						cloneBrick.GetComponent<Bricks> ().brickTypeNum = 4;
						cloneBrick.GetComponent<Bricks> ().SetColour ();
						cloneBrick.GetComponent<Bricks> ().AttachSpriteSheet();
					} 
				if (cloneBrick.GetComponent<Bricks> ().arrayY == originBrick.GetComponent<Bricks> ().arrayY - 1 && cloneBrick.GetComponent<Bricks> ().arrayX == originBrick.GetComponent<Bricks> ().arrayX) {
					cloneBrick.GetComponent<Bricks> ().brickTypeNum = 4;
					cloneBrick.GetComponent<Bricks> ().SetColour ();
					cloneBrick.GetComponent<Bricks> ().AttachSpriteSheet ();
				} else if (cloneBrick.GetComponent<Bricks> ().arrayY == originBrick.GetComponent<Bricks> ().arrayY - 1 && cloneBrick.GetComponent<Bricks> ().arrayX == originBrick.GetComponent<Bricks> ().arrayX + 1) {
						cloneBrick.GetComponent<Bricks> ().brickTypeNum = 4;
						cloneBrick.GetComponent<Bricks> ().SetColour ();
						cloneBrick.GetComponent<Bricks> ().AttachSpriteSheet();
					} else if (cloneBrick.GetComponent<Bricks> ().arrayY == originBrick.GetComponent<Bricks> ().arrayY + 1 && cloneBrick.GetComponent<Bricks> ().arrayX == originBrick.GetComponent<Bricks> ().arrayX + 1) {
						cloneBrick.GetComponent<Bricks> ().brickTypeNum = 4;
						cloneBrick.GetComponent<Bricks> ().SetColour ();
						cloneBrick.GetComponent<Bricks> ().AttachSpriteSheet();
					} else if (cloneBrick.GetComponent<Bricks> ().arrayY == originBrick.GetComponent<Bricks> ().arrayY + 1 && cloneBrick.GetComponent<Bricks> ().arrayX == originBrick.GetComponent<Bricks> ().arrayX - 1) {
						cloneBrick.GetComponent<Bricks> ().brickTypeNum = 4;
						cloneBrick.GetComponent<Bricks> ().SetColour ();
						cloneBrick.GetComponent<Bricks> ().AttachSpriteSheet();
					}  else if (cloneBrick.GetComponent<Bricks> ().arrayY == originBrick.GetComponent<Bricks> ().arrayY + 1 && cloneBrick.GetComponent<Bricks> ().arrayX == originBrick.GetComponent<Bricks> ().arrayX) {
					cloneBrick.GetComponent<Bricks> ().brickTypeNum = 4;
					cloneBrick.GetComponent<Bricks> ().SetColour ();
					cloneBrick.GetComponent<Bricks> ().AttachSpriteSheet ();
				} else if (cloneBrick.GetComponent<Bricks> ().arrayY == originBrick.GetComponent<Bricks> ().arrayY && cloneBrick.GetComponent<Bricks> ().arrayX == originBrick.GetComponent<Bricks> ().arrayX + 1) {
					cloneBrick.GetComponent<Bricks> ().brickTypeNum = 4;
					cloneBrick.GetComponent<Bricks> ().SetColour ();
					cloneBrick.GetComponent<Bricks> ().AttachSpriteSheet ();
				} else if (cloneBrick.GetComponent<Bricks> ().arrayY == originBrick.GetComponent<Bricks> ().arrayY && cloneBrick.GetComponent<Bricks> ().arrayX == originBrick.GetComponent<Bricks> ().arrayX - 1) {
					cloneBrick.GetComponent<Bricks> ().brickTypeNum = 4;
					cloneBrick.GetComponent<Bricks> ().SetColour ();
					cloneBrick.GetComponent<Bricks> ().AttachSpriteSheet ();
				}
			}
				

		} */
	}

	void ZapBricks() {
		bricks = GameObject.FindGameObjectsWithTag ("Brick");
		int randBrickNum;
		int randCol;

		randBrickNum = Random.Range (0, SpawnBricks.instance.bricks);
		randCol = Random.Range (0, SpawnBricks.instance.cols);

		bricks[randBrickNum].GetComponent<Bricks> ().DestroySurroundBricks ();
	}

	void FallingBricks() {
		GM.instance.fallingBricks = true;
	}

	void ShrinkBall () {
		foreach (GameObject cloneBall in balls) {
			Ball currentBall = cloneBall.GetComponent<Ball> ();

			currentBall.transform.localScale = new Vector3 (0.5f, 0.5f, 1f);
		}
	} 

	void SuperShrink () {
		foreach (GameObject cloneBall in balls) {
			Ball currentBall = cloneBall.GetComponent<Ball> ();

			// currentBall.transform.localScale = new Vector3 (0.5f, 0.5f, 1f);
		}

		paddle.transform.localScale = new Vector3 (1f, 0.75f, 1f);
	} 

	void KillPaddle () {
		GM.instance.LoseLife ();
	}

	void MegaBall() {
		foreach (GameObject cloneBall in balls) {
			Ball currentBall = cloneBall.GetComponent<Ball> ();
			if (paddle.GetComponent<Paddle> ().grabPaddle == true) {

			}
			currentBall.transform.localScale = new Vector3 (1.5f, 1.5f, 1f);
		}
	}

	void EightBall() {
		//GameObject cloneBall = balls [0];
		//cloneBall.GetComponent<Ball> ().EightBall();
		GM.instance.EightBall();
	}
}
