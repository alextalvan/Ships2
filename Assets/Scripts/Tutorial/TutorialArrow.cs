using UnityEngine;
using System.Collections;

public class TutorialArrow : MonoBehaviour {


	public GameObject cure;
	public GameObject enemy1;
	public GameObject enemy2;

	public TUTORIAL_ARROW_STATE currentState = TUTORIAL_ARROW_STATE.OFF;

	public enum TUTORIAL_ARROW_STATE
	{
		CURE,
		ENEMIES,
		OFF
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update 
	void FixedUpdate () 
	{
		GetComponentInChildren<Renderer> ().enabled = true;

		if (currentState == TUTORIAL_ARROW_STATE.CURE && cure != null) {
			GetComponentInChildren<Renderer> ().material.color = Color.yellow;
			transform.rotation = Quaternion.LookRotation (cure.transform.position - this.transform.position);
			return;
		}

		if(currentState == TUTORIAL_ARROW_STATE.ENEMIES && enemy1 !=null) {
			GetComponentInChildren<Renderer> ().material.color = Color.red;
			transform.rotation = Quaternion.LookRotation (enemy1.transform.position - this.transform.position);
			return;
		}

		if(currentState == TUTORIAL_ARROW_STATE.ENEMIES && enemy2 !=null) {
			GetComponentInChildren<Renderer> ().material.color = Color.red;
			transform.rotation = Quaternion.LookRotation (enemy2.transform.position - this.transform.position);
			return;
		}

		GetComponentInChildren<Renderer> ().enabled = false;
	}
}
