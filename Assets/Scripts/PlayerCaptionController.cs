using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerCaptionController : NetworkBehaviour
{
	OnlineSceneReferences onlineRef;
    UICaption _middleCaption;

    void Awake()
    {
		onlineRef = GameObject.Find ("OnlineSceneReferences").GetComponent<OnlineSceneReferences> ();
        _middleCaption = onlineRef.middleCaption;
    }


    [ClientRpc]
    public void RpcPushCaption(string text, float duration)
    {
        if (isLocalPlayer)
            _middleCaption.PushCaption(text, duration);
    }

    [ClientRpc]
    public void RpcPushDebugText(string text)
    {
        if (isLocalPlayer)
            UIConsole.Log(text);
    }

	public void PushCaptionLocally(string text, float duration)
	{
		_middleCaption.PushCaption(text, duration);
	}

	[ClientRpc]
	public void RpcPushGameEndDialog(string winnerName)
	{
		onlineRef.GameEndText.text = winnerName + " has finished their time with the cure and wins the game. The server will now restart.";
		onlineRef.GameEndMessage.Enable ();
	}


    public enum BROADCAST_MODE
    {
        CAPTION = 0,
        DEBUG,
        FULL
    }


    static public void BroadcastCaption(string text, float duration, BROADCAST_MODE mode = BROADCAST_MODE.FULL)
    {
        OnlineSceneReferences onlineRef = GameObject.Find("OnlineSceneReferences").GetComponent<OnlineSceneReferences>();
        foreach (CustomOnlinePlayer p in onlineRef.allOnlinePlayers)
        {
            if (mode == BROADCAST_MODE.CAPTION || mode == BROADCAST_MODE.FULL)
                p.GetComponent<PlayerCaptionController>().RpcPushCaption(text, duration);

            if (mode == BROADCAST_MODE.DEBUG || mode == BROADCAST_MODE.FULL)
                p.GetComponent<PlayerCaptionController>().RpcPushDebugText(text);
        }
    }
}
