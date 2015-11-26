using UnityEngine;
using System.Collections;


//@ Mast can be destroyed once the health is <50%  (try to get clarity on how)
public class SailScript : MonoBehaviour
{
    //ship index from 0-2!!!
    [SerializeField]
    int sailIndex;

    Renderer rend;

    float dissolveAmount;
    float scaledDisolve;

    [SerializeField]
    ShipAttributes myAttributes;


    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DegradeSail();
    }

    //visual destruction of sail
    public void DegradeSail()
    {
        rend.material.shader = Shader.Find("Shader Forge/SailDamage");
        rend.material.SetFloat("_Dissolveamount", (1 - (myAttributes.GetSailHealth[sailIndex] / myAttributes.GetSailHealthMax)));        
    }

    public void liftSail()
    {
        myAttributes.SetSailsLifted(true);
        myAttributes.ChangeSailSpeed(-myAttributes.GetSailSpeed);

        //visuals

    }
    public void dropSail()
    {
        myAttributes.SetSailsLifted(false);

        //visuals
    }
    //indetify which sail
    public int GetSailIndex
    {
        get { return sailIndex; }
    }
}
