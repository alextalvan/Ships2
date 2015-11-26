using UnityEngine;
using System.Collections;

//@TODO: Seperate Leveling up to new script(handle upgrades in there as well)
public class ShipAttributes : MonoBehaviour {


    
	// Use this for initialization
	void Start () {
        ChangeSailHealth(0,0);
	}

    bool sailsLifted;

    Upgrade upgrade;
    
    float averageSailHealth;

    [SerializeField]
    float[] sailHealth = {100,100,100};

    [SerializeField]
    float sailHealthMax = 100;

    [SerializeField]
    float hullHealth = 100;

    [SerializeField]
    float hullHealthMax = 100;

    //Value used by movement!
    float sailSpeed;

    [SerializeField]
    float reloadRate;

    [SerializeField]
    float range;

    public void DoUpgrade() {
        upgrade.ApplyUpgrade();
    }
    //Change em
    public void SetSailsLifted(bool isLifted)
    {
         sailsLifted = isLifted; 
    }
    public void ChangeReloadRate(float Modifier)
    {
        reloadRate += Modifier;
    }
    public void ChangeHealthMax(float Modifier)
    {
        sailHealthMax += Modifier;
        hullHealthMax += Modifier;
    }
    public void ChangeSailSpeed(float Modifier)
    {
        sailSpeed += Modifier;
    }
    public void ChangeRange(float Modifier)
    {
        range += Modifier;
    }
    public void ChangeHullHealth(float Modifier)
    {
        hullHealth += Modifier;
        if (hullHealth > hullHealthMax)
        {
            hullHealth = hullHealthMax;
        }
    }
    public void ChangeSailHealth(float Modifier, int Index)
    {
        sailHealth[Index] += Modifier;
        averageSailHealth = (sailHealth[0] + sailHealth[1] + sailHealth[2]) / sailHealth.Length;
        if (sailsLifted == false)
        {
            sailSpeed = (averageSailHealth / sailHealthMax);
        }
        if (sailHealth[Index] < 0) sailHealth[Index] = 0;
        if (sailHealth[Index] > sailHealthMax )
        {
            sailHealth[Index] = sailHealthMax;
        }

    }
        
    //Get em
    public bool GetSailsLifted {
        get { return sailsLifted; }
    }
    public float[] GetSailHealth {
        get { return sailHealth; }
    }
    public float GetAverageSailHealth
    {
        get { return averageSailHealth; }
    }
    public float GetRange
    {
        get { return range; }
    }
    public float GetSailHealthMax
    {
        get { return sailHealthMax; }
    }
    public float GetHullHealthMax
    {
        get { return hullHealthMax; }
    }
    public float GetHullHealth
    {
        get { return hullHealth; }
    }
    public float GetSailSpeed
    {
        get { return sailSpeed; }
    }
    public float GetReloadRate
    {
        get { return reloadRate; }
    }
    // Update is called once per frame
    void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeSailHealth(-10,0);
            ChangeSailHealth(-10, 1);
            ChangeSailHealth(-10, 2);


        }
    }
}
