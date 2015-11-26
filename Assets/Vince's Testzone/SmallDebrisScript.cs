using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SmallDebrisScript : MonoBehaviour
{
    List<Transform> smallChildren = new List<Transform>();
    float existanceTime;
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

        //magic as fuck
        debrisBody.AddExplosionForce(390, transform.position, 200, 0.5f);
        debrisBody.AddForce(-transform.forward * 10);
        debrisBody.AddRelativeTorque(5, 5, 5);

        name = "Debris";
        transform.parent = null;
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        if (col.transform.name != "Debris")
        {
            //Destroy(this.gameObject);
        }
    }
    void FixedUpdate()
    {
        existanceTime += Time.fixedDeltaTime;

        if (transform.position.y < 0 || existanceTime >= 5)
        {
            Destroy(this.gameObject);
        }
    }
}
