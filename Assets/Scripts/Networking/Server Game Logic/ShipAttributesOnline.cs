using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(CustomOnlinePlayer))]
public class ShipAttributesOnline : NetworkBehaviour {


	//public float hullHealth;
	public float hullMaxHealth;
	public float sailHealth;
	public float sailMaxHealth;
	//public int crewCount;
	public float speed;

	//fighting
	public float damage;
	public float shootRangeMultiplier;
	public float cannonChargeRate;
	public float reloadSpeedModifier;
	public float healthRegen;

	//level




	// Use this for initialization
	void Start () 
	{
		Reset ();
	}

	[Server]
	public void Reset()
	{
		//hullMaxHealth = 100f;
		GetComponent<HullOnline> ().Reset ();
		sailHealth = sailMaxHealth = 100f;
		//crewCount = 20;
		speed = 7.5f;

		damage = 5f;
		shootRangeMultiplier = 1.0f;
		reloadSpeedModifier = 1.0f;
		cannonChargeRate = 5f;
		healthRegen = 0f;

	}
	
	// Update 
	void FixedUpdate () {
	
	}
}
