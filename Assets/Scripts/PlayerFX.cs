using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

[NetworkSettings(channel = 0, sendInterval = 1e+6f)]
public class PlayerFX : NetworkBehaviour
{
    //[SerializeField]
    //private List<string> _audioEvents = new List<string>();

	[SerializeField]
	private List<AudioClip> _clipList = new List<AudioClip> ();

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

	OnlineSceneReferences onlineRefs;

	[SerializeField]
	float whaleSoundDistanceThreshold = 50f;

	[SerializeField]
	float harborSoundDistanceThreshold = 50f;



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


	public void PlaySound(PLAYER_SOUNDS s, bool _3d)
    {  
		if (_3d)
			_source.spatialBlend = 1.0f;
		else
			_source.spatialBlend = 0.0f;

		/*
		_source.Stop ();
		_source.clip = _clipList [(int)s];
		_source.Play ();
		*/
		_source.PlayOneShot (_clipList [(int)s]);
    }


    [ClientRpc]
    public void RpcPlaySound(PLAYER_SOUNDS s, bool _3d)
    {
        PlaySound(s, _3d);
    }

	[ClientRpc]
	public void RpcPlaySoundForMainPlayer(PLAYER_SOUNDS s, bool _3d)
	{
		if(isLocalPlayer)
			PlaySound(s, _3d);
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
                Light flash = _leftSideSmokes[i].GetComponent<Light>();
                if (flash)
                    flash.enabled = true;
                _leftSideSmokes[i].Play();
            }
        }
        else
        {
            for (int i = 0; i < cannonCount; ++i)
            {
                _rightSideSmokes[i].Stop();
                Light flash = _rightSideSmokes[i].GetComponent<Light>();
                if (flash)
                    flash.enabled = true;
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
		onlineRefs = GameObject.Find ("OnlineSceneReferences").GetComponent<OnlineSceneReferences> ();
    }

	void Update()
	{
		AdjustFmodMusic ();
	}

	[ClientCallback]
	void AdjustFmodMusic()
	{
		if (!isLocalPlayer)
			return;

		if (onlineRefs.whale != null) 
		{
			float whaleParam = ((onlineRefs.whale.transform.position - this.transform.position).magnitude <= whaleSoundDistanceThreshold) ? 1f : 0f;
			onlineRefs.ambience.setParameterValue ("Whale", whaleParam);
		}

		if (onlineRefs.harbor != null) 
		{
			float harborParam = Mathf.Clamp01(1f- (onlineRefs.harbor.transform.position - this.transform.position).magnitude / harborSoundDistanceThreshold) * 10f;
			onlineRefs.ambience.setParameterValue("HarborDistance",harborParam);
		}
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
