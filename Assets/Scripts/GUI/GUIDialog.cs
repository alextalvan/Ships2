using UnityEngine;
using System.Collections;

public class GUIDialog : MonoBehaviour 
{

	public bool isEnabled = false;

	void Start()
	{
		/*
		if (isEnabled)
			Enable ();
		else
			Disable ();
			*/
	}

	public void Disable()
	{
		gameObject.SetActive (false);
	}

	public void Enable()
	{
		gameObject.SetActive (true);
	}

	public void PushToTop()
	{
		transform.SetParent (transform.parent);
	}
}
