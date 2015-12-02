using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class RespawnPointsManager : MonoBehaviour {

	[SerializeField]
	List<RespawnPoint> _points = new List<RespawnPoint>();

	List<RespawnPoint> _initPoints;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update 
	void FixedUpdate () {
	
	}

	public Vector3 GetRespawnPoint()
	{
		List<RespawnPoint> _temp = new List<RespawnPoint> ();
		foreach (RespawnPoint p in _points) 
		{
			if(p.CapturedObjectsCount==0)
				_temp.Add(p);
		}

		Vector3 chosen = _temp [Random.Range (0, _temp.Count)].transform.position;
		//Debug.Log (chosen + " was chosen as respawn location");


		if (_temp.Count > 0)
			return chosen;
		else
			return Vector3.zero;
		//return 
	}

	public void SetupInitialSpawnPoints()
	{
		_initPoints = new List<RespawnPoint> ();
		_initPoints.AddRange (_points);
	}

	public GameObject GetInitialSpawnPoint()
	{
		int index = Random.Range (0, _initPoints.Count);
		//Debug.Log (_initPoints.Count + " possible SpawnPoints");
		GameObject ret = _initPoints [index].gameObject;
		_initPoints.RemoveAt (index);
		return ret;
	}
}
