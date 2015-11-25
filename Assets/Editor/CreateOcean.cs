using UnityEngine;
using UnityEditor;

public class CreateOcean : ScriptableWizard
{
    [SerializeField]
    GameObject plane;
    [SerializeField]
    Transform ocean;
    [SerializeField]
    int size;
    [SerializeField]
    float offsetY = 25f;

    [MenuItem("GameObject/Create Other/Custom Ocean...")]
    static void CreateWizard()
    {
        DisplayWizard("Create Ocean", typeof(CreateOcean));
    }

    void OnWizardUpdate()
    {

    }

    void OnWizardCreate()
    {
        float width = plane.GetComponent<Renderer>().bounds.size.x;
        float length = plane.GetComponent<Renderer>().bounds.size.z;

        int index = 0;
        for (int w = 0; w < size; w++)
        {
            for (int l = 0; l < size; l++)
            {
                Vector3 position = ocean.position + new Vector3(width * w + width / 2f, offsetY, length * l + width / 2f);
                GameObject newPlane = (GameObject)Instantiate(plane, position, Quaternion.identity);
                newPlane.name = plane.name + "_" + index;
                newPlane.transform.parent = ocean;
                index++;
            }
        }
    }
}
