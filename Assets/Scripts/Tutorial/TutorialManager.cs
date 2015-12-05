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
		LEVELED_UP,
		UPGRADED
	}

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (StartShip ());
		ship = player.GetComponent<TutorialShipScript> ();
		SetText("Welcome to the Cursed Waters tutorial.");
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
			SetText("In Cursed Waters, the primary objective is to grab the <color=yellow>CURE</color> " +
			        " to your curse and hold it for 2 minutes. The first player to do this wins the game!");
			continueButton.gameObject.SetActive(true);
			break;

		case TUTORIAL_STATES.WASD:
			SetText("Use the keys W, A, S, and D to move. Pick up the <color=orange>CHART PIECE</color> to continue.");
			//Instantiate(curePrefab);
			continueButton.gameObject.SetActive(false);
			ship.MovementLock = false;
			chart.SetActive(true);
			//ship.Freeze();
			break;

		case TUTORIAL_STATES.SCROLL_INTRO:
			SetText("The chart pieces increase the detection distance for the <color=yellow>CURE</color>. Find the cure to continue.");
			ship.Freeze();
			ship.MovementLock = false;
			arrow.currentState = TutorialArrow.TUTORIAL_ARROW_STATE.CURE;
			cure.SetActive(true);
			break;


		case TUTORIAL_STATES.COMBAT_INTRO:
			SetText("In order to defend the cure(if you have it) and take it from other players, you will have to engage in <color=red>COMBAT</color>");
			continueButton.gameObject.SetActive(true);
			ship.CombatLock = true;
			ship.Freeze();
			break;

		case TUTORIAL_STATES.COMBAT_AIMING:
			SetText("The camera orientation decides which side you shoot from. Hold then release <color=cyan>SPACE</color> to fire. " +
					"Use <color=cyan>TAB</color> to switch ammo type. Use <color=cyan>Q</color> to cancel the charge." +
					" Destroy the two enemy ships to continue");
			continueButton.gameObject.SetActive(false);
			ship.MovementLock = false;
			ship.CombatLock = false;
			enemy1.SetActive(true);
			enemy2.SetActive(true);
			arrow.currentState = TutorialArrow.TUTORIAL_ARROW_STATE.ENEMIES;
			break;

		case TUTORIAL_STATES.LEVEL_INTRO:
			SetText("You just leveled up. Right click to toggle the upgrade overlay. Choose wisely.");
			arrow.currentState = TutorialArrow.TUTORIAL_ARROW_STATE.OFF;
			screen.locked = false;
			break;

		case TUTORIAL_STATES.END:
			SetText("This concludes the Cursed Waters tutorial. Good luck and have fun playing.");
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

		case TUTORIAL_STATES.COMBAT_INTRO:
			SetState(TUTORIAL_STATES.COMBAT_AIMING);
			return;

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
