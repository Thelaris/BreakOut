using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitToWeb : MonoBehaviour {

	public GameObject backgroundPanel;
	public GameObject askLevelNameUI;
	public GameObject askCreatorNameUI;
	public GameObject confirmUI;

	public int rows = 20;
	public int cols = 13;
	public string url = "https://thelarisnet.com/breakout/createdlevels/upload.php";

	public InputField levelNameResult;
	public InputField creatorResult;
	public Text confirmText;
	private string levelName = "";
	private string creator = "";

	public int[,] bricksArray = new int[,] {
		//0  1  2  3  4  5  6  7  8  9  10 11 12
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //0
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //1
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //2
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //3
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //4
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //5
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //6
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //7
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //8
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //9
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //10
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //11
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //12
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //13
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //14
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //15
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //16
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //17
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //18
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }  //19

	};

	private string output = "";


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void DisableBrickInput() {
		GameObject[] bricks = GameObject.FindGameObjectsWithTag ("Brick");
		foreach (GameObject brick in bricks) {
			brick.layer = 2;
		}

		bricks = GameObject.FindGameObjectsWithTag ("LEBrick");
		foreach (GameObject brick in bricks) {
			brick.layer = 2;
		}
	}

	void EnableBrickInput() {
		GameObject[] bricks = GameObject.FindGameObjectsWithTag ("Brick");
		foreach (GameObject brick in bricks) {
			brick.layer = 0;
		}

		bricks = GameObject.FindGameObjectsWithTag ("LEBrick");
		foreach (GameObject brick in bricks) {
			brick.layer = 0;
		}
	}

	public void AskLevelName() {
		DisableBrickInput ();
		backgroundPanel.SetActive (true);
		askLevelNameUI.SetActive (true);
	}

	public void AskCreatorName() {
		DisableBrickInput ();
		levelName = levelNameResult.text;
		print (levelName);
		askLevelNameUI.SetActive (false);
		backgroundPanel.SetActive (true);
		askCreatorNameUI.SetActive (true);
	}

	public void Cancel() {
		EnableBrickInput ();
		backgroundPanel.SetActive (false);
		askLevelNameUI.SetActive (false);
		askCreatorNameUI.SetActive (false);
		confirmUI.SetActive (false);
	}
		

	public void Confirm() {
		DisableBrickInput ();
		creator = creatorResult.text;
		confirmText.text = levelName + " by " + creator;
		backgroundPanel.SetActive (true);
		askLevelNameUI.SetActive (false);
		askCreatorNameUI.SetActive (false);
		confirmUI.SetActive (true);

	}

	public void Submit() {
		GameObject[] bricks = GameObject.FindGameObjectsWithTag ("Brick");
		foreach (GameObject brick in bricks) {
			if (brick.GetComponent<Bricks> ().brickTypeNum == 98) {
				brick.GetComponent<Bricks> ().brickTypeNum = 13;
			}
			if (brick.GetComponent<Bricks> ().brickTypeNum != 99) {
				bricksArray [brick.GetComponent<Bricks> ().arrayY, brick.GetComponent<Bricks> ().arrayX] = brick.GetComponent<Bricks> ().brickTypeNum;
			}

		}


		//	output = "{ ";

		for (int i = 0; i < rows; i++)
		{
			output += "," + System.Environment.NewLine + "{ ";
			for (int j = 0; j < cols; j++) {

				output += bricksArray [i, j] + ", ";
				//print (i + j);
			}	
			output += "}";
		}
		output += System.Environment.NewLine;
		output = output.Remove (0, 1);
		//output += " }"; 
		//print (output);
		//output = "";


		WWWForm form = new WWWForm();
		form.AddField("levelname", levelName);
		form.AddField("creator", creator);
		form.AddField("array", output);
		WWW www = new WWW(url, form);

		StartCoroutine(WaitForRequest(www));
		output = "";
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

			// check for errors
			if (www.error == null)
			{
				Debug.Log("WWW Ok!: " + www.data);
			} else {
				Debug.Log("WWW Error: "+ www.error);
			}    
	}    

}

