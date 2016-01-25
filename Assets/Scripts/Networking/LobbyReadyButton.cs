using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//interface that is used for the HUD buttons to trigger networked ready/unready message sending
public class LobbyReadyButton : MonoBehaviour {

	[SerializeField]
	Text targetText;

	bool ready = false;


	public void Toggle()
	{
		if (ready)
			Unready ();
		else
			Ready ();

		ready = !ready;
	}

	void Ready()
	{
		GameObject.Find("OfflineSceneReferences").GetComponent<OfflineSceneReferences>().lobbyPlayer.SendReadyToBeginMessage ();

		targetText.text = "Unready";
	}

	void Unready()
	{
		GameObject.Find("OfflineSceneReferences").GetComponent<OfflineSceneReferences>().lobbyPlayer.SendNotReadyToBeginMessage ();
		targetText.text = "Ready";
	}
}
