using UnityEngine;
using System.Collections;

public class PushOnButton : MonoBehaviour {


	public Vector3 force = new Vector3();
	bool sw = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (Input.GetKeyUp (KeyCode.Space)) {

			GetComponent<Rigidbody> ().velocity = force * ((sw) ? -1f : 1f);
			sw = !sw;
		}
	
	}
}
