using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class spawnWhale : MonoBehaviour {
    [SerializeField]
    Object whalePrefab;
	// Use this for initialization
	void Start () {
        GameObject Whale = (GameObject) Instantiate(whalePrefab, GameObject.Find("WhaleBox").transform.position  , transform.rotation );
        NetworkServer.Spawn(Whale);

    }

    // Update is called once per frame
    void Update () {
	
	}
}
