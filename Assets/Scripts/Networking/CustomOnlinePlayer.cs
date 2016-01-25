using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

[NetworkSettings(channel = 0, sendInterval = 1e+6f)]
public class CustomOnlinePlayer : NetworkBehaviour
{

	//each map piece owned increases the radius at which the player can see the cure
    public static float distancePerMapPiece = 25f;

	//when the client is told it cannot see the cure, this is the position where it puts it
    static Vector3 hiddenLocation = new Vector3(100000, 100000, 100000);

    //[SyncVar]
    Vector3 cureLocation;

    //[SyncVar]
    bool canSeeCure = false;

    //[SyncVar]
    public bool cureisCarriedByAPlayer = false;

    //[SyncVar]
    public NetworkIdentity currentCureCarrier = null;

	//syncvars dropped in favor of manual info transmission, every 250ms. this is for network performance
	[SerializeField]
	float cureInfoSendRate = 0.25f;

    Transform clientCure;

    private int mapPieces = 1;

	//wrapper get/set is used to easily calculate when to RPC a specific particle effect on a player
	public int MapPieces {
		get {
			return mapPieces;
		}
		set {
			if(value > mapPieces)
				GetComponent<PlayerFX>().RpcEmitMapParticle(value);

			mapPieces = value;
		}
	}

	//internal game server logic
    public float cureCarryTimeLeft;

	//individual player info
    public Color IndividualColor = Color.white;
    public string IndividualName = "Player";

	//objects which will have their material use a tint of the individual color
    [SerializeField]
    List<Renderer> _objectsToColor = new List<Renderer>();

	//the nickname shown over the ship
	[SerializeField]
	TextMesh _overHeadText;

	//the arrow that shows the enarest enemy/the cure
    [SerializeField]
    GameObject arrow;

    OnlineSceneReferences onlineRef;

	//pool of colors used
	private static List<Color> _playerColors;

	public static void ResetColorList()
	{
		_playerColors = new List<Color> (10);
		_playerColors.Add(new Color (0.933f, 0.929f, 0.875f));//titanium hwhite
		_playerColors.Add(new Color (0.165f, 0.392f, 0.678f));//phthalo blue
		_playerColors.Add(new Color (0.000f, 0.000f, 0.000f));//midnight black
		_playerColors.Add(new Color (0.267f, 0.502f, 0.165f));//sap green
		_playerColors.Add(new Color (0.675f, 0.106f, 0.173f));//cadmium red
		_playerColors.Add(new Color (0.824f, 0.545f, 0.137f));//indian yellow
		_playerColors.Add(new Color (0.500f, 0.500f, 0.500f));//grey
		_playerColors.Add(new Color (0.000f, 0.878f, 0.878f));//cyan
		_playerColors.Add(new Color (1.000f, 0.529f, 0.220f));//orange
		_playerColors.Add(new Color (0.627f, 0.227f, 1.000f));//purple

	}

    // Use this for initialization
    void Start()
    {

        onlineRef = GameObject.Find("OnlineSceneReferences").GetComponent<OnlineSceneReferences>();
		onlineRef.allOnlinePlayers.Add(this);
        //GameObject.Find ("OnlineSceneReferences").GetComponent<OnlineSceneReferences> ()
        ClientSideSetup();
        //test
        ServersideSetup();  
    }

	IEnumerator ShowInitialMessage()
	{
		yield return new WaitForSeconds (5);
		GetComponent<PlayerCaptionController>().PushCaptionLocally("Obtain map pieces to increase cure detection radius",10f);
	}



    [ClientCallback]
    void ClientSideSetup()
    {
        clientCure = onlineRef.clientCure;

        if (isLocalPlayer) 
		{
			StartCoroutine (ShowInitialMessage());
			onlineRef.UpgradeScreen.SetTargetPlayer (this);
		}
    }

    [ServerCallback]
    void ServersideSetup()
    {
		cureCarryTimeLeft = GameManager.initialCureTime;

		SetupInfo ();

		SyncIndividualInfoForAll ();

		StartCoroutine (CRCalculateIfCanSeeCure ());
		
    }

	//server side setup for the player
	public void SetupInfo()
	{
		//color first
		int index = Random.Range (0, _playerColors.Count);
		Color c = _playerColors [index];
		_playerColors.RemoveAt (index);
		foreach (Renderer r in _objectsToColor)
		{
			r.material.color = c;
		}
		
		IndividualColor = c;

		//then nickname
		CustomNetManager net = GameObject.Find ("NetworkManager").GetComponent<CustomNetManager> ();
		int myIndex = NetworkServer.connections.IndexOf (this.connectionToClient);
		
		if (net.nicknames.ContainsKey (myIndex))
			this.IndividualName = net.nicknames [myIndex];

		_overHeadText.text = IndividualName;
		_overHeadText.color = IndividualColor;
	}

	//synchronizes name and color for every other player using rpcs
	public void SyncIndividualInfoForAll()
	{
		foreach (CustomOnlinePlayer p in onlineRef.allOnlinePlayers) 
		{
			p.RpcSetPlayerInfo(p.IndividualColor,p.IndividualName);
		}
	}

