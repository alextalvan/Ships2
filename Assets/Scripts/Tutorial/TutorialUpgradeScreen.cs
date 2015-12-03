using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIDialog))]
public class TutorialUpgradeScreen : MonoBehaviour {
	
	CustomOnlinePlayer _target;
	
	public ClientUpgradeChoiceContainer choiceContainer1;
	public ClientUpgradeChoiceContainer choiceContainer2;
	public ClientUpgradeChoiceContainer choiceContainer3;
	
	private bool isEnabled = false;

	public bool locked = false;
	
	public void Toggle()
	{
		if (locked)
			return;

		if (isEnabled)
			Disable ();
		else
			Enable ();
		
		//isEnabled = !isEnabled;
	}
	
	
	public void SetTargetPlayer(CustomOnlinePlayer p)
	{
		_target = p;
	}
	
	public void SendChoice1()
	{
		_target.GetComponent<TutorialLevelUser> ().CmdSendUpgradeChoice (1);
		_target.GetComponent<TutorialLevelUser> ().ChoicesBuffer.Dequeue ();
		RefreshImages ();
	}
	
	public void SendChoice2()
	{
		_target.GetComponent<TutorialLevelUser> ().CmdSendUpgradeChoice (2);
		_target.GetComponent<TutorialLevelUser> ().ChoicesBuffer.Dequeue ();
		RefreshImages ();
	}
	
	public void SendChoice3()
	{
		_target.GetComponent<TutorialLevelUser> ().CmdSendUpgradeChoice (3);
		_target.GetComponent<TutorialLevelUser> ().ChoicesBuffer.Dequeue ();
		RefreshImages ();
	}
	
	
	public void Enable()
	{

		if (_target == null) 
		{
			UIConsole.Log("UpgradeScreen activated without target player!");
			return;
		}
		
		if (_target.GetComponent<TutorialLevelUser> ().ChoicesBuffer.Count == 0) 
		{
			Disable ();
			return;
		}
		
		RefreshImages ();
		
		GetComponent<GUIDialog> ().Enable ();
		
		isEnabled = true;

		//snap mouse to middle of screen
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.lockState = CursorLockMode.None;
		
	}
	
	void RefreshImages()
	{
		if (_target.GetComponent<TutorialLevelUser> ().ChoicesBuffer.Count == 0) 
		{
			Disable ();
			return;
		}
		
		LevelUpChoice c = _target.GetComponent<TutorialLevelUser> ().ChoicesBuffer.Peek ();
		choiceContainer1.SetInfo (c.choice1);
		choiceContainer2.SetInfo (c.choice2);
		choiceContainer3.SetInfo (c.choice3);
	}
	
	public void Disable()
	{
		GetComponent<GUIDialog> ().Disable ();
		isEnabled = false;
	}
}
