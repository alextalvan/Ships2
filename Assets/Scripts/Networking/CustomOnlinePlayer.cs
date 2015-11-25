using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

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

    int mapPieces = 1;

    public float cureCarryTimeLeft = 60f;

	public Color IndividualColor = Color.white;
	public string IndividualName = "Player";

    OnlineSceneReferences onlineRef;
	// Use this for initialization
	void Start () 
	{
        onlineRef = GameObject.Find("OnlineSceneReferences").GetComponent<OnlineSceneReferences>();
        clientCure = onlineRef.clientCure;

		//test
		ServersideSetup ();
	}

	[ServerCallback]
	void ServersideSetup()
	{
		Color c = new Color (Random.value + 0.25f, Random.value + 0.25f, Random.value + 0.25f);
		RpcSetColor(c);
		GetComponent<Renderer> ().material.color = c;
		IndividualColor = c;
	}

	[ClientRpc]
	public void RpcSetColor(Color c)
	{
		GetComponent<Renderer> ().material.color = c; 
	}

	// Update 
	void FixedUpdate () 
	{
		CalculateIfCanSeeCure ();
        SyncCureLocation();
	}

    [ClientCallback]
    void SyncCureLocation()
    {
		if (cureisCarriedByAPlayer) 
		{
			clientCure.transform.parent = currentCureCarrier;
			clientCure.transform.localPosition = new Vector3 (0, 7, 0);
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
