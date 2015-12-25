using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

/// <summary>
/// Buoyancy script
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class TutorialBuoyancy : MonoBehaviour
{
    private TutorialShipAttributes shipAttributes;

    private const float CUBE_GIZMOS_SIZE = 0.5f;
    private const float SPHERE_GIZMOS_SIZE = 0.25f;

    public float GetTotalBuoyancyState { get { return totalBuoyancyState; } }
    public float GetVoxelsCount { get { return voxels.Count; } }

    private List<Voxel> voxels = new List<Voxel>();

    [SerializeField]
    private int voxelsX = 1;
    [SerializeField]
    private int voxelsY = 1;
    [SerializeField]
    private int voxelsZ = 1;
    //Voxels downwards shift
    [SerializeField]
    private float voxelsShiftY = 2f;
    //Center of a mass downwards shift
    [SerializeField]
    private float centerOfMassShiftY = 2f;
    [SerializeField]
    private float gravityModifier = 2f;

    //Object related
    private Vector3 boundingBoxCenter;
    private Rigidbody objRigidBody;
    private float buoyancyMagnitude;
    private Vector3 buoyancyForce;
    private float totalBuoyancyState;
    private float objVolume;
    private float objDensity;

    /// <summary>
    /// Provides initialization.
    /// </summary>
    private void Start()
    {
        if (!enabled)
            return;

        shipAttributes = GetComponent<TutorialShipAttributes>();
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
        objRigidBody.centerOfMass = FindCenterPoint(bounds, centerOfMassShiftY);
        boundingBoxCenter = FindCenterPoint(bounds, 1f);

        //Calculate approximate volume of object
        objVolume = GetObjectVolume(bounds);
        //Calculate it's density
        objDensity = objRigidBody.mass / objVolume;

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
        //Safe check (minimum 1 voxel is required)
        voxelsX = voxelsX < 1 ? 1 : voxelsX;
        voxelsY = voxelsY < 1 ? 1 : voxelsY;
        voxelsZ = voxelsZ < 1 ? 1 : voxelsZ;

        //Calculate offset between voxels
        float offsetX = bounds.size.x / (voxelsX + 1);
        float offsetY = bounds.size.y / (voxelsY + 1);
        float offsetZ = bounds.size.z / (voxelsZ + 1);

        //Generate voxels
        for (float ix = offsetX; ix < bounds.size.x; ix += offsetX)
        {
            for (float iy = offsetY; iy < bounds.size.y; iy += offsetY)
            {
                for (float iz = offsetZ; iz < bounds.size.z; iz += offsetZ)
                {
                    float x = bounds.min.x + ix;
                    float y = bounds.min.y + iy / voxelsShiftY;
                    float z = bounds.min.z + iz;

                    Vector3 point = transform.InverseTransformPoint(new Vector3(x, y, z));
                    Voxel newVoxel = new Voxel(point, Vector3.zero, 100f, Color.white);

                    voxels.Add(newVoxel);
                }
            }
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
        CalculatePhysics();
        UpdateTotalBuoyancy();

        if (shipAttributes)
            SinkDamagedVoxels(shipAttributes.IsDead);
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
}
