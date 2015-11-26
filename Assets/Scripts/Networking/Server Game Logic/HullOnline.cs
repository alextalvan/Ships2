using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

//server only
public class HullOnline : MonoBehaviour {


	float currentHealth;
	//float maxHealth;

	bool isDead = false;

	//OnlineSceneReferences onlineRef;
	
	void Start()
	{
		//onlineRef = GameObject.Find ("OnlineSceneReferences").GetComponent<OnlineSceneReferences> ();
	}

	public void Reset()
	{
		currentHealth = GetComponent<ShipAttributesOnline> ().hullMaxHealth;
		isDead = false;
	}


	public void Damage(float amount)
	{
		if (isDead)
			return;



		currentHealth -= amount;
		GetComponent<PlayerCaptionController> ().RpcPushDebugText ("My hull got damaged for " + amount + " damage. Remaining health: " + currentHealth);
		if (currentHealth <= 0.001f) 
		{
			isDead = true;
			OnDeath();
		}
		//
	}

	void OnDeath()
	{
		GetComponent<PlayerRespawn> ().StartRespawn ();
	}


}
