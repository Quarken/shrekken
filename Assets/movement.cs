﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class movement : NetworkBehaviour {

	public Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) return;
		
		if (Input.GetKey(KeyCode.RightArrow)) {
			rb.AddForce(new Vector2(15, 0));
		}
	}
}
