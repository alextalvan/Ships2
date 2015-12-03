using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Collider))]
public class TutorialPickup : MonoBehaviour
{
	public int EXP_Reward;
	public float lifeTime;
	private float deathTime;
	private bool taken = false;
	[SerializeField]
	private float repairPotential;
	
	public CustomOnlinePlayer owner;
	
	// Use this for initialization
	void Start()
	{
		GetComponent<Collider>().isTrigger = true;
		deathTime = Time.time + lifeTime;
	}
	
	//[ServerCallback]
	void CheckDelete()
	{
		if (Time.time > deathTime)
			Destroy(this.gameObject);
	}
	
	// Update 
	void FixedUpdate()
	{
		
	}
	
	void Update()
	{
		CheckDelete();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (taken)
			return;
		
		PlayerPickupHitbox hitbox = other.GetComponent<PlayerPickupHitbox>();
		
		if (hitbox != null && !hitbox.GetComponentInParent<TutorialShipAttributes>().IsDead && other.GetComponentInParent<CustomOnlinePlayer>() != owner)
		{
			float rnd = Random.Range(0, 3);
			
			if (rnd == 0 || rnd == 1)
				other.GetComponentInParent<TutorialHull>().Repair(repairPotential);
			else
				other.GetComponentInParent<TutorialShipAttributes>().RepairAllSails(repairPotential);
			
			OnPickup(hitbox.GetComponentInParent<CustomOnlinePlayer>());
			taken = true;
			Destroy(this.gameObject);
		}
	}
	
	protected virtual void OnPickup(CustomOnlinePlayer player)
	{
		player.GetComponent<TutorialLevelUser>().GainEXP(EXP_Reward);
		player.GetComponent<PlayerFX>().PlaySound(PlayerFX.PLAYER_SOUNDS.PICKUP,false);
	}
}
