﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


[RequireComponent(typeof(AudioSource))]
public class PlayerSound : NetworkBehaviour {

	[SerializeField]
	private List<AudioClip> _soundList = new List<AudioClip>();

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
		PlaySound (s);
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
