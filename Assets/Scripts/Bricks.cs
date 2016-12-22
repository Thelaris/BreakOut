using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour {
	public GameObject brickParticle;
	public int brickTypeNum;
	public int easyBrickBreaks;
	public int mediumBrickBreaks;
	public int hardBrickBreaks;
	public Texture easyBricks;
	public Texture mediumBricks;
	public Texture hardBricks;
	public Texture explodeBricks;
	public Texture smallCrackedTex;
	public Texture mediumCrackedTex;
	public Texture mostCrackedTex;
	public float bumpScale;
	public int arrayY;
	public int arrayX;
	public GameObject capsule;

	private int numOfHits;
	private GameObject[] bricks;
	private bool doOnce;

	void Start() {
		doOnce = true;

		AttachSpriteSheet ();

	}

	void OnCollisionEnter(Collision ball) {
		CheckShouldDestroy ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Ball") || other.gameObject.tag.Equals("GunLaser")) {
			CheckShouldDestroy ();
		}
	}

	public void AttachSpriteSheet () {
		if (brickTypeNum == 4) {
			gameObject.AddComponent <AnimateSpriteSheet> ();
		}
	}

	public void CheckShouldDestroy(){
		numOfHits++;
		if (GM.instance.fireBall == true) {
			DestroySurroundBricks ();
			DestroyBrick ();
		} else {


			if (brickTypeNum == 1) {
				DestroyBrick ();
			}

			if (brickTypeNum == 2) {
				if (numOfHits == mediumBrickBreaks) {
					DestroyBrick ();
				} else {
					/*
				GetComponent<Renderer> ().material.EnableKeyword("_NORMALMAP");
				GetComponent<Renderer> ().material.SetTexture("_MainTex", mediumCrackedTex);
				GetComponent<Renderer> ().material.SetFloat("_BumpScale", bumpScale);
				*/
				}
			}


			if (brickTypeNum == 3) {
				if (numOfHits == hardBrickBreaks) {
					DestroyBrick ();
				} else if (numOfHits == 1) {
					/*
				GetComponent<Renderer> ().material.EnableKeyword("_NORMALMAP");
				GetComponent<Renderer> ().material.SetTexture("_MainTex", mediumCrackedTex);
				GetComponent<Renderer> ().material.SetFloat("_BumpScale", bumpScale);
				*/
				} else if (numOfHits == 2) {
					/*
				GetComponent<Renderer> ().material.EnableKeyword("_NORMALMAP");
				GetComponent<Renderer> ().material.SetTexture("_MainText", mostCrackedTex);
				GetComponent<Renderer> ().material.SetFloat("_BumpScale", bumpScale);
				*/
				}
			}

			if (brickTypeNum == 4) {
				DestroyBrick ();
			}
		}
	}

	public void DestroyBrick() {
		if (doOnce) {
			if (brickTypeNum == 4) {
				ExplodeBricks ();
			}
			doOnce = false;
			Instantiate (brickParticle, transform.position, Quaternion.identity);
			GM.instance.DestroyBrick (transform.position , brickTypeNum);
			Destroy (gameObject);
		}
	}

	public void SetColour() {
		if (brickTypeNum == 1) {
			//GetComponent<Renderer> ().material.color = new Color (0.5f, 0.5f, 0.5f, 1);
			GetComponent<Renderer> ().material.SetTexture ("_MainTex", easyBricks);
		} else if (brickTypeNum == 2) {
			//GetComponent<Renderer> ().material.color = new Color (0, 0, 1, 1);
			GetComponent<Renderer> ().material.SetTexture ("_MainTex", mediumBricks);
		} else if (brickTypeNum == 3) {
			GetComponent<Renderer> ().material.SetTexture ("_MainTex", hardBricks);
			//GetComponent<Renderer> ().material.color = new Color (1, 0.92f, 0.016f, 1);
		} else if (brickTypeNum == 4) {
			GetComponent<Renderer> ().material.SetTexture ("_MainTex", explodeBricks);
		//	gameObject.tag = "ExplodeBrick";
		} else if (brickTypeNum == 5) {
		//GetComponent<Renderer> ().material.color = new Color (0, 0, 0, 1);
		}
	}

	void Update() {
		if (GM.instance.thruBrick == true) {
			GetComponent<Collider> ().isTrigger = true;
		} else {
			GetComponent<Collider> ().isTrigger = false;
		}
	}

	public void DestroySurroundBricks() {
		bricks = GameObject.FindGameObjectsWithTag ("Brick");
		foreach (GameObject cloneBrick in bricks) {
			if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY - 1 && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX - 1) {
				cloneBrick.GetComponent<Bricks> ().DestroyBrick ();
			} else if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY - 1 && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX) {
				cloneBrick.GetComponent<Bricks> ().DestroyBrick ();
			} else if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY - 1 && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX + 1) {
				cloneBrick.GetComponent<Bricks> ().DestroyBrick ();
			} else if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY + 1 && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX + 1) {
				cloneBrick.GetComponent<Bricks> ().DestroyBrick ();
			} else if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY + 1 && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX - 1) {
				cloneBrick.GetComponent<Bricks> ().DestroyBrick ();
			} else if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY + 1 && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX) {
				cloneBrick.GetComponent<Bricks> ().DestroyBrick ();
			} else if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX + 1) {
				cloneBrick.GetComponent<Bricks> ().DestroyBrick ();
			} else if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX - 1) {
				cloneBrick.GetComponent<Bricks> ().DestroyBrick ();
			}
		}
	}
	public void ExplodeBricks() {
		Instantiate (capsule, transform.position, Quaternion.Euler (0f, 0f, -90f));

	




		/*	bricks = GameObject.FindGameObjectsWithTag ("Brick");
		foreach (GameObject cloneBrick in bricks) {
			if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY - 1 && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX - 1 && cloneBrick.GetComponent<Bricks> ().brickTypeNum == 4) {
				cloneBrick.GetComponent<Bricks> ().ExplodeBricks ();
				cloneBrick.GetComponent<Bricks> ().DestroyBrick ();
			} else if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY - 1 && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX && cloneBrick.GetComponent<Bricks> ().brickTypeNum == 4) {
				cloneBrick.GetComponent<Bricks> ().ExplodeBricks ();
				cloneBrick.GetComponent<Bricks> ().DestroyBrick ();
			} else if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY - 1 && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX + 1 && cloneBrick.GetComponent<Bricks> ().brickTypeNum == 4) {
				cloneBrick.GetComponent<Bricks> ().ExplodeBricks ();
				cloneBrick.GetComponent<Bricks> ().DestroyBrick ();
			} else if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY + 1 && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX + 1 && cloneBrick.GetComponent<Bricks> ().brickTypeNum == 4) {
				cloneBrick.GetComponent<Bricks> ().ExplodeBricks ();
				cloneBrick.GetComponent<Bricks> ().DestroyBrick ();
			} else if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY + 1 && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX - 1 && cloneBrick.GetComponent<Bricks> ().brickTypeNum == 4) {
				cloneBrick.GetComponent<Bricks> ().ExplodeBricks ();
				cloneBrick.GetComponent<Bricks> ().DestroyBrick ();
			} else if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY + 1 && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX && cloneBrick.GetComponent<Bricks> ().brickTypeNum == 4) {
				cloneBrick.GetComponent<Bricks> ().ExplodeBricks ();
				cloneBrick.GetComponent<Bricks> ().DestroyBrick ();
			} else if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX + 1 && cloneBrick.GetComponent<Bricks> ().brickTypeNum == 4) {
				cloneBrick.GetComponent<Bricks> ().ExplodeBricks ();
				cloneBrick.GetComponent<Bricks> ().DestroyBrick ();
			} else if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX - 1 && cloneBrick.GetComponent<Bricks> ().brickTypeNum == 4) {
				cloneBrick.GetComponent<Bricks> ().ExplodeBricks ();
				cloneBrick.GetComponent<Bricks> ().DestroyBrick ();
			} else {
			//	cloneBrick.GetComponent<Bricks> ().DestroyBrick ();
			}
		} */
	}
		
}
