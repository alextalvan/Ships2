using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SmallDebrisScript : MonoBehaviour
{
    List<Transform> smallChildren = new List<Transform>();

    Rigidbody debrisBody;
    
    // Use this for initialization
    void Start()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                smallChildren.Add(transform.GetChild(i));
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
        else debrisBody = GetComponent<Rigidbody>();
        debrisBody.useGravity = true;
        debrisBody.isKinematic = false;
        //debrisBody.AddForce(((transform.up + transform.position) * 10) * debrisBody.mass);
        debrisBody.AddExplosionForce(250, transform.position, 50, 25);

        name = "Debris";
        transform.parent = null;
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        if (col.transform.name != "Debris")
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {

    }
}
