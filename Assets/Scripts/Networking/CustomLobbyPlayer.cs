using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CustomLobbyPlayer : NetworkLobbyPlayer
{
	//const int maxNickLength = 20;

	//executed on server
	[Command]
	public void CmdSendNickname(string nickname)
	{
		CustomNetManager net = GameObject.Find ("NetworkManager").GetComponent<CustomNetManager> ();//.nicknames [this.connectionToServer] = nickname;
		int myIndex = NetworkServer.connections.IndexOf (this.connectionToClient);
		if (!net.nicknames.ContainsKey (myIndex))
			net.nicknames.Add (myIndex, nickname);
		else
			net.nicknames [myIndex] = nickname;
	}
	
}
