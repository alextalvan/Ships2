using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Collider))]
public class TutorialHull : MonoBehaviour
{
	private TutorialBuoyancy buoyancy;
	[SerializeField]
	private TutorialShipAttributes shipAttributes;
	[SerializeField]
	private Renderer hpRend;

	[SerializeField]
	private float currentHealth;
	
	public TutorialBuoyancy SetBuoyancy
	{
		set { buoyancy = value; }
	}
	
	public float CurrentHealth
	{
		get { return currentHealth; }
		set { currentHealth = value; }
	}
	
	public void Reset()
	{
		currentHealth = shipAttributes.HullMaxHealth;
		buoyancy.Reset();
		UpdateHP ();
		//SendHealthBarRefresh();
	}
	
	public void Damage(Vector3 position, float damage, float radius, GameObject source = null)
	{
		if (currentHealth < Mathf.Epsilon)
			return;
		
		shipAttributes.GetPlayerFX.CameraShake(0.375f, damage / 2f);
		
		currentHealth -= damage;
		//buoyancy.DamageVoxels(position, damage, radius);
		
		if (currentHealth <= Mathf.Epsilon)
		{
			TutorialSceneReferences t = GameObject.Find("TutorialSceneReferences").GetComponent<TutorialSceneReferences>();
			if(this.gameObject!= t.player)
			{
				currentHealth = 0f;
				shipAttributes.IsDead = true;
				shipAttributes.OnDeath();

				if(GetComponent<TutorialEnemy>())
					GetComponent<TutorialEnemy>().OnDeath();
				
				if (source != null)
				{
					TutorialProjectile p = source.GetComponent<TutorialProjectile>();
					
					if (p != null && p.owner != GetComponent<CustomOnlinePlayer>())
						p.owner.GetComponent<TutorialLevelUser>().GainEXP(500);
				}
			}
			else
				currentHealth = 1f;
		}
		
		//GetComponent<PlayerCaptionController>().RpcPushDebugText("My hull got damaged for " + damage + " damage. Remaining health: " + currentHealth);
		UpdateHP();
	}
	
	public void Repair(float amount)
	{
		if (currentHealth < shipAttributes.HullMaxHealth)
		{
			currentHealth += amount;
			
			if (currentHealth > shipAttributes.HullMaxHealth)
				currentHealth = shipAttributes.HullMaxHealth;
			
			//GetComponent<PlayerCaptionController>().RpcPushDebugText("My hull got repaired for " + amount + ". Current hull health: " + currentHealth);
			UpdateHP();
		}
	}
	
	//Rammimg
	void OnCollisionEnter(Collision collision)
	{
		TutorialHull hull = collision.collider.GetComponent<TutorialHull>();

		if (hull)
		{
			Vector3 colPoint = collision.contacts[0].point;
			float power = collision.relativeVelocity.magnitude;
			
			hull.Damage(colPoint, power, 10f);
			Damage(colPoint, power, 10f);
			
			shipAttributes.GetPlayerFX.RpcCameraShake(0.375f, power);
			
			GetComponent<PlayerFX>().RpcPlaySound(PlayerFX.PLAYER_SOUNDS.COLLISION);
			UpdateHP();
		}
	}

	
	//[ClientRpc]
	public void UpdateHP()
	{
		hpRend.material.SetFloat("_Health", currentHealth/ shipAttributes.HullMaxHealth);
	}
}
