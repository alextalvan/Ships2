using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HullOnline : MonoBehaviour
{
    private BuoyancyScript buoyancy;
    [SerializeField]
    private ShipAttributesOnline shipAttributes;

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
    }

    public void Damage(Vector3 position, float damage, float radius, GameObject source = null)
    {
        if (currentHealth <= Mathf.Epsilon)
            return;

        currentHealth -= damage;
        buoyancy.ChangeBuoyancy(position, buoyancy.GetVoxelsCount * 2f, radius);
        GetComponent<PlayerCaptionController>().RpcPushDebugText("My hull got damaged for " + damage + " damage. Remaining health: " + currentHealth);

        if (currentHealth <= Mathf.Epsilon)
        {
            //currentHealth = 0f;
            shipAttributes.IsDead = true;
            shipAttributes.OnDeath();

			if(source!=null)
			{
				Projectile p = source.GetComponent<Projectile>();
				if(p!=null)
					p.owner.GetComponent<LevelUser>().GainEXP(500);
			}
        }
    }

    public void Repair(float amount)
    {
        if (currentHealth < shipAttributes.SailMaxHealth)
        {
            currentHealth += amount;
            GetComponent<PlayerCaptionController>().RpcPushDebugText("My hull got repaired for " + amount + ". Current hull health: " + currentHealth);

            if (currentHealth > shipAttributes.SailMaxHealth)
                currentHealth = shipAttributes.SailMaxHealth;
        }
    }
}
