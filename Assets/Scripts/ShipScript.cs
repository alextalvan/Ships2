using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(ShipAttributesOnline))]
public class ShipScript : NetworkBehaviour
{
    private ShipAttributesOnline shipAttributes;

    private const float CUBE_GIZMOS_SIZE = 0.5f;
    private const float SPHERE_GIZMOS_SIZE = 0.1f;

    //private enum Sails { Opened, Closed };
    //private Sails currentSailsState = Sails.Closed;

	[SyncVar][SerializeField]
	private float sailState = 0f;
	[SerializeField]
	float sailAccelerationPerFrame = 0.01f;

    private enum ShootInputState { Idle, Ready };
    private ShootInputState currentShootInputState = ShootInputState.Idle;

    [SerializeField]
    Transform rightSide = null;
    [SerializeField]
    Transform leftSide = null;

    private GameObject currentProjectileType;
    private GameObject projectileType1Prefab;
    private GameObject projectileType2Prefab;

    private OnlinePlayerInput onlineInput;

    //Object
    private Rigidbody objRigidBody;
    private Bounds objBounds;

    //Ship properties
    private Transform activeSide;
    private CannonGroup activeCannons;

    [SyncVar]
    private float shotPowerLeft = 0f;

	[SyncVar]
	private float shotPowerRight = 0f;
   
    //client trajectory fixing
    bool startedPreviewingTrajectory = false;
    bool storedSideIsLeft;


    // Use this for initialization
    void Start()
    {
        shipAttributes = GetComponent<ShipAttributesOnline>();
        objRigidBody = GetComponent<Rigidbody>();
        objBounds = GetComponent<Collider>().bounds;

        //Load ammo
        projectileType1Prefab = (GameObject)Resources.Load("Projectiles/ProjectileType1");
        projectileType2Prefab = (GameObject)Resources.Load("Projectiles/ProjectileType2");
        currentProjectileType = projectileType1Prefab;

        onlineInput = GetComponent<OnlinePlayerInput>();
        

        SetupCamera();

        GetComponent<OnlinePlayerInput>().OnServerReceiveRawInput += ChangeAmmoType;
		GetComponent<OnlinePlayerInput>().OnServerReceiveRawInput += HandleShootInput;

		ResetShootAndMovement ();
    }

    [ClientCallback]
    public void SetupCamera()
    {
        if (!isLocalPlayer)
            return;

        Transform _camera = GameObject.Find("OnlineSceneReferences").GetComponent<OnlineSceneReferences>().cameraRef;
        _camera.GetComponent<CameraScript>().AttachCameraTo(transform);
    }

    [ClientRpc]
    public void RpcChangeCameraState(bool attached)
    {
        if (!isLocalPlayer)
            return;

        Transform _camera = GameObject.Find("OnlineSceneReferences").GetComponent<OnlineSceneReferences>().cameraRef;

        if (attached)
            _camera.GetComponent<CameraScript>().AttachCameraTo(transform);
        else
            _camera.GetComponent<CameraScript>().DetachCamera();
    }

	[ServerCallback]
	public void ResetShootAndMovement()
	{
		sailState = 0f;
		currentShootInputState = ShootInputState.Idle;
		shotPowerLeft = leftSide.transform.childCount;
		shotPowerRight = rightSide.transform.childCount;
	}


    void Update()
    {
        SwitchClientAmmoType();
        PreviewTrajectory();
    }

    void FixedUpdate()
    {
        ControlSails();
        Move();
        Rotate();
        UpdateShootState();
    }

    private void ChangeAmmoType(OnlinePlayerInput.PlayerControlMessage m, Vector3 dir)
    {
        if (m == OnlinePlayerInput.PlayerControlMessage.SWITCH_START_HOLD_DOWN)
        {
            if (currentProjectileType == projectileType1Prefab)
                currentProjectileType = projectileType2Prefab;
            else if (currentProjectileType == projectileType2Prefab)
                currentProjectileType = projectileType1Prefab;
        }
    }

