using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	//[SerializeField]
	public GameObject player;

	TutorialShipScript ship;

	[SerializeField]
	GUIDialog tutorialDialog;

	[SerializeField]
	Text tutorialDialogText;

	[SerializeField]
	Button continueButton;

	[SerializeField]
	GameObject cure;

	[SerializeField]
	GameObject chart;

	[SerializeField]
	GameObject enemy1;
	[SerializeField]
	GameObject enemy2;

	[SerializeField]
	TutorialArrow arrow;

	[SerializeField]
	TutorialUpgradeScreen screen;

	[SerializeField]
	string menuScene;

	TUTORIAL_STATES currentState = TUTORIAL_STATES.START;

	enum TUTORIAL_STATES
	{
		START = 0,
		WINCOND_EXPLAIN,
		WASD,
		SCROLL_INTRO,
		COMBAT_INTRO,
		COMBAT_AIMING,
		LEVEL_INTRO,
		END
	}

	public enum TUTORIAL_EVENTS
	{
		GRABBED_CHART,
		GRABBED_CURE,
		PRESSED_TAB,
		LEVELED_UP,
		UPGRADED
	}

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (StartShip ());
		ship = player.GetComponent<TutorialShipScript> ();
		SetText("Welcome aboard, Captain! Press <color=red>Continue</color> to proceed" );
		ship.CombatLock = true;
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
			SetText("The Goal of this game is to find" + " <color=red>the CURE</color> and hold it on your ship for  <color=red>2 minutes</color>");
			continueButton.gameObject.SetActive(true);
			break;

		case TUTORIAL_STATES.WASD:
			SetText("Use <color=red> W, A, S, and D </color> to move you ship. Pick up the <color=white> CHART PIECE</color> to go further");
			//Instantiate(curePrefab);
			tutorialDialog.transform.localPosition = new Vector3(587,-350,0);
			continueButton.gameObject.SetActive(false);
			ship.MovementLock = false;
			chart.SetActive(true);
			//ship.Freeze();
			break;

		case TUTORIAL_STATES.SCROLL_INTRO:
			SetText("Nice Job, Cap! Now you can detect <color=red>the CURE</color>. Find <color=red>the CURE</color> in this area.\nThe <color=yellow>Yellow Arrow</color> will show you the way");
			ship.Freeze();
			ship.MovementLock = false;
			arrow.currentState = TutorialArrow.TUTORIAL_ARROW_STATE.CURE;
			cure.SetActive(true);
			break;


		case TUTORIAL_STATES.COMBAT_INTRO:
			SetText("Enemy ships on a horizon, Cap! Press <color=red>TAB</color> to switch ammo type!");
			//continueButton.gameObject.SetActive(true);
			ship.CombatLock = true;
			ship.Freeze();
			break;

		case TUTORIAL_STATES.COMBAT_AIMING:
			SetText("Destroy <color=red>Enemy Ships</color>! <color=white> Hold and then release </color> <color=red> SPACE </color> to shoot!");
			continueButton.gameObject.SetActive(false);
			ship.MovementLock = false;
			ship.CombatLock = false;
			enemy1.SetActive(true);
			enemy2.SetActive(true);
			arrow.currentState = TutorialArrow.TUTORIAL_ARROW_STATE.ENEMIES;
			break;

		case TUTORIAL_STATES.LEVEL_INTRO:
			SetText("Good Fight, Cap! We've just got leveled up! Click <color=red>Right Mouse Button</color> and hover it to upgrade your ship!");
			arrow.currentState = TutorialArrow.TUTORIAL_ARROW_STATE.OFF;
			screen.locked = false;
			break;

		case TUTORIAL_STATES.END:
			SetText("Now you ready for a real fight, Cap! Good luck!");
			continueButton.gameObject.SetActive(true);
			break;
		default:
			break;
		}

		currentState = s;
	}

	public void GetMessage(TUTORIAL_EVENTS e)
	{
		switch (e) 
		{
		case TUTORIAL_EVENTS.GRABBED_CHART:
			SetState(TUTORIAL_STATES.SCROLL_INTRO);
			break;

		case TUTORIAL_EVENTS.GRABBED_CURE:
			SetState(TUTORIAL_STATES.COMBAT_INTRO);
			break;

		case TUTORIAL_EVENTS.LEVELED_UP:
			SetState(TUTORIAL_STATES.LEVEL_INTRO);
			break;

		case TUTORIAL_EVENTS.UPGRADED:
			SetState(TUTORIAL_STATES.END);
			break;

		case TUTORIAL_EVENTS.PRESSED_TAB:
			if (currentState == TUTORIAL_STATES.COMBAT_INTRO)
				SetState (TUTORIAL_STATES.COMBAT_AIMING);
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

		//case TUTORIAL_STATES.COMBAT_INTRO:
		//	SetState(TUTORIAL_STATES.COMBAT_AIMING);
		//	return;

		case TUTORIAL_STATES.END:
			Application.LoadLevel(menuScene);
			return;
		default:
			return;
		}
	}

	IEnumerator StartShip()
	{
		yield return new WaitForSeconds(1f);
		player.SetActive (true);
		tutorialDialog.Enable ();
	}
}
