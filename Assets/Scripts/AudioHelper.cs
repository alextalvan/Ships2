using UnityEngine;
using System.Collections;

public class AudioHelper : MonoBehaviour {

	static public void PlayAt(AudioClip clip, Vector3 position)
	{
		GameObject i = (GameObject)Instantiate (Resources.Load ("TempAudio"), position, Quaternion.identity);
		i.GetComponent<AudioSource> ().clip = clip;
		i.GetComponent<AudioSource> ().Play ();
	}
}
