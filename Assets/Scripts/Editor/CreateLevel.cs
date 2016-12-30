using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateLevel : MonoBehaviour {

	[MenuItem("Assets/Create/Level")]
	public static Level  Create()
	{
		Level asset = ScriptableObject.CreateInstance<Level>();

		AssetDatabase.CreateAsset(asset, "Assets/Level00.asset");
		AssetDatabase.SaveAssets();


		return asset;
	}
}
