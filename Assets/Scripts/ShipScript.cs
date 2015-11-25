using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

/// <summary>
/// Cone display
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(ShipAttributesOnline))]
public class ShipScript : NetworkBehaviour
{
    private enum Seals { Opened, Closed };
    private Seals currentSealsState = Seals.Closed;

    private const float CUBE_GIZMOS_SIZE = 0.5f;
    private const float SPHERE_GIZMOS_SIZE = 0.1f;

//    [SerializeField]
//    float movementSpeed = 5f;
//    [SerializeField]
//    float turningSpeed = 5f;
//    [SerializeField]
//    float chargingSpeed = 5f;

    [SerializeField]
    Transform rightSide = null;
    [SerializeField]
    Transform leftSide = null;

    private enum State { Idle, Charging };
    private State currentState = State.Idle;

    private GameObject currentCannonBallType;
    [SerializeField]
    private GameObject cannonBallPrefab;
    [SerializeField]
    private GameObject chainCannonBallPrefab;

    //[SerializeField]
    //private GameObject cannonBallPrefab;
    private OnlinePlayerInput onlineInput;

    //Object
    private Rigidbody objRigidBody;
    private Bounds objBounds;

    //Ship properties
    private Transform activeSide;
    private CannonGroup activeCannons;

	[SyncVar]
    private float shotPower = 0f;
    private bool prevShootState = false;
    private bool prevMoveState = false;

    [SerializeField]
	private float forwardMovementModifier = 7.5f;

	[SerializeField]
	private float steeringModifier = 250f;

    private LineRenderer trajectoryRenderer;

    //client trajectory fixing
    bool startedPreviewingTrajectory = false;
	Vector3 storedSideForward;
	bool storedSideIsLeft;


    // Use this for initialization
    void Start()
    {
        objRigidBody = GetComponent<Rigidbody>();
        objBounds = GetComponent<Collider>().bounds;
        cannonBallPrefab = (GameObject)Resources.Load("CannonBallPrefab");
        onlineInput = GetComponent<OnlinePlayerInput>();
        trajectoryRenderer = GetComponent<LineRenderer>();

        SetupCamera ();
    }

	[ClientCallback]
	public void SetupCamera()
	{
		if (!isLocalPlayer)
			return;

		Transform _camera = GameObject.Find ("OnlineSceneReferences").GetComponent<OnlineSceneReferences> ().cameraRef;
		//_camera.gameObject.AddComponent<CameraScript> ();
		_camera.gameObject.GetComponent<CameraScript2> ().AttachTo (gameObject);

		//test
		//
		//_camera.parent = this.transform;
	}

	[ClientRpc]
	public void RpcChangeCameraState(bool attached)
	{
		if (!isLocalPlayer)
			return;

		Transform _camera = GameObject.Find ("OnlineSceneReferences").GetComponent<OnlineSceneReferences> ().cameraRef;

		if (attached) 
		{
			_camera.gameObject.GetComponent<CameraScript2> ().AttachTo (gameObject);
		}
		else
			_camera.gameObject.GetComponent<CameraScript2> ().Detach();
	}



    void Update()
    {
		//CheckSideClient ();
		PreviewTrajectory ();
    }

    void FixedUpdate()
    {
        Controls();
		UpdateState();
    }

    [ServerCallback]
    private void Controls()
    {
		//float movementSpeed = GetComponent<ShipAttributesOnline> ().speed;
		//float turningSpeed = GetComponent<ShipAttributesOnline> ().speed;

        float currentHorizonSpeed = new Vector3(objRigidBody.velocity.x, 0f, objRigidBody.velocity.z).magnitude;
        Vector3 forward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Vector3 right = new Vector3(transform.right.x, 0f, transform.right.z).normalized;

        //if (onlineInput.GetInputValue(OnlinePlayerInput.PlayerControls.FORWARD))
        //    objRigidBody.AddForce(forward * objRigidBody.mass * movementSpeed);

        bool currMoveState = onlineInput.GetInputValue(OnlinePlayerInput.PlayerControls.BACK);

        if (currMoveState && !prevMoveState)
        {
            if (currentSealsState == Seals.Opened)
                currentSealsState = Seals.Closed;
            else
                currentSealsState = Seals.Opened;
        }

        prevMoveState = currMoveState;

		float cureMod = (GetComponent<CustomOnlinePlayer> ().currentCureCarrier == transform) ? CureScript.cureCarrierSpeedDebuff : 1f;

        if (currentSealsState == Seals.Opened)
            objRigidBody.AddForce(forward * objRigidBody.mass * forwardMovementModifier * cureMod);

        //if (onlineInput.GetInputValue(OnlinePlayerInput.PlayerControls.BACK))
        //    objRigidBody.AddForce(-forward * objRigidBody.mass * movementSpeed);
        if (onlineInput.GetInputValue(OnlinePlayerInput.PlayerControls.LEFT))
        {
            objRigidBody.AddForceAtPosition(right * (objRigidBody.mass + currentHorizonSpeed * steeringModifier), transform.position - (transform.forward * objBounds.extents.z) - (transform.up * objBounds.extents.y));
			objRigidBody.AddForceAtPosition(-right * (objRigidBody.mass + currentHorizonSpeed * steeringModifier), transform.position + (transform.forward * objBounds.extents.z) - (transform.up * objBounds.extents.y));
        }
        //objRigidBody.AddForceAtPosition(right * (objRigidBody.mass + currentHorizonSpeed) * turningSpeed, transform.position - (transform.forward * objBounds.extents.z) - (transform.up * objBounds.extents.y));
        if (onlineInput.GetInputValue(OnlinePlayerInput.PlayerControls.RIGHT))
        {
			objRigidBody.AddForceAtPosition(-right * (objRigidBody.mass + currentHorizonSpeed * steeringModifier), transform.position - (transform.forward * objBounds.extents.z) - (transform.up * objBounds.extents.y));
			objRigidBody.AddForceAtPosition(right * (objRigidBody.mass + currentHorizonSpeed * steeringModifier), transform.position + (transform.forward * objBounds.extents.z) - (transform.up * objBounds.extents.y));
        }
        //objRigidBody.AddForceAtPosition(-right * (objRigidBody.mass + currentHorizonSpeed) * turningSpeed, transform.position - (transform.forward * objBounds.extents.z) - (transform.up * objBounds.extents.y));
    }

