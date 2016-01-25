using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

//interpolation of the transform over the network
[NetworkSettings(channel = 0, sendInterval = 1e+6f)]
public class OnlineTransform : NetworkBehaviour {

	//send rate of snapshots in seconds
	[SerializeField]
	float _sendRate = 0.05f;

	//value used in the lerp functions
	[SerializeField]
	float _interpRate = 0.1f;

	//the threshold at which interpolation is not executed and the transform is hard-set instead
	[SerializeField]
	float _snapThreshold = 20f;

	Vector3 prevPosition;
	Vector3 newPosition;

	Quaternion prevRotation;
	Quaternion newRotation;


	public bool syncRotation = true;

	[SerializeField]
	bool disableRigidbody = true;

	[SerializeField]
	bool disableCollider = true;

	//server
	//float nextTime = 0.0f;
	float _timeAccumulator = 0f;

	public void SetInterp(float value)
	{
		//value = Mathf.Clamp01 (value);
		_interpRate = value;
	}

	//on the clients the collider and rigidbody are generally disabled to prevent interference with the networked data
	void Awake()
	{
		
		if (NetworkManager.singleton != null && NetworkManager.singleton.IsClientConnected())
		{
			if(disableRigidbody && GetComponent<Rigidbody>())
				GetComponent<Rigidbody>().isKinematic = true;

			if(disableCollider && GetComponent<Collider>())
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
