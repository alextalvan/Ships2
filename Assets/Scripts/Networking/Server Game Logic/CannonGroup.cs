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

        currentCharge += Time.deltaTime * shipAttributes.ReloadRateModifier;
        if (currentCharge > cannonsCount)
            currentCharge = cannonsCount;
    }

    //[Client]
    public void DrawArea(float charge, float distance, bool side)
    {
        lineRenderer.SetVertexCount(4);

        Vector3 centerCannon = transform.GetChild(0).position;

        if (side)
        {
            float difference = (transform.GetChild(cannonsCount - 1).position - transform.GetChild(cannonsCount - 2).position).magnitude / 2;
            float chargeModifier = charge / cannonsCount;

            lineRenderer.SetPosition(0, centerCannon - transform.right * (difference * chargeModifier * 2f) + transform.forward * distance);
            lineRenderer.SetPosition(1, centerCannon - transform.right * (difference * chargeModifier));

            lineRenderer.SetPosition(2, centerCannon + transform.right * (difference * chargeModifier));
            lineRenderer.SetPosition(3, centerCannon + transform.right * (difference * chargeModifier * 2f) + transform.forward * distance);
        }
        else
        {
            //Vector3 lastCannon = transform.GetChild(cannonsCount - 1).position;
            //Vector3 localLastCannon = transform.GetChild(cannonsCount - 1).localPosition;
            //Vector3 localOppCannon = new Vector3(localLastCannon.x, localLastCannon.y, localLastCannon.z * 2f);
            //Vector3 oppositeCannon = transform.TransformPoint(localOppCannon);

            //lineRenderer.SetPosition(0, lastCannon + transform.forward * 2f - transform.right * distance);
            //lineRenderer.SetPosition(1, lastCannon);

            //lineRenderer.SetPosition(2, oppositeCannon);
            //lineRenderer.SetPosition(3, oppositeCannon - transform.forward * 2f - transform.right * distance);
        }
		
        //without outer limit showing

        //distance = 50f;

        //lineRenderer.SetVertexCount(5);

        //Vector3 centerCannon = transform.GetChild(0).position;
        //float difference = (transform.GetChild(cannonsCount - 1).position - transform.GetChild(cannonsCount - 2).position).magnitude / 2;
        //float chargeModifier = charge / cannonsCount;

        //lineRenderer.SetPosition(2, centerCannon);

        //lineRenderer.SetPosition(1, centerCannon - transform.right * (difference * chargeModifier));
        //lineRenderer.SetPosition(0, centerCannon - transform.right * (difference * chargeModifier * 2f) + new Vector3(transform.forward.x, 0f, transform.forward.z) * distance);

        //lineRenderer.SetPosition(4, centerCannon + transform.right * (difference * chargeModifier * 2f) + new Vector3(transform.forward.x, 0f, transform.forward.z) * distance);
        //lineRenderer.SetPosition(3, centerCannon + transform.right * (difference * chargeModifier));

        //lineRenderer.SetPosition(5, centerCannon);

        //lineRenderer.SetPosition(6, center + new Vector3(0f, 0f, difference * chargeModifier * 15f));
    }
}
