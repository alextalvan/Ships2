using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[NetworkSettings(channel = 0, sendInterval = 1e+6f)]
public class TimeSynchronizer : NetworkBehaviour {
	
	[SerializeField]
	float _delta;
	
	public float GetClientServerDelta { get { return _delta; } }
	
	float _firstTimestamp;
	
	static bool requestedSync = false;
	
	public bool IsSynced { get { return requestedSync; } }

	float _syncReqTime;

	void Awake()
	{

        
	}

	// Use this for initialization
	void Start () 
	{
		if (NetworkManager.singleton != null && NetworkManager.singleton.isNetworkActive && !NetworkServer.active && !this.isLocalPlayer)
		{
			this.enabled = false;
			return;
		}
		
		if (NetworkManager.singleton != null && NetworkManager.singleton.isNetworkActive && NetworkServer.active)
		{
			this.enabled = false;
			return;
		}

		_syncReqTime = Time.time + 2f;
		//NetworkLobbyManager mng
		//UIConsole.SetParams (16, new Rect (0, 320, 768, 256));
		//DontDestroyOnLoad (this.gameObject);
	}
	
	
	void Update()
	{
		//automatic sync
		if (!requestedSync && _syncReqTime < Time.time) 
		{
			requestedSync = true;
			CmdRequestServerTime();
		}
	}
	
	public void StartSync()
	{
		_firstTimestamp = Time.time;
		CmdRequestServerTime ();
		UIConsole.Log ("Started time synchronization at " + _firstTimestamp);
	}
	
	
	[Command]
	void CmdRequestServerTime()
	{
		UIConsole.Log ("Received time synchronization request at " + Time.time);
		RpcReceiveServerTime (Time.time);
	}
	
	[ClientRpc]
	void RpcReceiveServerTime(float serverTime)
	{
		float t2 = Time.time;
		_delta = serverTime - t2 + (t2 - _firstTimestamp) / 2.0f;
		requestedSync = true;
		WaterHelper.Delta = _delta;
		UIConsole.Log ("Synchronized the time at " + Time.time);
	}

	//[Client]
	void OnGUI()
	{
		/*
		GUI.Box (new Rect (600, 64, 256, 64), "My time: " + Time.time);
		
		ShowSyncRequestButton ();
		
		if(requestedSync)
			GUI.Box (new Rect (600, 128, 256, 64), "My approx of server time: " + (Time.time + _delta));
			*/
	}

	[Client]
	void ShowSyncRequestButton()
	{
		if (!requestedSync && GUI.Button (new Rect (600, 192, 256, 64), "Start time sync"))
			StartSync ();
	}
	
	
}
