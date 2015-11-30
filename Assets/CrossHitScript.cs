﻿using UnityEngine;
using System.Collections;

public class CrossHitScript : MonoBehaviour {

    [SerializeField]
    private float lifeTime;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0f)
            Destroy(gameObject);
	}
}
