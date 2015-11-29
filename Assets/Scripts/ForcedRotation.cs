using UnityEngine;
using System.Collections;

public class ForcedRotation : MonoBehaviour {

	void Update()
	{
		transform.rotation = Quaternion.identity;
	}
}
