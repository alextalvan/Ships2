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
        GameObject splashGO = (GameObject)Instantiate(splashPrefab, pos, new Quaternion(0f, Random.rotation.y, 0f, 0f));
        splashGO.GetComponent<ParticleSystem>().startRotation = Random.Range(0, 180);
        splashGO.GetComponent<ParticleSystem>().startSize = size;
    }

    [ClientRpc]
    protected void RpcExplode(Vector3 pos)
    {
        Instantiate(explosionPrefab, pos, new Quaternion(0f, Random.rotation.y, 0f, 0f));
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

    void OnCollisionEnter(Collision collision)
    {
        DealDamage(collision);
    }

    [ServerCallback]
    protected virtual void ProcessDeath()
    {
        if (Time.time > birthDate + lifeTime)
            Delete(false);
    }

    [ClientCallback]
    protected virtual void ProcessSplash()
    {

    }

    [ServerCallback]
    protected virtual void Delete(bool underWater)
    {
        NetworkServer.Destroy(gameObject);
    }

    [ClientRpc]
    protected void RpcSpawnWrecks(Vector3 pos)
    {
        Instantiate(debrisPrefab, pos, new Quaternion(0f, Random.rotation.y, 0f, 0f));
    }
}
