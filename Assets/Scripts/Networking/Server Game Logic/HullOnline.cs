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

    public BuoyancyScript SetBuoyancy
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
        SendHealthBarRefresh();
    }

    public void Damage(Vector3 position, float damage, float radius, GameObject source = null)
    {
        if (currentHealth < Mathf.Epsilon)
            return;

        shipAttributes.GetPlayerFX.RpcCameraShake(0.375f, damage/2f);

        currentHealth -= damage;
        buoyancy.ChangeBuoyancy(position, buoyancy.GetVoxelsCount, radius);

        if (currentHealth <= Mathf.Epsilon)
        {
            currentHealth = 0f;
            shipAttributes.IsDead = true;
            shipAttributes.OnDeath();

            if (source != null)
            {
                Projectile p = source.GetComponent<Projectile>();
                if (p != null)
                    p.owner.GetComponent<LevelUser>().GainEXP(500);
            }
        }

        GetComponent<PlayerCaptionController>().RpcPushDebugText("My hull got damaged for " + damage + " damage. Remaining health: " + currentHealth);
        SendHealthBarRefresh();
    }

    public void Repair(float amount)
    {
        currentHealth += amount;

        if (currentHealth > shipAttributes.HullMaxHealth)
            currentHealth = shipAttributes.HullMaxHealth;

        GetComponent<PlayerCaptionController>().RpcPushDebugText("My hull got repaired for " + amount + ". Current hull health: " + currentHealth);
        SendHealthBarRefresh();
    }

    //Rammimg
    void OnCollisionEnter(Collision collision)
    {
        HullOnline hull = collision.collider.GetComponent<HullOnline>();

        if (hull)
        {
            Rigidbody thisRb = GetComponent<Rigidbody>();
            Rigidbody enemyRb = collision.collider.GetComponent<Rigidbody>();

            if (thisRb.velocity.magnitude > enemyRb.velocity.magnitude)
            {
                hull.Damage(collision.contacts[0].point, collision.relativeVelocity.magnitude, 10f);
                Damage(collision.contacts[0].point, collision.relativeVelocity.magnitude / 3f, 10f);
                shipAttributes.GetPlayerFX.RpcCameraShake(0.375f, collision.relativeVelocity.magnitude / 3f);
            }
            else
            {
                Damage(collision.contacts[0].point, collision.relativeVelocity.magnitude, 10f);
                hull.Damage(collision.contacts[0].point, collision.relativeVelocity.magnitude / 3f, 10f);
                shipAttributes.GetPlayerFX.RpcCameraShake(0.375f, collision.relativeVelocity.magnitude);
            }
			GetComponent<PlayerFX> ().RpcPlaySound(PlayerFX.PLAYER_SOUNDS.COLLISION);
            SendHealthBarRefresh();
        }
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
