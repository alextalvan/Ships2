using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

//@TODO get list of players, Implement actual spawns, map pieces
public class ConditionScript : NetworkBehaviour {
    static float winTime = 90;
    //static float cureSpawnTime = 600;

    TextMesh announcer;

    float gameTime = 0;

    [SyncVar]
    float cureTime = 0;
    float waitTime;
    float countdownTime = 0;

    bool hasCure;
    bool isDisplayed;

    int cDigit = 4;
	// Use this for initialization
	void Start ()
    {

        announcer = GetComponentInChildren<TextMesh>();
        announcer.fontSize = 100;
    }
	// Update is called once per frame
	void FixedUpdate () {
        gameTime += Time.fixedDeltaTime;
        FightCountDown();
        if (hasCure == true)
        {
            OwningCure();
        }
    }
    [ClientRpc]
    public void RpcAddCure() {
        //attatch cure to ship
        hasCure = true;
    }
    //Counts down from 3-1-fight and displays it
    void FightCountDown()
    {
        if (cDigit > 0)
        {
            //disable controls
            if (gameTime >= countdownTime)
            {
                cDigit--;
                if (cDigit == 0)
                {
                    announcer.text = "Fight!";
                    announcer.gameObject.SetActive(false);
                }
                else announcer.text = cDigit.ToString();
                countdownTime++;
            }
        }
    }
    void OwningCure() {
        cureTime += Time.fixedDeltaTime;

        if (cureTime >= winTime)
        {
            //a winner is you!
        }
    }
    //Spawn Cure At Random Location
    void SpawnCure() {
        //!REQUIRES MAP
        //fixed location?
        //if not how would the generation work
    }
    [ClientRpc]
    void RpcPresentCure() {
        //questions: what if some 1 finds it; does it present to everyone? can i pick it up before warmup round is done?
        //if time has passed or some 1 picks the cure up make it visible to everyone else??
    }
    void SpawnMapPieces() {
        //!REQUIRES MAP
        //request playercount
        //if smaller than 10: spawn 10 - playerCount;
    }
}

