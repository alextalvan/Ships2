using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class OfflineSceneReferences : MonoBehaviour {

	void Start()
	{
		Cursor.visible = true;
	}


	//used by the networkManager after changing scene to offline scene to find objects
	public GUIDialog firstMenuPage;

	public GUIDialog lobbyMenuPage;

	public InputField socketInputField;
	
	public InputField portInputField;

	public Text playersReadyCountText;
	//public LobbyPlayerInfo lobbyPlayerInfo;

	public void StartClientAttempt()
	{
		GameObject.Find ("NetworkManager").GetComponent<CustomNetManager> ().StartClientAttempt ();
	}

	public void StartServerAttempt()
	{
		GameObject.Find ("NetworkManager").GetComponent<CustomNetManager> ().StartServerAttempt ();
	}



	//find lobbyplayer in all of the instances
	bool foundMyPlayer = false;

	private CustomLobbyPlayer _lobbyPlayer = null;

	public CustomLobbyPlayer lobbyPlayer 
	{
		get 
		{
			if(!foundMyPlayer)
			{
				CustomLobbyPlayer[] arr = GameObject.FindObjectsOfType<CustomLobbyPlayer> ();
				foreach (CustomLobbyPlayer obj in arr) 
				{
					if(obj.isLocalPlayer)
					{
						foundMyPlayer = true;
						_lobbyPlayer = obj;
						return _lobbyPlayer;
					}
				}
			}

			if(!foundMyPlayer)
				return null;
			else
				return _lobbyPlayer;
		}
	}
}
