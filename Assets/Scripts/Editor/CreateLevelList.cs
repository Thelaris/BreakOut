using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateLevelList : MonoBehaviour {

	[MenuItem("Assets/Create/Level List")]
	public static LevelList  Create()
	{
		LevelList asset = ScriptableObject.CreateInstance<LevelList>();

		AssetDatabase.CreateAsset(asset, "Assets/LevelList.asset");
		AssetDatabase.SaveAssets();
		return asset;
	}
}

