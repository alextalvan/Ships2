using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class LobbyPlayerInfo : NetworkBehaviour {

	[SyncVar]
	public int currentPlayerCount = 0;

	[SyncVar]
	public int currentReadyCount = 0;

	[SyncVar]
	public int minToStartCount = 0;
		
	void FixedUpdate()
	{
		if (!NetworkManager.singleton.IsClientConnected ())
			return;

		GameObject target = GameObject.Find ("OfflineSceneReferences");
		if (target != null) 
		{
			target.GetComponent<OfflineSceneReferences>().playersReadyCountText.text = currentReadyCount + "/" + currentPlayerCount;
		}
	}
}

