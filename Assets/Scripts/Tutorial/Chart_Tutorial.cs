using UnityEngine;
using System.Collections;

public class Chart_Tutorial : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		PlayerPickupHitbox hitbox = other.GetComponent<PlayerPickupHitbox> ();
		if (hitbox != null) 
		{

			GameObject.Find ("TutorialManager").GetComponent<TutorialManager> ().GetMessage(TutorialManager.TUTORIAL_EVENTS.GRABBED_CHART);
			Destroy(this.gameObject);

			PlayerFX fx = GameObject.Find ("TutorialManager").GetComponent<TutorialManager> ().player.GetComponent<PlayerFX>();
			fx.EmitMapParticle(4);
			fx.PlaySound(PlayerFX.PLAYER_SOUNDS.PICKUP_SCROLL, false);
			//StartCoroutine(Kill ());
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update 
	void FixedUpdate () {
	
	}
}
