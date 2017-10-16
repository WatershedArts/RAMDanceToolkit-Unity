using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {

	public GameObject Actor;
	public bool RotateCamera = false;
	public bool LookAtActor = false;
	private Vector3 lookAtPoint = new Vector3(0,10,0);
	public float rotationMultiplier = 1.0f;

	// Use this for initialization
	void Start () 
	{
		
	}

	void Update () 
	{
		if (LookAtActor)
		{
			lookAtPoint = Actor.transform.position;
		}

		transform.LookAt(lookAtPoint);

		if (RotateCamera)
		{
			transform.RotateAround(lookAtPoint, new Vector3(0.0f, 5.0f, 0.0f), 20 * Time.deltaTime * rotationMultiplier);
		}

	}
}
