using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class ShipScriptOFFLINE : MonoBehaviour
{
    private enum Seals { Opened, Closed };
    private Seals currentSealsState = Seals.Closed;

    private const float CUBE_GIZMOS_SIZE = 0.5f;
    private const float SPHERE_GIZMOS_SIZE = 0.25f;

    private LineRenderer trajectoryRenderer;

    [SerializeField]
    float movementSpeed = 5f;
    [SerializeField]
    float turningSpeed = 5f;
    [SerializeField]
    int maxCrew = 100;
    [SerializeField]
    float chargingSpeed = 5f;

    [SerializeField]
    Transform rightSide = null;
    [SerializeField]
    Transform leftSide = null;

    [SerializeField]
    private GameObject currentCannonBallType;
    [SerializeField]
    private GameObject cannonBallPrefab;
    [SerializeField]
    private GameObject chainCannonBallPrefab;

    //Object
    private Rigidbody objRigidBody;
    private Bounds objBounds;

    //Ship properties
    private Transform activeSide;
    private CannonsScript activeCannons;
    private int currentCrew;
    private float shotPower = 0f;

    public int GetCrewModifier
    {
        get { return currentCrew / maxCrew; }
    }

    // Use this for initialization
    void Start()
    {
        trajectoryRenderer = GetComponent<LineRenderer>();
        objRigidBody = GetComponent<Rigidbody>();

        //Store initial position and rotation
        Vector3 initialPosition = transform.position;
        Quaternion initialRotation = transform.rotation;
        //Reset object's position and rotation
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        objBounds = GetComponent<Collider>().bounds;

        //Set initial position and rotation back
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        
        currentCrew = maxCrew;
        currentCannonBallType = cannonBallPrefab;
    }

    void Update()
    {
        CheckSide();
        ChargeCannons();
        SealsControls();
        ChangeAmmoType();
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void SealsControls()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentSealsState == Seals.Opened)
                currentSealsState = Seals.Closed;
            else
                currentSealsState = Seals.Opened;
        }
    }

    private void ChangeAmmoType()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (currentCannonBallType == cannonBallPrefab)
                currentCannonBallType = chainCannonBallPrefab;
            else if (currentCannonBallType == chainCannonBallPrefab)
                currentCannonBallType = cannonBallPrefab;
        }
    }

    private void Move()
    {
        Vector3 forward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;

        if (currentSealsState == Seals.Opened)
            objRigidBody.AddForce(forward * objRigidBody.mass * 15f);
    }

    private void Rotate()
    {
        Vector3 right = new Vector3(transform.right.x, 0f, transform.right.z).normalized;

        float currentHorizonSpeed = new Vector3(objRigidBody.velocity.x, 0f, objRigidBody.velocity.z).magnitude;

        if (Input.GetKey(KeyCode.A))
        {
            objRigidBody.AddForceAtPosition(right * (objRigidBody.mass + currentHorizonSpeed * 250f), transform.position - (transform.forward * objBounds.extents.z) - (transform.up * objBounds.extents.y));
            objRigidBody.AddForceAtPosition(-right * (objRigidBody.mass + currentHorizonSpeed * 250f), transform.position + (transform.forward * objBounds.extents.z) - (transform.up * objBounds.extents.y));
        }
        if (Input.GetKey(KeyCode.D))
        {
            objRigidBody.AddForceAtPosition(-right * (objRigidBody.mass + currentHorizonSpeed * 250f), transform.position - (transform.forward * objBounds.extents.z) - (transform.up * objBounds.extents.y));
            objRigidBody.AddForceAtPosition(right * (objRigidBody.mass + currentHorizonSpeed * 250f), transform.position + (transform.forward * objBounds.extents.z) - (transform.up * objBounds.extents.y));
        }
    }

    private void CheckSide()
    {
        Transform newActiveSide = null;

        if (Vector3.Angle(Camera.main.transform.forward, rightSide.forward) < Vector3.Angle(Camera.main.transform.forward, leftSide.forward))
            newActiveSide = rightSide;
        else
            newActiveSide = leftSide;

        if (newActiveSide != activeSide)
        {
            if (activeSide)
            {
                activeSide.GetComponent<LineRenderer>().enabled = false;
            }
            shotPower = 0f;
        }

        activeSide = newActiveSide;
        activeCannons = activeSide.GetComponent<CannonsScript>();
    }

    private void ChargeCannons()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            trajectoryRenderer.enabled = true;
            activeSide.GetComponent<LineRenderer>().enabled = true;
            activeCannons.DrawArea(shotPower);

            Transform centerCannon = activeSide.GetChild(0);
            float cannonBallMass = currentCannonBallType.GetComponent<Rigidbody>().mass;
            Vector3 forwardDirection = new Vector3(centerCannon.forward.x, 0f, centerCannon.forward.z).normalized;
            Vector3 force = (Vector3.up * currentCannonBallType.GetComponent<ProjectileOFFLINE>().UpwardsModifier + (forwardDirection * 5000f)) * cannonBallMass;

            DrawTraject(activeSide.GetChild(0).position, force);

            if (shotPower < activeCannons.CurrentCharge)
                shotPower += Time.deltaTime * chargingSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Shoot(activeSide);
            trajectoryRenderer.enabled = false;
            activeSide.GetComponent<LineRenderer>().enabled = false;
        }
    }

    private void Shoot(Transform side)
    {
        for (int i = 0; i < (int)shotPower; i++)
        {
            Transform cannon = activeSide.GetChild(i);
            GameObject cannonBall = (GameObject)Instantiate(currentCannonBallType, cannon.position, Quaternion.identity);

            float cannonBallMass = cannonBall.GetComponent<Rigidbody>().mass;
            Vector3 forwardDirection = new Vector3(cannon.forward.x, 0f, cannon.forward.z).normalized;
            Vector3 force = (Vector3.up * cannonBall.GetComponent<ProjectileOFFLINE>().UpwardsModifier + (forwardDirection * 5000f)) * cannonBallMass;

            cannonBall.GetComponent<Rigidbody>().AddForce(force);
        }
        activeCannons.CurrentCharge -= (int)shotPower;
        shotPower = 0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (Transform cannon in rightSide)
        {
            Gizmos.DrawWireSphere(cannon.position, SPHERE_GIZMOS_SIZE);
        }
        foreach (Transform cannon in leftSide)
        {
            Gizmos.DrawWireSphere(cannon.position, SPHERE_GIZMOS_SIZE);
        }
    }

    void DrawTraject(Vector3 startPos, Vector3 force)
    {
        int areaSegments = 25;
        float areaRadius = 5f;
        List<Vector3> trajectoryPoints = new List<Vector3>();
        List<Vector3> circumferencePoints = new List<Vector3>();
        Vector3 startVelocity = force * Time.fixedDeltaTime;

        int maxVerts = 999;

        Vector3 position = startPos;
        Vector3 velocity = startVelocity;
        
        for (int i = 0; i < maxVerts; i++)
        {
            velocity += Physics.gravity * Time.fixedDeltaTime;
            position += velocity * Time.fixedDeltaTime;

            float waterLevel = WaterHelper.GetOceanHeightAt(new Vector2(position.x, position.z));

            if (waterLevel < position.y)
            {
                trajectoryPoints.Add(position);
            }
            else
            {
                trajectoryRenderer.SetVertexCount(i + areaSegments + 1);

                for (int p = 0; p < trajectoryPoints.Count; p++)
                {
                    trajectoryRenderer.SetPosition(p, new Vector3(trajectoryPoints[p].x, trajectoryPoints[p].y, trajectoryPoints[p].z));
                }


                float angle = 20f;

                for (int s = 0; s < areaSegments + 1; s++)
                {
                    float x = (Mathf.Sin(Mathf.Deg2Rad * angle) * areaRadius) + trajectoryPoints[i - 1].x;
                    float z = (Mathf.Cos(Mathf.Deg2Rad * angle) * areaRadius) + trajectoryPoints[i - 1].z;

                    trajectoryRenderer.SetPosition(i + s, new Vector3(x, trajectoryPoints[i - 1].y, z));

                    angle += (360f / areaSegments);
                }
                break;
            }

            //if (islandCol.Raycast(rayTraj, out hitTraj, Vector3.Distance(position, new Vector3(position.x, 0, position.z))))
            //{
            //    //Debug.DrawLine(position, new Vector3(position.x, -1, position.z));
            //}
            //else
            //{
            //    trajectoryLineRenderer.SetVertexCount(i);

            //    for (int p = 0; p < trajectoryPoints.Count; p++)
            //    {
            //        trajectoryLineRenderer.SetPosition(p, new Vector3(trajectoryPoints[p].x, trajectoryPoints[p].y, trajectoryPoints[p].z));
            //    }

            //    //float angle = 20f;

            //    //explosionAreaLineRenderer.SetVertexCount(segments + 1);

            //    //for (int s = 0; s < segments + 1; s++)
            //    //{
            //    //    float x = (Mathf.Sin(Mathf.Deg2Rad * angle) * radius) + trajectoryPoints[i - 1].x;
            //    //    float z = (Mathf.Cos(Mathf.Deg2Rad * angle) * radius) + trajectoryPoints[i - 1].z;
            //    //    float y = trajectoryPoints[i - 1].y;

            //    //    //Calculating Y position
            //    //    RaycastHit hitArea;
            //    //    Ray rayArea = new Ray(new Vector3(x, trajectoryPoints[i - 1].y + 100f, z), new Vector3(0, -1, 0));

            //    //    if (islandCol.Raycast(rayArea, out hitArea, 999f))
            //    //    {
            //    //        //Debug.DrawLine(new Vector3(x, trajectoryPoints[i - 1].y + 100f, z), new Vector3(x, -1, z));
            //    //        y = hitArea.point.y;
            //    //    }

            //    //    explosionAreaLineRenderer.SetPosition(s, new Vector3(x, y + 0.5f, z));

            //    //    angle += (360f / segments);
            //    //}
            //    break;
            //}
        }
    }
}
