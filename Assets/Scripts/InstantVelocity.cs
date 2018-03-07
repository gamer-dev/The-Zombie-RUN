using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantVelocity : MonoBehaviour {
	
	public Vector2 velocity = Vector2.zero;
	
	private Rigidbody2D body2d;
	
	void Awake(){
		
		body2d = GetComponent<Rigidbody2D> ();
	}

	//only gets called a limited number of times per frame, making it efficient for physics calculations
	void FixedUpdate()
	{
		
		body2d.velocity = velocity;
		
		
	}
	
	

}