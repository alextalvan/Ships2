using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebrisScript : MonoBehaviour
{
    Rigidbody debrisBody;
    List<Transform> smallChildren = new List<Transform>();

    void Awake()
    {

        //If it has Children also turn children into Debris (Used for sails and bigger objects) 
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name == "Small")
                {
                    smallChildren.Add(transform.GetChild(i));
                }
            }
            foreach (Transform child in smallChildren)
            {
                child.gameObject.AddComponent<SmallDebrisScript>();
            }
        }


        if (GetComponent<Rigidbody>() == null)
        {
            debrisBody = gameObject.AddComponent<Rigidbody>();
        }
        debrisBody = gameObject.GetComponent<Rigidbody>();
        debrisBody.useGravity = true;
        debrisBody.isKinematic = false;
        //debrisBody.AddForce(((transform.up + transform.position) * 2) * debrisBody.mass);
        debrisBody.AddExplosionForce(250, transform.position, 50, 25);
        transform.parent = null;
        name = "Debris";
        Destroy(this);


    }
}
