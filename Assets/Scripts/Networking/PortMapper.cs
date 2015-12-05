using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using Mono.Nat;

[RequireComponent(typeof(CustomNetManager))]
public class PortMapper : MonoBehaviour 
{
	INatDevice _device = null;
	int portToForward = -1;//negative value for error tracking
	List<Mapping> _maps = new List<Mapping> ();

	System.IO.StringWriter _logO = new System.IO.StringWriter ();




	private void DeviceFound(object sender, DeviceEventArgs e)
	{
		INatDevice device = e.Device;

		UIConsole.Log ("Found NAT device. External IP is " + device.GetExternalIP());
		_device = device;

	}

	private void DeviceLost(object sender, DeviceEventArgs e)
	{
		//INatDevice device = e.Device;
		UIConsole.Log ("Lost NAT device!");
	}
	


	// Use this for initialization
	void Start () 
	{
		NatUtility.Logger = _logO;
		NatUtility.DeviceFound += DeviceFound;
		NatUtility.DeviceLost += DeviceLost;
		UIConsole.Log ("Initiated NAT device discovery.");
		NatUtility.StartDiscovery ();
	}

	public void Init()
	{
		portToForward = GetComponent<CustomNetManager> ().networkPort;
		AddMapping ();
	}

	public void Stop()
	{
		//Debug.Log ("Stopped NAT device discovery.");
		RemoveAddedMappings ();
		//NatUtility.StopDiscovery ();
	}

	void ShowMappings()
	{
		if (_device == null)
			return;

		foreach (Mapping portMap in _device.GetAllMappings())
		{
			Debug.Log(portMap.ToString());
		}
	}

	void AddMapping()
	{
		if (_device == null || portToForward < 0 || portToForward > 65535)
			return;

		Mapping m = new Mapping (Protocol.Udp, portToForward, portToForward, 0);
		m.Description = "Cursed Waters (UDP)";
		Mapping m2 = new Mapping (Protocol.Tcp, portToForward, portToForward, 0);
		m2.Description = "Cursed Waters (TCP)";

		_maps.Add (m);
		_maps.Add (m2);

		_device.CreatePortMap (m);
		_device.CreatePortMap (m2);

		UIConsole.Log("Added TCP and UDP port mappings for port: " + portToForward);
	}

	void RemoveAddedMappings()
	{
		if (_device == null)
			return;

		foreach (Mapping m in _maps) 
		{
			_device.DeletePortMap(m);
		}

		UIConsole.Log ("Removed " + _maps.Count + " mappings I created on server start.");
	}


	// Update 
	void Update () 
	{
		/*
		if (Input.GetKeyUp (KeyCode.Alpha1))
			Init ();

		if (Input.GetKeyUp (KeyCode.Alpha2))
			ShowMappings ();

		if (Input.GetKeyUp (KeyCode.Alpha3))
			Stop ();

		if (Input.GetKeyUp (KeyCode.Alpha4))
			AddMapping ();

		if (Input.GetKeyUp (KeyCode.Alpha5))
			RemoveAddedMappings ();

		if (Input.GetKeyUp (KeyCode.Alpha6))
			Debug.Log (NatUtility.Logger.ToString());
			*/

	}






}
