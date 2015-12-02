using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	[SerializeField]
	GameObject player;

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

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (StartShip ());

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
