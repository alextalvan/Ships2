using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct LevelUpChoice 
{
	public int choice1;
	public int choice2;
	public int choice3;

	public LevelUpChoice(int a)
	{
		List<int> choices = new List<int> ();
		for (int i=0; i< LevelUser.upgradeChoiceCount; ++i) 
		{
			choices.Add(i);
		}

		choice1 = Random.Range (0, choices.Count);
		choices.Remove (choice1);

		choice2 = Random.Range (0, choices.Count);
		choices.Remove (choice2);

		choice3 = Random.Range (0, choices.Count);
	}
}
