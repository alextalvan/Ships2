using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;

public class OfflineSceneReferences : MonoBehaviour {

	AudioSource _soundEmitter;

	public List<AudioClip> sounds = new List<AudioClip> ();

	public enum MENU_SOUNDS
	{
		CLICK
	}

	void Start()
	{
		Cursor.visible = true;
		_soundEmitter = GetComponent<AudioSource> ();
	}

	public void PlaySound(MENU_SOUNDS s)
	{
		_soundEmitter.PlayOneShot(sounds[(int)s]);
	}

	public void PlayClick()
	{
		PlaySound (MENU_SOUNDS.CLICK);
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
