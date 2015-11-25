using UnityEngine;
using System.Collections;


//@TODO figure out what to do with health for sails
public class ShipAttributes : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
	}


    //watch related health stuff
    float sailHealth;
    float sailHealthMax = 100;
    float damage;
    float turningSpeed;
    float baseSpeed;
    float sailSpeed;
    float sailSpeedMax;

    int cannons;

    //Change em

    public void ChangeTurningSpeed(float Modifier)
    {
        turningSpeed += Modifier;
    }
    public void ChangeDamage(float Modifier)
    {
        damage += Modifier;
    }
    public void ChangeSailHealthMax(float Modifier)
    {
        sailHealthMax += Modifier;
    }
    public void ChangeSailSpeedMax(float Modifier)
    {
        sailSpeedMax += Modifier;
    }
    public void ChangeSailSpeed(float Modifier)
    {
        sailSpeed += Modifier;
    }
    public void ChangeSailHealth(float Modifier, int Index)
    {
        sailHealth += Modifier;
    }
    public void ChangeCannons(int Modifier)
    {
        cannons += Modifier;
    }

    //Get em
    public float GetSailHealth {
        get { return sailHealth; }
    }
    public float GetSailHealthMax
    {
        get { return sailHealthMax; }
    }
    public float GetSailSpeedMax
    {
        get { return sailSpeedMax; }
    }
    public float GetSailSpeed
    {
        get { return sailSpeed; }
    }
    public float GetDamage
    {
        get { return damage; }
    }
    public float GetTurningSpeed
    {
        get { return turningSpeed; }
    }
    public int GetCannons
    {
        get { return cannons; }
    }
 

    // Update is called once per frame
    void Update () {
	
	}
}
