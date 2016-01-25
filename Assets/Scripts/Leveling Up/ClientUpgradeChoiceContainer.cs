using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//simple behaviour for the sprites on the upgrade HUD
[RequireComponent(typeof(Image))]
public class ClientUpgradeChoiceContainer : MonoBehaviour {

	[SerializeField]
	ClientChoiceVisualData imageListHolder;

	Sprite _idle;
	Sprite _highlighted;


	public void SetInfo(int upgradeType)
	{
		_idle = imageListHolder.idleImages [upgradeType];
		_highlighted = imageListHolder.highlightImages [upgradeType];
		Unhighlight ();
	}

	//these are accessed as sprite hover/leave UnityEvents
	public void Highlight()
	{
		GetComponent<Image> ().sprite = _highlighted;
	}

	public void Unhighlight()
	{
		GetComponent<Image> ().sprite = _idle;
	}
}
