using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(OnlineTransform))]
[RequireComponent(typeof(Collider))]
public class Pickup : NetworkBehaviour
{
    public int EXP_Reward;
    public float lifeTime;
    private float deathTime;
    private bool taken = false;
    [SerializeField]
    private float repairPotential;

    public CustomOnlinePlayer owner;

    // Use this for initialization
    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        deathTime = Time.time + lifeTime;
    }

    [ServerCallback]
    void CheckDelete()
    {
        if (Time.time > deathTime)
            NetworkServer.Destroy(this.gameObject);
    }

    // Update 
    void FixedUpdate()
    {

    }

    void Update()
    {
        CheckDelete();
    }

    void OnTriggerEnter(Collider other)
    {
        if (taken)
            return;

        PlayerPickupHitbox hitbox = other.GetComponent<PlayerPickupHitbox>();

        if (hitbox != null && !hitbox.GetComponentInParent<ShipAttributesOnline>().IsDead && other.GetComponentInParent<CustomOnlinePlayer>() != owner)
        {
            float rnd = Random.Range(0, 3);

            if (rnd == 0 || rnd == 1)
            {
                other.GetComponentInParent<HullOnline>().Repair(repairPotential);
                OnPickup(hitbox.GetComponentInParent<CustomOnlinePlayer>(), "Hull");
            }
            else
            {
                other.GetComponentInParent<ShipAttributesOnline>().RepairAllSails(repairPotential);
                OnPickup(hitbox.GetComponentInParent<CustomOnlinePlayer>(), "Sails");
            }

            taken = true;
            NetworkServer.Destroy(this.gameObject);
        }
    }

    protected virtual void OnPickup(CustomOnlinePlayer player)
    {
        player.GetComponent<LevelUser>().GainEXP(EXP_Reward);
    }

    protected virtual void OnPickup(CustomOnlinePlayer player, string part)
    {
        player.GetComponent<LevelUser>().GainEXP(EXP_Reward, (int)repairPotential, part);
    }
}
