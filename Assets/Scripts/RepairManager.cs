using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RepairManager : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    List<Transform> armorPieces = new List<Transform>();
    [SerializeField]
    List<Transform> railingPieces = new List<Transform>();
    [SerializeField]
    List<Transform> mastPieces = new List<Transform>();
    [SerializeField]
    Transform masts;
    [SerializeField]
    Transform railings;
    [SerializeField]
    Transform armors;
    [SerializeField]
    Transform[] test;

    void Start () {
        armors = transform.FindChild("Armors");
        railings = transform.FindChild("Railings");
        masts = transform.FindChild("Masts");
        findPieces(armorPieces, armors);
        findPieces(railingPieces, railings);
        findPieces(mastPieces, masts);
        
    }
    void findPieces(List<Transform> list, Transform pieceKind) {
        for (int i = 0; i < pieceKind.transform.childCount; i++)
        {
            list.Add(pieceKind.transform.GetChild(i));
            for (int j = 0; j < list[i].transform.childCount; j++)
            {

                list.Add(list[i].transform.GetChild(j));

            }
        }
            
    }
    // Update is called once per frame
    void Update () {
	
	}
}
