using UnityEngine;
using UnityEngine.Networking;

public class CannonGroup : MonoBehaviour
{
	private LineRenderer lineRenderer;
	[SerializeField]
	private ShipAttributesOnline shipAttributes;
	private int cannonsCount;
	private float currentCharge;
	
	public int GetCannonsCount
	{
		get
		{
			return cannonsCount;
		}
	}
	
	public float CurrentCharge
	{
		get
		{
			return currentCharge;
		}
		set
		{
			currentCharge = value;
		}
	}
	
	// Use this for initialization
	void Start()
	{
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
		if (currentCharge < cannonsCount)
			currentCharge += Time.deltaTime * shipAttributes.reloadSpeedModifier;
	}

	//[Client]
	public void DrawArea(float charge)
	{
		lineRenderer.SetVertexCount(6);
		
		Vector3 center = transform.GetChild(0).localPosition;
		float difference = (transform.GetChild(cannonsCount - 1).localPosition - transform.GetChild(cannonsCount - 2).localPosition).magnitude/2;
		float chargeModifier = charge / cannonsCount;
		
		lineRenderer.SetPosition(0, center);
		
		lineRenderer.SetPosition(1, center + new Vector3(difference * chargeModifier, 0f, 0f));
		lineRenderer.SetPosition(2, center + new Vector3(difference * chargeModifier * 2f, 0f, difference * 10f));
		
		lineRenderer.SetPosition(3, center + new Vector3(-difference * chargeModifier * 2f, 0f, difference * 10f));
		lineRenderer.SetPosition(4, center + new Vector3(-difference * chargeModifier, 0f, 0f));
		
		lineRenderer.SetPosition(5, center);
		
		//lineRenderer.SetPosition(6, center + new Vector3(0f, 0f, difference * chargeModifier * 15f));
	}
}
