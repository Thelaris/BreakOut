using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour {
	public GameObject brickParticle;
	public int brickTypeNum;
	public int easyBrickBreaks;
	public int mediumBrickBreaks;
	public int hardBrickBreaks;
	public Texture blueBrick;
	public Texture darkBlueBrick;
	public Texture darkGreyBrick;
	public Texture greenBrick;
	public Texture greyBrick;
	public Texture hotPinkBrick;
	public Texture hotPurpleBrick;
	public Texture orangeBrick;
	public Texture purpleBrick;
	public Texture redBrick;
	public Texture yellowBrick;
	public Texture explodeBricks;
	public Texture invisLevelEditorBrick;
	public Texture levelEditorBrick;
	public Texture easyBricks;
	public Texture mediumBricks;
	public Texture hardBricks;
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
	public bool levelEditor = false;
	//private GameObject capsule;

	void Start() {
		doOnce = true;

	//	AttachSpriteSheet ();

	}

	void OnCollisionEnter(Collision ball) {
		CheckShouldDestroy ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Ball") && GM.instance.thruBrick == true) {
			DestroyBrick ();
		}
		if (other.gameObject.tag.Equals ("Ball") || other.gameObject.tag.Equals("GunLaser")) {
			CheckShouldDestroy ();
		}
	}

	public void AttachSpriteSheet () {
		if (brickTypeNum == 12 && GetComponent<AnimateSpriteSheet> () == null) {
			gameObject.AddComponent <AnimateSpriteSheet> ();
		} else if (brickTypeNum != 12 && GetComponent<AnimateSpriteSheet> () != null) {
			DestroyImmediate (GetComponent<AnimateSpriteSheet> ());
			ResetMatSize ();
		}
	}

	void ResetMatSize () {
		Vector2 offset = new Vector2(0.0f, 0.0f);
		Vector2 size = new Vector2(1f, 1f);
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
		GetComponent<Renderer>().sharedMaterial.SetTextureScale("_MainTex", size);
	}

	public void CheckShouldDestroy(){
		numOfHits++;
		if (GM.instance.fireBall == true) {
			DestroySurroundBricks ();
			DestroyBrick ();
		} else {


			if (brickTypeNum == 1 || brickTypeNum == 2 || brickTypeNum == 4 || brickTypeNum == 5 || brickTypeNum == 6 || brickTypeNum == 7 || brickTypeNum == 8 || brickTypeNum == 9 || brickTypeNum == 10 || brickTypeNum == 11 || brickTypeNum == 12) {
				DestroyBrick ();
			}

			if (brickTypeNum == 3) {
				brickTypeNum = 5;
				SetColour ();
			}

			if (brickTypeNum == 13) {
				brickTypeNum = 3;
				SetColour ();
			}
		}
	}

	public void DestroyBrick() {
		if (doOnce) {
			if (brickTypeNum == 12) {
				ExplodeBricks ();
			}
			doOnce = false;
			Instantiate (brickParticle, transform.position, Quaternion.identity);
			GM.instance.DestroyBrick (transform.position , brickTypeNum);
			Destroy (gameObject);
		}
	}

	public void SetColour() {
		GetComponent<Renderer> ().enabled = true;
		if (brickTypeNum == 1) {
			GetComponent<Renderer> ().material.SetTexture ("_MainTex", blueBrick);
		} else if (brickTypeNum == 2) {
			GetComponent<Renderer> ().material.SetTexture ("_MainTex", darkBlueBrick);
		} else if (brickTypeNum == 3) {
			GetComponent<Renderer> ().material.SetTexture ("_MainTex", darkGreyBrick);
		} else if (brickTypeNum == 4) {
			GetComponent<Renderer> ().material.SetTexture ("_MainTex", greenBrick);
		} else if (brickTypeNum == 5) {
			GetComponent<Renderer> ().material.SetTexture ("_MainTex", greyBrick);
		} else if (brickTypeNum == 6) {
			GetComponent<Renderer> ().material.SetTexture ("_MainTex", hotPinkBrick);
		} else if (brickTypeNum == 7) {
			GetComponent<Renderer> ().material.SetTexture ("_MainTex", hotPurpleBrick);
		} else if (brickTypeNum == 8) {
			GetComponent<Renderer> ().material.SetTexture ("_MainTex", orangeBrick);
		} else if (brickTypeNum == 9) {
			GetComponent<Renderer> ().material.SetTexture ("_MainTex", purpleBrick);
		} else if (brickTypeNum == 10) {
			GetComponent<Renderer> ().material.SetTexture ("_MainTex", redBrick);
		} else if (brickTypeNum == 11) {
			GetComponent<Renderer> ().material.SetTexture ("_MainTex", yellowBrick);
		} else if (brickTypeNum == 12) {
			GetComponent<Renderer> ().material.SetTexture ("_MainTex", explodeBricks);
		} else if (brickTypeNum == 13) {
			GetComponent<Renderer> ().enabled = false;
		} else if (brickTypeNum == 98) {
			//GetComponent<Renderer> ().enabled = false;
			GetComponent<Renderer> ().material.SetTexture ("_MainTex", invisLevelEditorBrick);
		} else if (brickTypeNum == 99) {
			//GetComponent<Renderer> ().enabled = false;
			GetComponent<Renderer> ().material.SetTexture ("_MainTex", levelEditorBrick);
		}
		AttachSpriteSheet ();
	}

	void Update() {
		if (GM.instance != null) {

			if (brickTypeNum == 0) {
				Destroy (gameObject);
			}

			if (GM.instance.thruBrick == true) {
				GetComponent<Collider> ().isTrigger = true;
			} else {
				GetComponent<Collider> ().isTrigger = false;
			} 
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

	public void ExpandExploding() {
		/* if (brickTypeNum == 4) {
		//	GameObject[] bricksToChange = new GameObject[4];



			bricks = GameObject.FindGameObjectsWithTag ("Brick");
			foreach (GameObject cloneBrick in bricks) {
				if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY - 1 && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX) {
					bricksToChange [0] = cloneBrick;
				} else if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY + 1 && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX) {
					bricksToChange [1] = cloneBrick;
				} else if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX + 1) {
					bricksToChange [2] = cloneBrick;
				} else if (cloneBrick.GetComponent<Bricks> ().arrayY == GetComponent<Bricks> ().arrayY && cloneBrick.GetComponent<Bricks> ().arrayX == GetComponent<Bricks> ().arrayX - 1) {
					bricksToChange [3] = cloneBrick;
				}
			}

			foreach (GameObject brick in bricksToChange) {
				brick.GetComponent<Bricks> ().brickTypeNum = 4;
				brick.GetComponent<Bricks> ().SetColour ();
				brick.GetComponent<Bricks> ().AttachSpriteSheet ();
			}
		} */
	}

	public void ExplodeBricks() {
	
		//	Instantiate (capsule, transform.position, Quaternion.Euler (0f, 0f, -90f));
		capsule = transform.Find("Capsule").gameObject;

		capsule.SetActive(true);
		capsule.transform.parent = null;
	




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

	void OnMouseDown() {
		if (LevelEditor.instance != null && levelEditor == false) {
			brickTypeNum = LevelEditor.instance.brickType;
			SetColour ();

			LevelEditor.instance.SetBricks ();
		//	GetComponent<Bricks> ().AttachSpriteSheet ();
		}
	}
}
