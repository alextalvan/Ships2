﻿using UnityEngine;

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

    public void Damage(Vector3 position, float damage, float radius)
    {
        if (currentHealth <= 0f)
            return;

        currentHealth -= damage;
        buoyancy.ChangeBuoyancy(position, buoyancy.GetVoxelsCount * 1.5f, radius);
        GetComponent<PlayerCaptionController>().RpcPushDebugText("My hull got damaged for " + damage + " damage. Remaining health: " + currentHealth);

        if (currentHealth <= 0f)
        {
            //currentHealth = 0f;
            shipAttributes.IsDead = true;
            shipAttributes.OnDeath();
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
