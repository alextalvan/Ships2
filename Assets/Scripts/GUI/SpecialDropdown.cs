using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;

//this special dropdown is made specifically to clamp another one's max options to this one's.
public class SpecialDropdown : MonoBehaviour {



	List<UnityEngine.UI.Dropdown.OptionData> _storedList;

	[SerializeField]
	Dropdown _thisDropdown;

	[SerializeField]
	Dropdown _targetToClampTo;

	void Start()
	{
		//modTarget.
		_storedList = _thisDropdown.options;
		ClampOptionCount ();
	}


	public void ClampOptionCount()
	{

		int count = _targetToClampTo.value+1;

		if (count < 0)
			return;

		List<UnityEngine.UI.Dropdown.OptionData> temp = new List<UnityEngine.UI.Dropdown.OptionData> ();

		if (count >= _storedList.Count) 
		{
			temp.InsertRange(0,_storedList);
			_thisDropdown.options = temp;
			return;
		}

		for (int i=0; i<count; ++i) 
		{
			temp.Add(_storedList[i]);
		}
		_thisDropdown.options = temp;
		return;

	}

	public void SetNetManagerMinPlayers()
	{
		GameObject.Find("NetworkManager").GetComponent<CustomNetManager>().maxPlayers = _targetToClampTo.value + 2;
	}

	public void SetNetManagerMaxPlayers()
	{
		GameObject.Find("NetworkManager").GetComponent<CustomNetManager>().minPlayers = _thisDropdown.value + 2;
	}
}