    [ClientCallback]
    private void SwitchClientAmmoType()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (currentProjectileType == projectileType1Prefab)
                currentProjectileType = projectileType2Prefab;
            else if (currentProjectileType == projectileType2Prefab)
                currentProjectileType = projectileType1Prefab;
        }
    }

    [ServerCallback]
    private void ControlSails()
    {
        
		if (onlineInput.GetInputValue(OnlinePlayerInput.PlayerControls.FORWARD))
			sailState += sailAccelerationPerFrame;

		if (onlineInput.GetInputValue(OnlinePlayerInput.PlayerControls.BACK))
			sailState -= sailAccelerationPerFrame;

		sailState = Mathf.Clamp01 (sailState);

       
    }

    private void Move()
    {
        Vector3 forward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        float cureMod = (GetComponent<CustomOnlinePlayer>().currentCureCarrier == transform) ? CureScript.cureCarrierSpeedDebuff : 1f;

        float baseWeight = shipAttributes.BasicSpeed * 0.25f;
        float sailWeight = shipAttributes.BasicSpeed * 0.75f;

        float totalSpeed = (baseWeight + sailWeight * shipAttributes.SailSpeedModifier) * cureMod * sailState;

        
        objRigidBody.AddForce(forward * objRigidBody.mass * totalSpeed);
    }

    [ServerCallback]
    private void Rotate()
    {
        Vector3 right = new Vector3(transform.right.x, 0f, transform.right.z).normalized;
        float currentHorizonSpeed = new Vector3(objRigidBody.velocity.x, 0f, objRigidBody.velocity.z).magnitude;

        if (onlineInput.GetInputValue(OnlinePlayerInput.PlayerControls.LEFT))
        {
            objRigidBody.AddForceAtPosition(right * (objRigidBody.mass + currentHorizonSpeed * shipAttributes.SteeringModifier), transform.position - (transform.forward * objBounds.extents.z) - (transform.up * objBounds.extents.y));
            objRigidBody.AddForceAtPosition(-right * (objRigidBody.mass + currentHorizonSpeed * shipAttributes.SteeringModifier), transform.position + (transform.forward * objBounds.extents.z) - (transform.up * objBounds.extents.y));
        }
        if (onlineInput.GetInputValue(OnlinePlayerInput.PlayerControls.RIGHT))
        {
            objRigidBody.AddForceAtPosition(-right * (objRigidBody.mass + currentHorizonSpeed * shipAttributes.SteeringModifier), transform.position - (transform.forward * objBounds.extents.z) - (transform.up * objBounds.extents.y));
            objRigidBody.AddForceAtPosition(right * (objRigidBody.mass + currentHorizonSpeed * shipAttributes.SteeringModifier), transform.position + (transform.forward * objBounds.extents.z) - (transform.up * objBounds.extents.y));
        }
    }

    [ClientCallback]
    private void PreviewTrajectory()
    {
        if (!isLocalPlayer)
            return;


        rightSide.GetComponent<LineRenderer>().enabled = false;
        leftSide.GetComponent<LineRenderer>().enabled = false;
       

        if (Input.GetKeyDown(KeyCode.Space) && !startedPreviewingTrajectory)
        {
            startedPreviewingTrajectory = true;
            if (Vector3.Dot(Camera.main.transform.forward, leftSide.forward) > 0f)   
                storedSideIsLeft = true;
            else             
                storedSideIsLeft = false;
        }

        if (startedPreviewingTrajectory)
        {

            if (storedSideIsLeft)
            {
                leftSide.GetComponent<LineRenderer>().enabled = true;
                Transform centerCannon = leftSide.GetChild(0);
                float projectileMass = currentProjectileType.GetComponent<Rigidbody>().mass;
                float upwardsModifier = currentProjectileType.GetComponent<Projectile>().UpwardsModifier;
         
                Vector3 forwardDirection = new Vector3(centerCannon.forward.x, 0f, centerCannon.forward.z).normalized * shipAttributes.RangeMultiplier;
                Vector3 force = (Vector3.up * upwardsModifier + (forwardDirection * 5000f)) * projectileMass;

                float shotDist = GetTrajectoryDistance(centerCannon.position, force);
                leftSide.GetComponent<CannonGroup>().DrawArea(shotPowerLeft, shotDist);
            }
            else
            {
                rightSide.GetComponent<LineRenderer>().enabled = true;
                
                Transform centerCannon = rightSide.GetChild(0);
                float projectileMass = currentProjectileType.GetComponent<Rigidbody>().mass;
                float upwardsModifier = currentProjectileType.GetComponent<Projectile>().UpwardsModifier;
               
                Vector3 forwardDirection = new Vector3(centerCannon.forward.x, 0f, centerCannon.forward.z).normalized;
                Vector3 force = (Vector3.up * upwardsModifier + (forwardDirection * 5000f)) * projectileMass * shipAttributes.RangeMultiplier;

                float shotDist = GetTrajectoryDistance(centerCannon.position, force);
                rightSide.GetComponent<CannonGroup>().DrawArea(shotPowerRight, shotDist);
              
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyDown(KeyCode.Q) || GetComponent<PlayerRespawn>().IsDead)
        {
            startedPreviewingTrajectory = false;
        }

    }

    [ClientCallback]
    float GetTrajectoryDistance(Vector3 startPos, Vector3 force)
    {
        float maxDist = 999;
        Vector3 startVelocity = force * Time.fixedDeltaTime;

        Vector3 position = startPos;
        Vector3 velocity = startVelocity;

        for (int i = 0; i < maxDist; i++)
        {
            velocity += Physics.gravity * Time.fixedDeltaTime;
            position += velocity * Time.fixedDeltaTime;

            float waterLevel = WaterHelper.GetOceanHeightAt(new Vector2(position.x, position.z));

            if (waterLevel > position.y)
            {
                float dist = Vector3.Distance(startPos, position);
                return dist;
            }
        }
        return 0f;
    }

	private void HandleShootInput(OnlinePlayerInput.PlayerControlMessage m, Vector3 dir)
	{
		if (m == OnlinePlayerInput.PlayerControlMessage.SHOOT_START_HOLD_DOWN && currentShootInputState == ShootInputState.Idle) 
		{
			//shotPower = 0f;
			currentShootInputState = ShootInputState.Ready;
			activeSide = (Vector3.Dot(dir,leftSide.forward) > 0f) ? leftSide : rightSide;
			activeCannons = activeSide.GetComponent<CannonGroup>();
			return;
		}

		if (m == OnlinePlayerInput.PlayerControlMessage.SHOOT_RELEASE && currentShootInputState == ShootInputState.Ready) 
		{
			currentShootInputState = ShootInputState.Idle;


			if(activeSide == leftSide)
			{
				Shoot(leftSide,shotPowerLeft);
				shotPowerLeft = 0f;
			}
			else
			{
				Shoot(rightSide,shotPowerRight);
				shotPowerRight = 0f;
			}

			return;
		}

		if (m == OnlinePlayerInput.PlayerControlMessage.CANCEL_START_HOLD && currentShootInputState == ShootInputState.Ready) 
		{
			currentShootInputState = ShootInputState.Idle;
			return;
		}

	}



    [ServerCallback]
    private void UpdateShootState()
    {
		CannonGroup leftCannon = leftSide.GetComponent<CannonGroup> ();
		CannonGroup rightCannon = rightSide.GetComponent<CannonGroup> ();

        float chargingSpeed = shipAttributes.CannonChargeRate * Time.deltaTime;

        shotPowerLeft += chargingSpeed;
		if(shotPowerLeft > leftCannon.CurrentCharge)
			shotPowerLeft =  leftCannon.CurrentCharge;

		shotPowerRight += chargingSpeed;
		if (shotPowerRight > rightCannon.CurrentCharge)
			shotPowerRight = rightCannon.CurrentCharge;   
    }

    [ServerCallback]
    private void Shoot(Transform side, float shotPower)
    {

		if((int)shotPower>0)
			GetComponent<PlayerSound>().RpcPlaySound(PlayerSound.PLAYER_SOUNDS.FIRE_CANNON1);

        for (int i = 0; i < (int)shotPower; i++)
        {
            Transform cannon = activeSide.GetChild(i);
            GameObject projectile = (GameObject)Instantiate(currentProjectileType, cannon.position, Quaternion.identity);
            float projectileMass = projectile.GetComponent<Rigidbody>().mass;
            float upwardsModifier = currentProjectileType.GetComponent<Projectile>().UpwardsModifier;

            Vector3 forwardDirection = new Vector3(cannon.forward.x, 0f, cannon.forward.z).normalized;
            Vector3 force = (Vector3.up * upwardsModifier + (forwardDirection * 5000f)) * projectileMass * shipAttributes.RangeMultiplier;

            projectile.GetComponent<Rigidbody>().AddForce(force);
            projectile.GetComponent<Projectile>().owner = GetComponent<CustomOnlinePlayer>();
			projectile.GetComponent<Projectile>().HullDamage *= shipAttributes.DamageModifier;
			projectile.GetComponent<Projectile>().SailDamage *= shipAttributes.DamageModifier;

            NetworkServer.Spawn(projectile);
        }
        activeCannons.CurrentCharge -= (int)shotPower;
        
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
