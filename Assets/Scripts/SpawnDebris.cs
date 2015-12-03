using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class SpawnDebris : NetworkBehaviour {

    [SerializeField]
    private List<GameObject> debris = new List<GameObject>();

    List<GameObject> debrisList = new List<GameObject>();

    [SerializeField]
    Transform sea;

    float existanceTime = 0;

    RaycastHit hit;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        existanceTime += Time.deltaTime;
        if (existanceTime >= 30f)
        {
            debrisList.Clear();
            existanceTime = 0f;
        }
        if (debrisList.Count <= 10f)
        {
            spawnDebris();
        }
    }

    [ServerCallback]
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

        int rndDebr = Random.Range(0, debris.Count);

        GameObject rndDebris = (GameObject)Instantiate(debris[rndDebr], rndPosWithinSea, Quaternion.identity);
        NetworkServer.Spawn(rndDebris);
        debrisList.Add(rndDebris);
    }
}
