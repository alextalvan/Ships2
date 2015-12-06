using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;

public class SpawnDebris : NetworkBehaviour {

    [SerializeField]
    private List<GameObject> debris = new List<GameObject>();

	[SerializeField]
	float timeBetweenWaves = 35f;

	[SerializeField]
	int debrisPerWave = 20;

	[SerializeField]
	float depthOfSpawn = 10f;

    //List<GameObject> debrisList = new List<GameObject>();

    [SerializeField]
	PickupSpawnPointList helper;

   

    //RaycastHit hit;
	// Use this for initialization
	void Start () 
	{
		StartCoroutine (ContinuousSpawn ());
	}


	IEnumerator ContinuousSpawn()
	{
		for (;;) 
		{
			spawnDebris();
			yield return new WaitForSeconds(timeBetweenWaves);
		}
	}

    [ServerCallback]
    void spawnDebris() {
		for (int i=0; i<debrisPerWave; ++i) 
		{
			Vector3 rndPosWithinSea = helper.GetRandomPoint() - new Vector3(0,depthOfSpawn,0);

			int rndDebr = Random.Range (0, debris.Count);

			GameObject rndDebris = (GameObject)Instantiate (debris [rndDebr], rndPosWithinSea, Quaternion.identity);
			NetworkServer.Spawn (rndDebris);
		}
    }
}
