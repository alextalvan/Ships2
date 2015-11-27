using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

/// <summary>
/// Buoyancy script
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class BuoyancyScript : NetworkBehaviour
{
    private ShipAttributesOnline shipAttributes;

    private const float CUBE_GIZMOS_SIZE = 0.5f;
    private const float SPHERE_GIZMOS_SIZE = 0.25f;

    private List<Voxel> voxels = new List<Voxel>();

    [SerializeField]
    private int voxelsX = 1;
    [SerializeField]
    private int voxelsY = 1;
    [SerializeField]
    private int voxelsZ = 1;
    [SerializeField]
    private float voxelsYDownMod = 1f;
    [SerializeField]
    private float centerOfMassYOffset = 2f;
    [SerializeField]
    private float buoyancyLimit = 70f;

    //Object related
    private Rigidbody objRigidBody;
    private float buoyancyMagnitude;
    private Vector3 buoyancyForce;
    private float objSize;
    private float totalBuoyancyState;

    public float GetTotalBuoyancyState
    {
        get { return totalBuoyancyState; }
    }

    public float GetBuoyancyLimit
    {
        get { return buoyancyLimit; }
    }

    public float GetVoxelsCount
    {
        get { return voxels.Count; }
    }

    void Awake()
    {
        if (NetworkManager.singleton != null && NetworkManager.singleton.isNetworkActive && !NetworkServer.active)
        {
            enabled = false;
            return;
        }
    }

    /// <summary>
    /// Provides initialization.
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

        //Generate buoyancy points
        GenerateVoxels(bounds);
        //Get bounding box center, with applied Y axis offset
        objRigidBody.centerOfMass = FindCenterPoint(bounds, -bounds.extents.y / centerOfMassYOffset);
        //Get total object size
        objSize = GetObjectSize(bounds);

        //Set initial position and rotation back
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        buoyancyMagnitude = (objRigidBody.mass * Mathf.Abs(Physics.gravity.y * 2f)) / voxels.Count;
        buoyancyForce = new Vector3(0f, buoyancyMagnitude, 0f);
        UpdateTotalBuoyancy();
    }

    /// <summary>
    /// Generate voxels.
    /// </summary>
    /// <returns>List of voxels</returns>
    private void GenerateVoxels(Bounds bounds)
    {
        //Safe check (minimum 1 voxel is required)
        voxelsX = voxelsX < 1 ? 1 : voxelsX;
        voxelsY = voxelsY < 1 ? 1 : voxelsY;
        voxelsZ = voxelsZ < 1 ? 1 : voxelsZ;

        //Generate voxels
        float offsetX = bounds.size.x / (voxelsX - 1);
        float offsetY = bounds.size.y / (voxelsY - 1);
        float offsetZ = bounds.size.z / (voxelsZ - 1);
        if (voxelsX + voxelsY + voxelsZ > 3)
        {
            for (float ix = 0; ix <= bounds.size.x; ix += offsetX)
            {
                for (float iy = 0; iy <= bounds.size.y; iy += offsetY)
                {
                    for (float iz = 0; iz <= bounds.size.z; iz += offsetZ)
                    {
                        float x = bounds.min.x + ix;
                        float y = bounds.min.y + (iy / voxelsYDownMod);
                        float z = bounds.min.z + iz;

                        Vector3 point = transform.InverseTransformPoint(new Vector3(x, y, z));
                        Voxel newVoxel = new Voxel(point, Vector3.zero, 100f, Color.white);

                        voxels.Add(newVoxel);
                    }
                }
            }
        }
        else
        {
            Voxel newVoxel = new Voxel(Vector3.zero, Vector3.zero, 100f, Color.white);

            voxels.Add(newVoxel);
        }
    }

    /// <summary>
    /// Calculate center point
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="offsetY"></param>
    /// <returns>Center point of bounding box</returns>
    private Vector3 FindCenterPoint(Bounds bounds, float offsetY)
    {
        if (voxelsX + voxelsY + voxelsZ > 4)
        {
            Vector3 centerPos = Vector3.zero;

            foreach (Voxel voxel in voxels)
            {
                centerPos += voxel.Position;
            }

            centerPos /= voxels.Count;

            return new Vector3(centerPos.x, centerPos.y + offsetY, centerPos.z);
        }
        else
        {
            return new Vector3(0f, 0f, 0f);
        }
    }

    /// <summary>
    /// Calculate total size of all object's sides
    /// </summary>
    /// <param name="bounds"></param>
    /// <returns>Total object size</returns>
    private float GetObjectSize(Bounds bounds)
    {
        float[] sides = new float[3] { bounds.size.x, bounds.size.y, bounds.size.z };
        return sides[0] * sides[1] * sides[2];
    }

    private void FixedUpdate()
    {
        CalculatePhysics();
        UpdateTotalBuoyancy();

		if(shipAttributes!=null)
        	SinkDamagedVoxels(shipAttributes.IsDead);
    }

    /// <summary>
    /// Calculates physics.
    /// </summary>
    private void CalculatePhysics()
    {
        foreach (Voxel voxel in voxels)
        {
            Vector3 voxelLocalPos = voxel.Position;
            float voxelBuoyancy = voxel.BuoyancyState / 100f;

            Vector3 voxelWorldPos = transform.TransformPoint(voxelLocalPos);

            float waterLevel = WaterHelper.GetOceanHeightAt(new Vector2(voxelWorldPos.x, voxelWorldPos.z));

            if (voxelWorldPos.y < waterLevel)
            {
                float depthMagnitude = Mathf.Sqrt((waterLevel - voxelWorldPos.y) + (objSize / objRigidBody.mass) / voxels.Count);

                if (depthMagnitude > 2f)
                    depthMagnitude = 2f;

                float resistanceMagnitude = (objRigidBody.mass + depthMagnitude) / voxels.Count;

                Vector3 dragForce = -objRigidBody.GetPointVelocity(voxelWorldPos);
                Vector3 dampingForce = dragForce * resistanceMagnitude;
                Vector3 force = dampingForce + depthMagnitude * buoyancyForce;
                Vector3 totalForce = force * voxelBuoyancy;

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
        objRigidBody.AddForce(Physics.gravity * objRigidBody.mass);
    }

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

    public void ChangeBuoyancy(Vector3 position, float damage, float radius)
    {
        Voxel closestVoxel = null;
        float minDist = radius;

        foreach (Voxel voxel in voxels)
        {
            float dist = (position - transform.TransformPoint(voxel.Position)).magnitude;
            if (dist < minDist && voxel.BuoyancyState > 0f)
            {
                closestVoxel = voxel;
                minDist = dist;
            }
        }

        if (closestVoxel != null)
        {
            if (closestVoxel.BuoyancyState > 0f)
                closestVoxel.BuoyancyState -= damage;

            if (closestVoxel.BuoyancyState < 0f)
                closestVoxel.BuoyancyState = 0f;
        }
    }

    public void UpdateTotalBuoyancy()
    {
        totalBuoyancyState = 0f;

        foreach (Voxel voxel in voxels)
        {
            totalBuoyancyState += voxel.BuoyancyState;
        }

        totalBuoyancyState /= voxels.Count;
    }

    public void Reset()
    {
        foreach (Voxel voxel in voxels)
        {
            voxel.BuoyancyState = 100f;
        }
        UpdateTotalBuoyancy();
    }

    /// <summary>
    /// Draws gizmos.
    /// </summary>
    private void OnDrawGizmos()
    {
        foreach (Voxel voxel in voxels)
        {
            Gizmos.color = voxel.Color;
            Vector3 voxelWorldPos = transform.TransformPoint(voxel.Position);
            Gizmos.DrawWireSphere(voxelWorldPos, SPHERE_GIZMOS_SIZE);
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

    public Color Color
    {
        get { return color; }
        set { color = value; }
    }

    public void Sink()
    {
        if (buoyancyState > 40f)
            buoyancyState-=0.175f;
    }
}