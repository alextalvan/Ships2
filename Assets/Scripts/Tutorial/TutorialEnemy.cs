using UnityEngine;
using System.Collections;

public class TutorialEnemy : MonoBehaviour {

	public TutorialArrow arrow;


	public void OnDeath()
	{
		if(arrow.enemy1 == this.gameObject)
			arrow.enemy1 = null;

		if (arrow.enemy2 == this.gameObject)
			arrow.enemy2 = null;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update 
	void FixedUpdate () {
	
	}
}
