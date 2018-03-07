using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour {

	/* As we have only one camera in the game, 
	   let's create some static values to be imported by 
	   every other scripts that will use the camera. */
	
	//Keeping track of the pixels-to-unit ratio 
	public static float pixelsToUnits = 1f;
	
	//property to represent the scale
	public static float scale = 1f;
	
	//Value to represent Native resolution of the game
	public Vector2 nativeResolution = new Vector2(248, 160);
	
	//This is the resolution of the Nintendo GBA
	//As it is a device, originally intended for pixel art
	//But, no modern devices have that resolution.
	
	//To make the sprites look as originally intended,
	//in every resolution, 
	//writing a script to automatically change size of the camera
	//so that it zooms in more to maintain the original look.
	
	//Using the Awake() to let the camera to calculate the 
	//scale before everything else starts


	void Awake()
	{
		var camera = GetComponent<Camera> (); //reference to the camera we are working in.
		
		//To set camera mode to automatic orthograhic:
		
		if(camera.orthographic) //In 2D mode, it is already set
		{
			// How to get our scale size:
			scale = Screen.height/nativeResolution.y;
			
			//Now to change the pixels to units, so that it relates to scale in the game:
			
			pixelsToUnits *= scale; //increases as the screen size (resolution) increases
			
			//Now, change size of the orthograhic camera:
			
			camera.orthographicSize = (Screen.height/2.0f) / pixelsToUnits;
			
		}
		
	}
	
	
}
