using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


[RequireComponent(typeof(AudioSource))]
public class PlayerFX : NetworkBehaviour
{

    [SerializeField]
    private List<AudioClip> _audioClips = new List<AudioClip>();

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
        FIRE_CANNON1,
        HIT1,
        HIT2,
        COLLISION,
        EXPLOSION,
        LEVEL_UP,
        PICKUP1,
        PICKUP2,
        PICKUP3,
        PICKUP_CURE,
        UPGRADE//,
               //DETECT_CURE
    }


    public void PlaySound(PLAYER_SOUNDS s)
    {
        _source.Stop();
        _source.clip = _audioClips[(int)s];
        _source.Play();
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
                _leftSideSmokes[i].GetComponent<Light>().enabled = true;
                _leftSideSmokes[i].Play();
            }
        }
        else
        {
            for (int i = 0; i < cannonCount; ++i)
            {
                _rightSideSmokes[i].Stop();
                _rightSideSmokes[i].GetComponent<Light>().enabled = true;
                _rightSideSmokes[i].Play();
            }
        }
    }

    [ClientRpc]
    public void RpcEmitCannonSmoke(bool leftSide, int cannonCount)
    {
        EmitCannonSmoke(leftSide, cannonCount);
    }

    public void DoLevelUpParticle()
    {
        _levelUpParticle.Stop();
        _levelUpParticle.Play();
    }

    [ClientRpc]
    public void RpcDoLevelUpParticle()
    {
        DoLevelUpParticle();
    }

    public void SpawnDeathParticle()
    {
        //GameObject skull = (GameObject)
        Instantiate(_deathParticle, transform.position + new Vector3(0, 20, 0), Quaternion.identity);
    }

    [ClientRpc]
    public void RpcSpawnDeathParticle()
    {
        SpawnDeathParticle();
    }

    public void EmitMapParticle(int mapCount)
    {
        _mapPickupParticle.Stop();
        _mapPickupParticle.startSize = CustomOnlinePlayer.distancePerMapPiece * mapCount * 0.5f;
        _mapPickupParticle.Play();
    }

    [ClientRpc]
    public void RpcEmitMapParticle(int mapCount)
    {
        EmitMapParticle(mapCount);
    }
    
    // Use this for initialization
    void Start()
    {
        _source = GetComponent<AudioSource>();
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
