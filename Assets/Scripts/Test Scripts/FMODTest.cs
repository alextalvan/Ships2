using UnityEngine;
using System.Collections;

public class FMODTest : MonoBehaviour {


	//declare events and instances

	FMOD.Studio.EventInstance example;
	FMOD.Studio.ParameterInstance examplePara;
	FMOD.Studio.ParameterInstance examplePara2;

	// Use this for initialization
	void Start () {

		//connect FMOD events and instances to variables
		example = FMOD_StudioSystem.instance.GetEvent ("event:/Pickup");
		example.getParameter ("Type", out examplePara);
		example.getParameter ("Volume", out examplePara2);

			
	}
	
	// Update is called once per frame
	void Update () {
	
		//to change the parameter value use
		//examplePara.setValue();

		//to stop the event
		//example.stop(); example.release();

		if (Input.GetKeyDown ("space")) {

			//set parameter to desireable value, then play the event
			examplePara.setValue(1);
			examplePara2.setValue(9);
			example.start ();

		}


	}
}