    private void CheckSide(Vector3 camVec)
    {
        Transform newActiveSide = null;

        if (Vector3.Angle(camVec, rightSide.forward) < Vector3.Angle(camVec, leftSide.forward))
            newActiveSide = rightSide;
        else
            newActiveSide = leftSide;

        if (newActiveSide != activeSide)
        {
            shotPower = 0f;
        }

        activeSide = newActiveSide;
        activeCannons = activeSide.GetComponent<CannonGroup>();
    }

	[ClientCallback]
	private void PreviewTrajectory()
	{
		if (!isLocalPlayer)
			return;


		rightSide.GetComponent<LineRenderer> ().enabled = false;
		leftSide.GetComponent<LineRenderer> ().enabled = false;
		trajectoryRenderer.enabled = false;

		if (Input.GetKeyDown (KeyCode.Space) && !startedPreviewingTrajectory) 
		{
			startedPreviewingTrajectory = true;
			if(Vector3.Dot (Camera.main.transform.forward,leftSide.forward)>0f) 
			{
				storedSideForward = leftSide.forward;
				storedSideIsLeft = true;
			}
			else
			{
				storedSideForward = rightSide.forward;
				storedSideIsLeft = false;
			}
		}

		if (startedPreviewingTrajectory) 
		{

			if(Vector3.Dot (Camera.main.transform.forward,storedSideForward)>0f)
			{
				trajectoryRenderer.enabled = true;
				if(storedSideIsLeft)
				{
					leftSide.GetComponent<LineRenderer>().enabled = true;
					leftSide.GetComponent<CannonGroup>().DrawArea(shotPower);
					Transform centerCannon = leftSide.GetChild(0);
					float cannonBallMass = cannonBallPrefab.GetComponent<Rigidbody>().mass;
					Vector3 forwardDirection = new Vector3(centerCannon.forward.x, 0f, centerCannon.forward.z).normalized;
					Vector3 force = (Vector3.up + (forwardDirection * 5000f)) * cannonBallMass;
					
					DrawTraject(centerCannon.position, force);
				}
				else
				{
					rightSide.GetComponent<LineRenderer>().enabled = true;
					rightSide.GetComponent<CannonGroup>().DrawArea(shotPower);

					Transform centerCannon = rightSide.GetChild(0);
					float cannonBallMass = cannonBallPrefab.GetComponent<Rigidbody>().mass;
					Vector3 forwardDirection = new Vector3(centerCannon.forward.x, 0f, centerCannon.forward.z).normalized;
					Vector3 force = (Vector3.up + (forwardDirection * 5000f)) * cannonBallMass;
					
					DrawTraject(centerCannon.position, force);
				}
			}
		}

		if(Input.GetKeyUp(KeyCode.Space))
		{
			startedPreviewingTrajectory = false;
		}

	}


	[ClientCallback]
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
			
			
		}
	}

    [ServerCallback]
    private void UpdateState()
    {
        Vector3 camVec;
        bool shootReqThisFrame = onlineInput.GetInputValue(OnlinePlayerInput.PlayerControls.SHOOT, out camVec);
        bool shootReqLastFrame = prevShootState;

        prevShootState = shootReqThisFrame;

        switch (currentState)
        {
            case State.Idle:
                if (shootReqThisFrame && !shootReqLastFrame)
                {
                    currentState = State.Charging;
                }
                break;
            case State.Charging:
                CheckSide(camVec);
				float chargingSpeed = GetComponent<ShipAttributesOnline>().cannonChargeRate;

                if (shotPower < activeCannons.CurrentCharge)
                    shotPower += Time.deltaTime * chargingSpeed;
                if (!shootReqThisFrame)
                {
                    currentState = State.Idle;
                    Shoot(activeSide);
                }
                break;
        }
    }

	[ServerCallback]
    private void Shoot(Transform side)
    {
		float rangeMultiplier = GetComponent<ShipAttributesOnline>().shootRangeMultiplier;
		for (int i = 0; i < (int)shotPower; i++)
		{
			Transform cannon = activeSide.GetChild(i);
			GameObject cannonBall = (GameObject)Instantiate(cannonBallPrefab, cannon.position, Quaternion.identity);
			
			float cannonBallMass = cannonBall.GetComponent<Rigidbody>().mass;
			Vector3 forwardDirection = new Vector3(cannon.forward.x, 0f, cannon.forward.z).normalized;
			Vector3 force = (Vector3.up + (forwardDirection * 5000f)) * cannonBallMass; //* rangeMultiplier;
			
			cannonBall.GetComponent<Rigidbody>().AddForce(force);
			cannonBall.GetComponent<Cannonball>().owner = GetComponent<CustomOnlinePlayer>();
			NetworkServer.Spawn(cannonBall);
		}
		activeCannons.CurrentCharge -= (int)shotPower;
		shotPower = 0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        foreach (Transform cannon in rightSide)
        {
            Gizmos.DrawWireSphere(cannon.position, CUBE_GIZMOS_SIZE);
        }
        foreach (Transform cannon in leftSide)
        {
            Gizmos.DrawWireSphere(cannon.position, CUBE_GIZMOS_SIZE);
        }
    }
}
