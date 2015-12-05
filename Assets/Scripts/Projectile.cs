using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public abstract class Projectile : NetworkBehaviour
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
    protected float explosionForce;
    [SerializeField]
    protected float damageRadius;
    [SerializeField]
    protected float coolDown;

    public CustomOnlinePlayer owner;

    [SerializeField]
    private List<GameObject> debris = new List<GameObject>();

    //splash
    [SerializeField]
    protected GameObject splashPrefab;
    [SerializeField]
    protected GameObject explosionPrefab;
    [SerializeField]
    protected GameObject debrisPrefab;

    protected bool spawnedSplash = false;

	[SerializeField]
	private List<AudioClip> _impactSounds;

	protected enum ImpactSoundType
	{
		SHIP_HULL = 0,
		ROCK,
		EXPLOSION,
		NONE
	}

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
        ProcessSplash();
        ProcessDeath();
    }


	[ClientRpc]
	protected void RpcSpawnSplash(Vector3 pos, float size)
	{
		GameObject splashGO = (GameObject)Instantiate(splashPrefab, pos, Quaternion.identity);
		splashGO.GetComponent<ParticleSystem>().startRotation = Random.Range(0, 180);
		splashGO.GetComponent<ParticleSystem>().startSize = size;
	}


    protected void SpawnSplash(float size)
    {
        //GameObject splashGO = (GameObject)Instantiate(splashPrefab, transform.position, new Quaternion(0f, Random.rotation.y, 0f, 0f));
		GameObject splashGO = (GameObject)Instantiate(splashPrefab, transform.position, Quaternion.identity);
        splashGO.GetComponent<ParticleSystem>().startRotation = Random.Range(0, 180);
        splashGO.GetComponent<ParticleSystem>().startSize = size;
    }

    [ClientRpc]
    protected void RpcExplode(Vector3 pos, ImpactSoundType type)
    {
        Instantiate(explosionPrefab, pos, new Quaternion(0f, Random.rotation.y, 0f, 0f));

		if (type == ImpactSoundType.NONE)
			return;
		AudioHelper.PlayAt (_impactSounds [(int)type], pos);
    }

    [ServerCallback]
    protected virtual void DealDamage(Collision collision)
    {
        int randomNmb = Random.Range(0, 4);

        if (randomNmb != 1)
            return;

        int rndDebris = Random.Range(0, debris.Count);

        float rndForce = Random.Range(50f, 100f);

        Vector3 dir = collision.contacts[0].point + collision.contacts[0].normal * 5f;

        GameObject debrisObj = (GameObject)Instantiate(debris[rndDebris], dir, Random.rotation);
        Rigidbody debrisObjRB = debrisObj.GetComponent<Rigidbody>();

        float debrisMass = debrisObjRB.mass;

        Vector3 force = (Vector3.up * upwardsModifier + dir.normalized * rndForce) * debrisMass;

        debrisObjRB.AddForce(force);

        debrisObj.GetComponent<Pickup>().owner = collision.collider.GetComponent<CustomOnlinePlayer>();

        NetworkServer.Spawn(debrisObj);
    }

	[ServerCallback]
	protected void SpawnDebrisAt(Vector3 point, Vector3 normal, CustomOnlinePlayer owner)
	{
		int randomNmb = Random.Range(0, 4);
		
		if (randomNmb != 1)
			return;
		
		int rndDebris = Random.Range(0, debris.Count);
		
		float rndForce = Random.Range(50f, 100f);
		
		Vector3 dir = point + normal * 5f;
		
		GameObject debrisObj = (GameObject)Instantiate(debris[rndDebris], dir, Random.rotation);
		Rigidbody debrisObjRB = debrisObj.GetComponent<Rigidbody>();
		
		float debrisMass = debrisObjRB.mass;
		
		Vector3 force = (Vector3.up * upwardsModifier + dir.normalized * rndForce) * debrisMass;
		
		debrisObjRB.AddForce(force);
		
		debrisObj.GetComponent<Pickup> ().owner = owner;
		
		NetworkServer.Spawn(debrisObj);
	}

    void OnCollisionEnter(Collision collision)
    {
        DealDamage(collision);
    }

    [ServerCallback]
    protected virtual void ProcessDeath()
    {
        if (Time.time > birthDate + lifeTime)
            Delete();
    }

    [ClientCallback]
    protected virtual void ProcessSplash(float size = 5f)
    {
		if (!spawnedSplash && transform.position.y < WaterHelper.GetOceanHeightAt (new Vector2 (transform.position.x, transform.position.z))) 
		{
			spawnedSplash = true;
			gameObject.SetActive(false);
			SpawnSplash(size);
		}
    }

    [ServerCallback]
    protected virtual void Delete()
    {
        NetworkServer.Destroy(gameObject);
    }

    [ClientRpc]
    protected void RpcSpawnWrecks(Vector3 pos)
    {
        Instantiate(debrisPrefab, pos, new Quaternion(0f, Random.rotation.y, 0f, 0f));
    }
}
