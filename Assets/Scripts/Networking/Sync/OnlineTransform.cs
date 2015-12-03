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


	public bool syncRotation = true;

	//server
	//float nextTime = 0.0f;
	float _timeAccumulator = 0f;

	public void SetInterp(float value)
	{
		//value = Mathf.Clamp01 (value);
		_interpRate = value;
	}

	void Awake()
	{
		
		if (NetworkManager.singleton != null && NetworkManager.singleton.isNetworkActive && !NetworkServer.active)
		{
			if(GetComponent<Rigidbody>())
				GetComponent<Rigidbody>().isKinematic = true;

			if(GetComponent<Collider>())
				GetComponent<Collider>().enabled = false;
			return;
		}
	}

	// Use this for initialization
	void Start () 
	{
		prevPosition = newPosition = transform.position;
		prevRotation = newRotation = transform.rotation;

		PushSnapshot ();
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

		if (syncRotation) 
		{
			prevRotation = Quaternion.Lerp (prevRotation, newRotation, _interpRate);
			transform.rotation = prevRotation;
		}
	}

	[ServerCallback]
	void PushSnapshot()
	{
		_timeAccumulator += Time.deltaTime;
		if(_timeAccumulator >= _sendRate) 
		{
			_timeAccumulator %= _sendRate;
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
