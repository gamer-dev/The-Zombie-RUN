using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputState : MonoBehaviour {

//Let's find out the state of the player - stand or jump or what?
//Every other script will look at this script to find out what
//the actual state of the player is?
//keep track of standing, jump, x, y, velocity or any type of input is taking place


	public bool actionButton;  //the button which causes action
	public float absVelX = 0f; //absolute velocity of X
	public float absVelY = 0f; //absolute velocity of Y
	public bool standing; //player standing up or not
	public float standingThreshold = 1; //keep track of the threshold between when the velocity is slowing down or when he is standing?
	
	private Rigidbody2D body2D;

	void Awake () {
		
		body2D = GetComponent<Rigidbody2D> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		actionButton = Input.anyKeyDown;
		
	}
	
	void FixedUpdate()
	{
		
		absVelX = System.Math.Abs(body2D.velocity.x);
		absVelY = System.Math.Abs(body2D.velocity.y);
		
		standing = absVelY <= standingThreshold;

	}
	
}
