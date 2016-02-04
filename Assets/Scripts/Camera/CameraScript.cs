using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    enum CamState { Attached, Free };
    CamState currentCamState = CamState.Attached;
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

    float rotX;
    float rotY;

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
        ChangeCamState();
    }

    void LateUpdate()
    {
        if (currentCamState == CamState.Attached)
        {
            FollowCurrentObject();
            RotateCamera();
            ResetOrbit();
            UpdateObjPosition();
        }
        else
        {
            FreeCameraBehaviour();
        }
    }

    private void ChangeCamState()
    {
        if (Input.GetKeyDown("1") && currentCamState == CamState.Free)
        {
            if (currentObject != null)
            {
                currentCamState = CamState.Attached;
                InitializeSettings(currentObject);
            }
        }
        else if(Input.GetKeyDown("2") && currentCamState == CamState.Attached)
        {
            currentCamState = CamState.Free;
            _parent.transform.rotation = Quaternion.identity;
            transform.rotation = Quaternion.identity;
        }
    }

    private void FreeCameraBehaviour()
    {
        //Mouse input
        rotX += Input.GetAxis("Mouse X");
        rotY -= Input.GetAxis("Mouse Y");
        rotY = Mathf.Clamp(rotY, -90f, 90f);

        _parent.transform.rotation = Quaternion.Euler(rotY, rotX, 0);

        //Key input
        if (Input.GetKey(KeyCode.UpArrow))
            _parent.transform.Translate(_parent.transform.forward, Space.World);
        if (Input.GetKey(KeyCode.DownArrow))
            _parent.transform.Translate(-_parent.transform.forward, Space.World);
        if (Input.GetKey(KeyCode.RightArrow))
            _parent.transform.Translate(_parent.transform.right, Space.World);
        if (Input.GetKey(KeyCode.LeftArrow))
            _parent.transform.Translate(-_parent.transform.right, Space.World);
    }

    /// <summary>
    /// make camera to follow target object
    /// </summary>
    void FollowCurrentObject()
    {
        if (currentObject)
            // Keep us at the last known relative position
			_parent.transform.position = currentObject.position + orbitDistance;
    }

    /// <summary>
    /// camera rotation calculation
    /// </summary>
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

    /// <summary>
    /// reserved pos update
    /// </summary>
    private void UpdateObjPosition()
    {
        if (currentObject)
            currentPos = currentObject.position;
    }

    /// <summary>
    /// reset relative position after rotate
    /// </summary>
    private void ResetOrbit()
    {
        if (currentObject)
            // Reset relative position after rotate
			orbitDistance = _parent.transform.position - currentObject.position;
    }

    /// <summary>
    /// rotate camera about object according to mouse input
    /// </summary>
    /// <param name="point"></param>
    /// <param name="axis"></param>
    /// <param name="rotationSpeed"></param>
    void RotateCameraAround(Vector3 point, Vector3 axis, float rotationSpeed)
    {
		_parent.transform.RotateAround(point, axis, Mathf.SmoothStep(rotationSpeed, 0f, Time.deltaTime));
        FadeRotation();
    }

    /// <summary>
    /// fade rotation speed
    /// </summary>
    void FadeRotation()
    {
        rotation = Mathf.Lerp(rotation, 0f, rotationSpeed / 2f * Time.deltaTime);
    }

    /// <summary>
    /// attach camera to object
    /// </summary>
    /// <param name="obj"></param>
    public void AttachCameraTo(Transform obj)
    {
        currentObject = obj;
        currentPos = currentObject.position;
        InitializeSettings(obj);
    }

    /// <summary>
    /// detach camera from current object
    /// </summary>
    public void DetachCamera()
    {
        currentPos = currentObject.position;
        currentObject = null;
    }

    /// <summary>
    /// set up settings
    /// </summary>
    /// <param name="obj"></param>
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
