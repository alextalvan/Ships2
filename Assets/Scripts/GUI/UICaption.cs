using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text),typeof(CanvasRenderer))]
public class UICaption : MonoBehaviour 
{
	Text _text;
	float _timeLeft;
	float _storedDuration = 1f;

	void Awake()
	{
		_text = GetComponent<Text> ();
	}

	public void PushCaption(string text, float duration)
	{
		_text.text = text;
		_storedDuration = _timeLeft = duration;
	}

	void Update()
	{
		_timeLeft -= Time.deltaTime;
		GetComponent<CanvasRenderer>().SetAlpha(Mathf.Clamp01 (_timeLeft / _storedDuration));
	}
	
}
