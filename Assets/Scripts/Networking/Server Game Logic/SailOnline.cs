using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SailOnline : MonoBehaviour
{
    [SerializeField]
    private ShipAttributesOnline shipAttributes;

    [SerializeField]
    private float currentHealth;

    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= Mathf.Epsilon)
        {
            currentHealth = 0f;
            GetComponent<Collider>().enabled = false;
        }

        shipAttributes.gameObject.GetComponent<PlayerCaptionController>().RpcPushDebugText("My sail got damaged for " + damage + " damage. Remaining sail health: " + currentHealth);
        SendShaderUpdate();
        shipAttributes.UpdateSailsState();
    }

    public void Repair(float amount)
    {
        if (currentHealth < shipAttributes.SailMaxHealth)
        {
            currentHealth += amount;

            if (currentHealth > shipAttributes.SailMaxHealth)
                currentHealth = shipAttributes.SailMaxHealth;

            shipAttributes.gameObject.GetComponent<PlayerCaptionController>().RpcPushDebugText("My sail got repaired for " + amount + ". Current sail health: " + currentHealth);
            SendShaderUpdate();
            shipAttributes.UpdateSailsState();
        }
    }

    private void SendShaderUpdate()
    {
        int myIndex = shipAttributes.GetSailsList.IndexOf(this);
        shipAttributes.RpcChangeSailDissolve(myIndex, 1f - currentHealth / shipAttributes.SailMaxHealth);
    }

    public void Reset()
    {
        currentHealth = shipAttributes.SailMaxHealth;
        GetComponent<Collider>().enabled = true;
        SendShaderUpdate();
        shipAttributes.UpdateSailsState();
    }
}
