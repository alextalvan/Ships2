using UnityEngine;
using System.Collections;

//used on the server to spectate and enable/disable audio,rendering
public class ServerControlPanel : MonoBehaviour {

	[SerializeField]
	GameObject cameraTarget;

	[SerializeField]
	LayerMask layerMask;

	//rendering 
	private int _cachedCameraMask;
	private bool _renderingOn = false;

	//audio
	private bool _audioOn = false;

	// Use this for initialization
	void Start () 
	{
		_cachedCameraMask = Camera.main.cullingMask;
		Camera.main.farClipPlane = 2000f;
		Camera.main.cullingMask = layerMask.value;

		Camera.main.GetComponent<AudioListener> ().enabled = false;
		MusicManager.Singleton.Music.setPaused (true);
		GameObject.Find ("OnlineSceneReferences").GetComponent<OnlineSceneReferences> ().ambience.setPaused (true);
	}

	void Update()
	{

	}

	public void ToggleAudio()
	{
		_audioOn = !_audioOn;

		Camera.main.GetComponent<AudioListener> ().enabled = _audioOn;
		MusicManager.Singleton.Music.setPaused (!_audioOn);
		GameObject.Find ("OnlineSceneReferences").GetComponent<OnlineSceneReferences> ().ambience.setPaused (!_audioOn);
	}

	public void ToggleRendering()
	{
		_renderingOn = !_renderingOn;

		if (_renderingOn)
			Camera.main.cullingMask = _cachedCameraMask;
		else
			Camera.main.cullingMask = layerMask.value;
	}
	
	// Update 
	void FixedUpdate () 
	{
		Vector3 cameraHorizDir = new Vector3 (Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z).normalized;
		
		Vector3 movement = Vector3.zero;
		
		if (Input.GetKey (KeyCode.W))
			movement += cameraHorizDir * 5f;
		if (Input.GetKey (KeyCode.S))
			movement -= cameraHorizDir * 5f;
		if (Input.GetKey (KeyCode.A))
			movement += (new Vector3(-cameraHorizDir.z,0f,cameraHorizDir.x)) * 5f;
		if (Input.GetKey (KeyCode.D))
			movement -= (new Vector3(-cameraHorizDir.z,0f,cameraHorizDir.x)) * 5f;
		if (Input.GetKey (KeyCode.Space))
			movement += Vector3.up * 5f;
		if (Input.GetKey (KeyCode.LeftControl))
			movement -= Vector3.up * 5f;
		
		cameraTarget.transform.position += movement;
	}
}
