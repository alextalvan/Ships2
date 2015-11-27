using UnityEngine;

public class CannonGroup : MonoBehaviour
{
	private LineRenderer lineRenderer;
	private ShipAttributesOnline shipAttributes;
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
        shipAttributes = transform.parent.GetComponent<ShipAttributesOnline>();
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
			currentCharge += Time.deltaTime * shipAttributes.ReloadRateModifier;
	}

	//[Client]
	public void DrawArea(float charge, float distance)
	{
		lineRenderer.SetVertexCount(6);
		
		Vector3 centerCannon = transform.GetChild(0).position;
		float difference = (transform.GetChild(cannonsCount - 1).position - transform.GetChild(cannonsCount - 2).position).magnitude/2;
		float chargeModifier = charge / cannonsCount;
		
		lineRenderer.SetPosition(0, centerCannon);
		
		lineRenderer.SetPosition(1, centerCannon - transform.right * (difference * chargeModifier));
		lineRenderer.SetPosition(2, centerCannon - transform.right * (difference * chargeModifier * 2f) + transform.forward * distance);
		
		lineRenderer.SetPosition(3, centerCannon + transform.right * (difference * chargeModifier * 2f) + transform.forward * distance);
        lineRenderer.SetPosition(4, centerCannon + transform.right * (difference * chargeModifier));
		
		lineRenderer.SetPosition(5, centerCannon);
		
		//lineRenderer.SetPosition(6, center + new Vector3(0f, 0f, difference * chargeModifier * 15f));
	}
}
