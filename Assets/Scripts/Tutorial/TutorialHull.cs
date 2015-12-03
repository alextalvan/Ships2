﻿using UnityEngine;
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
    GameObject debrisRamming;
    [SerializeField]
    GameObject splashPrefab;
    [SerializeField]
    GameObject explosionPrefab;

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
        Instantiate(debrisRamming, position, new Quaternion(0f, Random.rotation.y, 0f, 0f));
        //buoyancy.DamageVoxels(position, damage, radius);

        if (currentHealth <= Mathf.Epsilon)
		{
			TutorialSceneReferences t = GameObject.Find("TutorialSceneReferences").GetComponent<TutorialSceneReferences>();
			if(this.gameObject!= t.player)
			{
				currentHealth = 0f;
				shipAttributes.IsDead = true;
				shipAttributes.OnDeath();

                GameObject splashGO = (GameObject)Instantiate(splashPrefab, transform.position, new Quaternion(0f, Random.rotation.y, 0f, 0f));
                splashGO.GetComponent<ParticleSystem>().startRotation = Random.Range(0, 180);
                splashGO.GetComponent<ParticleSystem>().startSize = 50f;

                Instantiate(explosionPrefab, transform.position, new Quaternion(0f, Random.rotation.y, 0f, 0f)); ;

                if (GetComponent<TutorialEnemy>())
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

		GetComponent<PlayerFX> ().PlaySound (PlayerFX.PLAYER_SOUNDS.HIT);
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
			
			shipAttributes.GetPlayerFX.CameraShake(0.375f, power);

            GetComponent<PlayerFX>().PlaySound(PlayerFX.PLAYER_SOUNDS.COLLISION);
			UpdateHP();
		}
	}

	
	//[ClientRpc]
	public void UpdateHP()
	{
		hpRend.material.SetFloat("_Health", currentHealth/ shipAttributes.HullMaxHealth);
	}
}