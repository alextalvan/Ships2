using UnityEngine;
using System.Collections;

public class KillPlayerInstantly : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		HullOnline h = other.GetComponent<HullOnline> ();
		if (h != null) 
		{
			h.Damage(Vector3.zero,300,1);
		}
	}
}
