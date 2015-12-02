using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
//[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(TutorialShipAttributes))]
public class TutorialShipScript : MonoBehaviour
{
	private TutorialShipAttributes shipAttributes;
	
	private const float CUBE_GIZMOS_SIZE = 0.5f;
	private const float SPHERE_GIZMOS_SIZE = 0.1f;

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
	
	private TutorialCannonGroup rightCannons;
	private TutorialCannonGroup leftCannons;
	
	private LineRenderer rightLR;
	private LineRenderer leftLR;
	
	[SerializeField]
	private List<GameObject> projectiles = new List<GameObject>();
	private int currentProjIndex = 0;
	
	//private OnlinePlayerInput onlineInput;
	
	//Object
	private Rigidbody objRigidBody;
	private Bounds objBounds;
	
	//Ship properties
	private Transform activeSide;
	private TutorialCannonGroup activeCannons;
	
	//[SyncVar]
	private float shotPowerLeft = 0f;
	
	//[SyncVar]
	private float shotPowerRight = 0f;
	
	//[SyncVar]
	private float barrelCoolDown = 0f;
	
	//client trajectory fixing
	bool startedPreviewingTrajectory = false;
	bool storedSideIsLeft;
	
	[SerializeField]
	float cannonRecoil = 100000f;
	
	//Used components
	//private OnlinePlayerInput onlinePlayerInput;
	//private CustomOnlinePlayer customOnlinePlayer;
	//private PlayerRespawn playerRespawn;
	private OnlineSceneReferences onlineRef;
	
	public float GetMaxBarrelCoolDown
	{
		get { return projectiles[2].GetComponent<TutorialProjectile>().GetCoolDown; }
	}
	public float GetCurrentBarrelCoolDown
	{
		get { return barrelCoolDown; }
	}
	public float GetSailState
	{
		get { return sailState; }
	}

	public bool MovementLock = true;
	public bool CombatLock = true;
	

	
	// Use this for initialization
	void Start()
	{
		onlineRef = GameObject.Find("OnlineSceneReferences").GetComponent<OnlineSceneReferences>();
		shipAttributes = GetComponent<TutorialShipAttributes>();
		objRigidBody = GetComponent<Rigidbody>();
		objBounds = GetComponent<Collider>().bounds;
		
		//onlineInput = GetComponent<OnlinePlayerInput>();
		//customOnlinePlayer = GetComponent<CustomOnlinePlayer>();
		//playerRespawn = GetComponent<PlayerRespawn>();
		
		rightCannons = rightSide.GetComponent<TutorialCannonGroup>();
		leftCannons = leftSide.GetComponent<TutorialCannonGroup>();
		rightLR = rightSide.GetComponent<LineRenderer>();
		leftLR = leftSide.GetComponent<LineRenderer>();
		
		GameObject.Find("TutorialSceneReferences").GetComponent<TutorialSceneReferences>().UpgradeScreen.SetTargetPlayer (GetComponent<CustomOnlinePlayer> ());
		
		//onlinePlayerInput = GetComponent<OnlinePlayerInput>();
		//onlinePlayerInput.OnServerReceiveRawInput += ChangeAmmoType;
		//onlinePlayerInput.OnServerReceiveRawInput += HandleShootInput;
		

	}
	
	void Update()
	{
		SwitchClientAmmoType();
		PreviewTrajectory();
		UpdateSailUI();
		UpdateShootState();
		HandleShootInput ();
	}
	
	void FixedUpdate()
	{
		ControlSails();
		Move();
		Rotate();
	}

