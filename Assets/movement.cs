using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class movement : NetworkBehaviour {

	public Rigidbody2D rb;

	public int movement_speed = 5;

	// Use this for initialization
	void Start () {
	}

	[Command]
	void CmdMove(float axis_value) {
		rb.AddForce(Vector2.right * axis_value * movement_speed);
		RpcUpdatePosition(rb.transform.position);
	}

	[ClientRpc]
	void RpcUpdatePosition(Vector2 position) {
		rb.transform.position = position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) return;
		
		var horizontal_value = Input.GetAxis("Horizontal");
		if (horizontal_value != 0) {
			CmdMove(horizontal_value);
		}
	}
}
