using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class TutorialProjectile : MonoBehaviour
{
	protected float birthDate;
	
	[SerializeField]
	protected float lifeTime;
	
	[SerializeField]
	protected float upwardsModifier;
	[SerializeField]
	protected float hullDamage;
	[SerializeField]
	protected float sailDamage;
	[SerializeField]
	protected float damageRadius;
	[SerializeField]
	protected float coolDown;
	
	public CustomOnlinePlayer owner;
	
	[SerializeField]
	private List<GameObject> debris = new List<GameObject> ();
	
	[SerializeField]
	GameObject crossHit;

	[SerializeField]
	public int projectileType;
	
	//splash
	[SerializeField]
	GameObject splashPrefab;
	
	private bool spawnedSplash = false;
	
	public float UpwardsModifier
	{
		get { return upwardsModifier; }
		set { upwardsModifier = value; }
	}
	
	public float HullDamage
	{
		get { return hullDamage; }
		set { hullDamage = value; }
	}
	
	public float SailDamage
	{
		get { return sailDamage; }
		set { sailDamage = value; }
	}
	
	public float DamageRadius
	{
		get { return damageRadius; }
		set { damageRadius = value; }
	}
	
	public float GetCoolDown
	{
		get { return coolDown; }
	}
	
	// Use this for initialization
	protected virtual void Start()
	{
		birthDate = Time.time;
	}
	
	void FixedUpdate()
	{
		ProcessDeath ();
		ProcessSplash ();
	}
	
	//[ClientCallback]
	void ProcessSplash()
	{
		Vector3 pos = transform.position;
		if(!spawnedSplash && pos.y <= WaterHelper.GetOceanHeightAt(new Vector2(pos.x,pos.z)))
		{
			spawnedSplash = true;
			Instantiate(splashPrefab,pos,Quaternion.identity);
		}
	}
	
	//[ServerCallback]
	protected virtual void DealDamage(Collision collision)
	{


		//if (projectileType != 3)
		//	return;

		if (collision.collider.GetComponent<TutorialProjectile>() && collision.collider.GetComponent<TutorialProjectile>().projectileType == 3)
			return;
		
		TutorialHull hull = collision.collider.GetComponent<TutorialHull>();
		if (hull)
		{
			hull.Damage(collision.contacts[0].point, hullDamage, damageRadius, gameObject);
			hull.GetComponent<TutorialShipAttributes>().DamageAllSails(sailDamage);
			SpawnHit(collision.contacts[0].point);
			hull.GetComponent<PlayerFX> ().PlaySound(PlayerFX.PLAYER_SOUNDS.EXPLOSION);

			int randomNmb = Random.Range(0, 4);
			
			if (randomNmb != 1)
			{
				Delete();
				return;
			}
			
			int rndDebris = Random.Range(0, debris.Count);
			
			float rndForce = Random.Range(50f, 100f);
			
			Vector3 dir = collision.contacts[0].point + collision.contacts[0].normal * 5f;
			
			GameObject debrisObj = (GameObject)Instantiate(debris[rndDebris], dir, Random.rotation);
			Rigidbody debrisObjRB = debrisObj.GetComponent<Rigidbody>();
			
			float debrisMass = debrisObjRB.mass;
			
			Vector3 force = (Vector3.up * upwardsModifier + dir.normalized * rndForce) * debrisMass;
			
			debrisObjRB.AddForce(force);
			
			debrisObj.GetComponent<TutorialPickup>().owner = collision.collider.GetComponent<CustomOnlinePlayer>();
		}
		Delete();


		//NetworkServer.Spawn(debrisObj);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		DealDamage(collision);
	}
	
	//[ServerCallback]
	void ProcessDeath()
	{
		if (Time.time > birthDate + lifeTime)
		{
			Delete();
		}
	}
	
	//[ServerCallback]
	protected virtual void Delete()
	{
		Destroy(gameObject);
	}
	
	//[ClientRpc]
	protected void SpawnHit(Vector3 pos)
	{
		Instantiate(Resources.Load("CrossHit"), pos, Quaternion.LookRotation(Camera.main.transform.position - pos));
	}
}