	private void SwitchClientAmmoType()
	{
		//if (playerRespawn.IsDead)
		//    return;



		if (CombatLock)
			return;

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			onlineRef.AmmoIcons[currentProjIndex].SetActive(false);
			
			currentProjIndex++;
			
			if (currentProjIndex > projectiles.Count - 1)
				currentProjIndex = 0;
			
			onlineRef.AmmoIcons[currentProjIndex].SetActive(true);
		}
	}

	private void ControlSails()
	{
		if (MovementLock) 
		{
			sailState = 0f;
			return;
		}

		
		if (Input.GetKey(KeyCode.W))
			sailState += sailAccelerationPerFrame;
		
		if (Input.GetKey(KeyCode.S))
			sailState -= sailAccelerationPerFrame;
		
		sailState = Mathf.Clamp01(sailState);
	}
	
	private void Move()
	{
		if (shipAttributes.IsDead || MovementLock)
			return;
		
		Vector3 forward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
		//float cureMod = (customOnlinePlayer.currentCureCarrier == transform) ? CureScript.cureCarrierSpeedDebuff : 1f;
		
		float baseWeight = shipAttributes.BasicSpeed * 0.25f;
		float sailWeight = shipAttributes.BasicSpeed * 0.75f;
		
		float totalSpeed = (baseWeight + sailWeight * shipAttributes.SailSpeedModifier) * sailState;
		
		
		objRigidBody.AddForce(forward * objRigidBody.mass * totalSpeed);
	}

	private void Rotate()
	{
		if (MovementLock)
			return;

		Vector3 right = new Vector3(transform.right.x, 0f, transform.right.z).normalized;
		float currentHorizonSpeed = new Vector3(objRigidBody.velocity.x, 0f, objRigidBody.velocity.z).magnitude;
		
		if (Input.GetKey(KeyCode.A))
		{
			objRigidBody.AddForceAtPosition(right * (objRigidBody.mass + currentHorizonSpeed * shipAttributes.SteeringModifier), transform.position - (transform.forward * objBounds.extents.z) - (transform.up * objBounds.extents.y));
			objRigidBody.AddForceAtPosition(-right * (objRigidBody.mass + currentHorizonSpeed * shipAttributes.SteeringModifier), transform.position + (transform.forward * objBounds.extents.z) - (transform.up * objBounds.extents.y));
		}
		if (Input.GetKey(KeyCode.D))
		{
			objRigidBody.AddForceAtPosition(-right * (objRigidBody.mass + currentHorizonSpeed * shipAttributes.SteeringModifier), transform.position - (transform.forward * objBounds.extents.z) - (transform.up * objBounds.extents.y));
			objRigidBody.AddForceAtPosition(right * (objRigidBody.mass + currentHorizonSpeed * shipAttributes.SteeringModifier), transform.position + (transform.forward * objBounds.extents.z) - (transform.up * objBounds.extents.y));
		}
	}

	private void PreviewTrajectory()
	{
		rightLR.enabled = false;
		leftLR.enabled = false;

		if (CombatLock) 
			return;
		
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
			float upwardsModifier = projectiles[currentProjIndex].GetComponent<TutorialProjectile>().UpwardsModifier;
			
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
		
		if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyDown(KeyCode.Q))
		{
			startedPreviewingTrajectory = false;
		}
	}

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
	
	private void HandleShootInput()
	{
		if (CombatLock)
			return;

		if (currentProjIndex == 2)
		{
			if (Input.GetKeyDown(KeyCode.Space) && currentShootInputState == ShootInputState.Idle)
			{
				currentShootInputState = ShootInputState.Ready;
				return;
			}
			if (Input.GetKeyUp(KeyCode.Space) && currentShootInputState == ShootInputState.Ready)
			{
				currentShootInputState = ShootInputState.Idle;
				Shoot(null, 0f);
			}
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Space) && currentShootInputState == ShootInputState.Idle)
			{
				currentShootInputState = ShootInputState.Ready;
				activeSide = (Vector3.Dot(Camera.main.transform.forward, leftSide.forward) > 0f) ? leftSide : rightSide;
				activeCannons = activeSide.GetComponent<TutorialCannonGroup>();
				return;
			}
			if (Input.GetKeyUp(KeyCode.Space) && currentShootInputState == ShootInputState.Ready)
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
		
		if (Input.GetKeyDown(KeyCode.Q) && currentShootInputState == ShootInputState.Ready)
		{
			currentShootInputState = ShootInputState.Idle;
			return;
		}
	}

	private void UpdateShootState()
	{
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

	private void Shoot(Transform side, float shotPower)
	{
		
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
					TutorialProjectile barrelProj = barrel.GetComponent<TutorialProjectile>();
					
					float projectileMass = barrelRB.mass;
					float upwardsModifier = barrelProj.UpwardsModifier;
					
					Vector3 forwardDirection = new Vector3(-transform.forward.x + rndPos.x, rndPos.y, -transform.forward.z + rndPos.z).normalized;
					Vector3 force = (Vector3.up * upwardsModifier + forwardDirection * rndForce) * projectileMass * shipAttributes.RangeMultiplier;
					
					barrelRB.AddForce(force);
					//barrelProj.owner = customOnlinePlayer;
					barrelProj.HullDamage *= shipAttributes.DamageModifier;
					barrelProj.SailDamage *= shipAttributes.DamageModifier;
					
					//NetworkServer.Spawn(barrel);
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
				fx.PlaySound(PlayerFX.PLAYER_SOUNDS.FIRE_CANNON1);
				fx.CameraShake(0.375f, 1.5f * cannonRatio);
				fx.EmitCannonSmoke((side == leftSide), (int)shotPower);
			}
			
			float projectileMass = projectiles[currentProjIndex].GetComponent<Rigidbody>().mass;
			float upwardsModifier = projectiles[currentProjIndex].GetComponent<TutorialProjectile>().UpwardsModifier;
			
			for (int i = 0; i < (int)shotPower; i++)
			{
				Transform cannon = activeSide.GetChild(i);
				GameObject projectile = (GameObject)Instantiate(projectiles[currentProjIndex], cannon.position, Random.rotation);
				Rigidbody projRB = projectile.GetComponent<Rigidbody>();
				TutorialProjectile projProj = projectile.GetComponent<TutorialProjectile>();
				
				float rndGunPowderAmount = Random.Range(4500f, 5000f);
				
				Vector3 forwardDirection = new Vector3(cannon.forward.x, 0f, cannon.forward.z).normalized;
				Vector3 force = (Vector3.up * upwardsModifier + (forwardDirection * rndGunPowderAmount)) * projectileMass * shipAttributes.RangeMultiplier;
				
				projRB.AddForce(force);
				//projProj.owner = customOnlinePlayer;
				projProj.HullDamage *= shipAttributes.DamageModifier;
				projProj.SailDamage *= shipAttributes.DamageModifier;
				
				//NetworkServer.Spawn(projectile);
			}
			activeCannons.CurrentCharge -= (int)shotPower;
		}
	}

	void UpdateSailUI()
	{	
		onlineRef.SailSpeedText.gameObject.SetActive (!MovementLock);

		onlineRef.SailSpeedText.text = "Speed: " + (int)(sailState * 100) + "%";


		onlineRef.BarrelCd.gameObject.SetActive (!CombatLock);
		
		onlineRef.BarrelCd.text = "Barrel: " + (int)((1f - barrelCoolDown / projectiles[2].GetComponent<TutorialProjectile>().GetCoolDown) * 100) + "%";
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
