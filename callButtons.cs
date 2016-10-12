using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class callButtons : MonoBehaviour {
	/* Calls a number of buttons representing choices in the dialogue*/
	
	public Button firstButton;
	public Button secondButton;
	public Button thirdButton;
	public Button fourthButton;
	private Button[] buttons; 


	public static callButtons instance = null;




	void Start(){
	buttons = new Button[]{firstButton,secondButton,thirdButton,fourthButton};

	}

	public void callButton(int number){
		for(int i =0; i<number; i++) 
		{
			buttons[i].gameObject.SetActive(true);

		}
}
	public void killButtons() 
	{
		for(int i =0; i<buttons.Length; i++)
		{
			buttons[i].gameObject.SetActive(false);
		}
	}
}