	[ClientRpc]
	public void RpcSetPlayerInfo(Color c, string name)
	{
		foreach (Renderer r in _objectsToColor)
		{
			r.material.color = c;
			r.material.SetVector("_SecColor",new Vector4(c.r,c.g,c.b,c.a));
		}
		
		IndividualColor = c;

		if (isLocalPlayer)
			return;
		IndividualName = name;
		_overHeadText.text = IndividualName;
		_overHeadText.color = IndividualColor;
	}

    // Update 
    void Update()
    {
        //CalculateIfCanSeeCure();
        SyncCureLocation();
        //SyncColor();
        UpdateArrow();
    }

    [ClientCallback]
    void SyncCureLocation()
    {
        if (cureisCarriedByAPlayer)
        {
            clientCure.transform.position = currentCureCarrier.transform.position + currentCureCarrier.transform.up * 20f;
            return;
        }

        if (canSeeCure)
            clientCure.transform.position = cureLocation;
        else
            clientCure.transform.position = hiddenLocation;
    }

	IEnumerator CRCalculateIfCanSeeCure()
	{
		while(true)
		{
			CalculateIfCanSeeCure();
			yield return new WaitForSeconds(cureInfoSendRate);
		}
	}

	//this calculates how the cure should be displayed(or not) for the current player and then updates him over the entwork
    //[ServerCallback]
    void CalculateIfCanSeeCure()
    {
		Debug.Log ("Calculating cure sight");

		if (cureisCarriedByAPlayer && currentCureCarrier != null) 
		{
			RpcSendCureHolder (currentCureCarrier);
			return;
		}

        float distance = (transform.position - onlineRef.serverCure.transform.position).magnitude;

        if (distance <= mapPieces * distancePerMapPiece)
        {
			RpcSendCureRawLocation(onlineRef.serverCure.transform.position);
            return;
        }

        if (onlineRef.gameManager.phase1Finished)
        {
			RpcSendCureRawLocation(onlineRef.serverCure.transform.position);
            return;
        }
		
		RpcHideCure ();
        return;
    }

	//various RPCs for game logic
    [ClientCallback]
    void UpdateArrow()
    {
        arrow.SetActive(false);

        if (currentCureCarrier == GetComponent<NetworkIdentity>() || !isLocalPlayer || GetComponent<PlayerRespawn>().IsDead)
            return;

        GameObject target = null;
        if (canSeeCure)
        {
            target = onlineRef.clientCure.gameObject;
            arrow.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.yellow;
            //arrow.SetActive(true);
            //arrow.transform.rotation = Quaternion.identity;
        }
        else
        {
            float mindist = float.MaxValue;
            CustomOnlinePlayer best = null;
            foreach (CustomOnlinePlayer p in onlineRef.allOnlinePlayers)
            {
                if (p.isLocalPlayer || p.GetComponent<ShipAttributesOnline>().IsDead)
                    continue;

                float dist = (p.transform.position - transform.position).magnitude;

                if (dist < mindist)
                {
                    best = p;
                    mindist = dist;
                }
            }

            if (best != null)
                target = best.gameObject;

            arrow.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
        }

        if (target == null)
            return;

        Vector3 targetVPpos = Camera.main.WorldToViewportPoint(target.transform.position);

        if (targetVPpos.x < 0f || targetVPpos.x > 1f || targetVPpos.y < 0f || targetVPpos.y > 1f)
        {
            arrow.SetActive(true);
            arrow.transform.localRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        }
    }

	[ClientRpc]
	void RpcSendCureRawLocation(Vector3 location)
	{
		cureLocation = location;
		canSeeCure = true;
		cureisCarriedByAPlayer = false;
		currentCureCarrier = null;
	}

	[ClientRpc]
	public void RpcSendCureHolder(NetworkIdentity holder)
	{
		currentCureCarrier = holder;
		cureisCarriedByAPlayer = true;
		canSeeCure = true;
	}

	[ClientRpc]
	void RpcHideCure()
	{
		currentCureCarrier = null;
		cureLocation = hiddenLocation;
		cureisCarriedByAPlayer = false;
		canSeeCure = false;
	}

    void OnDestroy()
    {
        OnDestroyedOnServer();//this wrapper is used because it is only called on the server
        onlineRef.allOnlinePlayers.Remove(this);
    }

	//extremely important, if a player that is carrying the cure dies, we must be sure it doesn't sink the cure with him
    [ServerCallback]
    void OnDestroyedOnServer()
    {
        if (onlineRef.serverCure != null && onlineRef.serverCure.GetComponent<GameManager>().cureCarrier == this)
            onlineRef.serverCure.DetachFromHolder();
    }


    public string ColorHexCode
    {
        get
        {
            string rgbString = System.Drawing.Color.FromArgb(255, (int)(Mathf.Clamp01(IndividualColor.r) * 255), (int)(Mathf.Clamp01(IndividualColor.g) * 255), (int)(Mathf.Clamp01(IndividualColor.b) * 255)).Name;
            //UIConsole.Log ("generated color string: " + rgbString);

            return "#" + rgbString.Remove(0, 2) + "ff";
        }
    }

    public string ColoredName
    {
        get
        {
            return "<color=" + ColorHexCode + ">" + IndividualName + "</color>";
        }
    }


}
