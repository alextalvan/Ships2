using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


[RequireComponent(typeof(AudioSource))]
public class PlayerFX : NetworkBehaviour {

	[SerializeField]
	private List<AudioClip> _soundList = new List<AudioClip>();

	[SerializeField]
	private List<ParticleSystem> _leftSideSmokes = new List<ParticleSystem> ();

	[SerializeField]
	private List<ParticleSystem> _rightSideSmokes = new List<ParticleSystem> ();

	AudioSource _source;

	public enum PLAYER_SOUNDS
	{
		FIRE_CANNON1,
		FIRE_CANNON2,
		LEVEL_UP,
		PICKUP1,
		PICKUP2,
		PICKUP3,
		UPGRADE1,
		UPGRADE2,
		UPGRADE3,
		PICKUP_CURE
	}


	public void PlaySound(PLAYER_SOUNDS s)
	{
		_source.Stop ();
		_source.clip = _soundList [(int)s];
		_source.Play ();
	}

	[ClientRpc]
	public void RpcPlaySound(PLAYER_SOUNDS s)
	{
		//if(isLocalPlayer)
		PlaySound (s);
	}

	public void CameraShake(float duration, float strength)
	{
		Camera.main.GetComponent<CameraShake> ().Shake (duration, strength);
	}

	[ClientRpc]
	public void RpcCameraShake(float duration, float strength)
	{
		if(isLocalPlayer)
			CameraShake (duration, strength);
	}

	[ClientRpc]
	public void RpcEmitCannonSmoke(bool leftSide, int cannonCount)
	{
		if (leftSide) 
		{
			for (int i=0; i<cannonCount; ++i) 
			{
				_leftSideSmokes [i].Stop ();
				_leftSideSmokes [i].Play ();
			}
		} 
		else 
		{
			for (int i=0; i<cannonCount; ++i) 
			{
				_rightSideSmokes [i].Stop ();
				_rightSideSmokes [i].Play ();
			}
		}
	}


	// Use this for initialization
	void Start () 
	{
		_source = GetComponent<AudioSource> ();
	}


	//test
	/*
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.P))
			PlaySound (PLAYER_SOUNDS.FIRE_CANNON1);
	}
	*/
}
