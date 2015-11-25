using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class OnlineTransform : NetworkBehaviour {

	[SerializeField]
	float _sendRate = 0.05f;
	[SerializeField]
	float _interpRate = 0.1f;

	[SerializeField]
	float _snapThreshold = 20f;

	Vector3 prevPosition;
	Vector3 newPosition;

	Quaternion prevRotation;
	Quaternion newRotation;

	//server
	//float nextTime = 0.0f;
	float _timeAccumulator = 0.0f;

	public void SetInterp(float value)
	{
		//value = Mathf.Clamp01 (value);
		_interpRate = value;
	}

	void Awake()
	{
		
		if (NetworkManager.singleton != null && NetworkManager.singleton.isNetworkActive && !NetworkServer.active)
		{
			GetComponent<Rigidbody>().isKinematic = true;
			GetComponent<Collider>().enabled = false;
			return;
		}
	}

	// Use this for initialization
	void Start () 
	{
		prevPosition = newPosition = transform.position;
		prevRotation = newRotation = transform.rotation;
	}

	void Update()
	{
		PushSnapshot ();
		UpdateTransform ();
	}
	
	// Update 
	void FixedUpdate () 
	{
		//UpdateTransform ();
	}

	[ClientCallback]
	void UpdateTransform()
	{

		if ((newPosition - prevPosition).magnitude >= _snapThreshold)
			prevPosition = newPosition;
		else
			prevPosition = Vector3.Lerp (prevPosition, newPosition, _interpRate);

		transform.position = prevPosition;

		prevRotation = Quaternion.Lerp (prevRotation, newRotation, _interpRate);
		transform.rotation = prevRotation;
	}

	[ServerCallback]
	void PushSnapshot()
	{
		_timeAccumulator += Time.deltaTime;
		while (_timeAccumulator >= _sendRate) 
		{
			_timeAccumulator -= _sendRate;
			SendSnapshot();
		}

		/*
		if (nextTime < Time.time) 
		{
			SendSnapshot ();
			nextTime = Time.time + _sendRate;
		}
		*/
	}

	[Server]
	void SendSnapshot()
	{
		RpcReceiveSnapshot (transform.position,transform.rotation);
	}


	[ClientRpc]
	void RpcReceiveSnapshot(Vector3 position, Quaternion rotation)
	{

		//prevPosition = newPosition;
		newPosition = position;

		//prevRotation = newRotation;
		newRotation = rotation;
	}
	
}
