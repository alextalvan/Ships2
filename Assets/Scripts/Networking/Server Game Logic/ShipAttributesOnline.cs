using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(CustomOnlinePlayer))]
public class ShipAttributesOnline : NetworkBehaviour {


	//public float hullHealth;
	public float hullMaxHealth;
	public float sailHealth;
	public float sailMaxHealth;
	//public int crewCount;
	public float speed;

	//fighting
	public float damage;
	public float shootRangeMultiplier;
	public float cannonChargeRate;
	public float reloadSpeedModifier;
	public float healthRegen;

	//level
	int _level;
	int _currentEXP;
	int _nextLevelEXP;

	public delegate void voidvoid();

	public event voidvoid OnLevelUp;

	void GainEXP(int amount)
	{
		_currentEXP += amount;
		if (_currentEXP >= _nextLevelEXP) 
		{
			if(OnLevelUp!=null)
				OnLevelUp();

			//TODO: function to calculate next level exp
			_nextLevelEXP += 100;
		}
	}



	// Use this for initialization
	void Start () 
	{
		Reset ();
	}

	[Server]
	public void Reset()
	{
		//hullMaxHealth = 100f;
		GetComponent<HullOnline> ().Reset ();
		sailHealth = sailMaxHealth = 100f;
		//crewCount = 20;
		speed = 5f;

		damage = 5f;
		shootRangeMultiplier = 1.0f;
		reloadSpeedModifier = 1.0f;
		cannonChargeRate = 5f;
		healthRegen = 0f;

	}
	
	// Update 
	void FixedUpdate () {
	
	}
}
