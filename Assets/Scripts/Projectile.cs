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

    public CustomOnlinePlayer owner;

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

    // Use this for initialization
    protected virtual void Start()
    {
        birthDate = Time.time;
    }

    /*
    void Awake()
    {

        if (NetworkManager.singleton != null && NetworkManager.singleton.isNetworkActive && !NetworkServer.active)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().enabled = false;
            return;
        }
    }
    */

    void FixedUpdate()
    {
        if (Time.time > birthDate + lifeTime)
        {
            Delete();
        }
    }

    [ServerCallback]
    protected abstract void DealDamage(Collision collision);

    void OnCollisionEnter(Collision collision)
    {
        DealDamage(collision);
    }

    [ServerCallback]
    protected virtual void Delete()
    {
        NetworkServer.Destroy(gameObject);
    }
}
