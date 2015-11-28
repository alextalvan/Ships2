using UnityEngine;
using UnityEngine.Networking;

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
        shipAttributes.gameObject.GetComponent<PlayerCaptionController>().RpcPushDebugText("My sail got damaged for " + damage + " damage. Remaining sail health: " + currentHealth);

        if (currentHealth <= Mathf.Epsilon)
        {
            currentHealth = 0f;
            GetComponent<Collider>().enabled = false;
        }

        SendShaderUpdate();
        shipAttributes.UpdateSailsState();
    }

    public void Repair(float amount)
    {
        if (currentHealth < shipAttributes.SailMaxHealth)
        {
            currentHealth += amount;
            shipAttributes.gameObject.GetComponent<PlayerCaptionController>().RpcPushDebugText("My sail got repaired for " + amount + ". Current sail health: " + currentHealth);

            if (currentHealth > shipAttributes.SailMaxHealth)
                currentHealth = shipAttributes.SailMaxHealth;

            SendShaderUpdate();
            shipAttributes.UpdateSailsState();
        }
    }


    private void SendShaderUpdate()
    {
        int myIndex = shipAttributes.sails.IndexOf(this);
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
