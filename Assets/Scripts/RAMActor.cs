using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RAMActor : MonoBehaviour 
{
	private string actorName = "";
	public Dictionary<string, GameObject> nodes = new Dictionary<string, GameObject>();
	private Transform[] tmpNodes;

	LineRenderer leftLower;
	LineRenderer rightLower;
	LineRenderer rightUpper;
	LineRenderer leftUpper;
	LineRenderer spine;

	public RAMActor(GameObject gb)
	{
		tmpNodes = gb.GetComponentsInChildren<Transform>();
		foreach (Transform s in tmpNodes)
		{
			if (s.tag.Equals("Nodes"))
			{
				nodes.Add(s.name, s.gameObject);
			}
			else if (s.tag.Equals("Bones"))
			{
				if (s.name.Equals("LeftLower"))
				{
					Debug.Log("Found Left Lower Bone");
					leftLower = s.GetComponent<LineRenderer>();
					leftLower.SetVertexCount(5);
					leftLower.SetPosition(0, nodes["LEFT_TOE"].transform.position);
					leftLower.SetPosition(1, nodes["LEFT_ANKLE"].transform.position);
					leftLower.SetPosition(2, nodes["LEFT_KNEE"].transform.position);
					leftLower.SetPosition(3, nodes["LEFT_HIP"].transform.position);
					leftLower.SetPosition(4, nodes["HIPS"].transform.position);
				}
				else if (s.name.Equals("RightLower"))
				{
					Debug.Log("Found Right Lower Bone");
					rightLower = s.GetComponent<LineRenderer>();
					rightLower.SetVertexCount(5);
					rightLower.SetPosition(0, nodes["RIGHT_TOE"].transform.position);
					rightLower.SetPosition(1, nodes["RIGHT_ANKLE"].transform.position);
					rightLower.SetPosition(2, nodes["RIGHT_KNEE"].transform.position);
					rightLower.SetPosition(3, nodes["RIGHT_HIP"].transform.position);
					rightLower.SetPosition(4, nodes["HIPS"].transform.position);
				}
				else if (s.name.Equals("LeftUpper"))
				{
					Debug.Log("Found Left Upper Bone");
					leftUpper = s.GetComponent<LineRenderer>();
					leftUpper.SetVertexCount(6);
					leftUpper.SetPosition(0, nodes["LEFT_HAND"].transform.position);
					leftUpper.SetPosition(1, nodes["LEFT_WRIST"].transform.position);
					leftUpper.SetPosition(2, nodes["LEFT_ELBOW"].transform.position);
					leftUpper.SetPosition(3, nodes["LEFT_SHOULDER"].transform.position);
					leftUpper.SetPosition(4, nodes["LEFT_COLLAR"].transform.position);
					leftUpper.SetPosition(5, nodes["CHEST"].transform.position);
				}
				else if (s.name.Equals("RightUpper"))
				{
					Debug.Log("Found Right Upper Bone");
					rightUpper = s.GetComponent<LineRenderer>();
					rightUpper.SetVertexCount(6);
					rightUpper.SetPosition(0, nodes["RIGHT_HAND"].transform.position);
					rightUpper.SetPosition(1, nodes["RIGHT_WRIST"].transform.position);
					rightUpper.SetPosition(2, nodes["RIGHT_ELBOW"].transform.position);
					rightUpper.SetPosition(3, nodes["RIGHT_SHOULDER"].transform.position);
					rightUpper.SetPosition(4, nodes["RIGHT_COLLAR"].transform.position);
					rightUpper.SetPosition(5, nodes["CHEST"].transform.position);
				}
				else if (s.name.Equals("Spine"))
				{
					Debug.Log("Found Spine Bone");
					spine = s.GetComponent<LineRenderer>();
					spine.SetVertexCount(5);
					spine.SetPosition(0, nodes["HEAD"].transform.position);
					spine.SetPosition(1, nodes["NECK"].transform.position);
					spine.SetPosition(2, nodes["CHEST"].transform.position);
					spine.SetPosition(3, nodes["ABDOMEN"].transform.position);
					spine.SetPosition(4, nodes["HIPS"].transform.position);
				}
			}
		}
	}

	public void MoveNodes(Dictionary<string, Vector3> positions,Dictionary<string, Quaternion> rotations)
	{
		rightUpper.SetPosition(0, positions["RIGHT_HAND"]);
		rightUpper.SetPosition(1, positions["RIGHT_WRIST"]);
		rightUpper.SetPosition(2, positions["RIGHT_ELBOW"]);
		rightUpper.SetPosition(3, positions["RIGHT_SHOULDER"]);
		rightUpper.SetPosition(4, positions["RIGHT_COLLAR"]);
		rightUpper.SetPosition(5, positions["CHEST"]);
		spine.SetPosition(0, positions["HEAD"]);
		spine.SetPosition(1, positions["NECK"]);
		spine.SetPosition(2, positions["CHEST"]);
		spine.SetPosition(3, positions["ABDOMEN"]);
		spine.SetPosition(4, positions["HIPS"]);
		leftUpper.SetPosition(0, positions["LEFT_HAND"]);
		leftUpper.SetPosition(1, positions["LEFT_WRIST"]);
		leftUpper.SetPosition(2, positions["LEFT_ELBOW"]);
		leftUpper.SetPosition(3, positions["LEFT_SHOULDER"]);
		leftUpper.SetPosition(4, positions["LEFT_COLLAR"]);
		leftUpper.SetPosition(5, positions["CHEST"]);
		rightLower.SetPosition(0, positions["RIGHT_TOE"]);
		rightLower.SetPosition(1, positions["RIGHT_ANKLE"]);
		rightLower.SetPosition(2, positions["RIGHT_KNEE"]);
		rightLower.SetPosition(3, positions["RIGHT_HIP"]);
		rightLower.SetPosition(4, positions["HIPS"]);
		leftLower.SetPosition(0, positions["LEFT_TOE"]);
		leftLower.SetPosition(1, positions["LEFT_ANKLE"]);
		leftLower.SetPosition(2, positions["LEFT_KNEE"]);
		leftLower.SetPosition(3, positions["LEFT_HIP"]);
		leftLower.SetPosition(4, positions["HIPS"]);



		foreach (KeyValuePair<string, Vector3> node in positions)
		{
			nodes[node.Key].transform.position = node.Value;
		}
		foreach (KeyValuePair<string, Quaternion> node in rotations)
		{
			nodes[node.Key].transform.rotation = node.Value;
		}
	}

	public Vector3 GetLimbCoordinates(string limb)
	{
		if(nodes.ContainsKey(limb)) 
		{
			return nodes[limb].transform.position;
		}
		Debug.Log("Joint Does Not Exist");
		return new Vector3(0, 0, 0);
	}
}
