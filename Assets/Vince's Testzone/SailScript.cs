using UnityEngine;
using System.Collections;

public class SailScript : MonoBehaviour {

    ShipAttributes myAttributes;
    [SerializeField]
    //Material[] mats = new Material[2];
    //Material currentMat;
	// Use this for initialization
	void Start () {
        //currentMat = GetComponent<Material>();
        myAttributes = GetComponentInParent<ShipAttributes>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void damageSail(int Index) {
        //bullcrap
        //currentMat = mats[Index];
        myAttributes.ChangeSailHealth(-25,0);
    }
     
    public void liftSail() {
        
    }
    public void dropSail() {

    }
}
