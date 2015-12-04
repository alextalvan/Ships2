using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

public class CustomOnlinePlayer : NetworkBehaviour
{

    public const float distancePerMapPiece = 25f;
    static Vector3 hiddenLocation = new Vector3(100000, 100000, 100000);

    [SyncVar]
    Vector3 cureLocation;

    [SyncVar]
    bool canSeeCure = false;


    [SyncVar]
    public bool cureisCarriedByAPlayer = false;

    [SyncVar]
    public Transform currentCureCarrier = null;

    Transform clientCure;

    private int mapPieces = 1;

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

    public float cureCarryTimeLeft = 60f;

    [SyncVar]
    public Color IndividualColor = Color.white;
    public string IndividualName = "Player";

    [SerializeField]
    List<Renderer> _objectsToColor = new List<Renderer>();

    [SerializeField]
    GameObject arrow;

    OnlineSceneReferences onlineRef;
    // Use this for initialization
    void Start()
    {

        onlineRef = GameObject.Find("OnlineSceneReferences").GetComponent<OnlineSceneReferences>();
        //GameObject.Find ("OnlineSceneReferences").GetComponent<OnlineSceneReferences> ()
        ClientSideSetup();
        //test
        ServersideSetup();

        onlineRef.allOnlinePlayers.Add(this);
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
        Color c = new Color(Mathf.Clamp01(Random.value + 0.25f), Mathf.Clamp01(Random.value + 0.25f), Mathf.Clamp01(Random.value + 0.25f));
        foreach (Renderer r in _objectsToColor)
        {
            r.material.color = c;
        }

        IndividualColor = c;

		CustomNetManager net = GameObject.Find ("NetworkManager").GetComponent<CustomNetManager> ();
		int myIndex = NetworkServer.connections.IndexOf (this.connectionToServer);

		if (net.nicknames.ContainsKey (myIndex))
			this.IndividualName = net.nicknames [myIndex];
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
    void FixedUpdate()
    {
        CalculateIfCanSeeCure();
        SyncCureLocation();
        SyncColor();
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

    [ServerCallback]
    void CalculateIfCanSeeCure()
    {
        float distance = (transform.position - onlineRef.serverCure.transform.position).magnitude;

        if (distance < mapPieces * distancePerMapPiece)
        {
            cureLocation = onlineRef.serverCure.transform.position;
            canSeeCure = true;
            return;
        }

        if (onlineRef.gameManager.phase1Finished)
        {
            cureLocation = onlineRef.serverCure.transform.position;
            canSeeCure = true;
            return;
        }

        cureLocation = hiddenLocation;
        canSeeCure = false;
        return;
    }

    [ClientCallback]
    void UpdateArrow()
    {
        arrow.SetActive(false);

        if (currentCureCarrier == this.transform || !isLocalPlayer || GetComponent<PlayerRespawn>().IsDead)
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

    void OnDestroy()
    {
        OnDestroyedOnServer();
        onlineRef.allOnlinePlayers.Remove(this);
    }

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
