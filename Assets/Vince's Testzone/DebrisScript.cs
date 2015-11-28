using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Turns Ship's Add's into a debri (rigidbody object with force applied)
public class DebrisScript : MonoBehaviour
{
    Rigidbody debrisBody;
    List<Transform> smallChildren = new List<Transform>();
    float existanceTime;

    void Awake()
    {
        if (transform.CompareTag("Hull") || transform.CompareTag("Mast"))
        {
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

            if (transform.CompareTag("Hull"))
            {
                //wood particle
                //i believe it's magic
                debrisBody.AddExplosionForce(390, transform.position, 200, 0.5f);
                debrisBody.AddForce(-transform.forward * 100);
                debrisBody.AddRelativeTorque(5, 5, 5);
            }
            else if (transform.CompareTag("Mast"))
            {
                //so magic
                //sail particle
                debrisBody.AddForce(transform.forward * 10);
                debrisBody.AddRelativeTorque(70, 5, 5);
            }

            transform.parent = null;
            transform.tag = "Untagged";
            name = "Debris";
        }
    }
    void FixedUpdate() {
        //existanceTime += Time.fixedDeltaTime;

        //if (transform.position.y < 0 || existanceTime >= 5 && transform.CompareTag("Hull"))
        //{
        //    Destroy(this.gameObject);
        //}
        //if (transform.position.y < 0 || existanceTime >= 10 && transform.CompareTag("Mast"))
        //{
        //    Destroy(this.gameObject);
        //}

    }

}
