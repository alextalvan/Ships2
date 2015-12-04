using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;


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

