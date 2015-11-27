using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {

    float phase1Time = 60f;

	//temp
    bool phase1inProgress = false;
	bool finishedGame = false;

    public CustomOnlinePlayer cureCarrier = null;

    public bool phase1Finished {  get { return !phase1inProgress; } }
    //float phase2Time = 600f;

	[SerializeField]
	GameObject chartPrefab;

	// Use this for initialization
	void Start () 
	{
		//test
		GameObject chart = Instantiate (chartPrefab);
		NetworkServer.Spawn (chart);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
	    if(phase1inProgress)
        {
            phase1Time -= Time.fixedDeltaTime;
            if (phase1Time <= 0.0f)
                phase1inProgress = false;
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


    void WinAction(CustomOnlinePlayer winner)
    {
		PlayerCaptionController.BroadcastCaption (winner.ColoredName + " has finished their timer with the cure and wins the game!",5f);
    }
}
