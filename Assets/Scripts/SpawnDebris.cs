using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class SpawnDebris : MonoBehaviour {

    List<GameObject> debrisList = new List<GameObject>();

    [SerializeField]
    Transform sea;

    [SerializeField]
    GameObject pickupPrefab;

    GameObject debris;

    float existanceTime = 0;

    RaycastHit hit;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        existanceTime += Time.fixedDeltaTime;
        if (existanceTime >= 30)
        {
            debrisList.Clear();
            existanceTime = 0;
        }
        if (debrisList.Count <= 10)
        {
            spawnDebris();
        }
    }
    void spawnDebris() {
        Vector3 rndPosWithinSea;
        rndPosWithinSea = new Vector3(Random.Range(-450f, 350f), Random.Range(-1, 1f), Random.Range(0f, 800f));
        //rndPosWithinSea = sea.TransformPoint(rndPosWithinSea * 0.5f);
        if (Physics.Raycast(transform.position, rndPosWithinSea, out hit, (rndPosWithinSea - transform.position).magnitude))
        {
            print("Bad Debris at: " + rndPosWithinSea);
            if (hit.transform != transform)
            {
                return;
               
            }
        }
        debris = (GameObject)Instantiate(pickupPrefab, rndPosWithinSea, Quaternion.identity);
        NetworkServer.Spawn(debris);
        debrisList.Add(debris);
        

    }
}
