using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour {

	public AudioSource audioSource;
	public string LimbName = "HIPS";

	// Use this for initialization
	void Awake() 
	{
		audioSource = GetComponent<AudioSource>();
		if (audioSource != null)
		{
			Debug.Log("Found Sound Player");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.name);
		if (other.name == LimbName)
		{
			audioSource.Play();
		}
	}


}
