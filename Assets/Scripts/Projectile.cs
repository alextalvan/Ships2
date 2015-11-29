using UnityEngine;
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
    protected float damageRadius;
    [SerializeField]
    protected float coolDown;

    public CustomOnlinePlayer owner;

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

	[ClientCallback]
	void ProcessSplash()
	{
		Vector3 pos = transform.position;
		if(!spawnedSplash && pos.y <= WaterHelper.GetOceanHeightAt(new Vector2(pos.x,pos.z)))
		{
			spawnedSplash = true;
			Instantiate(splashPrefab,pos,Quaternion.identity);
		}
	}

    [ServerCallback]
    protected abstract void DealDamage(Collision collision);

    void OnCollisionEnter(Collision collision)
    {
        DealDamage(collision);
    }

	[ServerCallback]
	void ProcessDeath()
	{
		if (Time.time > birthDate + lifeTime)
		{
			Delete();
		}
	}

    [ServerCallback]
    protected virtual void Delete()
    {
        NetworkServer.Destroy(gameObject);
    }
}
