using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class TutorialLevelUser : MonoBehaviour 
{
	
	int _level = 0;
	int _currentEXP;
	int _nextLevelEXP = 500;
	
	[SerializeField]
	int _maxLevel = 10;
	
	
	public delegate void voidvoid();
	
	public event voidvoid OnLevelUp;
	
	public enum UPGRADES
	{
		ANGLE_CORRECTION,
		CRAZY_GUNNERS,
		DEFENCE_EXPERT,
		POWDER_MONKEY,
		PRECISION,
		REINFORCED_PLANKS,
	}
	
	public const int upgradeChoiceCount = 6;
	
	
	public void GainEXP(int amount)
	{
		GetComponent<PlayerCaptionController>().PushCaptionLocally("<color=#41DD92>+" + amount + " EXP</color>",3f);
		
		_currentEXP += amount;
		if (_currentEXP >= _nextLevelEXP && _level < _maxLevel) 
		{
			if(OnLevelUp!=null)
				OnLevelUp();
			
			ServerLevelUp();
			
			_level++;
			_nextLevelEXP += 1000;
		}
	}
	
	private Queue<LevelUpChoice> _choicesBuffer = new Queue<LevelUpChoice>();
	
	public Queue<LevelUpChoice> ChoicesBuffer
	{
		get {
			return _choicesBuffer;
		}
	}
	
	//[ServerCallback]
	void ServerLevelUp()
	{
		LevelUpChoice c = new LevelUpChoice (0);
		//RpcReceiveLevelUpChoice (c);
		_choicesBuffer.Enqueue (c);
		GetComponent<PlayerFX> ().PlaySound (PlayerFX.PLAYER_SOUNDS.LEVEL_UP);
		//GetComponent<PlayerCaptionController> ().RpcPushCaption ("LEVEL UP! Right click to upgrade",4f);
		GetComponent<PlayerFX> ().DoLevelUpParticle ();
		
	}

	
	//[ClientCallback]
	void UpgradeScreenCheck()
	{
		if (Input.GetMouseButtonUp (1)) 
		{
			ToggleUpgradeScreen();
		}
	}
	
	void ToggleUpgradeScreen()
	{
		//GameObject.Find
		GameObject.Find("TutorialSceneReferences").GetComponent<TutorialSceneReferences>().UpgradeScreen.Toggle ();
	}
	
	//[Command]
	public void CmdSendUpgradeChoice(int choice)
	{
		if (choice < 0 || choice > 3)
			return;
		
		if (_choicesBuffer.Count == 0)
			return;
		
		//LevelUpChoice c = 
		//_choicesBuffer.Dequeue ();

		/*
		if (choice == 1)
			UpgradeHelper.ApplyUpgrade ((UPGRADES)c.choice1,GetComponent<ShipAttributesOnline>());
		
		if (choice == 2)
			UpgradeHelper.ApplyUpgrade ((UPGRADES)c.choice2,GetComponent<ShipAttributesOnline>());
		
		if (choice == 3)
			UpgradeHelper.ApplyUpgrade ((UPGRADES)c.choice3,GetComponent<ShipAttributesOnline>());
			*/
		
		GetComponent<PlayerFX> ().PlaySound (PlayerFX.PLAYER_SOUNDS.UPGRADE);
	}
	
	
	// Update 
	void Update () 
	{
		UpgradeScreenCheck ();
		DebugFunc ();
	}
	
	//[ServerCallback]
	void DebugFunc()
	{
		if (Input.GetKeyDown (KeyCode.J))
			GainEXP (1000);
	}
}
