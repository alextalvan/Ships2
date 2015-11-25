using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerRespawn : NetworkBehaviour
{

    public float respawnTime = 10f;

    [SyncVar]
    bool initiatedRespawn = false;
    float spawnTime = 0f;

    OnlineSceneReferences onlineRef;

    public bool IsDead { get { return initiatedRespawn; } }

    void Start()
    {
        onlineRef = GameObject.Find("OnlineSceneReferences").GetComponent<OnlineSceneReferences>();
    }

    [ServerCallback]
    public void StartRespawn()
    {
        GetComponent<PlayerCaptionController>().RpcPushDebugText("I died");
        GetComponent<OnlinePlayerInput>().enabled = false;
        GetComponent<BuoyancyScript>().enabled = false;
        GetComponent<ShipScript>().RpcChangeCameraState(false);
        //GetComponent<Rigidbody>().isKinematic = true;

        if (GetComponent<CustomOnlinePlayer>().currentCureCarrier == this.transform)
            onlineRef.serverCure.DetachFromHolder();

        spawnTime = Time.time + respawnTime;
        initiatedRespawn = true;
    }

    void Update()
    {
        CheckRespawn();
    }

    [ServerCallback]
    void CheckRespawn()
    {
        if (Time.time > spawnTime && initiatedRespawn)
        {
            Respawn();
            initiatedRespawn = false;
        }
    }

    [ServerCallback]
    void Respawn()
    {
        GetComponent<PlayerCaptionController>().RpcPushDebugText("I respawned");
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<OnlinePlayerInput>().Reset();
        GetComponent<OnlinePlayerInput>().enabled = true;
        GetComponent<BuoyancyScript>().Reset();
        GetComponent<BuoyancyScript>().enabled = true;
        GetComponent<ShipScript>().RpcChangeCameraState(true);
        GetComponent<HullOnline>().Reset();


        Vector3 spawnPos = GameObject.Find("RespawnManager").GetComponent<RespawnPointsManager>().GetRespawnPoint();

        transform.position = spawnPos;
        GetComponent<Rigidbody>().position = spawnPos;
        GetComponent<Rigidbody>().rotation = Quaternion.identity;
        transform.rotation = Quaternion.identity;


        //GetComponent<OnlineTransform> ().RpcForceNewTransform (spawnPos,Quaternion.identity);
    }


    //[ClientRpc]



}
