using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickupSpawnPointList : MonoBehaviour {

	private Transform[] _points;

	public Vector3 GetRandomPoint()
	{
		int index = Random.Range (0, _points.Length);
		return _points [index].position;
	}

	// Use this for initialization
	void Awake () 
	{
		_points = GetComponentsInChildren<Transform> ();
	}
	
	// Update 
	void FixedUpdate () {
	
	}
}
