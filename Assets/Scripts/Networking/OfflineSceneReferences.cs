using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;

public class OfflineSceneReferences : MonoBehaviour {

	public string tutorialScene;

	public void StartTutorial()
	{
		Application.LoadLevel (tutorialScene);
	}

	[SerializeField]
	List<AudioClip> _sounds = new List<AudioClip>();

	AudioSource _source;

	public enum MENU_SOUNDS
	{
		CLICK
	}

	void Start()
	{
		Cursor.visible = true;
		MusicManager.Singleton.Music.setParameterValue ("Menu", 1f);
		_source = GetComponent<AudioSource> ();
	}

	public void PlaySound(MENU_SOUNDS s)
	{
		_source.Stop ();
		_source.clip = _sounds [(int)s];
		_source.Play ();
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

	public void SetConnectAttemptText()
	{
		CustomNetManager net = GameObject.Find ("NetworkManager").GetComponent<CustomNetManager> ();
		connectAttemptText.text = "Attempting to connect to the server " + net.networkAddress + ":" + net.networkPort + "." +
								  "\nPress K+L to disconnect";
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

	public void SendNickname()
	{
		if (lobbyPlayer == null)
			return;

		lobbyPlayer.CmdSendNickname (nicknameInput.text);
	}

	[SerializeField]
	InputField nicknameInput;

	public GUIDialog autoConnectMessage;
	public GUIDialog autoRestartMessage;
	public GUIDialog serverRunningMessage;
	public GUIDialog connectAttempMessage;
	public Text connectAttemptText;
}
