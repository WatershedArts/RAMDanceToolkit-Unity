using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Rug.Osc;
using System.Linq;
using UnityEditor;
using UnityEngine.UI;

public class RAMActorManager: MonoBehaviour 
{
	public GameObject ActorPrefab;
	private OscConnection oscConnection;

	public string OscAddress = "/ram/skeleton";

	Dictionary<string, Vector3> defaultNodes = new Dictionary<string, Vector3>();
	Dictionary<string, RAMActor> actors = new Dictionary<string, RAMActor>();

	private OscMessage newMessage;

	public GameObject trail1;
	public GameObject trail2;

	public string WhichActor = "ACTOR NAME";
	public string WhichJoint1 = "HIPS";
	public string WhichJoint2 = "HIPS";

	public GameObject Kyle;
	private Transform[] tmpNodes;
	private Dictionary<string, GameObject> kyleNodes = new Dictionary<string, GameObject>();

	//--------------------------------------------------------
	// * Make the Default Nodes
	//--------------------------------------------------------
	void MakeDefaultNodes()
	{
		defaultNodes.Add("HIPS", new Vector3(0, 75, 0));
		defaultNodes.Add("ABDOMEN", new Vector3(0, 100, 0));
		defaultNodes.Add("CHEST", new Vector3(0, 120, 0));
		defaultNodes.Add("NECK", new Vector3(0, 130, 0));
		defaultNodes.Add("HEAD", new Vector3(0, 145, 0));

		defaultNodes.Add("LEFT_HIP", new Vector3(-12.5f, 75, 0));
		defaultNodes.Add("LEFT_KNEE", new Vector3(-12.5f, 35, 0));
		defaultNodes.Add("LEFT_ANKLE", new Vector3(-12.5f, 5, 0));
		defaultNodes.Add("LEFT_TOE", new Vector3(-12.5f, 7, -15));

		defaultNodes.Add("RIGHT_HIP", new Vector3(12.5f, 75, 0));
		defaultNodes.Add("RIGHT_KNEE", new Vector3(12.5f, 35, 0));
		defaultNodes.Add("RIGHT_ANKLE", new Vector3(12.5f, 5, 0));
		defaultNodes.Add("RIGHT_TOE", new Vector3(12.5f, 7, -15));

		defaultNodes.Add("LEFT_COLLAR", new Vector3(-10, 125, 0));
		defaultNodes.Add("LEFT_SHOULDER", new Vector3(-50, 125, 0));
		defaultNodes.Add("LEFT_ELBOW", new Vector3(-50, 100, 0));
		defaultNodes.Add("LEFT_WRIST", new Vector3(-50, 75, 0));
		defaultNodes.Add("LEFT_HAND", new Vector3(-50, 70, 0));

		defaultNodes.Add("RIGHT_COLLAR", new Vector3(10, 125, 0));
		defaultNodes.Add("RIGHT_SHOULDER", new Vector3(12.5f, 125, 0));
		defaultNodes.Add("RIGHT_ELBOW", new Vector3(12.5f, 100, 0));
		defaultNodes.Add("RIGHT_WRIST", new Vector3(12.5f, 75, 0));
		defaultNodes.Add("RIGHT_HAND", new Vector3(12.5f, 70, 0));
	}
	//--------------------------------------------------------
	// * On OSC Message
	//--------------------------------------------------------
	void Awake() 
	{
		bool failedSetup = false;

		// Check the OSC address is valid
		if (Rug.Osc.OscAddress.IsValidAddressPattern(OscAddress) == false) 
		{
			Debug.LogError(string.Format("Invalid OSC address \"{0}\".", OscAddress), this);
			failedSetup = true;
		}

		// try and find the connection object
		oscConnection = FindObjectOfType<OscConnection>();

		if (oscConnection == null)
		{
			Debug.LogError("Could not find an OSC connection object within the scene hierarchy.", this);
			failedSetup = true;
		}

		if (failedSetup == true)
		{
			gameObject.SetActive(false);
		}
		oscConnection.OscListener.Attach(OscAddress, OnOscMessage);

		if (Kyle != null)
		{
			tmpNodes = Kyle.GetComponentsInChildren<Transform>();

			foreach (Transform item in tmpNodes)
			{
				if (item.tag.Equals("KyleBones"))
				{
					kyleNodes.Add(item.name, item.gameObject);
				}
			}
			Debug.Log(kyleNodes.Count);
		}


	}

	//--------------------------------------------------------
	// * On OSC Message
	//--------------------------------------------------------
	private void OnOscMessage(OscMessage message)
	{
		newMessage = message;	
		// So the incoming message from Motioner is described as a 187 point message
		// 0 is the name of the Motioner Kit
		// 1 is the number of limbs attached
		// This is repeated for the number of limbs attached
		// 2 LIMB
		// 3 Pos x
		// 4 Pos y
		// 5 Pos z
		// 6 Rotation x
		// 7 Rotation y
		// 8 Rotation z
		// 9 Rotation w
	}

