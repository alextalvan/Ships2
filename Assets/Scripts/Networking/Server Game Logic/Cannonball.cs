using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody),typeof(Collider),typeof(NetworkTransform))]
public class Cannonball : NetworkBehaviour {

	float birthDate;
	[SerializeField]
	float lifeTime = 10f;

	[SerializeField]
	float buoyancyDamage = 34f;

	[SerializeField]
	float impactShockArea = 5f;

	public CustomOnlinePlayer owner;

	// Use this for initialization
	void Start () 
	{
		birthDate = Time.time;
	}
	
	// Update 
	void FixedUpdate () 
	{
		if (Time.time > birthDate + lifeTime) 
		{
			Delete();
		}
	}

	[Server]
	void Delete()
	{
		NetworkServer.Destroy(this.gameObject);
	}

	void OnCollisionEnter(Collision info)
	{
		HandleCollision (info);
	}

	[Server]
	void HandleCollision(Collision info)
	{
		HullOnline hull = info.gameObject.GetComponent<HullOnline> ();
		if (hull != null) 
		{
			hull.Damage(owner.gameObject.GetComponent<ShipAttributesOnline>().damage);
			info.gameObject.GetComponent<BuoyancyScript>().ChangeBuoyancy(info.contacts[0].point, buoyancyDamage,impactShockArea);
		}

		if (info.gameObject.GetComponent<SailOnline> ()) 
		{
			//todo
		}

		Delete ();

	}
}
