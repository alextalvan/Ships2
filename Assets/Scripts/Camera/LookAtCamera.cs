using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update 
	void Update () 
	{
		transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
		                 Camera.main.transform.rotation * Vector3.up);
	}
}
