using UnityEngine;
using System.Collections;
using UnityEditor;


public enum LIMBS
{
	HIPS = 0,
	ABDOMEN = 1,
	CHEST = 2,
	NECK = 3,
	HEAD = 4,
	LEFT_HIP = 5,
	LEFT_KNEE = 6,
	LEFT_ANKLE = 7,
	LEFT_TOE = 8,
	RIGHT_HIP = 9,
	RIGHT_KNEE = 10,
	RIGHT_ANKLE = 11,
	RIGHT_TOE = 12,
	LEFT_COLLAR = 13,
	LEFT_SHOULDER = 14,
	LEFT_ELBOW = 15,
	LEFT_WRIST = 16,
	LEFT_HAND = 17,
	RIGHT_COLLAR = 18,
	RIGHT_SHOULDER = 19,
	RIGHT_ELBOW = 20,
	RIGHT_WRIST = 21,
	RIGHT_HAND = 22
}
//[CustomEditor (typeof(RAMActorManager))]
public class DropDown : Editor {

	public LIMBS limb;
	public int index = 0;
	public GameObject obj;
	public string OscAddress = "/ram/skeleton";


	[MenuItem("Example/Editor")]
	public override void OnInspectorGUI()
	{
		GUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Osc Address");
		OscAddress = (string)EditorGUILayout.TextField(OscAddress);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Actor Prefab");
		obj = (GameObject) EditorGUILayout.ObjectField(obj, typeof(GameObject));
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		limb = (LIMBS)EditorGUILayout.EnumPopup("Choose Joint", limb);
		GUILayout.EndHorizontal();
	}

	//void OnInspectorGUI()
	//{
	//	limb = (LIMBS)EditorGUILayout.EnumPopup("Choose Limb", limb);
	//	if (GUILayout.Button("Select"))
	//	{
	//		Debug.Log("Send " + limb);
	//	}
	//	EditorUtility.SetDirty(target);
	//}
}
