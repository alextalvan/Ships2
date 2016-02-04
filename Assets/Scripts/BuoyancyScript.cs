using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

/// <summary>
/// Buoyancy script
/// Assumes that any object it attached to can float
/// Both mass and size of object affect it's floating behaviour
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class BuoyancyScript : NetworkBehaviour
{
    private const float CUBE_GIZMOS_SIZE = 0.5f;
    private const float SPHERE_GIZMOS_SIZE = 0.25f;

    public float GetTotalBuoyancyState { get { return totalBuoyancyState; } }
    public float GetVoxelsCount { get { return voxels.Count; } }

    private List<Voxel> voxels = new List<Voxel>();

    private ShipAttributesOnline shipAttributes;

    /// <summary>
    /// Voxels properties
    /// </summary>
    [SerializeField]
    private int voxelsPerAxis = 1;
    [Range(1, 10)]
    [SerializeField]
    private float voxelsShiftY = 2f; //Voxels downwards shift
    [Range(1, 10)]
    [SerializeField]
    private float centerOfMassShiftY = 2f; //Center of mass downwards shift
    [Range(1, 5)]
    [SerializeField]
    private float gravityModifier = 2f;

    /// <summary>
    /// Object related
    /// </summary>
    private Vector3 boundingBoxCenter;
    private Rigidbody objRigidBody;
    private float buoyancyMagnitude;
    private Vector3 buoyancyForce;
    private float totalBuoyancyState;
    private float objVolume;
    private float objDensity;

    private Vector3 centerOfMass;

    void Awake()
    {
        if (NetworkManager.singleton != null && NetworkManager.singleton.isNetworkActive && !NetworkServer.active)
        {
            enabled = false;
            return;
        }
    }

    /// <summary>
    /// Provides initialization
    /// </summary>
    private void Start()
    {
        if (!enabled)
            return;

        shipAttributes = GetComponent<ShipAttributesOnline>();
        objRigidBody = GetComponent<Rigidbody>();

        //Store initial position and rotation
        Vector3 initialPosition = transform.position;
        Quaternion initialRotation = transform.rotation;

        //Reset object's position and rotation
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        Bounds bounds = GetComponent<Collider>().bounds;

        GenerateVoxels(bounds); //Generate buoyancy points

        objRigidBody.centerOfMass = FindCenterPoint(bounds, centerOfMassShiftY); //Get bounding box center, with applied Y axis offset and assign it as center of mass
        centerOfMass = objRigidBody.centerOfMass;
        boundingBoxCenter = FindCenterPoint(bounds, 1f); //Get bounding box center

        objVolume = GetObjectVolume(bounds); //Calculate approximate volume of an object
        objDensity = objRigidBody.mass / objVolume; //Calculate it's density

        //Set initial position and rotation back
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        buoyancyMagnitude = (objRigidBody.mass * Mathf.Abs(Physics.gravity.y * gravityModifier));
        buoyancyForce = new Vector3(0f, buoyancyMagnitude, 0f);
        UpdateTotalBuoyancy();
    }

    /// <summary>
    /// Generate voxels
    /// </summary>
    /// <returns>List of voxels</returns>
    private void GenerateVoxels(Bounds bounds)
    {
        voxelsPerAxis = voxelsPerAxis < 1 ? 1 : voxelsPerAxis;

        if (voxelsPerAxis > 1)
        {
            //Calculate offset between voxels
            float offsetX = bounds.size.x / (voxelsPerAxis - 1);
            float offsetY = bounds.size.y / (voxelsPerAxis - 1);
            float offsetZ = bounds.size.z / (voxelsPerAxis - 1);

            //Generate voxels
            for (int ix = 0; ix < voxelsPerAxis; ix++)
            {
                for (int iy = 0; iy < voxelsPerAxis; iy++)
                {
                    for (int iz = 0; iz < voxelsPerAxis; iz++)
                    {
                        float x = bounds.min.x + (offsetX * ix);
                        float y = bounds.min.y + ((offsetY * iy) / voxelsShiftY);
                        float z = bounds.min.z + (offsetZ * iz);

                        Vector3 point = transform.InverseTransformPoint(new Vector3(x, y, z));
                        Voxel newVoxel = new Voxel(point, Vector3.zero, 100f, Color.white);

                        voxels.Add(newVoxel);
                    }
                }
            }
        } else {
            Vector3 point = FindCenterPoint(bounds, voxelsShiftY);
            Voxel newVoxel = new Voxel(point, Vector3.zero, 100f, Color.white);
            voxels.Add(newVoxel);
        }
    }

    /// <summary>
    /// Calculate center point of mass
    /// </summary>
    /// <param name="bounds">Axially alligned bounds</param>
    /// <param name="offsetY">Y axis offset</param>
    /// <returns>Center point of bounding box</returns>
    private Vector3 FindCenterPoint(Bounds bounds, float shiftY)
    {
        return new Vector3(bounds.min.x + bounds.extents.x, bounds.min.y + (bounds.extents.y / shiftY), bounds.min.z + bounds.extents.z);
    }

    /// <summary>
    /// Calculate result of multiplication of all object's sides 
    /// Gives us ~ object's volume
    /// </summary>
    /// <param name="bounds">Axially alligned bounds</param>
    /// <returns>Approximate object's volume</returns>
    private float GetObjectVolume(Bounds bounds)
    {
        float[] sides = new float[3] { bounds.size.x, bounds.size.y, bounds.size.z };
        return sides[0] * sides[1] * sides[2];
    }

    private void FixedUpdate()
    {
        objRigidBody.centerOfMass = centerOfMass;
        CalculatePhysics();

        if (shipAttributes)
        {
            UpdateTotalBuoyancy();
            SinkDamagedVoxels(shipAttributes.IsDead);
        }
    }

    /// <summary>
    /// Calculates physics
    /// </summary>
    private void CalculatePhysics()
    {
        foreach (Voxel voxel in voxels)
        {
            Vector3 voxelLocalPos = voxel.Position;
            Vector3 voxelWorldPos = transform.TransformPoint(voxelLocalPos);

            float waterLevel = WaterHelper.GetOceanHeightAt(new Vector2(voxelWorldPos.x, voxelWorldPos.z));

            if (voxelWorldPos.y < waterLevel)
            {
                float depthMagnitudeSqr = Mathf.Sqrt(waterLevel - voxelWorldPos.y);
                float voxelRelativePosFactor = Mathf.Abs(voxelWorldPos.y - boundingBoxCenter.y) / objDensity;

                if ((shipAttributes && !shipAttributes.IsDead) || !shipAttributes)
                    depthMagnitudeSqr += voxelRelativePosFactor;

                Vector3 dampingForce = -objRigidBody.GetPointVelocity(voxelWorldPos) * objRigidBody.mass;

                Vector3 force = dampingForce + buoyancyForce * depthMagnitudeSqr;
                Vector3 totalForce = (force * voxel.BuoyancyModifier) / voxels.Count;

                objRigidBody.AddForceAtPosition(totalForce, voxelWorldPos);

                voxel.Color = Color.blue;
                voxel.Force = totalForce;
            }
            else
            {
                voxel.Color = Color.yellow;
                voxel.Force = Vector3.zero;
            }
        }
        objRigidBody.AddForce(Physics.gravity * objRigidBody.mass * gravityModifier);
    }

    /// <summary>
    /// Constantly decrease voxels buoyancy state if player is dead
    /// </summary>
    /// <param name="dead">Player is dead</param>
    private void SinkDamagedVoxels(bool dead)
    {
        if (dead)
        {
            foreach (Voxel voxel in voxels)
            {
                voxel.Sink();
            }
        }
    }

    /// <summary>
    /// Decrease voxels buoyancy state for certain amount
    /// </summary>
    /// <param name="position">Impact position</param>
    /// <param name="damage">Damage amount</param>
    /// <param name="radius">Action radius</param>
    public void DamageVoxels(Vector3 position, float damage, float radius)
    {
        damage /= voxels.Count;

        foreach (Voxel voxel in voxels)
        {
            float distToVoxel = (position - transform.TransformPoint(voxel.Position)).magnitude;

            if (distToVoxel < radius && voxel.BuoyancyState > 50f)
            {
                voxel.BuoyancyState -= damage;

                if (voxel.BuoyancyState < 50f)
                    voxel.BuoyancyState = 50f;
            }
        }
    }

    /// <summary>
    /// Increase voxels buoyancy state for certain amount
    /// </summary>
    /// <param name="amount">Repair amount</param>
    public void RepairVoxels(float amount)
    {
        amount /= voxels.Count;

        foreach (Voxel voxel in voxels)
        {
            if (voxel.BuoyancyState < 100f)
            {
                voxel.BuoyancyState += amount;

                if (voxel.BuoyancyState > 100f)
                    voxel.BuoyancyState = 100f;
            }
        }
    }

    /// <summary>
    /// Update total buoyancy value
    /// </summary>
    public void UpdateTotalBuoyancy()
    {
        totalBuoyancyState = 0f;

        foreach (Voxel voxel in voxels)
        {
            totalBuoyancyState += voxel.BuoyancyState;
        }

        totalBuoyancyState /= voxels.Count;
    }

    /// <summary>
    /// Reset buoyancy state for all voxels
    /// </summary>
    public void Reset()
    {
        foreach (Voxel voxel in voxels)
        {
            voxel.BuoyancyState = 100f;
        }

        UpdateTotalBuoyancy();
    }

    /// <summary>
    /// Draws gizmos
    /// </summary>
    private void OnDrawGizmos()
    {
        DrawGizmos();
    }

    [ServerCallback]
    void DrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(objRigidBody.worldCenterOfMass, SPHERE_GIZMOS_SIZE * 5f);

        foreach (Voxel voxel in voxels)
        {
            Gizmos.color = voxel.Color;
            Vector3 voxelWorldPos = transform.TransformPoint(voxel.Position);
            Gizmos.DrawWireCube(voxelWorldPos, new Vector3(CUBE_GIZMOS_SIZE, CUBE_GIZMOS_SIZE, CUBE_GIZMOS_SIZE));
            Gizmos.DrawLine(voxelWorldPos, voxelWorldPos + voxel.Force / objRigidBody.mass);
        }
    }
}

/// <summary>
/// Voxel class
/// </summary>
public class Voxel
{
    private Vector3 position;
    private Vector3 force;
    private float buoyancyState;
    private Color color;

    public Voxel(Vector3 pPosition, Vector3 pForce, float pBuoyancyState, Color pColor)
    {
        position = pPosition;
        force = pForce;
        buoyancyState = pBuoyancyState;
        color = pColor;
    }

    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }

    public Vector3 Force
    {
        get { return force; }
        set { force = value; }
    }

    public float BuoyancyState
    {
        get { return buoyancyState; }
        set { buoyancyState = value; }
    }

    public float BuoyancyModifier
    {
        get { return buoyancyState / 100f; }
    }

    public Color Color
    {
        get { return color; }
        set { color = value; }
    }

    public void Sink()
    {
        if (buoyancyState > 0f)
            buoyancyState -= BuoyancyModifier;
    }
}