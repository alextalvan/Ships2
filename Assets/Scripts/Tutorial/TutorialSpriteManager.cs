using UnityEngine;
using System.Collections;

public class TutorialSpriteManager : MonoBehaviour {
    SpriteRenderer sprite;
    TutorialScript controller;
	// Use this for initialization
	void Start () {
        controller = GetComponentInParent<TutorialScript>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
       float alpha = sprite.color.a;
        alpha -= 0.01f;
        sprite.color = new Color(sprite.color.r , sprite.color.g, sprite.color.b, alpha);
        if (alpha <= 0)
        {
            controller.RemoveItem(this);
            DestroyObject(gameObject);
        }
	}
}
