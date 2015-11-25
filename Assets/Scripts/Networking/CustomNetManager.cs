using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CustomNetManager : NetworkLobbyManager {

	//bool isClient = true;
	//bool startedServer = false;
	bool resetSpawnPointsOnSceneChange = false;

	[SerializeField]
	string addressField = "192.168.0.100";
	[SerializeField]
	int portField = 7777;
	public bool isHudEnabled;
	
	[SerializeField]
	GameObject onlinePlayerPrefab;

	[SerializeField]
	GameObject lobbyInfoPrefab;

	[SerializeField]
	LobbyPlayerInfo _lobbyInfo;

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
		//isClient = true;
	}

	public void StartServerAttempt()
	{
		networkAddress = addressField;
		networkPort = portField;
		UIConsole.Log("Starting server with port: "  + portField);
		StartServer ();
		resetSpawnPointsOnSceneChange = true;
		//isClient = false;
	}

	public void Disconnect()
	{
		UIConsole.Log ("Disconnecting...");
		StopHost ();
		resetSpawnPointsOnSceneChange = true;
	}


	public override void OnLobbyClientConnect (NetworkConnection conn)
	{
		//TryToAddPlayer ();
		ClientScene.Ready (conn);
		ClientScene.AddPlayer (0);

	}
	

	public override void OnClientSceneChanged (NetworkConnection conn)
	{
		//UIConsole.Log ("OnClientSceneChanged " + Application.loadedLevelName);


		ClientScene.RemovePlayer(0);
		ClientScene.Ready(conn);
		//ClientScene.AddPlayer(0);

		StartCoroutine (SpawnPlayerWithDelay (6f));

	}
	

	public override void OnServerSceneChanged (string sceneName)
	{
		//base.OnServerSceneChanged (sceneName);
	}


	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId)
	{
		//UIConsole.Log ("OnServerAddPlayer " + Application.loadedLevelName);
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

			Vector3 spawnPos = GameObject.Find("RespawnManager").GetComponent<RespawnPointsManager>().GetInitialSpawnPoint();
			//UIConsole.Log());
			GameObject player = (GameObject)Object.Instantiate(onlinePlayerPrefab,spawnPos,Quaternion.identity);
			NetworkServer.AddPlayerForConnection(conn,player,playerControllerId);
				//UIConsole.Log("FAILED TO ADD PLAYER");

		}
	}
	

	public override void OnServerRemovePlayer (NetworkConnection conn, PlayerController player)
	{
		//UIConsole.Log ("OnServerRemovePlayer" + Application.loadedLevelName);
		base.OnServerRemovePlayer (conn, player);
	}

	public override void OnStartServer ()
	{
		base.OnStartServer ();

		PortMapper pmapper = GetComponent<PortMapper> ();

		if (pmapper != null) 
		{
			pmapper.Init();
		}

		//UI
		GameObject.Find ("OfflineSceneReferences").GetComponent<OfflineSceneReferences> ().firstMenuPage.Disable ();
	}

	public override void OnStopServer ()
	{

		Debug.Log ("Stopping Server...");

		//clean up ports
		PortMapper pmapper = GetComponent<PortMapper> ();
		
		if (pmapper != null) 
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

		string lobbyScene = this.lobbyScene;
		StopHost ();
		//startedServer = false;
		Application.LoadLevel(lobbyScene);
		return;


	}
	
	void ShowLobbyInterface()
	{
		GameObject.Find ("OfflineSceneReferences").GetComponent<OfflineSceneReferences> ().lobbyMenuPage.Enable ();
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



	//lobby information handling

	void Update()
	{
		//if (_lobbyInfo == null)
		//	_lobbyInfo = GameObject.Find ("LobbyInfo").GetComponent<LobbyPlayerInfo> ();

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


		if (Input.GetKey (KeyCode.K) && Input.GetKeyDown (KeyCode.L)) 
		{
			Disconnect();
		}

	}


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
