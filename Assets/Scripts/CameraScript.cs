using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    Vector3 offset = new Vector3(30f, 30f, 30f);
    [SerializeField]
    float rotationSpeed = 5f;
    [SerializeField]
    float rotationSpeedLimit = 25f;
    [SerializeField]
    float angleOffset = 15f;

    public Transform currentObject = null;
    private Vector3 currentPos;
    private Vector3 orbitDistance;
    private float rotation;

    // Use this for initialization
    void Start()
    {
        InitializeSettings(currentObject);
    }

    void LateUpdate()
    {
        FollowCurrentObject();
        RotateCamera();
        ResetOrbit();
        UpdateObjPosition();
    }

    void FollowCurrentObject()
    {
        if (currentObject)
            // Keep us at the last known relative position
            transform.position = Vector3.MoveTowards(transform.position, currentObject.position + orbitDistance, orbitDistance.magnitude);
    }

    void RotateCamera()
    {
        if (Mathf.Abs(rotation) < rotationSpeedLimit)
            rotation += Input.GetAxis("Mouse X");

        if (rotation != 0)
        {
            if (currentObject)
                RotateCameraAround(currentObject.position, Vector3.up, rotation * rotationSpeed * Time.deltaTime);
            else
                RotateCameraAround(currentPos, Vector3.up, rotation * rotationSpeed * Time.deltaTime);
        }
    }

    private void UpdateObjPosition()
    {
        if (currentObject)
            currentPos = currentObject.position;
    }

    private void ResetOrbit()
    {
        if (currentObject)
            // Reset relative position after rotate
            orbitDistance = transform.position - currentObject.position;
    }

    void RotateCameraAround(Vector3 point, Vector3 axis, float rotationSpeed)
    {
        transform.RotateAround(point, axis, Mathf.SmoothStep(rotationSpeed, 0f, Time.deltaTime));
        FadeRotation();
    }

    void FadeRotation()
    {
        rotation = Mathf.Lerp(rotation, 0f, rotationSpeed / 2f * Time.deltaTime);
    }

    public void AttachCameraTo(Transform obj)
    {
        currentObject = obj;
        currentPos = currentObject.position;
        InitializeSettings(obj);
    }

    public void DetachCamera()
    {
        currentPos = currentObject.position;
        currentObject = null;
    }

    private void InitializeSettings(Transform obj)
    {
        if (obj)
        {
            transform.position = currentObject.position + offset;
            transform.LookAt(currentObject);
            transform.Rotate(Vector3.right, -angleOffset);
            orbitDistance = transform.position - currentObject.position;
        }
    }
}
