using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Buoyancy script
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class BuoyancyScriptOFFLINE : MonoBehaviour
{
    private const float CUBE_GIZMOS_SIZE = 0.5f;
    private const float SPHERE_GIZMOS_SIZE = 0.25f;

    [SerializeField]
    int voxelsX = 1;
    [SerializeField]
    int voxelsY = 1;
    [SerializeField]
    int voxelsZ = 1;
    [SerializeField]
    float voxelsYDownMod = 1f;
    [SerializeField]
    float centerOfMassYOffset = 2f;

    private List<Voxel> voxels = new List<Voxel>();

    //Object related
    private Rigidbody objRigidBody;
    private float buoyancyMagnitude;
    private Vector3 buoyancyForce;
    private float objSize;
    private float totalBuoyancyState;

    /// <summary>
    /// Provides initialization.
    /// </summary>
    private void Start()
    {
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

    public void ChangeBuoyancy(Vector3 position, float damage, float radius)
    {
        //if (totalBuoyancyState < 70f)
        //    return;

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

        UpdateTotalBuoyancy();
    }

    public void UpdateTotalBuoyancy()
    {
        float buoyancy = 0f;

        foreach (Voxel voxel in voxels)
        {
            buoyancy += voxel.BuoyancyState;
        }

        buoyancy /= voxels.Count;

        totalBuoyancyState = buoyancy;
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