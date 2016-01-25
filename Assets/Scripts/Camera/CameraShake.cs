using UnityEngine;
using System.Collections;

//note that the camera needs to be a child of a container for this script to work
public class CameraShake : MonoBehaviour
{
	// How long the object should shake for.
	public float shakeTimeLeft = 0f;
	private float shakeTotalTime = 1f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;

	
	void Update()
	{
		if (shakeTimeLeft > 0)
		{

			//linear decay curve
			//float decayedShake = shakeAmount * shakeTimeLeft / shakeTotalTime;


			//logarithmic decay curve
			float e = 2.718281f;
			float inverse_e = 0.367879f;
			float x = 1f - shakeTimeLeft / shakeTotalTime;

			float decayedShake = ((e - x * inverse_e * Mathf.Exp(2*x)) * inverse_e) * shakeAmount;


			transform.localPosition = Random.insideUnitSphere * decayedShake;
			shakeTimeLeft -= Time.deltaTime;
		}
		else
		{
			shakeTimeLeft = 0f;
			transform.localPosition = Vector3.zero;
		}
	}


	public void Shake(float duration = 0.375f, float shakeStrength = 1.5f)
	{
		shakeTimeLeft = duration;
		shakeAmount = shakeStrength;
		shakeTotalTime = duration;
	}

	//test 
	/*
	void OnGUI()
	{
		if (GUI.Button (new Rect (Screen.width / 2f, Screen.height / 2f, 64, 32), "Shake"))
			Shake ();
	}*/
}