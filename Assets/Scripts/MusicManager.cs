using UnityEngine;
using System.Collections;

public class MusicManager  
{
	private FMOD.Studio.EventInstance music;

	public FMOD.Studio.EventInstance Music { get { return music; } }

	private MusicManager ()
	{
		music = FMOD_StudioSystem.instance.GetEvent ("event:/Music");
		music.start ();
	}

	~MusicManager()
	{
		music.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
		music.release ();
	}
	
	static private MusicManager _instance;

	public void Init()
	{
	}

	static public MusicManager Singleton 
	{
		get 
		{ 
			if(_instance==null)
				_instance = new MusicManager();

			return _instance;
		}
	}

}
