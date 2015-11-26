using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ClientUpgradeChoiceContainer : MonoBehaviour {

	[SerializeField]
	ClientChoiceVisualData imageListHolder;


	public void SetInfo(int upgradeType)
	{
		GetComponent<Image> ().sprite = imageListHolder.images [upgradeType];
	}
}