	//--------------------------------------------------------
	// * Generate Actor
	//--------------------------------------------------------
	IEnumerator GenerateNewActor(string actorName)
	{
		// Make a GameObject for the Actor. This make it easier to Delete Later On
		GameObject NewActor = Instantiate(ActorPrefab, new Vector3(0, 0, 0), transform.rotation, transform) as GameObject;
		NewActor.name = actorName;
		actors.Add(actorName, new RAMActor(NewActor));
		Debug.Log("Have Instantiated New Actor: " + actorName);
		yield return new WaitForSeconds(2);
	}

	//--------------------------------------------------------
	// * Start
	//--------------------------------------------------------
	void Start () 
	{
		// Enable it to run in the Background
		Application.runInBackground = true;

		// Make the Default Nodes
		MakeDefaultNodes();

		// Generate a Default Skeleton
		StartCoroutine(GenerateNewActor("DefaultRAMActor"));

		// Activate the Checkers 
		InvokeRepeating("CheckForActorEntered", 0.0f, 5.0f);
		InvokeRepeating("CheckForActorExited", 5.0f, 7.5f);
	}

	//--------------------------------------------------------
	// * Check for new Actor
	//--------------------------------------------------------
	void CheckForActorEntered()
	{
		try
		{
			// If the actors Dictionary Does NOT contain the New Actors Name 
			if (!actors.ContainsKey((string)newMessage[0]))
			{
				Debug.Log("We Don't have a " + (string)newMessage[0]);

				// Generate New Actor
				StartCoroutine(GenerateNewActor((string)newMessage[0]));
			}
		}
		catch (System.Exception ex)
		{
			Debug.Log("No Osc Message");
		}
	}

	//--------------------------------------------------------
	// * Check whether the Actor has exitted
	//--------------------------------------------------------
	void CheckForActorExited()
	{
		try
		{
			// Make a list of the current Actors in the OSC Message
			List<string> l = new List<string>();
			l.Add((string)newMessage[0]);
			Debug.Log("Found these Actors: " + (string)newMessage[0]);

			// Exclude those active actors
			List<string> lss = actors.Keys.Except(l).ToList();

			// Iterate through them and remove them from the Dictionary
			foreach (string i in lss)
			{
				Debug.Log("Going to DELETE: "+ i);
				GameObject p = GameObject.Find(i);
				actors.Remove(i);
				Destroy(p);
			}
		}
		catch (System.Exception ex)
		{
			// Is Null
			Debug.Log("No Osc Message");
		}
	}
	//--------------------------------------------------------
	// * Update Frame
	//--------------------------------------------------------
	void Update () 
	{
		// If we have a Valid OSC Message
		try
		{
			// Loop through the current actors in the Dictionary
			foreach (KeyValuePair<string, RAMActor> actor in actors)
			{
				// Grab all of the Positions from the Message
				Dictionary<string, Vector3> positions = new Dictionary<string, Vector3>();
				Dictionary<string, Quaternion> rotations = new Dictionary<string, Quaternion>();
				// Loop through the message
				for (int i = 0; i < newMessage.Count; i++)
				{
					// If the OSC Message Actor Name Matches any actor dictionary
					if (newMessage[0].Equals(actor.Key))
					{
						// Loop through the defaultNames to make sure they are Valid
						foreach (KeyValuePair<string, Vector3> node in defaultNodes)
						{
							// If the OSC Message Element matches the valid nodes
							if (newMessage[i].Equals(node.Key))
							{
								//Debug.Log("Current Actor: "+actor.Key+" Current Limb: " + node.Key);
								positions.Add(node.Key,new Vector3(((float)newMessage[i + 1])*0.1f, ((float)newMessage[i + 2]) * 0.1f, ((float)newMessage[i + 3]) * 0.1f));
								rotations.Add(node.Key, new Quaternion((float)newMessage[i + 4], (float)newMessage[i + 5], (float)newMessage[i + 6], (float)newMessage[i + 7])); //new Vector3((float)newMessage[i + 1], (float)newMessage[i + 2], (float)newMessage[i + 3]));
							}
						}
					}	
				}
				actors[actor.Key].MoveNodes(positions,rotations);
			}
		}
		catch (System.Exception ex)
		{
			// Is Null
		}

		if (actors.ContainsKey(WhichActor))
		{

			if (Kyle != null)
			{
				foreach (KeyValuePair<string, GameObject> obj in kyleNodes)
				{
					kyleNodes[obj.Key].transform.position = actors[WhichActor].GetLimbCoordinates(obj.Key) + new Vector3(-250,0,0);
					//kyleNodes[obj.Key].transform.rotation = actors[WhichActor].GetLimbRotation(obj.Key);
				}
			}

			if (trail1 != null)
			{
				trail1.transform.position = actors[WhichActor].GetLimbCoordinates(WhichJoint1);
			}

			if (trail2 != null)
			{
				trail2.transform.position = actors[WhichActor].GetLimbCoordinates(WhichJoint2);
			}
		}
	}
}
