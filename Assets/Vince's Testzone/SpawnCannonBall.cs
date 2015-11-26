using UnityEngine;
using System.Collections;

public class SpawnCannonBall : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    GameObject cannonBallPrefab;
    GameObject cannonBall;
	void Start () { 
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cannonBall = Instantiate(cannonBallPrefab,transform.position,transform.rotation) as GameObject;
        }
	}
}
