using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(CustomNetManager))]
public class AutoNetworkStarter : MonoBehaviour 
{
	[SerializeField]
	CustomNetManager net;

	void OnLevelWasLoaded(int n)
	{
		if (net.CheckIfLobbyScene ()) 
		{
			if(net.clientAutoReconnect)
			{
				StartCoroutine(DelayedClientConnect());
				//net.StartClientAttempt();
				return;
			}

			if(net.serverAutoRestart)
			{
				NetworkServer.Reset();
				StartCoroutine(DelayedServerStart());
				//net.StartServerAttempt();
				return;
			}
		}
	}

	IEnumerator DelayedClientConnect()
	{
		yield return new WaitForSeconds(5);
		net.StartClientAttempt();
	}

	IEnumerator DelayedServerStart()
	{
		yield return new WaitForSeconds(2);
		net.StartServerAttempt ();
	}

	// Use this for initialization
	void Awake () 
	{

	}
	
	// Update 
	void FixedUpdate () 
	{
	
	}
}
