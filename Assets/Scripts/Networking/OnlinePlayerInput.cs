using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


[NetworkSettings(channel = 0, sendInterval = 1e+6f)]
public class OnlinePlayerInput : NetworkBehaviour {

	public enum PlayerControlMessage
	{
		MOVE_FORWARD_START_HOLD = 0,
		MOVE_FORWARD_RELEASE,
		MOVE_BACK_START_HOLD,
		MOVE_BACK_RELEASE,
		MOVE_LEFT_START_HOLD,
		MOVE_LEFT_RELEASE,
		MOVE_RIGHT_START_HOLD,
		MOVE_RIGHT_RELEASE,
		SHOOT_START_HOLD_DOWN,
		SHOOT_RELEASE,
		SWITCH_START_HOLD_DOWN,
		SWITCH_RELEASE,
		CANCEL_START_HOLD,
		CANCEL_RELEASE
	}

	public enum PlayerControls
	{
		FORWARD = 0,
		BACK,
		LEFT,
		RIGHT,
		SHOOT,
		SWITCH
	}

	public delegate void RawInputDelegate(PlayerControlMessage m, Vector3 direction);

	public event RawInputDelegate OnServerReceiveRawInput;

	float[] inputTimeStamps = new float[14];
	bool[] inputValues = new bool[7];
	Vector3 _storedCameraDirection;

	public float GetLastTimeStamp(PlayerControlMessage targetControl)
	{
		return inputTimeStamps [(int)(targetControl)];
	}

	public bool GetInputValue(PlayerControls c)
	{
		return inputValues[(int)c];
	}

	public bool GetInputValue(PlayerControls c, out Vector3 direction)
	{
		if (c == PlayerControls.SHOOT)
			direction = _storedCameraDirection;
		else
			direction = new Vector3 (0, 0, 0);


		return inputValues[(int)c];
	}

	[Command]
	void CmdReceiveInput(PlayerControlMessage i)
	{
		inputTimeStamps [(int)i] = Time.time;

		//process
		//UIConsole.Log ("Received input " + i.ToString ());
		ProcessInputMessage (i);

		if (OnServerReceiveRawInput != null)
			OnServerReceiveRawInput (i, new Vector3(0,0,0));
	}

	[Command]
	void CmdReceiveDirectionShootInput(PlayerControlMessage i, Vector3 cameraDirection)
	{
		inputTimeStamps [(int)i] = Time.time;
		_storedCameraDirection = cameraDirection;
		ProcessInputMessage (i);

		if (OnServerReceiveRawInput != null)
			OnServerReceiveRawInput (i, cameraDirection);
		//GetComponent<PlayerCaptionController> ().RpcPushCaption ("I know you pressed space",2f);
	}

	void ProcessInputMessage(PlayerControlMessage i)
	{

		int intCast = (int)i;

		inputValues [intCast / 2] = (intCast % 2 == 0);

	}



	void Start()
	{
		//prevent duplicate commands from the multiple ship instances of each client
		if (NetworkManager.singleton != null && NetworkManager.singleton.isNetworkActive && 
		    NetworkManager.singleton.IsClientConnected() && !this.isLocalPlayer) 
		{
			this.enabled = false;
			return;
		}
	}
	
	 
	void Update () 
	{
		SendInput ();
	}

	[ClientCallback]
	void SendInput()
	{
		if (Input.GetKeyDown (KeyCode.W))
			CmdReceiveInput (PlayerControlMessage.MOVE_FORWARD_START_HOLD);

		if (Input.GetKeyUp (KeyCode.W))
			CmdReceiveInput (PlayerControlMessage.MOVE_FORWARD_RELEASE);

		if (Input.GetKeyDown (KeyCode.A))
			CmdReceiveInput (PlayerControlMessage.MOVE_LEFT_START_HOLD);
		
		if (Input.GetKeyUp (KeyCode.A))
			CmdReceiveInput (PlayerControlMessage.MOVE_LEFT_RELEASE);

		if (Input.GetKeyDown (KeyCode.S))
			CmdReceiveInput (PlayerControlMessage.MOVE_BACK_START_HOLD);
		
		if (Input.GetKeyUp (KeyCode.S))
			CmdReceiveInput (PlayerControlMessage.MOVE_BACK_RELEASE);

		if (Input.GetKeyDown (KeyCode.D))
			CmdReceiveInput (PlayerControlMessage.MOVE_RIGHT_START_HOLD);
		
		if (Input.GetKeyUp (KeyCode.D))
			CmdReceiveInput (PlayerControlMessage.MOVE_RIGHT_RELEASE);

		if (Input.GetKeyDown (KeyCode.Space))
        {
            Vector3 dir = GameObject.Find("OnlineSceneReferences").GetComponent<OnlineSceneReferences>().cameraRef.forward;
            CmdReceiveDirectionShootInput(PlayerControlMessage.SHOOT_START_HOLD_DOWN, dir);
        }

        //camera
        if (Input.GetKeyUp (KeyCode.Space))
		{
			Vector3 dir = GameObject.Find("OnlineSceneReferences").GetComponent<OnlineSceneReferences>().cameraRef.forward;
			CmdReceiveDirectionShootInput(PlayerControlMessage.SHOOT_RELEASE, dir);
		}

		if (Input.GetKeyDown (KeyCode.Tab))
			CmdReceiveInput (PlayerControlMessage.SWITCH_START_HOLD_DOWN);

		if (Input.GetKeyUp (KeyCode.Tab))
			CmdReceiveInput (PlayerControlMessage.SWITCH_RELEASE);

		if (Input.GetKeyDown (KeyCode.Q))
			CmdReceiveInput (PlayerControlMessage.CANCEL_START_HOLD);
		
		if (Input.GetKeyUp (KeyCode.Q))
			CmdReceiveInput (PlayerControlMessage.CANCEL_RELEASE);

	}



	[Client]
	public void ManuallySendInput(PlayerControlMessage i)
	{
		CmdReceiveInput (i);
	}
	
	public void Toggle(bool state)
	{
		if (state) 
		{
			this.enabled = true;
		} 
		else 
		{
			this.enabled = false;
			Reset();
		}

		RpcToggle (state);
	}

	[ClientRpc]
	void RpcToggle(bool state)
	{
		if (state) 
		{
			this.enabled = true;
		} 
		else 
		{
			this.enabled = false;
			Reset();
		}
	}


	public void Reset()
	{
		for (int i=0; i<inputValues.Length; i++)
			inputValues [i] = false;
	}


}

