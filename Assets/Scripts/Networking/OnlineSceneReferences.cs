using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//this is a central delivery of references for all "static" objects in the online scene
//instantiated objects must use this in order to have correct references
public class OnlineSceneReferences : MonoBehaviour {

	public GameObject harbor;

	public GameObject whale;

	public FMOD.Studio.EventInstance ambience;

	//bool reconnectChuckMarkState
	public void ChangeClientReconnectSetting()
	{
		CustomNetManager net = GameObject.Find ("NetworkManager").GetComponent<CustomNetManager> ();
		net.clientAutoReconnect = !net.clientAutoReconnect;
	}

	public void Disconnect()
	{
		CustomNetManager net = GameObject.Find ("NetworkManager").GetComponent<CustomNetManager> ();
		net.Disconnect ();
	}


	void OnDestroy()
	{
		ambience.stop (FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		ambience.release ();
	}

	void Start()
	{
		ambience = FMOD_StudioSystem.instance.GetEvent ("event:/Ambient");
		ambience.setVolume (0.25f);
		ambience.start();
		MusicManager.Singleton.Music.setParameterValue ("Menu", 0f);
		//temp disabled cause annoying
		//Cursor.visible = false;
	}

    //find player in all of the instances
    bool foundMyPlayer = false;

    private CustomOnlinePlayer _onlinePlayer = null;

    public CustomOnlinePlayer onlinePlayer
    {
        get
        {
            if (!foundMyPlayer)
            {
                CustomOnlinePlayer[] arr = GameObject.FindObjectsOfType<CustomOnlinePlayer>();
                foreach (CustomOnlinePlayer obj in arr)
                {
                    if (obj.isLocalPlayer)
                    {
                        foundMyPlayer = true;
                        _onlinePlayer = obj;
                        return _onlinePlayer;
                    }
                }
            }

            if (!foundMyPlayer)
                return null;
            else
                return _onlinePlayer;
        }
    }
	/*
    private CustomOnlinePlayer[] _allOnline = null;

    public CustomOnlinePlayer[] allOnlinePlayers
    {
        get
        {
            if (_allOnline == null)
            {
                _allOnline = GameObject.FindObjectsOfType<CustomOnlinePlayer>();
            }
            return _allOnline;
        }
    }
    */

	public List<CustomOnlinePlayer> allOnlinePlayers = new List<CustomOnlinePlayer> ();

    public Transform clientCure;

    public CureScript serverCure;

    public GameManager gameManager { get { return serverCure.gameObject.GetComponent<GameManager>(); } }

	public Transform cameraRef;

	public UICaption middleCaption;

	public Slider interpSlider;
	public void ChangeInterp()
	{
		List<CustomOnlinePlayer> list = allOnlinePlayers;
		foreach (CustomOnlinePlayer p in list) 
		{
			p.GetComponent<OnlineTransform>().SetInterp(interpSlider.value);
		}

		UIConsole.Log ("Changed interpolation to " + interpSlider.value);
	}

	public ClientUpgradeScreen UpgradeScreen;
	
	public List<GameObject> AmmoIcons = new List<GameObject>();

	//placeholder
	public Text SailSpeedText;

	public Text BarrelCd;

	public GUIDialog GameEndMessage;
	public Text GameEndText;
}
