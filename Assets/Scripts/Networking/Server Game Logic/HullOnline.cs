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
<<<<<<< HEAD
    
=======

>>>>>>> 7c58a6c6db3fd9d9194b53f353e7c33f618bc9ad
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
<<<<<<< HEAD
        RpcUpdateHP(currentHealth / shipAttributes.HullMaxHealth);
=======
		shipAttributes.IsDead = false;
		RpcUpdateHealthBar(1f);
>>>>>>> 7c58a6c6db3fd9d9194b53f353e7c33f618bc9ad
        buoyancy.Reset();
    }

    public void Damage(Vector3 position, float damage, float radius, GameObject source = null)
    {
        if (currentHealth < Mathf.Epsilon)
            return;

        currentHealth -= damage;
        buoyancy.ChangeBuoyancy(position, buoyancy.GetVoxelsCount, radius);
<<<<<<< HEAD
        GetComponent<PlayerCaptionController>().RpcPushDebugText("My hull got damaged for " + damage + " damage. Remaining health: " + currentHealth);
        RpcUpdateHP(currentHealth / shipAttributes.HullMaxHealth);

=======
       
>>>>>>> 7c58a6c6db3fd9d9194b53f353e7c33f618bc9ad
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
		SendHealthBarRefresh ();
    }

    public void Repair(float amount)
    {
<<<<<<< HEAD
        if (currentHealth < shipAttributes.SailMaxHealth)
        {
            currentHealth += amount;
            GetComponent<PlayerCaptionController>().RpcPushDebugText("My hull got repaired for " + amount + ". Current hull health: " + currentHealth);
            RpcUpdateHP(currentHealth / shipAttributes.HullMaxHealth);
=======
        currentHealth += amount;
        if (currentHealth > shipAttributes.HullMaxHealth)
            currentHealth = shipAttributes.HullMaxHealth;
>>>>>>> 7c58a6c6db3fd9d9194b53f353e7c33f618bc9ad

		GetComponent<PlayerCaptionController>().RpcPushDebugText("My hull got repaired for " + amount + ". Current hull health: " + currentHealth);
		SendHealthBarRefresh ();
    
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
            }
            else
            {
                Damage(collision.contacts[0].point, collision.relativeVelocity.magnitude, 10f);
                hull.Damage(collision.contacts[0].point, collision.relativeVelocity.magnitude / 3f, 10f);
            }
        }
    }

	[ServerCallback]
	public void SendHealthBarRefresh()
	{
		RpcUpdateHealthBar (currentHealth / shipAttributes.HullMaxHealth);
	}

    [ClientRpc]
<<<<<<< HEAD
    void RpcUpdateHP(float hp)
    {
        hpRend.material.SetFloat("_Health", hp);
=======
    void RpcUpdateHealthBar(float healthRatio)
    {
        hpRend.material.SetFloat("_Health", healthRatio);
>>>>>>> 7c58a6c6db3fd9d9194b53f353e7c33f618bc9ad
    }
}
