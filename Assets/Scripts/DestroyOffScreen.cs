using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffScreen : MonoBehaviour {
	
	public delegate void OnDestroy();
	public event OnDestroy DestroyCallback;
	

	public float offset = 32f; // value of how far off of the screen the prefab needs to be, before it gets destroyed
	
	private bool offScreen;    //flag to tell if the object is off the screen and has to be destroyed NOW
	
	private float offScreenX = 0; //how much is the object off the screen by?
	
	private Rigidbody2D body2d;
	
	void Awake()
	{
		body2d = GetComponent<Rigidbody2D> ();
		
	}
	

	// Use this for initialization
	void Start () {
		
		offScreenX = (Screen.width/PixelPerfectCamera.pixelsToUnits)/2 + offset;
		
		Debug.Log(Screen.width);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		var posX = transform.position.x;
		var dirX = body2d.velocity.x;
		
		Debug.Log(posX);
		Debug.Log(dirX);
		
		
		if(Mathf.Abs(posX) > offScreenX)
		{
			//obstacles, going from Right To left:
			if (dirX < 0 && posX < -offScreenX)
			{
				offScreen = true;
			}
			//obstacles, going from Left To Right:
			else if(dirX > 0 && posX > offScreenX)
			{
				offScreen = true;
			}
		}
			
			else
			{
				offScreen = false;
			}
			
			if(offScreen)
			{
				OutOfBounds();
			}
	}

	public void OutOfBounds()
	{
		offScreen = false;
		GameObjectUtil.Destroy(gameObject);
		
		if(DestroyCallback != null)
		{
				DestroyCallback();
			
		}
		
	}
	
}
