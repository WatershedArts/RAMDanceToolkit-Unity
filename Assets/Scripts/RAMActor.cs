using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RAMActor : MonoBehaviour {

	private string actorName = "";
	public GameObject nodeObject;
	public Dictionary<string, GameObject> nodes = new Dictionary<string, GameObject>();
	public RAMActor(string name, Dictionary<string, GameObject> _nodes)
	{
		
		actorName = name;
		nodes = _nodes;
	}

	public void MoveNodes(Dictionary<string, Vector3> positions)
	{
		foreach (KeyValuePair<string, Vector3> node in positions)
		{
			nodes[node.Key].transform.position = node.Value;
		}
	}
}
