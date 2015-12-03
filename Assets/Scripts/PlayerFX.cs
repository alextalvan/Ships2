using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


public class PlayerFX : NetworkBehaviour
{
    [SerializeField]
    private List<string> _audioEvents = new List<string>();

	[SerializeField]
	private List<AudioClip> _spatialSounds = new List<AudioClip> ();

    [SerializeField]
    private List<ParticleSystem> _leftSideSmokes = new List<ParticleSystem>();

    [SerializeField]
    private List<ParticleSystem> _rightSideSmokes = new List<ParticleSystem>();

	[SerializeField]
	private ParticleSystem _levelUpParticle;

	[SerializeField]
	private GameObject _deathParticle;

	[SerializeField]
	private ParticleSystem _mapPickupParticle;

	AudioSource _source;


    public enum PLAYER_SOUNDS
    {
        SHOOT,
        HIT,
        COLLISION,
        EXPLOSION,
        LEVEL_UP,
		PICKUP,
		PICKUP_SCROLL,
        PICKUP_CURE,
        UPGRADE,
		RESPAWN,
		SINK,
		WIN,
    }


	public void PlaySound(PLAYER_SOUNDS s)
    {  

		//return;
		//FMOD_StudioEventEmitter em = GetComponent<FMOD_StudioEventEmitter> ();
		//em.
		//first parameter checks
		FMOD.Studio.EventInstance e = FMOD_StudioSystem.instance.GetEvent (_audioEvents [(int)s]);
		//FMOD.Studio.ParameterInstance p;

		switch (s) 
		{
		case PLAYER_SOUNDS.PICKUP:
			e.setParameterValue("Type",0);
			e.start ();
			break;
		case PLAYER_SOUNDS.PICKUP_SCROLL:
			e.setParameterValue("Type",1);
			e.start ();
			break;
		case PLAYER_SOUNDS.PICKUP_CURE:
			e.setParameterValue("Type",2);
			e.start ();
			break;
		case PLAYER_SOUNDS.RESPAWN:
			e.setParameterValue("State",0);
			e.start ();
			break;
		case PLAYER_SOUNDS.SINK:
			e.setParameterValue("State",1);
			e.start ();
			break;
		case PLAYER_SOUNDS.WIN:
			e.setParameterValue("State",2);
			e.start ();
			break;
		case PLAYER_SOUNDS.LEVEL_UP:
		case PLAYER_SOUNDS.UPGRADE:
			e.start ();
			break;
		case PLAYER_SOUNDS.SHOOT:
			_source.Stop();
			_source.clip = _spatialSounds[0];
			_source.Play ();
			break;
		case PLAYER_SOUNDS.HIT:
			_source.Stop();
			_source.clip = _spatialSounds[1];
			_source.Play ();
			break;
		case PLAYER_SOUNDS.COLLISION:
			_source.Stop();
			_source.clip = _spatialSounds[2];
			_source.Play ();
			break;
		case PLAYER_SOUNDS.EXPLOSION:
			_source.Stop();
			_source.clip = _spatialSounds[3];
			_source.Play ();
			break;
		}
		
        //FMOD_StudioSystem.instance.PlayOneShot(_audioEvents[(int)s],transform.position);
    }


    [ClientRpc]
    public void RpcPlaySound(PLAYER_SOUNDS s)
    {
        PlaySound(s);
    }

    public void CameraShake(float duration, float strength)
    {
        Camera.main.GetComponent<CameraShake>().Shake(duration, strength);
    }

    [ClientRpc]
    public void RpcCameraShake(float duration, float strength)
    {
        if (isLocalPlayer)
            CameraShake(duration, strength);
    }

    
    public void EmitCannonSmoke(bool leftSide, int cannonCount)
    {
        if (leftSide)
        {
            for (int i = 0; i < cannonCount; ++i)
            {
                _leftSideSmokes[i].Stop();
                _leftSideSmokes[i].Play();
            }
        }
        else
        {
            for (int i = 0; i < cannonCount; ++i)
            {
                _rightSideSmokes[i].Stop();
                _rightSideSmokes[i].Play();
            }
        }
    }

	[ClientRpc]
	public void RpcEmitCannonSmoke(bool leftSide, int cannonCount)
	{
		EmitCannonSmoke (leftSide, cannonCount);
	}
		
	public void DoLevelUpParticle()
	{
		_levelUpParticle.Stop ();
		_levelUpParticle.Play ();
	}

	[ClientRpc]
	public void RpcDoLevelUpParticle()
	{
		DoLevelUpParticle ();
	}

	public void SpawnDeathParticle()
	{
		//GameObject skull = (GameObject)
		Instantiate (_deathParticle, transform.position + new Vector3 (0, 20, 0), Quaternion.identity);
	}

	[ClientRpc]
	public void RpcSpawnDeathParticle()
	{
		SpawnDeathParticle ();
	}

	public void EmitMapParticle(int mapCount)
	{
		_mapPickupParticle.Stop ();
		_mapPickupParticle.startSize = CustomOnlinePlayer.distancePerMapPiece * mapCount * 0.5f;
		_mapPickupParticle.Play ();
	}

	[ClientRpc]
	public void RpcEmitMapParticle(int mapCount)
	{
		EmitMapParticle (mapCount);
	}


    // Use this for initialization
    void Start()
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
