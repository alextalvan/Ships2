using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SailOnline : MonoBehaviour
{
    private ShipAttributesOnline shipAttributes;

    private float currentHealth;

    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    // Use this for initialization
    void Start()
    {
        shipAttributes = transform.parent.parent.GetComponent<ShipAttributesOnline>();
    }

    public void Damage(float damage)
    {
        if (currentHealth > 0f)
        {
            currentHealth -= damage;
            GetComponent<PlayerCaptionController>().RpcPushDebugText("My sail got damaged for " + damage + " damage. Remaining sail health: " + currentHealth);

            if (currentHealth < 0f)
                currentHealth = 0f;
        }
    }

    public void Repair(float amount)
    {
        if (currentHealth < shipAttributes.SailMaxHealth)
        {
            currentHealth += amount;
            GetComponent<PlayerCaptionController>().RpcPushDebugText("My sail got repaired for " + amount + ". Current sail health: " + currentHealth);

            if (currentHealth > shipAttributes.SailMaxHealth)
                currentHealth = shipAttributes.SailMaxHealth;
        }
    }

    public void Reset()
    {
        currentHealth = shipAttributes.SailMaxHealth;
    }
}
