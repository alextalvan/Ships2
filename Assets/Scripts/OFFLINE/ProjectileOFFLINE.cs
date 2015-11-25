using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class ProjectileOFFLINE : MonoBehaviour
{
    [SerializeField]
    protected float upwardsModifier = 1f;
    [SerializeField]
    protected float hullDamage = 50f;
    [SerializeField]
    protected float sailDamage = 25f;
    [SerializeField]
    protected float damageRadius = 5f;

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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected abstract void DealDamage(Collision collision);

    void OnCollisionEnter(Collision collision)
    {
        DealDamage(collision);
        Destroy(gameObject);
    }
}
