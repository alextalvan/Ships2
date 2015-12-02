using UnityEngine;
using System.Collections;

public class ConstantRotation : MonoBehaviour {


    [SerializeField]
    Vector3 step;
	// Use this for initialization
	
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles + step);
        
	}
}
