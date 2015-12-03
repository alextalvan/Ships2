using UnityEngine;
using System.Collections;

public class DisableLight : MonoBehaviour
{

    Light flash;
    float time = 0.1f;

    // Use this for initialization
    void Start()
    {
        flash = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flash.enabled)
        {
            time -= Time.deltaTime;
            if (time <= 0f)
                flash.enabled = false;
        }
    }
}
