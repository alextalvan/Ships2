using UnityEngine;
using System.Collections;

public class CameraScript2 : MonoBehaviour 
{
	[SerializeField]
	GameObject target;

	float horizRotation = 0f;

	[SerializeField]
	float rotationSpeed = 2f;

	[SerializeField]
	float heightOffset;

	[SerializeField]
	float horizontalOffset;

	[SerializeField]
	float verticalAngle = 15f;

	[SerializeField]
	GameObject reserve;

	//float rotDelta;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update 
	void Update () 
	{
		horizRotation -= rotationSpeed * 0.012f * PollMouseAxis ();

		//rotDelta *= 0.6f;

		//horizRotation += rotDelta;

		Vector3 offset = new Vector3 (Mathf.Cos (horizRotation) * horizontalOffset, heightOffset, Mathf.Sin (horizRotation) * horizontalOffset);

		transform.position = target.transform.position + offset;
		transform.LookAt (target.transform.position);
		transform.Rotate (verticalAngle, 0f, 0f);
	}

	public void AttachTo(GameObject obj)
	{
		target = obj;
	}

	public void Detach()
	{
		reserve.transform.position = target.transform.position;
		target = reserve;
	}


	float PollMouseAxis()
	{
		return Input.GetAxis("Mouse X");
	}
}
