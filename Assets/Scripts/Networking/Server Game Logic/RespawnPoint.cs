using UnityEngine;
//using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class RespawnPoint : MonoBehaviour 
{

	int capturedObjectsCount = 0;

	public int CapturedObjectsCount {
		get {
			return capturedObjectsCount;
		}
	}

	// Use this for initialization
	void Awake () {
		GetComponent<Collider> ().isTrigger = true;
		GetComponent<Renderer> ().enabled = false;
	}
	
	void OnTriggerEnter(Collider other)
	{
		capturedObjectsCount++;
	}

	void OnTriggerExit(Collider other)
	{
		capturedObjectsCount--;
	}
}
