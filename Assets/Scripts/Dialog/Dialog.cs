using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Use it as an object to pass to the DialogManager when we want to start a new dialog
//It will have all information about a single dialog

[System.Serializable]
public class Dialog{
	
	public string name; //name of npc

	[TextArea(3, 10)]
	public string[] sentences; //the sentences that will be present in the dialouge

}