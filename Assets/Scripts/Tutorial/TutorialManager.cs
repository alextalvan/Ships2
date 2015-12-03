using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	[SerializeField]
	GameObject player;

	TutorialShipScript ship;

	[SerializeField]
	GUIDialog tutorialDialog;

	[SerializeField]
	Text tutorialDialogText;

	[SerializeField]
	Button continueButton;

	TUTORIAL_STATES currentState = TUTORIAL_STATES.START;

	enum TUTORIAL_STATES
	{
		START = 0,
		WINCOND_EXPLAIN,
		WASD
	}

	public enum TUTORIAL_EVENTS
	{
		GRABBED_CURE
	}

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (StartShip ());
		ship = player.GetComponent<TutorialShipScript> ();
		SetText("Welcome to the [game name] tutorial.");
	}
	
	// Update 
	void Update () 
	{
	
	}

	void SetText(string s)
	{
		tutorialDialogText.text = s;
	}

	void SetState(TUTORIAL_STATES s)
	{
		switch (s) 
		{
		case TUTORIAL_STATES.WINCOND_EXPLAIN:
			SetText("In [game name], the primary objective is to grab the <color=yellow>CURE</color> " +
			        "and hold it for [x] minutes. The first player to do this wins the game!");
			continueButton.enabled = true;
			break;

		case TUTORIAL_STATES.WASD:
			SetText("Use the keys W, A, S, and S to move. Pick up the <color=yellow>CURE</color> to continue.");
			continueButton.enabled = false;
			ship.MovementLock = false;
			break;
		default:
			break;
		}
	}

	public void GetMessage(TUTORIAL_EVENTS e)
	{
		switch (e) 
		{
		case TUTORIAL_EVENTS.GRABBED_CURE:
			break;
		default:
			break;
		}
	}

	public void ContinueButton()
	{
		switch (currentState) 
		{
		case TUTORIAL_STATES.START:
			SetState(TUTORIAL_STATES.WINCOND_EXPLAIN);
			return;
		case TUTORIAL_STATES.WINCOND_EXPLAIN:
			SetState(TUTORIAL_STATES.WASD);
			return;
		default:
			return;
		}
	}

	IEnumerator StartShip()
	{
		yield return new WaitForSeconds(5);
		player.SetActive (true);
		tutorialDialog.Enable ();
	}
}
