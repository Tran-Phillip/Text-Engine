using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using LitJson;

public class callEvent : MonoBehaviour {
	/* Controls the event system and plays dialogue as part of a scene*/
	private JsonData eventDatabase;
	public Text textBox;
	private static string text; 
	public bool branch;
	public bool selector1;
	public string chosenEvent;
	public int pather;
	public Text firsttext;
	public Text secondtext;
	public Text thirdtext;
	public Text fourthtext;
	public static bool pass;
	public static bool fail;
	public Text nameBox;
	public bool textDone;
	public int choice;
	public bool afterEvent;
	private audioController audio;
	private callImage image;
	private callButtons button;
	public bool weDone;

	// Use this for initialization
	void Start () {
	audio = GetComponent<audioController>();
	image = GetComponent<callImage>();
	button = GetComponent<callButtons>();
	afterEvent = false;
	textDone = false;
	pass = false;
	fail=false;
	weDone=false;

	pather =0;
	selector1 = false;
	string jsonText = File.ReadAllText(Application.dataPath + "/Resources/Databases/events.json");
	eventDatabase = JsonMapper.ToObject(jsonText);

	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape))
		{
			
			Invoke("doEvent",.5f);
		}
		
	}
	
	
	
	void CallText(int id, int begin,int end, string call)
	{
		
		StartCoroutine(makeText(id,begin,end,call));
		


	}


	private IEnumerator makeText(int id, int begin, int end, string call){ // Main function that controls dialogue
		for(int i = begin; i<=end;i++)
		{
			
			choice = 0;
			
			yield return new WaitForSeconds(1);
			textDone= false;
			string dialogue  = "";
			string tempText = (string)eventDatabase["Events"][id][call+i];
			string name = tempText.Substring(0,4);
			tempText = tempText.Substring(4,tempText.Length-4);
			
			if(name == "####")	
			{
				nameBox.text = "";
			}
			else
			{
			nameBox.text = name;
			}
			text=tempText;
			for(int x =0; x < text.Length;x++)
			{	
				if(Input.GetKey(KeyCode.Space))
				{
					textBox.text = text;

					break;
				}
				dialogue+=text[x];
				yield return new WaitForSeconds(.1f);
				textBox.text = dialogue;

			}
			Invoke("ResetCooldown",.5f);
			yield return StartCoroutine(WaitForKeyDown(KeyCode.Space));

					
		}

		if(weDone==true)
		{
			pather = 0;
			selector1=false;
			choice = 0;
			eventController.eventPhase = false;
			eventController.getPhase = true;
			yield return null;
		}
		else
		{
			
			
			if(branch == false)
			{
				pather+=1;
				Invoke(chosenEvent,.5f);
			}
			else
			{

			}
		}
			
	}
	
	public void branchOff()
	{
		branch = false;
	}
	
	IEnumerator WaitForKeyDown(KeyCode keyCode)
 	{
    	 while (!Input.GetKeyDown(keyCode))
        	 yield return null;
 	}


	public void doEvent()
	{
		if(selector1 == false)
		{
		selector1 = true;
		weDone=false;
		int selector = Random.Range(5,6);
		
		chosenEvent = (string) eventDatabase["Events"][selector]["name"];
		
		}
		
		Invoke(chosenEvent,.5f);
	}

	public void increaseChoice()
	{
		choice+=1;
		
	}
	public void ResetCooldown()
	{
		textDone = true;
	}
	
	public void OntheRoad()
	{	
		if(pather ==0)
		{
		audio.callAudio("Crow");
			
		image.CallImage("OntheRoad");
		CallText(0,1,2,"string");
		}
		else{
		weDone=true;
		audio.callAudio("Driving");
		CallText(0,3,3,"string");
	}
}
		
		

	public void FoodStop()
	{
		if(pather==0){
		CallText(1,1,5,"string");
		

	}
		else if(pather ==1 && choice == 0)
		{
			branch = true;
			inventoryReferencer.CheckItem("Gernade");
			if(inventoryReferencer.itemExist == true)
			{
			button.callButton(3);
			firsttext.text = "Force the door open";
			secondtext.text = "Leave";
			thirdtext.text = "Throw a gernade";
			}
			else
			{
				button.callButton(2);
				firsttext.text = "Force the door open";
				secondtext.text = "Leave";
			}
		}

		if(choice ==1 )
		{
			pather +=1;
			button.killButtons();
			characterReferencer.statCheck("str",6,0);
			if(pass == true && fail == false)
			{
			weDone = true;
			CallText(1,1,11,"choice1p");
			eventController.resource ="food";
			eventController.amount = Random.Range(1,5);
			}		
			else if(pass ==false && fail == true)
			{
			weDone = true;
			pather +=1;
			CallText(1,1,2,"choice1f");
			
			}
			
			
		}
		else if(choice ==2)
		{
			pather +=1;
			button.killButtons();
			weDone = true;
			CallText(1,1,1,"choice3f");
			

		}
		else if(choice ==3)
		{
			pather +=1;
			button.killButtons();
			weDone=true;
			CallText(1,1,4,"choice2p");
			
		}
	
}
		
	
	public void SomberDrive()
	{
		weDone=true;
		CallText(2,1,21,"string");
		
	}
	public void ShantyTown(){//fix all the get componetns
		if(pather==0)
		{

		CallText(3,1,7,"string");
		}
		if(pather == 1)
		{
			characterReferencer.statCheck("dex",8,3);
			if(pass==true && fail == false)
			{
				CallText(3,1,2,"checkp");
			}
			else if(pass == false&&fail == true)
			{
				weDone = true;
				CallText(3,1,1,"checkf");
				
			}
		}
		if(pather == 2 && choice == 0)
		{
			branch = true;
			button.callButton(2);
			firsttext.text = "What do you want?";
			secondtext.text = "Let's go John";
		}
		if(pather ==2 && choice ==1)
		{
			button.killButtons();
			CallText(3,1,4,"stringc");
		}
		if(pather ==2 && choice ==2)
		{
			button.killButtons();
			CallText(3,1,1,"choicef");
		}
		if(pather ==3 && choice ==0)
		{	branch = true;
			button.callButton(4);
			firsttext.text = "Give a little food";
			secondtext.text = "Give moderate amount of food";
			thirdtext.text = "Give a lot of food";
			fourthtext.text ="Give no food";
		}
		if(pather ==3&& choice == 1)
		{
			weDone = true;
			button.killButtons();
			CallText(3,1,3,"string1");
		

		}
		if(pather ==3 && choice ==2)
		{
			button.killButtons();
			CallText(3,1,2,"string2");
			afterEvent = true;

		}
		if(pather ==3&& choice ==3)
		{
			button.killButtons();
			CallText(3,1,2,"string3");
		}
		if(pather ==3 && choice == 4)
		{
			button.killButtons();
			CallText(3,1,4,"string4");
		}
		if(pather ==4 && afterEvent == true)
		{
			weDone = true;
			CallText(3,1,6,"hyun2");
			
		}
		if(pather ==4 && afterEvent == false)
		{
			weDone = true;
			CallText(3,1,7,"hyun3");
			
		}

	}
	public void CampForTheNight()
	{
		if(pather ==0)
		{
		image.CallImage("CampForTheNight");
		audio.callBackground("bg-1");
		audio.callAudio("Fire");
		CallText(4,1,15,"string");
		}
		if(pather ==1 && choice == 0)
		{
			branch = true;
			button.callButton(2);
			firsttext.text = "I agree with Hyun";
			secondtext.text = "I agree with John";

		}
		if(pather == 1 && choice == 1)
		{
			button.killButtons();
			weDone = true;
			CallText(4,1,3,"stringh");
			eventController.distanceTo -= 10;
		}
		if(pather ==1 && choice ==2)
		{
			button.killButtons();
			CallText(4,1,1,"stringj");

		}
		if(pather ==2)
		{
			if(eventController.food >=4)
			{
				weDone= true;
				CallText(4,2,24,"stringj");

			}
			else
			{
				weDone= true;
				CallText(1,2,2,"stringjf");
			}
		}
	}
	public void ConvoWithPhil1()
	{
		if(pather ==0)
		{
		audio.callAudio("Driving");
		audio.callBackground("bg-1");
		CallText(5,1,2,"string");
		}
		if(pather ==1 && choice ==0)
		{
			branch = true;
			button.callButton(3);
			firsttext.text = "Yea, it's a nice ride";
			secondtext.text = "Hardly";
			thirdtext.text ="This isn't a road trip";

		}
		if(pather==1 && choice ==1)
		{
			button.killButtons();
			weDone = true;
			CallText(5,1,7,"string1");
		}
		if(pather == 1 && choice ==2)
		{
			button.killButtons();
			CallText(5,1,1,"string2");

		}
		if(pather ==1 && choice ==3)
		{
			weDone=true;
			button.killButtons();
			CallText(5,1,3,"string3");
		}
		if(pather ==2)
		{
			weDone = true;
			CallText(5,2,7,"string1");

		}

	}

	public void Hopeless()
	{
		if(pather==0)
		{
			audio.callBackground("Driving");
			CallText(6,1,1,"string");
		}
		if(pather==2 && choice ==0)
		{
			button.callButton(2);
			firsttext.text = "What's wrong?";
			secondtext.text = "Stay silent";

		}
		if(pather ==2 && choice ==1)
		{
			CallText(6,1,4,"string1");
		}
		if(pather ==2 && choice ==2)
		{
			CallText(6,4,4,"hyun1");
			chioce =0;
		}
		if(pather ==3 && choice ==0)
		{
			button.callButton(3);
			firsttext.text = "Probably not, but we can try";
			secondtext.text = "Its to rebuild society";
			thirdtext.text = "You're right";

		}
		if(pather ==3 && choice ==1);
	}
	


}
