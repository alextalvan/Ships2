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

    [SyncVar]
    //[SerializeField]
    private float sailState = 0f;
    [SerializeField]
    float sailAccelerationPerFrame = 0.01f;

    private enum ShootInputState { Idle, Ready };
    private ShootInputState currentShootInputState = ShootInputState.Idle;

    [SerializeField]
    private Transform rightSide = null;
    [SerializeField]
    private Transform leftSide = null;

    private CannonGroup rightCannons;
    private CannonGroup leftCannons;

    private LineRenderer rightLR;
    private LineRenderer leftLR;

    [SerializeField]
    private List<GameObject> projectiles = new List<GameObject>();
    private int currentProjIndex = 0;

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

    [SyncVar]
    private float barrelCoolDown = 0f;

    //client trajectory fixing
    bool startedPreviewingTrajectory = false;
    bool storedSideIsLeft;

    [SerializeField]
    float cannonRecoil = 100000f;

    //Used components
    private OnlinePlayerInput onlinePlayerInput;
    private CustomOnlinePlayer customOnlinePlayer;
    private PlayerRespawn playerRespawn;
    private OnlineSceneReferences onlineRef;

    public float GetMaxBarrelCoolDown
    {
        get { return projectiles[2].GetComponent<Projectile>().GetCoolDown; }
    }
    public float GetCurrentBarrelCoolDown
    {
        get { return barrelCoolDown; }
    }

    // Use this for initialization
    void Start()
    {
        onlineRef = GameObject.Find("OnlineSceneReferences").GetComponent<OnlineSceneReferences>();
        shipAttributes = GetComponent<ShipAttributesOnline>();
        objRigidBody = GetComponent<Rigidbody>();
        objBounds = GetComponent<Collider>().bounds;

        onlineInput = GetComponent<OnlinePlayerInput>();
        customOnlinePlayer = GetComponent<CustomOnlinePlayer>();
        playerRespawn = GetComponent<PlayerRespawn>();

        rightCannons = rightSide.GetComponent<CannonGroup>();
        leftCannons = leftSide.GetComponent<CannonGroup>();
        rightLR = rightSide.GetComponent<LineRenderer>();
        leftLR = leftSide.GetComponent<LineRenderer>();

        SetupCamera();

        onlinePlayerInput = GetComponent<OnlinePlayerInput>();
        onlinePlayerInput.OnServerReceiveRawInput += ChangeAmmoType;
        onlinePlayerInput.OnServerReceiveRawInput += HandleShootInput;

        ResetShootAndMovement();
    }

    [ClientCallback]
    public void SetupCamera()
    {
        if (!isLocalPlayer)
            return;

        Transform _camera = onlineRef.cameraRef;
        _camera.GetComponent<CameraScript>().AttachCameraTo(transform);
    }

    [ClientRpc]
    public void RpcChangeCameraState(bool attached)
    {
        if (!isLocalPlayer)
            return;

        Transform _camera = onlineRef.cameraRef;

        if (attached)
            _camera.GetComponent<CameraScript>().AttachCameraTo(transform);
        else
            _camera.GetComponent<CameraScript>().DetachCamera();
    }

    [ServerCallback]
    public void ResetShootAndMovement()
    {
        sailState = 0f;
        barrelCoolDown = 0f;
        currentShootInputState = ShootInputState.Idle;
        shotPowerLeft = leftSide.transform.childCount;
        shotPowerRight = rightSide.transform.childCount;
    }

    void Update()
    {
        SwitchClientAmmoType();
        PreviewTrajectory();
        UpdateSailUI();
        UpdateShootState();
    }

    void FixedUpdate()
    {
        ControlSails();
        Move();
        Rotate();
    }

    private void ChangeAmmoType(OnlinePlayerInput.PlayerControlMessage m, Vector3 dir)
    {
        //if (playerRespawn.IsDead)
        //    return;

        if (m == OnlinePlayerInput.PlayerControlMessage.SWITCH_START_HOLD_DOWN)
        {
            currentProjIndex++;

            if (currentProjIndex > projectiles.Count - 1)
                currentProjIndex = 0;
        }
    }

    [ClientCallback]
    private void SwitchClientAmmoType()
    {
        //if (playerRespawn.IsDead)
        //    return;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            onlineRef.AmmoIcons[currentProjIndex].SetActive(false);

            currentProjIndex++;

            if (currentProjIndex > projectiles.Count - 1)
                currentProjIndex = 0;

            onlineRef.AmmoIcons[currentProjIndex].SetActive(true);
        }
    }

    [ServerCallback]
    private void ControlSails()
    {
        if (playerRespawn.IsDead)
            return;

        if (onlineInput.GetInputValue(OnlinePlayerInput.PlayerControls.FORWARD))
            sailState += sailAccelerationPerFrame;

        if (onlineInput.GetInputValue(OnlinePlayerInput.PlayerControls.BACK))
            sailState -= sailAccelerationPerFrame;

        sailState = Mathf.Clamp01(sailState);
    }

    private void Move()
    {
        if (shipAttributes.IsDead)
            return;

        Vector3 forward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        float cureMod = (customOnlinePlayer.currentCureCarrier == transform) ? CureScript.cureCarrierSpeedDebuff : 1f;

        float baseWeight = shipAttributes.BasicSpeed * 0.25f;
        float sailWeight = shipAttributes.BasicSpeed * 0.75f;

        float totalSpeed = (baseWeight + sailWeight * shipAttributes.SailSpeedModifier) * cureMod * sailState;


        objRigidBody.AddForce(forward * objRigidBody.mass * totalSpeed);
    }

    [ServerCallback]
    private void Rotate()
    {
        if (playerRespawn.IsDead)
            return;

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
        if (!isLocalPlayer || playerRespawn.IsDead)
            return;

        rightLR.enabled = false;
        leftLR.enabled = false;

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
            float projectileMass = projectiles[currentProjIndex].GetComponent<Rigidbody>().mass;
            float upwardsModifier = projectiles[currentProjIndex].GetComponent<Projectile>().UpwardsModifier;

            if (currentProjIndex == 2)
            {
                leftLR.enabled = true;

                Vector3 backwardDirection = new Vector3(-transform.forward.x, 0f, -transform.forward.z).normalized * shipAttributes.RangeMultiplier;
                Vector3 force = (Vector3.up * upwardsModifier + (backwardDirection * 500f)) * projectileMass;

                float shotDist = GetTrajectoryDistance(transform.position - transform.forward * objBounds.extents.z, force);
                leftCannons.DrawArea(shotPowerLeft, shotDist, false, currentProjIndex);
            }
            else
            {
                if (storedSideIsLeft)
                {
                    leftLR.enabled = true;
                    Transform centerCannon = leftSide.GetChild(0);

                    Vector3 forwardDirection = new Vector3(centerCannon.forward.x, 0f, centerCannon.forward.z).normalized * shipAttributes.RangeMultiplier;
                    Vector3 force = (Vector3.up * upwardsModifier + (forwardDirection * 5000f)) * projectileMass;

                    float shotDist = GetTrajectoryDistance(centerCannon.position, force);
                    leftCannons.DrawArea(shotPowerLeft, shotDist, true, currentProjIndex);
                }
                else
                {
                    rightLR.enabled = true;

                    Transform centerCannon = rightSide.GetChild(0);

                    Vector3 forwardDirection = new Vector3(centerCannon.forward.x, 0f, centerCannon.forward.z).normalized;
                    Vector3 force = (Vector3.up * upwardsModifier + (forwardDirection * 5000f)) * projectileMass * shipAttributes.RangeMultiplier;

                    float shotDist = GetTrajectoryDistance(centerCannon.position, force);
                    rightCannons.DrawArea(shotPowerRight, shotDist, true, currentProjIndex);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyDown(KeyCode.Q) || playerRespawn.IsDead)
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
        if (playerRespawn.IsDead)
            return;

        if (currentProjIndex == 2)
        {
            if (m == OnlinePlayerInput.PlayerControlMessage.SHOOT_START_HOLD_DOWN && currentShootInputState == ShootInputState.Idle)
            {
                currentShootInputState = ShootInputState.Ready;
                return;
            }
            if (m == OnlinePlayerInput.PlayerControlMessage.SHOOT_RELEASE && currentShootInputState == ShootInputState.Ready)
            {
                currentShootInputState = ShootInputState.Idle;
                Shoot(null, 0f);
            }
        }
        else
        {
            if (m == OnlinePlayerInput.PlayerControlMessage.SHOOT_START_HOLD_DOWN && currentShootInputState == ShootInputState.Idle)
            {
                currentShootInputState = ShootInputState.Ready;
                activeSide = (Vector3.Dot(dir, leftSide.forward) > 0f) ? leftSide : rightSide;
                activeCannons = activeSide.GetComponent<CannonGroup>();
                return;
            }
            if (m == OnlinePlayerInput.PlayerControlMessage.SHOOT_RELEASE && currentShootInputState == ShootInputState.Ready)
            {
                currentShootInputState = ShootInputState.Idle;

                if (activeSide == leftSide)
                {
                    Shoot(leftSide, shotPowerLeft);
                    shotPowerLeft = 0f;
                }
                else
                {
                    Shoot(rightSide, shotPowerRight);
                    shotPowerRight = 0f;
                }

                return;
            }
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
        if (playerRespawn.IsDead)
            return;

        float chargingSpeed = shipAttributes.CannonChargeRate * Time.deltaTime;

        shotPowerLeft += chargingSpeed;
        if (shotPowerLeft > leftCannons.CurrentCharge)
            shotPowerLeft = leftCannons.CurrentCharge;

        shotPowerRight += chargingSpeed;
        if (shotPowerRight > rightCannons.CurrentCharge)
            shotPowerRight = rightCannons.CurrentCharge;

        if (barrelCoolDown > 0f)
            barrelCoolDown -= chargingSpeed;
    }

    [ServerCallback]
    private void Shoot(Transform side, float shotPower)
    {
        if (playerRespawn.IsDead)
            return;

        if (currentProjIndex == 2)
        {
            if (barrelCoolDown <= 0f)
            {
                for (int i = 0; i < 5; i++)
                {
                    Vector3 rndPos = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), Random.Range(-3f, 3f));
                    float rndForce = Random.Range(250f, 500f);

                    GameObject barrel = (GameObject)Instantiate(projectiles[currentProjIndex], transform.position + rndPos - (transform.forward * objBounds.size.z), Random.rotation);
                    Rigidbody barrelRB = barrel.GetComponent<Rigidbody>();
                    Projectile barrelProj = barrel.GetComponent<Projectile>();

                    float projectileMass = barrelRB.mass;
                    float upwardsModifier = barrelProj.UpwardsModifier;

                    Vector3 forwardDirection = new Vector3(-transform.forward.x + rndPos.x, rndPos.y, -transform.forward.z + rndPos.z).normalized;
                    Vector3 force = (Vector3.up * upwardsModifier + forwardDirection * rndForce) * projectileMass * shipAttributes.RangeMultiplier;

                    barrelRB.AddForce(force);
                    barrelProj.owner = customOnlinePlayer;
                    barrelProj.HullDamage *= shipAttributes.DamageModifier;
                    barrelProj.SailDamage *= shipAttributes.DamageModifier;

                    NetworkServer.Spawn(barrel);
                    barrelCoolDown = barrelProj.GetCoolDown;
                }
            }
        }
        else
        {
            float cannonRatio = (int)shotPower / (float)side.childCount;

            //recoil
            float recoilDirection = (side == leftSide) ? -1f : 1f;
            objRigidBody.AddRelativeTorque(0f, 0f, recoilDirection * cannonRecoil * cannonRatio, ForceMode.Impulse);

            //push audio and visual feedback to client
            PlayerFX fx = GetComponent<PlayerFX>();

            if ((int)shotPower > 0)
            {
				fx.RpcPlaySoundWithParam(PlayerFX.PLAYER_SOUNDS.FIRE_CANNON,"canonsUsed",(int)shotPower);
                fx.RpcCameraShake(0.375f, 1.5f * cannonRatio);
                fx.RpcEmitCannonSmoke((side == leftSide), (int)shotPower);
            }

            float projectileMass = projectiles[currentProjIndex].GetComponent<Rigidbody>().mass;
            float upwardsModifier = projectiles[currentProjIndex].GetComponent<Projectile>().UpwardsModifier;

            for (int i = 0; i < (int)shotPower; i++)
            {
                Transform cannon = activeSide.GetChild(i);
                GameObject projectile = (GameObject)Instantiate(projectiles[currentProjIndex], cannon.position, Random.rotation);
                Rigidbody projRB = projectile.GetComponent<Rigidbody>();
                Projectile projProj = projectile.GetComponent<Projectile>();

                Vector3 forwardDirection = new Vector3(cannon.forward.x, 0f, cannon.forward.z).normalized;
                Vector3 force = (Vector3.up * upwardsModifier + (forwardDirection * 5000f)) * projectileMass * shipAttributes.RangeMultiplier;

                projRB.AddForce(force);
                projProj.owner = customOnlinePlayer;
                projProj.HullDamage *= shipAttributes.DamageModifier;
                projProj.SailDamage *= shipAttributes.DamageModifier;

                NetworkServer.Spawn(projectile);
            }
            activeCannons.CurrentCharge -= (int)shotPower;
        }
    }

    [ClientCallback]
    void UpdateSailUI()
    {
        if (!isLocalPlayer)
            return;

        onlineRef.SailSpeedText.text = "Speed: " + (int)(sailState * 100) + "%";

        onlineRef.BarrelCd.text = "Barrel: " + (int)((1f - barrelCoolDown / projectiles[2].GetComponent<ProjectileType3>().GetCoolDown) * 100) + "%";
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
