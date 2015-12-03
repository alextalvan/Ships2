using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

//[RequireComponent(typeof(CustomOnlinePlayer))]
public class TutorialShipAttributes : MonoBehaviour
{
	//private PlayerRespawn playerRespawn;
	private TutorialHull tutHull;
	private PlayerFX pfx;
	private Rigidbody rb;
	private TutorialShipScript shipScript;
	
	[SerializeField]
	private Transform sailParent;
	//[SerializeField]
	//private Transform hpBar;
	
	private List<TutorialSail> sails = new List<TutorialSail>();
	
	[SerializeField]
	private float hullMaxHealth;
	[SerializeField]
	private float sailMaxHealth;
	[SerializeField]
	private float basicSpeed;
	[SerializeField]
	private float steeringModifier;
	
	[SerializeField]
	//[SyncVar]
	private float shootRangeMultiplier;
	[SerializeField]
	private float cannonChargeRate;
	[SerializeField]
	private float reloadRateModifier;
	
	[SerializeField]
	private float damageModifier;
	
	private float sailSpeedModifier;
	
	//[SyncVar]
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
	public List<TutorialSail> GetSailsList
	{
		get { return sails; }
	}
	public float HullMaxHealth
	{
		get { return hullMaxHealth; }
		set
		{
			TutorialHull hull = GetComponent<TutorialHull>();
			float delta = value - hullMaxHealth;
			hullMaxHealth = value;
			
			if (hull.CurrentHealth > hullMaxHealth)
				hull.CurrentHealth = hullMaxHealth;
			
			if (delta > 0f)
				hull.CurrentHealth += delta;
			
			hull.UpdateHP();
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
	public float GetCurrentSpeed
	{
		get { return rb.velocity.magnitude; }
	}
	
	// Use this for initialization
	void Start()
	{
		shipScript = GetComponent<TutorialShipScript>();
		rb = GetComponent<Rigidbody>();
		pfx = GetComponent<PlayerFX>();
		//playerRespawn = GetComponent<PlayerRespawn>();
		tutHull = GetComponent<TutorialHull>();
		tutHull.SetBuoyancy = GetComponent<TutorialBuoyancy>();
		
		foreach (Transform child in sailParent)
		{
			TutorialSail sail = child.GetComponent<TutorialSail>();
			sails.Add(sail);
		}
		
		Reset();
	}
	
	//[Server]
	public void Reset()
	{
		isDead = false;
		tutHull.Reset();

		/*
		foreach (TutorialSail sail in sails)
		{
			//sail.Reset();
		}
		*/
		
		UpdateSailsState();
	}
	
	//[ServerCallback]
	public void UpdateSailsState()
	{
		float totalSailHealth = 100f;
		foreach (TutorialSail sail in sails)
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

	void FixedUpdate()
	{
		//PassiveRegen ();
	}

	void PassiveRegen()
	{
		tutHull.Repair (1f);
		RepairAllSails (1f);
	}
	
	//[ServerCallback]
	public void DamageAllSails(float damage)
	{
		pfx.CameraShake(0.375f, damage / 3f);
		
		foreach (TutorialSail sail in sails)
		{
			sail.Damage(damage);
		}
	}
	
	//[ServerCallback]
	public void RepairAllSails(float amount)
	{
		foreach (TutorialSail sail in sails)
		{
			sail.Repair(amount);
		}
	}
	
	//[ClientCallback]
	private void ChangeSailsShaking()
	{
		foreach(TutorialSail sail in sails)
		{
			Material sailsMat = sail.GetComponent<Renderer>().material;
			float currentSailVal = sailsMat.GetFloat("_SailControls");
			float lerpedSailVal = Mathf.Lerp(currentSailVal, 1 - shipScript.GetSailState, Time.deltaTime);
			sailsMat.SetFloat("_SailControls", lerpedSailVal);
			
			//sailsMat.SetFloat("_Frequency", 1f - (GetCurrentSpeed / 20f)); //still working on it
			//sailsMat.SetFloat("_Amplitude", 1f - (GetCurrentSpeed / 20f));
		}
	}
	
	//[ClientRpc]
	public void ChangeSailDissolve(int index, float amount)
	{
		sails[index].GetComponent<Renderer>().material.SetFloat("_Dissolveamount", amount);
	}
	
	//[ServerCallback]
	public void OnDeath()
	{
		GetComponent<PlayerFX> ().SpawnDeathParticle ();
		
		int amount = Random.Range(minDrop, maxDrop);
		
		for (int i = 0; i < amount; i++)
		{
			int index = Random.Range(0, pickUps.Count);
			Vector3 rndOffset = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), Random.Range(-3f, 3f));
			Instantiate(pickUps[index], transform.position + rndOffset, Random.rotation);
			
			//NetworkServer.Spawn(debris);
		}

		//GetComponent<TutorialBuoyancy>().
	}
}
