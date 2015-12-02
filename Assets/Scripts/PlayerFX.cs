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

    private List<ParticleSystem> _leftSideWaves = new List<ParticleSystem>();
    private List<ParticleSystem> _rightSideWaves = new List<ParticleSystem>();

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

    [ClientRpc]
    public void RpcEmitCannonSmoke(bool leftSide, int cannonCount)
    {
        if (leftSide)
        {
            for (int i = 0; i < cannonCount; ++i)
            {
                _leftSideSmokes[i].Stop();
                _leftSideWaves[i].Stop();
                _leftSideSmokes[i].Play();
                _leftSideWaves[i].Play();
            }
        }
        else
        {
            for (int i = 0; i < cannonCount; ++i)
            {
                _rightSideSmokes[i].Stop();
                _rightSideWaves[i].Stop();
                _rightSideSmokes[i].Play();
                _rightSideWaves[i].Play();
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        _source = GetComponent<AudioSource>();

        foreach (ParticleSystem smoke in _leftSideSmokes)
            _leftSideWaves.Add(smoke.transform.GetChild(0).GetComponent<ParticleSystem>());
        foreach (ParticleSystem smoke in _rightSideSmokes)
            _rightSideWaves.Add(smoke.transform.GetChild(0).GetComponent<ParticleSystem>());
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
