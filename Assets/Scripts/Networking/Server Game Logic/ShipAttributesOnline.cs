using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

[RequireComponent(typeof(CustomOnlinePlayer))]
public class ShipAttributesOnline : NetworkBehaviour
{
    private PlayerRespawn playerRespawn;
    private HullOnline hullOnline;
    private PlayerFX pfx;
    private Rigidbody rb;
    private ShipScript shipScript;

    [SerializeField]
    private Transform sailParent;
    [SerializeField]
    private Transform hpBar;

    private List<SailOnline> sails = new List<SailOnline>();

    [SerializeField]
    private float hullMaxHealth;
    [SerializeField]
    private float sailMaxHealth;
    [SerializeField]
    private float basicSpeed;
    [SerializeField]
    private float steeringModifier;

    [SerializeField]
    [SyncVar]
    private float shootRangeMultiplier;
    [SerializeField]
    private float cannonChargeRate;
    [SerializeField]
    private float reloadRateModifier;

    [SerializeField]
    private float damageModifier;

    private float sailSpeedModifier;

    [SyncVar]
    private bool isDead = false;

    [SerializeField]
    List<GameObject> pickUps = new List<GameObject>();
    [SerializeField]
    private int minDrop;
    [SerializeField]
    private int maxDrop;

    public PlayerFX GetPlayerFX
    {
        get { return pfx; }
    }
    public List<SailOnline> GetSailsList
    {
        get { return sails; }
    }
    public Transform HpBar
    {
        get { return hpBar; }
    }
    public float HullMaxHealth
    {
        get { return hullMaxHealth; }
        set
        {
            HullOnline hull = GetComponent<HullOnline>();
            float delta = value - hullMaxHealth;
            hullMaxHealth = value;

            if (hull.CurrentHealth > hullMaxHealth)
                hull.CurrentHealth = hullMaxHealth;

            if (delta > 0f)
                hull.CurrentHealth += delta;

            hull.SendHealthBarRefresh();
        }
    }
    public float SailMaxHealth
    {
        get { return sailMaxHealth; }
        set { sailMaxHealth = value; }
    }
    public float BasicSpeed
    {
        get { return basicSpeed; }
        set { basicSpeed = value; }
    }
    public float SteeringModifier
    {
        get { return steeringModifier; }
        set { steeringModifier = value; }
    }

    public float RangeMultiplier
    {
        get { return shootRangeMultiplier; }
        set { shootRangeMultiplier = value; }
    }
    public float CannonChargeRate
    {
        get { return cannonChargeRate; }
        set { cannonChargeRate = value; }
    }
    public float ReloadRateModifier
    {
        get { return reloadRateModifier; }
        set { reloadRateModifier = value; }
    }
    public float SailSpeedModifier
    {
        get { return sailSpeedModifier; }
        set { sailSpeedModifier = value; }
    }
    public float DamageModifier
    {
        get { return damageModifier; }
        set { damageModifier = value; }
    }
    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }
    public Rigidbody GetRigidBody
    {
        get { return rb; }
    }
    public float GetCurrentSpeed
    {
        get { return rb.velocity.magnitude; }
    }

    // Use this for initialization
    void Start()
    {
        shipScript = GetComponent<ShipScript>();
        rb = GetComponent<Rigidbody>();
        pfx = GetComponent<PlayerFX>();
        playerRespawn = GetComponent<PlayerRespawn>();
        hullOnline = GetComponent<HullOnline>();
        hullOnline.SetBuoyancy = GetComponent<BuoyancyScript>();

        foreach (Transform child in sailParent)
        {
            SailOnline sail = child.GetComponent<SailOnline>();
            sails.Add(sail);
        }

        Reset();
    }

    [Server]
    public void Reset()
    {
        isDead = false;
        hullOnline.Reset();

        foreach (SailOnline sail in sails)
        {
            sail.Reset();
        }

        UpdateSailsState();
    }

    [ServerCallback]
    public void UpdateSailsState()
    {
        float totalSailHealth = 100f;
        foreach (SailOnline sail in sails)
        {
            totalSailHealth += sail.CurrentHealth;
        }
        totalSailHealth /= sails.Count;
        totalSailHealth /= sailMaxHealth;
        sailSpeedModifier = totalSailHealth;
    }

    void Update()
    {
        ChangeSailsShaking();
    }

    [ServerCallback]
    public void DamageAllSails(float damage)
    {
        pfx.RpcCameraShake(0.375f, damage / 3f);

        foreach (SailOnline sail in sails)
        {
            sail.Damage(damage);
        }
    }

    [ServerCallback]
    public void RepairAllSails(float amount)
    {
        foreach (SailOnline sail in sails)
        {
            sail.Repair(amount);
        }
    }

    [ClientCallback]
    private void ChangeSailsShaking()
    {
        foreach(SailOnline sail in sails)
        {
            Material sailsMat = sail.GetComponent<Renderer>().material;
            float currentSailVal = sailsMat.GetFloat("_SailControls");
            float lerpedSailVal = Mathf.Lerp(currentSailVal, 1 - shipScript.GetSailState, Time.deltaTime);
            sailsMat.SetFloat("_SailControls", lerpedSailVal);

            //sailsMat.SetFloat("_Frequency", 1f - (GetCurrentSpeed / 20f)); //still working on it
            //sailsMat.SetFloat("_Amplitude", 1f - (GetCurrentSpeed / 20f));
        }
    }

    [ClientRpc]
    public void RpcChangeSailDissolve(int index, float amount)
    {
        sails[index].GetComponent<Renderer>().material.SetFloat("_Dissolveamount", amount);
    }

    [ServerCallback]
    public void OnDeath()
    {
        playerRespawn.StartRespawn();
		GetComponent<PlayerFX> ().RpcSpawnDeathParticle ();

        int amount = Random.Range(minDrop, maxDrop);

        for (int i = 0; i < amount; i++)
        {
            int index = Random.Range(0, pickUps.Count);
            Vector3 rndOffset = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            GameObject debris = (GameObject)Instantiate(pickUps[index], transform.position + rndOffset, Random.rotation);

            NetworkServer.Spawn(debris);
        }
    }
}
