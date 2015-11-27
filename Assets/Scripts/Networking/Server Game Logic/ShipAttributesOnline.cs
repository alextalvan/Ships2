using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

[RequireComponent(typeof(CustomOnlinePlayer))]
public class ShipAttributesOnline : NetworkBehaviour
{
    private PlayerRespawn playerRespawn;
    private HullOnline hullOnline;

    [SerializeField]
    private Transform sailParent;

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
    private float shootRangeMultiplier;
    [SerializeField]
    private float cannonChargeRate;
    [SerializeField]
    private float reloadRateModifier;
    [SerializeField]
    private float regenerationRate;

	[SerializeField]
	private float damageModifier;

    private float sailSpeedModifier;

    private bool isDead = false;


	[SerializeField]
	List<GameObject> _wreckagePickupPrefabs = new List<GameObject> (); 

    public float HullMaxHealth
    {
        get { return hullMaxHealth; }
        set { hullMaxHealth = value; }
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
    public float RegenerationRate
    {
        get { return regenerationRate; }
        set { regenerationRate = value; }
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

    // Use this for initialization
    void Start()
    {
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

		/*
        foreach (SailOnline sail in sails)
        {
            sail.Reset();
        }*/


        //shootRangeMultiplier = 1.0f;
        //reloadRateModifier = 1.0f;
        //cannonChargeRate = 5f;
        //regenerationRate = 0f;
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

    // Update 
    void FixedUpdate()
    {

    }

	[ServerCallback]
    public void OnDeath()
    {
        playerRespawn.StartRespawn();

		if (_wreckagePickupPrefabs.Count > 0) 
		{
			int index = Random.Range(0,_wreckagePickupPrefabs.Count);

			GameObject o = (GameObject)Instantiate(_wreckagePickupPrefabs[index],this.transform.position,Quaternion.identity);

			NetworkServer.Spawn(o);
		}

    }
}
