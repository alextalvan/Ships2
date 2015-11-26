using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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


	public void Highlight()
	{
		GetComponent<Image> ().sprite = _highlighted;
	}

	public void Unhighlight()
	{
		GetComponent<Image> ().sprite = _idle;
	}
}
