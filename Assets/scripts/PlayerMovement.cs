using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : NetworkBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Animator anim = GetComponent<Animator> ();
		Rigidbody2D rig2 = GetComponent<Rigidbody2D> ();
		anim.SetBool ("walking", rig2.velocity.magnitude > 0.1f);
		if (!isLocalPlayer) {
			return;
		}
		
		Vector2 newVel = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis ("Vertical"));
		newVel = newVel * speed;
		rig2.velocity = newVel;
	}
}
