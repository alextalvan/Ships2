using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //[SerializeField]
    //Transform debug;

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

	private GameObject _parent;

	/*bool isRotationEnabled = true;

	public void ToggleRotation(bool value)
	{
		isRotationEnabled = value;
	}*/

    // Use this for initialization
	
    void Start()
    {
		_parent = new GameObject ("Camera parent");
		transform.parent = _parent.transform;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;

        InitializeSettings(currentObject);
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //    AttachCameraTo(debug);
        //if (Input.GetKeyDown(KeyCode.D))
        //    DetachCamera();
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
			_parent.transform.position = currentObject.position + orbitDistance;
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
			orbitDistance = _parent.transform.position - currentObject.position;
    }

    void RotateCameraAround(Vector3 point, Vector3 axis, float rotationSpeed)
    {
		_parent.transform.RotateAround(point, axis, Mathf.SmoothStep(rotationSpeed, 0f, Time.deltaTime));
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
            _parent.transform.position = currentObject.position + offset;
			_parent.transform.LookAt(currentObject);
			_parent.transform.Rotate(Vector3.right, -angleOffset);
			orbitDistance = _parent.transform.position - currentObject.position;
        }
    }
}
