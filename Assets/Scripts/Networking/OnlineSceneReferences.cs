using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnlineSceneReferences : MonoBehaviour {


	void Start()
	{
		Cursor.visible = false;
	}

    //find lobbyplayer in all of the instances
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

    public Transform clientCure;

    public CureScript serverCure;

    public GameManager gameManager { get { return serverCure.gameObject.GetComponent<GameManager>(); } }

	public Transform cameraRef;

	public UICaption middleCaption;

	public Slider interpSlider;
	public void ChangeInterp()
	{
		CustomOnlinePlayer[] list = allOnlinePlayers;
		foreach (CustomOnlinePlayer p in list) 
		{
			p.GetComponent<OnlineTransform>().SetInterp(interpSlider.value);
		}

		UIConsole.Log ("Changed interpolation to " + interpSlider.value);
	}

	public ClientUpgradeScreen UpgradeScreen;

}
