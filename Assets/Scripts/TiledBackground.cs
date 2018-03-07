﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiledBackground : MonoBehaviour {
	
	//Take current size of the tile 32x32, and then figure out how many of 
	//it fit in the current screen height and width
	//Then change the materials value to that
	//so that they fit in the background
	
	public int textureSize = 32;
	
	public bool scaleHorizontally = true;
	public bool scaleVertically = true;

	// Use this for initialization
	void Start () {
		
		
		
		var newWidth = !scaleHorizontally ? 1 : Mathf.Ceil(Screen.width / (textureSize * PixelPerfectCamera.scale));

		var newHeight = !scaleVertically ? 1 : Mathf.Ceil(Screen.height / (textureSize * PixelPerfectCamera.scale));
		
		transform.localScale = new Vector3 (newWidth * textureSize, newHeight * textureSize, 1);
		
		GetComponent <Renderer>().material.mainTextureScale = new Vector3 (newWidth, newHeight, 1);
			
	}
	
}
