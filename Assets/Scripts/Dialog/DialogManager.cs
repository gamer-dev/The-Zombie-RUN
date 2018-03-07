using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour {

	public Text nameText;
	public Text dialogText;
	
	//public Animator animator;

	//Variable to Keep track of all of the sentences of the dialog
	//Use Queue as it is the more suitable data structure
	
	private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		
		sentences = new Queue<string>();
		
	}
	
	public void StartDialog(Dialog dialog)
	{
		//animator.SetBool("IsOpen",true);
		
		nameText.text  = dialog.name;
		
		sentences.Clear();
		
		//go through each string in the dialog.sentences array:
		
		foreach (string sentence in dialog.sentences)
		{
			
			sentences.Enqueue(sentence);
			
		}
		
		
		//display the sentence
		DisplayNextSentence();
	}
	
	public void DisplayNextSentence()
	{
		
		//if there are no sentences left:
		if(sentences.Count == 0)
		{
			EndDialog();
			return;
		}
		
		//If there are sentences in the queue:
		string sentence = sentences.Dequeue();

		StopAllCoroutines();
		
			StartCoroutine(TypeSentence(sentence));
		
	}
	
	IEnumerator TypeSentence(string sentence)
	{
		dialogText.text = "";
		
		foreach(char letter in sentence.ToCharArray())
		{
			
			dialogText.text += letter;
			yield return null;
			
		}
		
	}
	
	
	void EndDialog()
	{
		//animator.SetBool("IsOpen",false);
		Debug.Log("END OF ALL SENTENCES!");
		SceneManager.LoadScene(1);
	}
	
}
