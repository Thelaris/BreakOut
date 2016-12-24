using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintToConsole : MonoBehaviour {

	public int rows = 20;
	public int cols = 13;

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
	/*	if (LevelEditor.instance != null) {
			GetComponent<Renderer> ().enabled = true;
			GetComponent<Collider> ().enabled = true;
			transform.Find ("New Text").gameObject.GetComponent<Renderer> ().enabled = true;
		} else {
			GetComponent<Renderer> ().enabled = false;
			GetComponent<Collider> ().enabled = false;
			transform.Find ("New Text").gameObject.GetComponent<Renderer> ().enabled = false;
		} */
	}

	public void Print() {
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
		print (output);
		output = "";
	}
}
