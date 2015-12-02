using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Collider))]
public class HullOnline : NetworkBehaviour
{
    private BuoyancyScript buoyancy;
    [SerializeField]
    private ShipAttributesOnline shipAttributes;
    [SerializeField]
    private Renderer hpRend;

    private float currentHealth;
    private float velocity;

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

    public void Reset()
    {
        currentHealth = shipAttributes.HullMaxHealth;
        buoyancy.Reset();
        SendHealthBarRefresh();
    }

    void FixedUpdate()
    {
        velocity = shipAttributes.GetCurrentSpeed;
    }

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

            if (source != null)
            {
                Projectile p = source.GetComponent<Projectile>();

                if (p != null && p.owner != GetComponent<CustomOnlinePlayer>())
                    p.owner.GetComponent<LevelUser>().GainEXP(500);
            }
        }

        GetComponent<PlayerCaptionController>().RpcPushDebugText("My hull got damaged for " + damage + " damage. Remaining health: " + currentHealth);
        SendHealthBarRefresh();
    }

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

    //Rammimg
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Projectile>() || collision.collider.GetComponent<SailOnline>())
            return;

        if (collision.relativeVelocity.magnitude < 15f)
            return;

        HullOnline hull = collision.collider.GetComponent<HullOnline>();
        Vector3 colPoint = collision.contacts[0].point;

        if (hull)
        {
            HullOnline colHull = collision.collider.GetComponent<HullOnline>();
            Damage(colPoint, colHull.CurrentVelocity, 10f);
        }
        else
        {
            Damage(colPoint, velocity, 10f);
        }

        shipAttributes.GetPlayerFX.RpcCameraShake(0.375f, collision.relativeVelocity.magnitude / velocity);
        GetComponent<PlayerFX>().RpcPlaySound(PlayerFX.PLAYER_SOUNDS.COLLISION);
        SendHealthBarRefresh();
    }

    [ServerCallback]
    public void SendHealthBarRefresh()
    {
        RpcUpdateHP(currentHealth / shipAttributes.HullMaxHealth);
    }

    [ClientRpc]
    void RpcUpdateHP(float healthRatio)
    {
        hpRend.material.SetFloat("_Health", healthRatio);
    }
}
