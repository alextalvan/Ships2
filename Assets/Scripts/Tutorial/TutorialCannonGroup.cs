using UnityEngine;
using System.Collections.Generic;

public class TutorialCannonGroup : MonoBehaviour
{
	[SerializeField]
	private List<Material> materials = new List<Material>();
	[SerializeField]
	private Transform anotherSide;
	private Transform ship;
	private LineRenderer lineRenderer;
	private TutorialShipAttributes shipAttributes;
	private TutorialShipScript shipScript;
	private int cannonsCount;
	private float currentCharge;
	
	public int GetCannonsCount
	{
		get { return cannonsCount; }
	}
	
	public float CurrentCharge
	{
		get { return currentCharge; }
		set { currentCharge = value; }
	}
	
	// Use this for initialization
	void Start()
	{
		shipAttributes = transform.parent.GetComponent<TutorialShipAttributes>();
		ship = transform.parent;
		shipScript = ship.GetComponent<TutorialShipScript>();
		lineRenderer = GetComponent<LineRenderer>();
		currentCharge = transform.childCount;
		cannonsCount = transform.childCount;
	}
	
	// Update is called once per frame
	void Update()
	{
		ReloadCannons();
	}
	
	//[Server]
	private void ReloadCannons()
	{
		currentCharge += Time.deltaTime * shipAttributes.ReloadRateModifier;
		if (currentCharge > cannonsCount)
			currentCharge = cannonsCount;
	}
	
	//[Client]
	public void DrawArea(float charge, float distance, bool side, int matIndex)
	{
		lineRenderer.SetVertexCount(4);
		lineRenderer.material = materials[matIndex];
		
		Vector3 nullYforward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
		Vector3 nullYright = new Vector3(transform.right.x, 0f, transform.right.z).normalized;
		
		if (side)
		{
			Vector3 centerCannon = transform.GetChild(0).position;
			
			float difference = (transform.GetChild(cannonsCount - 1).position - transform.GetChild(cannonsCount - 2).position).magnitude * 0.5f;
			float chargeModifier = charge / cannonsCount;
			
			lineRenderer.SetPosition(0, centerCannon - nullYright * (difference * chargeModifier * 2f) + nullYforward * distance);
			lineRenderer.SetPosition(1, centerCannon - nullYright * (difference * chargeModifier));
			
			lineRenderer.SetPosition(2, centerCannon + nullYright * (difference * chargeModifier));
			lineRenderer.SetPosition(3, centerCannon + nullYright * (difference * chargeModifier * 2f) + nullYforward * distance);
		}
		else
		{
			Vector3 lastCannon = transform.GetChild(cannonsCount - 1).position;
			Vector3 oppositeCannon = anotherSide.GetChild(cannonsCount - 1).position;
			Vector3 centerCannon = oppositeCannon + (lastCannon - oppositeCannon) * 0.5f;
			
			float difference = (lastCannon - oppositeCannon).magnitude * 0.5f;
			
			float coolDownModifier = 1 - (shipScript.GetCurrentBarrelCoolDown / shipScript.GetMaxBarrelCoolDown);
			
			lineRenderer.SetPosition(0, centerCannon + nullYforward * (difference * coolDownModifier * 2f) - nullYright * distance);
			lineRenderer.SetPosition(1, centerCannon + nullYforward * (difference * coolDownModifier));
			
			lineRenderer.SetPosition(2, centerCannon - nullYforward * (difference * coolDownModifier));
			lineRenderer.SetPosition(3, centerCannon - nullYforward * (difference * coolDownModifier * 2f) - nullYright * distance);
		}
	}
}
