using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

//simple object that is used to sync the lobby state for all players
[NetworkSettings(channel = 0, sendInterval = 0.5f)]
public class LobbyPlayerInfo : NetworkBehaviour {

	[SyncVar]
	public int currentPlayerCount = 0;

	[SyncVar]
	public int currentReadyCount = 0;

	[SyncVar]
	public int minToStartCount = 0;
	

	void Start()
	{
		DontDestroyOnLoad (this.gameObject);
	}
		
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

