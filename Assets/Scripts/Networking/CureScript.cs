using UnityEngine;
using UnityEngine.Networking;


public class CureScript : NetworkBehaviour {

    bool isDropped = true;
	OnlineSceneReferences onlineRef;
	Transform holder = null;

	//safety for disconnects and other unpredictable events
	//bool wasHeldPrevFrame;
	//Vector3 lastPos;

	public static float cureCarrierSpeedDebuff = 0.85f;

	void Start()
	{
		onlineRef = GameObject.Find ("OnlineSceneReferences").GetComponent<OnlineSceneReferences> ();
	}

    void OnTriggerStay(Collider col)
    {
		if (!GetComponent<GameManager> ().phase1Finished)
			return;

        PlayerPickupHitbox hitbox = col.gameObject.GetComponent<PlayerPickupHitbox>();
        if (!hitbox||!isDropped)
            return;

		PlayerRespawn res = hitbox.GetComponentInParent<PlayerRespawn> ();
		if (!res || res.IsDead)
			return;

		CustomOnlinePlayer player = hitbox.GetComponentInParent<CustomOnlinePlayer> ();
		
		player.GetComponent<PlayerFX> ().RpcPlaySound (PlayerFX.PLAYER_SOUNDS.PICKUP_CURE);

        onlineRef.gameManager.cureCarrier = player;
		holder = player.transform;
        isDropped = false;

		//broadcast cure owner to all players
		foreach (CustomOnlinePlayer p in onlineRef.allOnlinePlayers) 
		{
			p.cureisCarriedByAPlayer = true;
			p.currentCureCarrier = player.transform;
		}

		string message = player.ColoredName + " has picked up the cure.";
		PlayerCaptionController.BroadcastCaption (message, 5f);
		//player.hasCure = true;

		//transform.SetParent (player.transform);
		//transform.localPosition = new Vector3 (0, 6, 0);
    }

	void Update()
	{
		if (holder != null) 
		{
			transform.position = holder.position + Vector3.up * 6f;
			//lastPos = holder.position;
			//wasHeldPrevFrame = true;
		} 
		/*
		else 
		{
			if(wasHeldPrevFrame)
			{
				DetachFromHolder();
				wasHeldPrevFrame = false;
			}

		}*/

	}

	public void DetachFromHolder()
	{
		if (holder != null)
			transform.position = holder.transform.position;
		//else
		//	transform.position = lastPos;

		holder = null;
		isDropped = true;

		foreach (CustomOnlinePlayer p in onlineRef.allOnlinePlayers) 
		{
			p.cureisCarriedByAPlayer = false;
			p.currentCureCarrier = this.transform;//doesn't matter what we put here because it wont be used
		}

		PlayerCaptionController.BroadcastCaption (GetComponent<GameManager> ().cureCarrier.ColoredName + " has dropped the cure!",5f);
		GetComponent<GameManager> ().cureCarrier = null;

	}
}
