using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This will sit on an object and will trigger a Dialouge!

public class DialogTrigger : MonoBehaviour {

	public Dialog dialog;
	public GameObject dialogBox;
	
	public void TriggerDialog()
	{
		//locate DialogManager
		gameObject.SetActive(false);
		dialogBox.SetActive(true);
		FindObjectOfType<DialogManager>().StartDialog(dialog);
		
		//clear previous senetences:
		
	}
}