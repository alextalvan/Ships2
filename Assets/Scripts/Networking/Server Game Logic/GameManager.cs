using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

    float phase1Time = 120f;

	//temp
	[SerializeField]
    bool phase1inProgress = true;
	bool finishedGame = false;

    public CustomOnlinePlayer cureCarrier = null;

    public bool phase1Finished {  get { return !phase1inProgress; } }
    //float phase2Time = 600f;

	[SerializeField]
	GameObject chartPrefab;

	[SerializeField]
	GameObject pickupRespawnPointsParent;

	[SerializeField]
	int initialScrollCount = 10;

	OnlineSceneReferences onlineRef;

	// Use this for initialization
	void Start () 
	{
		//test
		onlineRef = GameObject.Find ("OnlineSceneReferences").GetComponent<OnlineSceneReferences> ();

		if(NetworkServer.active)
			ArrangeArena ();
	}

	void Update()
	{
		DebugFunc ();
	}

	void DebugFunc()
	{
		if (Input.GetKeyDown (KeyCode.G))
			WinAction (onlineRef.allOnlinePlayers [0]);
	}

	// Update is called once per frame
	void FixedUpdate ()
    {
	    if(phase1inProgress)
        {
            phase1Time -= Time.fixedDeltaTime;
            if (phase1Time <= 0.0f)
			{
				PlayerCaptionController.BroadcastCaption("The cure may now be picked up", 5f);
                phase1inProgress = false;
			}
        }

        if(cureCarrier!=null)
        {
            cureCarrier.cureCarryTimeLeft -= Time.fixedDeltaTime;
            if (cureCarrier.cureCarryTimeLeft <= 0.0f && !finishedGame)
            {
                WinAction(cureCarrier);
				finishedGame = true;
            }

        }
	}
	

	[ServerCallback]
	void ArrangeArena()
	{

		List<Transform> _spawnPoints = new List<Transform> ();
		pickupRespawnPointsParent.GetComponentsInChildren<Transform> (_spawnPoints);

		//first put ourself (cure) to a random pickup node
		int index = Random.Range (1, _spawnPoints.Count);

		transform.position = _spawnPoints [index].position;
		_spawnPoints.RemoveAt (index);

		for (int i=0; i< initialScrollCount; ++i) 
		{
			index = Random.Range (1, _spawnPoints.Count);

			GameObject chart = (GameObject) Instantiate (chartPrefab,_spawnPoints[index].position,Quaternion.identity);
			NetworkServer.Spawn (chart);
			_spawnPoints.RemoveAt (index);
		}

	}

    void WinAction(CustomOnlinePlayer winner)
    {
		string name = winner.ColoredName;
		foreach (CustomOnlinePlayer p in onlineRef.allOnlinePlayers) 
		{
			p.GetComponent<ShipScript>().SetFreeze(true);
			p.GetComponent<PlayerCaptionController>().RpcPushGameEndDialog(name);
		}

		StartCoroutine (StopServer ());

		//PlayerCaptionController.BroadcastCaption (winner.ColoredName + " has finished their timer with the cure and wins the game!",5f);
    }

	IEnumerator StopServer()
	{
		yield return new WaitForSeconds (10);
		GameObject.Find ("NetworkManager").GetComponent<CustomNetManager> ().Disconnect ();
	}
}
