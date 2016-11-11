using System.Net;
using Rug.Osc;
using UnityEngine;

public class OscConnection : MonoBehaviour
{
	public ushort ListenPort = 10000;
	public OscListener OscListener { get; private set; }

	private void Awake()
	{
		OscListener = new OscListener(ListenPort);
	}

	private void OnDestroy()
	{
		OscListener.Dispose();
	}
	private void Start()
	{
		OscListener.Connect();
	}
}
