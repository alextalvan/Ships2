using UnityEngine;
using System.Collections;

//a simple lookat used by text meshes to correctly show up straight on the user's screen(for their nicknames etc)
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
