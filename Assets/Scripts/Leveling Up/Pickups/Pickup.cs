using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(OnlineTransform))]
[RequireComponent(typeof(Collider))]
public class Pickup : NetworkBehaviour {

	public int EXP_Reward;
	public float lifeTime;
	private float deathTime;
	private bool taken = false;

	// Use this for initialization
	void Start () 
	{
		GetComponent<Collider> ().isTrigger = true;
		deathTime = Time.time + lifeTime;
	}

	[ServerCallback]
	void CheckDelete()
	{
		if (Time.time > deathTime)
			NetworkServer.Destroy (this.gameObject);
	}
	
	// Update 
	void FixedUpdate () {
	
	}

	void Update()
	{
		CheckDelete ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (taken)
			return;

		PlayerPickupHitbox hitbox = other.GetComponent<PlayerPickupHitbox> ();

		if (hitbox != null && !hitbox.GetComponentInParent<ShipAttributesOnline>().IsDead) 
		{
			OnPickup(hitbox.GetComponentInParent<CustomOnlinePlayer>());
			taken = true;
			NetworkServer.Destroy (this.gameObject);
		}
	}

	protected virtual void OnPickup(CustomOnlinePlayer player)
	{
		player.GetComponent<LevelUser>().GainEXP(EXP_Reward);
	}


}
