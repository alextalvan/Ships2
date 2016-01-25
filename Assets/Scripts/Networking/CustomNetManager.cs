using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CustomNetManager : NetworkLobbyManager {

	//flag whether to reset the list of respawn points next time the scene changes to the online one
	bool resetSpawnPointsOnSceneChange = false;

	[SerializeField]
	string addressField = "127.0.0.1";
	[SerializeField]
	int portField = 7777;
	public bool isHudEnabled;


	//important prefabs
	[SerializeField]
	GameObject onlinePlayerPrefab;

	[SerializeField]
	GameObject lobbyInfoPrefab;

	[SerializeField]
	LobbyPlayerInfo _lobbyInfo;
	
	public bool clientAutoReconnect = false;
	public bool serverAutoRestart = false;

	//nicknames stored by the server and passed to the online player prefabs
	public Dictionary<int,string> nicknames; 

	void SetAddress(string ip)
	{
		addressField = ip;
	}

	bool SetPort(string target)
	{
		int port;
		bool parseResult = int.TryParse(target,out port);
		if (parseResult && port>=0 && port <=65535) 
		{
			portField = port;
			return true;
		}
		return false;
	}

	//this is called by the menu HUD port input field
	public void SetPort()
	{

		InputField portInputField = GameObject.Find ("OfflineSceneReferences").GetComponent<OfflineSceneReferences> ().portInputField;

		int port;
		bool parseResult = int.TryParse(portInputField.text,out port);
		if (parseResult && port>=0 && port <=65535) 
		{
			portField = port;
		}
	}

	//this is called by the menu HUD IP input field
	public void SetSocketInfo()
	{
		InputField socketInputField = GameObject.Find("OfflineSceneReferences").GetComponent<OfflineSceneReferences>().socketInputField;


		string ip_port = socketInputField.text;

		string[] bits = ip_port.Split (':');
		if (bits.Length != 2)
			return;

		if(SetPort(bits[1]))
		   SetAddress(bits[0]);

	}

	public void StartClientAttempt()
	{
		networkAddress = addressField;
		networkPort = portField;
		UIConsole.Log("Attempting to connect to the server " + addressField + ":" + portField);
		StartClient();
		clientAutoReconnect = false;
		serverAutoRestart = false;
		GameObject.Find ("OfflineSceneReferences").GetComponent<OfflineSceneReferences> ().autoConnectMessage.Disable ();
		//isClient = true;
	}

	public void StartServerAttempt()
	{
		networkAddress = addressField;
		networkPort = portField;
		UIConsole.Log("Starting server with port: "  + portField);
		resetSpawnPointsOnSceneChange = true;
		serverAutoRestart = true;
		nicknames = new Dictionary<int, string>();
		CustomOnlinePlayer.ResetColorList ();
		StartServer ();
		//isClient = false;
	}

	//disable the "auto restart mesage" dialog
	public override void OnLobbyStartServer ()
	{
		GameObject.Find ("OfflineSceneReferences").GetComponent<OfflineSceneReferences> ().autoRestartMessage.Disable ();
	}



	public void Disconnect()
	{
		UIConsole.Log ("Disconnecting...");
		StopHost ();
		resetSpawnPointsOnSceneChange = true;
	}

	//callback for a new client connecting to the server in lobby state
	//this is executed on the client, it sends a request to the server to instantiate a lobby player object for it
	public override void OnLobbyClientConnect (NetworkConnection conn)
	{
		//TryToAddPlayer ();
		ClientScene.Ready (conn);
		ClientScene.AddPlayer (0);

	}
	
	//callback for a new client changing scene to the online one
	//this is executed on the client, it sends a request to the server to swap the lobby player object with an online one (the ship)
	public override void OnClientSceneChanged (NetworkConnection conn)
	{
		//UIConsole.Log ("OnClientSceneChanged " + Application.loadedLevelName);
		ClientScene.RemovePlayer(0);
		ClientScene.Ready(conn);
		//ClientScene.AddPlayer(0);

		//delayed respawn to ensure that over LAN networks, the message does not arrive before the scene change finishes
		//(internally, the network manager changes networked scenes asynchronously)
		StartCoroutine (SpawnPlayerWithDelay (6f));

	}

	public override void OnServerSceneChanged (string sceneName)
	{
		//base.OnServerSceneChanged (sceneName);
	}

	//the message sent from client to the server to spawn its player object results in this callback on the server
	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId)
	{
		//remove previous player objects for the connection, if they exist
		NetworkServer.DestroyPlayersForConnection (conn);
	
		if (CheckIfLobbyScene ()) 
		{
			base.OnServerAddPlayer (conn, playerControllerId);
			if(_lobbyInfo!=null)
				NetworkServer.Destroy(_lobbyInfo.gameObject);

			_lobbyInfo = ((GameObject)Instantiate(lobbyInfoPrefab)).GetComponent<LobbyPlayerInfo>();
			NetworkServer.Spawn(_lobbyInfo.gameObject);
		}
		else 
		{
			int num = 0;
			foreach (PlayerController current in conn.playerControllers)
			{
				if (current.IsValid)
				{
					num++;
				}
			}
			if (num > this.maxPlayersPerConnection)
			{
				UnityEngine.Networking.NetworkSystem.EmptyMessage msg = new UnityEngine.Networking.NetworkSystem.EmptyMessage ();
				conn.Send (45, msg);
				return;
			}


			if(resetSpawnPointsOnSceneChange)
			{
				GameObject.Find("RespawnManager").GetComponent<RespawnPointsManager>().SetupInitialSpawnPoints();
				resetSpawnPointsOnSceneChange = false;
			}

			GameObject spawnPos = GameObject.Find("RespawnManager").GetComponent<RespawnPointsManager>().GetInitialSpawnPoint();
			//UIConsole.Log());
			GameObject player = (GameObject)Object.Instantiate(onlinePlayerPrefab,spawnPos.transform.position,spawnPos.transform.rotation);
			NetworkServer.AddPlayerForConnection(conn,player,playerControllerId);
				//UIConsole.Log("FAILED TO ADD PLAYER");

		}
	}
	

	public override void OnServerRemovePlayer (NetworkConnection conn, PlayerController player)
	{
		//UIConsole.Log ("OnServerRemovePlayer" + Application.loadedLevelName);
		base.OnServerRemovePlayer (conn, player);
	}


	//callback executed on the server when it starts
	//dialog disabling and activation of NAT punchthrough (via NAT device detection and UPNP)
	public override void OnStartServer ()
	{
		base.OnStartServer ();

		PortMapper pmapper = GetComponent<PortMapper> ();

		if (pmapper != null && pmapper.enabled) 
		{
			pmapper.Init();
		}

		//UI
		GameObject.Find ("OfflineSceneReferences").GetComponent<OfflineSceneReferences> ().firstMenuPage.Disable ();
		GameObject.Find ("OfflineSceneReferences").GetComponent<OfflineSceneReferences> ().serverRunningMessage.Enable ();


	}

	//callback executed on the server when it stops for any reason
	public override void OnStopServer ()
	{

		Debug.Log ("Stopping Server...");

		//clean up ports
		PortMapper pmapper = GetComponent<PortMapper> ();
		
		if (pmapper != null && pmapper.enabled) 
		{
			pmapper.Stop();
		}

		//clean up lobby players
		lobbySlots = new NetworkLobbyPlayer[maxPlayers];



	}


	public override void OnStartClient (NetworkClient lobbyClient)
	{
		base.OnStartClient (lobbyClient);
		GameObject.Find ("OfflineSceneReferences").GetComponent<OfflineSceneReferences> ().firstMenuPage.Disable ();
	}



	//disconnect behaviour
	public override void OnClientDisconnect (NetworkConnection conn)
	{
		base.OnClientDisconnect (conn);
		UIConsole.Log ("Client has been disconnected.");

		//string lobbyScene = this.lobbyScene;
		//StopHost ();
		//startedServer = false;
		//Application.LoadLevel(lobbyScene);
		return;


	}
	
	void ShowLobbyInterface()
	{
		OfflineSceneReferences refs = GameObject.Find ("OfflineSceneReferences").GetComponent<OfflineSceneReferences> ();
		refs.lobbyMenuPage.Enable ();
		refs.connectAttempMessage.Disable ();
	}
	

	public bool CheckIfLobbyScene()
	{
		//return Application.loadedLevel == 0;
		return Application.loadedLevelName == this.lobbyScene;
	}

	public bool CheckIfOnlineScene()
	{
		//return Application.loadedLevel == 1;
		return Application.loadedLevelName == this.onlineScene;
	}

	public override void OnServerReady (NetworkConnection conn)
	{
		//test
	}



	void Update()
	{
		//if (_lobbyInfo == null)
		//	_lobbyInfo = GameObject.Find ("LobbyInfo").GetComponent<LobbyPlayerInfo> ();

		//lobby information handling
		if(_lobbyInfo!=null && CheckIfLobbyScene() && !this.IsClientConnected() && this.isNetworkActive)
		{
			NetworkLobbyPlayer[] players = this.lobbySlots;
			_lobbyInfo.currentPlayerCount = numPlayers;
			_lobbyInfo.currentReadyCount = 0;

			for(int i=0; i< players.Length;++i)
			{
				if(players[i]!=null && players[i].readyToBegin)
					_lobbyInfo.currentReadyCount++;
			}

			_lobbyInfo.minToStartCount = this.minPlayers;

		}

		//forced disconnect by pressing K+L
		//intended to be used by developers only
		if (Input.GetKey (KeyCode.K) && Input.GetKeyDown (KeyCode.L)) 
		{
			Disconnect();
			serverAutoRestart = false;
			clientAutoReconnect = false;

			if(CheckIfLobbyScene())
			{
				OfflineSceneReferences refs = GameObject.Find ("OfflineSceneReferences").GetComponent<OfflineSceneReferences> ();
				refs.lobbyMenuPage.Disable ();
				refs.firstMenuPage.Enable();
				refs.autoConnectMessage.Disable();
				refs.autoRestartMessage.Disable();

			}
		}

	}

	//callback on a client when it connects to the lobby
	public override void OnLobbyClientEnter ()
	{
		ShowLobbyInterface ();
	}

	IEnumerator SpawnPlayerWithDelay(float delay)
	{
		if (delay < 2f)
			delay = 2f;
		yield return new WaitForSeconds(delay);
		ClientScene.AddPlayer (0);
	}
	
}
