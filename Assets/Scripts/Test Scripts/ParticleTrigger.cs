using UnityEngine;
using System.Collections;

public class ParticleTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	
	void Update () {

		if (Input.GetKeyDown (KeyCode.G))
			GetComponent<ParticleSystem> ().Play ();
	
	}


}
