using UnityEngine;
using System.Collections;

public class TutorialCure : MonoBehaviour {

	bool attached = false;

	public TutorialArrow arrow;

	void OnTriggerEnter(Collider other)
	{
		PlayerPickupHitbox hitbox = other.GetComponent<PlayerPickupHitbox> ();
		if (hitbox != null) 
		{
			attached = true;
			GameObject.Find ("TutorialManager").GetComponent<TutorialManager> ().GetMessage(TutorialManager.TUTORIAL_EVENTS.GRABBED_CURE);
			GameObject.Find ("TutorialManager").GetComponent<TutorialManager> ().player.GetComponent<PlayerFX>().PlaySound(PlayerFX.PLAYER_SOUNDS.PICKUP_CURE);
			StartCoroutine(Kill ());
		}
	}

	// Use this for initialization
	void Start () 
	{
		GetComponentInChildren<Renderer> ().enabled = false;
	}
	
	// Update 
	void Update () 
	{
		bool inRange = ((GameObject.Find ("TutorialManager").GetComponent<TutorialManager> ().player.transform.position - transform.position).magnitude <= 100f);

		GetComponentInChildren<Renderer> ().enabled = inRange;

		if (inRange && !attached)
			arrow.currentState = TutorialArrow.TUTORIAL_ARROW_STATE.CURE;
		else
			arrow.currentState = TutorialArrow.TUTORIAL_ARROW_STATE.OFF;

		if (attached)
			transform.position = GameObject.Find ("TutorialManager").GetComponent<TutorialManager> ().player.transform.position + new Vector3 (0, 20, 0);
	}


	IEnumerator Kill()
	{
		yield return new WaitForSeconds (5);
		Destroy (this.gameObject);
	}
}
