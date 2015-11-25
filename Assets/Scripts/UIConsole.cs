using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIConsole : MonoBehaviour 
{
	public int charSize = 16;
	public Rect containerInfo = new Rect(0,0,768,256);

	string _mainText = "";
	GUIStyle _consoleStyle = new GUIStyle();

	static UIConsole _instance = null;
	bool hide = false;

	static UIConsole Instance
	{
		get 
		{ 
			if (_instance == null)
			{
				_instance = new GameObject ("UIConsole", typeof(UIConsole)).GetComponent<UIConsole> ();
				DontDestroyOnLoad(_instance.gameObject);
			}

			return _instance;
		}
	}


	static public void SetParams(int charSize, Rect boundingBox, bool enabled = true)
	{
		Instance.charSize = charSize;
		Instance.containerInfo = boundingBox;
		Instance.hide = !enabled;
	}

	static public void Log(string message)
	{
		Instance.PushDebugLine (message);
	}

	public void PushDebugLine(string message)
	{
		Debug.Log (message);
		_mainText += message;
		_mainText += "\n";
	}

	void OnGUI()
	{
		if (hide)
			return;

		_consoleStyle = GUI.skin.box;
		_consoleStyle.alignment = TextAnchor.UpperLeft;
		_consoleStyle.fontSize = charSize;

		int lines = 0;
		foreach (char c in _mainText)
			if (c == '\n')
				lines++;

		int maxLines = (int)(containerInfo.height / _consoleStyle.lineHeight);


		if (lines > maxLines) 
		{
			string[] separateLines = _mainText.Split ('\n');
			string newMainText = "";

			for(int i= lines - maxLines; i < lines; ++i)
			{
				newMainText += separateLines[i];
				newMainText += "\n";
			}

			_mainText = newMainText;
		}
		 
		GUI.Box (containerInfo, _mainText, _consoleStyle);
	}

}
