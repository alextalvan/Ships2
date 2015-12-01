using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using FMODUnity;
using FMOD;

//[RequireComponent(typeof(StudioEventEmitter))]
public class PlayerFX : NetworkBehaviour {

	[SerializeField]
	private List<string> _soundList = new List<string>();

	[SerializeField]
	private List<ParticleSystem> _leftSideSmokes = new List<ParticleSystem> ();

	[SerializeField]
	private List<ParticleSystem> _rightSideSmokes = new List<ParticleSystem> ();

	//StudioEventEmitter _source;

	public enum PLAYER_SOUNDS
	{
		FIRE_CANNON,
		HIT,
		COLLISION,
		LEVEL_UP,
		PICKUP,
		UPGRADE//,
		//DETECT_CURE
	}


	public void PlaySound(PLAYER_SOUNDS s, string[] paramNames = null, float[] paramValues = null)
	{
		FMOD.Studio.EventInstance sndInst = FMODUnity.RuntimeManager.CreateInstance (_soundList [(int)s]);

		if(paramNames!=null && paramValues !=null)
			for(int i=0;i<paramNames.Length;++i)
			{
				sndInst.setParameterValue (paramNames[i], paramValues[i]);
			}


		FMOD.ATTRIBUTES_3D a = new FMOD.ATTRIBUTES_3D();
		sndInst.get3DAttributes (out a);
		a.position = transform.position.ToFMODVector();
		sndInst.set3DAttributes (a);
		sndInst.start ();
		sndInst.release ();
	}

	public void PlaySound(PLAYER_SOUNDS s, string pName, float pValue)
	{
		FMOD.Studio.EventInstance i = FMODUnity.RuntimeManager.CreateInstance (_soundList [(int)s]);
		i.setParameterValue (pName, pValue);
		FMOD.ATTRIBUTES_3D a = new FMOD.ATTRIBUTES_3D();
		i.get3DAttributes (out a);
		a.position = transform.position.ToFMODVector();
		i.set3DAttributes (a);
		i.start ();
		i.release ();


	}

	[ClientRpc]
	public void RpcPlaySoundWithParams(PLAYER_SOUNDS s, string[] paramNames, float[] paramValues)
	{
		PlaySound (s, paramNames, paramValues);
	}

	[ClientRpc]
	public void RpcPlaySoundWithParam(PLAYER_SOUNDS s, string pName, float pValue)
	{
		PlaySound (s, pName, pValue);
	}

	[ClientRpc]
	public void RpcPlaySound(PLAYER_SOUNDS s)
	{
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
		//_source = GetComponent<StudioEventEmitter> ();
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
