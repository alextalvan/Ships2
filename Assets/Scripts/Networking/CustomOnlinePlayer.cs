using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class CustomOnlinePlayer : NetworkBehaviour {

    static float distancePerMapPiece = 50f;
    static Vector3 hiddenLocation = new Vector3(10000, 10000, 10000);

    [SyncVar]
    Vector3 cureLocation;

    [SyncVar]
    bool canSeeCure = false;


	[SyncVar]
	public bool cureisCarriedByAPlayer = false;

	[SyncVar]
	public Transform currentCureCarrier = null;

    Transform clientCure;

    public int mapPieces = 1;

    public float cureCarryTimeLeft = 60f;

	[SyncVar]
	public Color IndividualColor = Color.white;
	public string IndividualName = "Player";

	[SerializeField]
	List<Renderer> _objectsToColor = new List<Renderer>();

    OnlineSceneReferences onlineRef;
	// Use this for initialization
	void Start () 
	{
        
		onlineRef = GameObject.Find("OnlineSceneReferences").GetComponent<OnlineSceneReferences>();
		//GameObject.Find ("OnlineSceneReferences").GetComponent<OnlineSceneReferences> ()
		ClientSideSetup ();
		//test
		ServersideSetup ();
	}

	[ClientCallback]
	void ClientSideSetup()
	{

		clientCure = onlineRef.clientCure;

		if (isLocalPlayer)
			onlineRef.UpgradeScreen.SetTargetPlayer (this);
	}

	[ServerCallback]
	void ServersideSetup()
	{
		Color c = new Color ( Mathf.Clamp01(Random.value + 0.25f), Mathf.Clamp01(Random.value + 0.25f), Mathf.Clamp01(Random.value + 0.25f));
		//RpcSetColor(c);
		//GetComponent<Renderer> ().material.color = c;
		foreach (Renderer r in _objectsToColor) 
		{
			r.material.color = c;
		}

		IndividualColor = c;
	}

	[ClientCallback]
	public void SyncColor()
	{
		//GetComponent<Renderer> ().material.color = IndividualColor; 
		foreach (Renderer r in _objectsToColor) 
		{
			r.material.color = IndividualColor;
		}
	}

	// Update 
	void FixedUpdate () 
	{
		CalculateIfCanSeeCure ();
        SyncCureLocation();
		SyncColor ();
	}

    [ClientCallback]
    void SyncCureLocation()
    {
		if (cureisCarriedByAPlayer) 
		{
			//clientCure.transform.parent = currentCureCarrier;
			//clientCure.transform.localPosition = new Vector3 (0, 7, 0);
			clientCure.transform.position = currentCureCarrier.transform.position + currentCureCarrier.transform.up * 18f;
			return;
		} 
		else
			clientCure.transform.parent = null;


        if (canSeeCure)
            clientCure.transform.position = cureLocation;
        else
            clientCure.transform.position = hiddenLocation;
    }

    [ServerCallback]
    void CalculateIfCanSeeCure()
    {
        float distance = (transform.position - onlineRef.serverCure.transform.position).magnitude;

        if(distance < mapPieces * distancePerMapPiece)
        {
            cureLocation = onlineRef.serverCure.transform.position;
            canSeeCure = true;
            return;
        }

        if(onlineRef.gameManager.phase1Finished)
        {
            cureLocation = onlineRef.serverCure.transform.position;
            canSeeCure = true;
            return;
        }

        cureLocation = hiddenLocation;
        canSeeCure = false;
        return;


    }

	void OnDestroy()
	{
		OnDestroyedOnServer ();
	}

	[ServerCallback]
	void OnDestroyedOnServer()
	{
		if (onlineRef.serverCure.GetComponent<GameManager> ().cureCarrier == this)
			onlineRef.serverCure.DetachFromHolder ();
	}


	public string ColorHexCode
	{
		get 
		{ 
			string rgbString = System.Drawing.Color.FromArgb (255, (int)(Mathf.Clamp01(IndividualColor.r) * 255), (int)(Mathf.Clamp01(IndividualColor.g) * 255), (int)(Mathf.Clamp01(IndividualColor.b) * 255)).Name;
			//UIConsole.Log ("generated color string: " + rgbString);
			
			return "#" + rgbString.Remove (0, 2) + "ff";
		}
	}

	public string ColoredName
	{
		get 
		{
			return "<color=" + ColorHexCode + ">" + IndividualName +"</color>";
		}
	}


}
