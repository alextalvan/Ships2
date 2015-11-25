using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(OnlinePlayerInput))]
public class NetMoveTest : NetworkBehaviour {
	
	Rigidbody _rigid;
	OnlinePlayerInput _input;
	float speed = 5f;

	// Use this for initialization
	void Start () 
	{
		_rigid = GetComponent<Rigidbody> ();
		_input = GetComponent<OnlinePlayerInput> ();
	}

	[Server]
	void UpdateMovement()
	{
		_rigid.velocity = new Vector3 (0, 0, 0);

		if (_input.GetInputValue(OnlinePlayerInput.PlayerControls.FORWARD))
			_rigid.velocity += transform.TransformVector(new Vector3 (0, 0, speed));
		
		if (_input.GetInputValue(OnlinePlayerInput.PlayerControls.BACK))
			_rigid.velocity += transform.TransformVector(new Vector3 (0, 0, -speed));
		
		if (_input.GetInputValue(OnlinePlayerInput.PlayerControls.LEFT))
			_rigid.velocity += transform.TransformVector(new Vector3 (-speed, 0, 0));
		
		if (_input.GetInputValue(OnlinePlayerInput.PlayerControls.RIGHT))
			_rigid.velocity += transform.TransformVector(new Vector3 (speed, 0, 0));
	}

	
	// Update 
	void FixedUpdate () 
	{
		//if (NetworkManager.singleton == null || (NetworkManager.singleton.isNetworkActive && NetworkManager.singleton.IsClientConnected ()))
		//	return;

		UpdateMovement ();
	}
}
