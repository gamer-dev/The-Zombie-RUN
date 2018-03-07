using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

	//to reposition floor at the bottom of the screen irrespective of screen size and res
	
	public GameObject playerPrefab;
	
	public Text continueText;
	public float blinkTime = 0f;
	private bool blink;
	
	public Text scoreText;
	public float timeElapsed;
	public float bestTime = 0f;
	
	private bool beatBestTime;
	
	
	private GameObject player;
	private GameObject floor;
	private Spawner spawner; //turn it off when the game 1st begins
	private TimeManager timeManager;
	
	private bool gameStarted;

	
	void Awake()
	{
		//Connect the floor and spawner
		
		floor = GameObject.Find("Foreground");
		spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
		timeManager = GetComponent<TimeManager>();
		
	}
	

	void Start () {
		
		var floorHeight = floor.transform.localScale.y;
		
		//create a position vector to manipulate to where the floor needs to go to the screen
		var pos = floor.transform.position;
		
		pos.x = 0;
		
		//calculate where the bottom of the screen is:
		
		pos.y = -((Screen.height / PixelPerfectCamera.pixelsToUnits) / 2 ) + (floorHeight/2);
		floor.transform.position = pos;
		
		spawner.active = false;
		
		Time.timeScale = 0;
		
		continueText.text  ="PRESS ANY BUTTON TO START";
		
		bestTime = PlayerPrefs.GetFloat("BestTime");
		
	}
	
	
	void Update () {
		
		if(!gameStarted && Time.timeScale == 0 )
		{
			
			if(Input.anyKeyDown)
			{
				timeManager.ManipulateTime(1,1f);
				ResetGame();
			}
			
			
		}
		
		if(!gameStarted)
		{
			
			blinkTime ++;
			
			if(blinkTime % 40 == 0)
			{
				blink = !blink;
			}
			
			continueText.canvasRenderer.SetAlpha(blink ? 0 : 1 );
			
			var textColor = beatBestTime ? "#FF0" : "#FFF";
			
			scoreText.text = "TIME: " + FormatTime(timeElapsed) + "\n<color="+textColor+">BEST: "+ FormatTime(bestTime) +"</color>";
			
		
		}
		
		else
		{
			//when game is running, we want to show the current time:
			timeElapsed += Time.deltaTime;
			scoreText.text = "TIME: " + FormatTime(timeElapsed);
			
			
			
		}
		
		
	}
	
	void OnPlayerKilled()
	{
		gameStarted = false;
		spawner.active = false;
		var playerDestroyScript = player.GetComponent<DestroyOffScreen>();
		
		playerDestroyScript.DestroyCallback -= OnPlayerKilled;
		
		player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		
		timeManager.ManipulateTime(0,5.5f);
		
		continueText.text = "PRESS ANY BUTTON TO RESTART!";
		
		if(timeElapsed> bestTime)
		{
			
			bestTime = timeElapsed;
			PlayerPrefs.SetFloat("BestTime",bestTime);
			beatBestTime = true;
			
		}
		
		
		
	}
	
	void ResetGame()
	{
		gameStarted = true;
		spawner.active = true;
		
		player = GameObjectUtil.Instantiate(playerPrefab, new Vector3(0, (Screen.height/PixelPerfectCamera.pixelsToUnits)/2+100, 0));
		
		var playerDestroyScript = player.GetComponent<DestroyOffScreen>();
		
		playerDestroyScript.DestroyCallback += OnPlayerKilled;
		
		continueText.canvasRenderer.SetAlpha(0);
		
		timeElapsed = 0;
		
		beatBestTime = false;
		
	}
	
	
	
	string FormatTime(float value)
	{
		
		TimeSpan t = TimeSpan.FromSeconds(value);
		
		
		return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
		
		
	}
	

	
}
