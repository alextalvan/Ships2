using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Collider))]
[NetworkSettings(channel = 0, sendInterval = 1e+6f)]
public class HullOnline : NetworkBehaviour
{
    private BuoyancyScript buoyancy;
    [SerializeField]
    private ShipAttributesOnline shipAttributes;
    [SerializeField]
    private Renderer hpRend;
    [SerializeField]
    GameObject debrisRamming;
    [SerializeField]
    GameObject splashPrefab;
    [SerializeField]
    GameObject explosionPrefab;
    private Rigidbody rb;

    private float currentHealth;
    private float velocity;

    public Rigidbody GetRigidBody
    {
        get { return rb; }
    }
    public BuoyancyScript SetBuoyancy
    {
        set { buoyancy = value; }
    }
    public float CurrentVelocity
    {
        get { return velocity; }
    }
    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    /// <summary>
    /// reset properties
    /// </summary>
    public void Reset()
    {
        rb = shipAttributes.GetRigidBody;
        currentHealth = shipAttributes.HullMaxHealth;
        buoyancy.Reset();
        SendHealthBarRefresh();
    }

    void FixedUpdate()
    {
        velocity = shipAttributes.GetCurrentSpeed;
    }

    /// <summary>
    /// damage hull
    /// </summary>
    /// <param name="position"></param>
    /// <param name="damage"></param>
    /// <param name="radius"></param>
    /// <param name="source"></param>
    public void Damage(Vector3 position, float damage, float radius, GameObject source = null)
    {
        if (currentHealth < Mathf.Epsilon)
            return;

        shipAttributes.GetPlayerFX.RpcCameraShake(0.375f, damage / 2f);

        currentHealth -= damage;
        buoyancy.DamageVoxels(position, damage, radius);

        if (currentHealth <= Mathf.Epsilon)
        {
            currentHealth = 0f;
            shipAttributes.IsDead = true;
            shipAttributes.OnDeath();
            RpcExplode(new Vector3(transform.position.x, WaterHelper.GetOceanHeightAt(new Vector2(transform.position.x, transform.position.z)), transform.position.z), 50f, 5f);

            if (source != null)
            {
                Projectile p = source.GetComponent<Projectile>();
                HullOnline hullKilla = source.GetComponent<HullOnline>();

                if (p != null && p.owner != GetComponent<CustomOnlinePlayer>())
                    p.owner.GetComponent<LevelUser>().GainEXP(500);
                else if (hullKilla != null)
                    hullKilla.GetComponent<CustomOnlinePlayer>().GetComponent<LevelUser>().GainEXP(500);
            }
        }

        GetComponent<PlayerCaptionController>().RpcPushDebugText("My hull got damaged for " + damage + " damage. Remaining health: " + currentHealth);
        SendHealthBarRefresh();
    }

    /// <summary>
    /// repair hull
    /// </summary>
    /// <param name="amount"></param>
    public void Repair(float amount)
    {
        if (currentHealth < shipAttributes.HullMaxHealth)
        {
            currentHealth += amount;

            if (currentHealth > shipAttributes.HullMaxHealth)
                currentHealth = shipAttributes.HullMaxHealth;

            GetComponent<PlayerCaptionController>().RpcPushDebugText("My hull got repaired for " + amount + ". Current hull health: " + currentHealth);
            SendHealthBarRefresh();
        }
    }

    /// <summary>
    /// explosion spawn
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="size"></param>
    /// <param name="duration"></param>
    [ClientRpc]
    private void RpcExplode(Vector3 pos, float size, float duration)
    {
        GameObject splashGO = (GameObject)Instantiate(splashPrefab, pos, new Quaternion(0f, Random.rotation.y, 0f, 0f));
        splashGO.GetComponent<ParticleSystem>().startRotation = Random.Range(0, 180);
        splashGO.GetComponent<ParticleSystem>().startSize = size;
        splashGO.GetComponent<ParticleSystem>().startLifetime = duration;

        Instantiate(explosionPrefab, pos, new Quaternion(0f, Random.rotation.y, 0f, 0f));
    }

    /// <summary>
    /// collision damage etc (ramming)
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Projectile>() || collision.collider.GetComponent<SailOnline>())
            return;

        if (collision.relativeVelocity.magnitude < 10f)
            return;

        HullOnline hull = collision.collider.GetComponent<HullOnline>();
        Vector3 colPoint = collision.contacts[0].point;

        if (hull)
        {
            HullOnline colHull = collision.collider.GetComponent<HullOnline>();
            Damage(colPoint, colHull.CurrentVelocity, 10f, collision.collider.gameObject);
            rb.AddForce(collision.impulse * colHull.CurrentVelocity);
        }
        else
        {
            Damage(colPoint, velocity, 10f);
            rb.AddForce(collision.impulse * velocity);
        }

        RpcSpawnWrecks(colPoint);

        shipAttributes.GetPlayerFX.RpcCameraShake(0.375f, collision.relativeVelocity.magnitude / velocity);
        GetComponent<PlayerFX>().RpcPlaySound(PlayerFX.PLAYER_SOUNDS.COLLISION,true);
        SendHealthBarRefresh();
    }

    /// <summary>
    /// update healthbar
    /// </summary>
    [ServerCallback]
    public void SendHealthBarRefresh()
    {
        RpcUpdateHP(currentHealth / shipAttributes.HullMaxHealth);
    }

    /// <summary>
    /// update shader hp
    /// </summary>
    /// <param name="healthRatio"></param>
    [ClientRpc]
    void RpcUpdateHP(float healthRatio)
    {
        hpRend.material.SetFloat("_Health", healthRatio);
    }

    /// <summary>
    /// spawn wrecks
    /// </summary>
    /// <param name="pos"></param>
    [ClientRpc]
    protected void RpcSpawnWrecks(Vector3 pos)
    {
        Instantiate(debrisRamming, pos, new Quaternion(0f, Random.rotation.y, 0f, 0f));
    }
}
