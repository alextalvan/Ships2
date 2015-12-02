using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialScript : MonoBehaviour {
    [SerializeField]
    List<TutorialSpriteManager> sprites = new List<TutorialSpriteManager>();
    int stage;
	// Use this for initialization
	void Start () {
        
	}

    public void RemoveItem(TutorialSpriteManager item)
    {
        sprites.Remove(item);
    }
	// Update is called once per frame
	void Update () {
        switch (stage)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.W))
                {
                    sprites[0].enabled = true;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    sprites[1].enabled = true;
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    sprites[2].enabled = true;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    sprites[3].enabled = true;
                }
                break;
        }
        
	}
}
