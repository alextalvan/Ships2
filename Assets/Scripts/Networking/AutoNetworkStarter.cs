using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(CustomNetManager))]
public class AutoNetworkStarter : MonoBehaviour 
{
	[SerializeField]
	CustomNetManager net;

	OfflineSceneReferences refs;

	void OnLevelWasLoaded(int n)
	{

		if (net.CheckIfLobbyScene ()) 
		{
			PerformCleanup();

			refs = GameObject.Find("OfflineSceneReferences").GetComponent<OfflineSceneReferences>();

			if(net.clientAutoReconnect)
			{
				refs.firstMenuPage.Disable();
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
		refs.autoConnectMessage.Enable ();
		yield return new WaitForSeconds(5);
		net.StartClientAttempt();
		//refs.autoConnectMessage.Disable ();
	}

	IEnumerator DelayedServerStart()
	{
		refs.autoRestartMessage.Enable ();
		yield return new WaitForSeconds(2);
		net.StartServerAttempt ();
		//refs.autoRestartMessage.Disable ();
	}

	// Use this for initialization
	void Awake () 
	{

	}
	
	// Update 
	void FixedUpdate () 
	{
	
	}

	void PerformCleanup()
	{

		LobbyPlayerInfo[] garbage = GameObject.FindObjectsOfType<LobbyPlayerInfo> ();
		foreach (LobbyPlayerInfo p in garbage)
			Destroy (p.gameObject);


		CustomLobbyPlayer[] garbage2 = GameObject.FindObjectsOfType<CustomLobbyPlayer> ();

		foreach (CustomLobbyPlayer l in garbage2)
			Destroy (l.gameObject);
	}
}
