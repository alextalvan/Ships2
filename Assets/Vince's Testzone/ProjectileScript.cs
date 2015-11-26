using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

    // Use this for initialization
    Rigidbody myBody;
    Rigidbody debrisBody;
    float destroyTime;
    bool die = false;
    void Start () {
        myBody = GetComponent<Rigidbody>();
        myBody.AddForce(transform.position + transform.forward * 100);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (die == true)
        {
            destroyTime += Time.fixedDeltaTime;

            if (destroyTime >= 0.1f)
            {
                Destroy(this.gameObject);
                die = false;
            }
        }
        
    }
    void OnTriggerEnter(Collider col) {
        if (col.name != "Small")
        {
            die = true;
            col.gameObject.AddComponent<DebrisScript>();
        }
        
        
    }
}
