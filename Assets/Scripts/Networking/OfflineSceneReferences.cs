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
	

	public List<string> sounds = new List<string> ();

	public enum MENU_SOUNDS
	{
		CLICK
	}

	void Start()
	{
		Cursor.visible = true;
		MusicManager.Singleton.Music.setParameterValue ("Menu", 1f);
	}

	public void PlaySound(MENU_SOUNDS s)
	{
		FMOD_StudioSystem.instance.PlayOneShot (sounds [(int)s],Camera.main.transform.position);
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
}
