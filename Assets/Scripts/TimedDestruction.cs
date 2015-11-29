using UnityEngine;
using System.Collections;

public class TimedDestruction : MonoBehaviour {

	public float lifeTime;

	private float deathTime;

	// Use this for initialization
	void Start () 
	{
		deathTime = Time.time + lifeTime;
	}
	
	// Update 
	void Update () 
	{
		if (Time.time > deathTime)
			Destroy (this.gameObject);
	}
}
