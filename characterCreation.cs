using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using LitJson;

public class characterCreation : MonoBehaviour {
	/* responsible for allocating character skill points WIP WIP WIP */
	public InputField charName;
	public int pointsRemaining = 20;

	public Text strText;
	public Text dexText;
	public Text intText;
	public Text agiText;
	public GameObject referencer;
	public Text descriptionText;
	public Text pointsText;
	
	public int id=0;
	private int str;
	private int dex;
	private int intel;
	private int agi; 
	private string jsonText;
	public GameObject parent;
	private JsonData creationData;
	public GameObject eventController;
	public Canvas TextUI;
	// Use this for initialization
	void Start () {
	
		jsonText = File.ReadAllText(Application.dataPath + "/Resources/Databases/characterDatabase.json");
		creationData = JsonMapper.ToObject(jsonText);
		charName.text = "Please enter a name:";
		descriptionText.text = (string)creationData["character"][0]["description"];
		
	}

	
	// Update is called once per frame
	void Update () {
	pointsText.text = "Points Remaining: "+pointsRemaining;
	strText.text = "str:" + str;
	dexText.text = "dex:" + dex;
	intText.text = "int:" + intel;
	agiText.text = "agi:" + agi;
	}
	public void name(){

		if(id == 1){
			
			charName.text = "Phil";
			descriptionText.text = (string)creationData["character"][1]["description"];
		}
		else if(id ==2){
			charName.text = "Hyun";
			descriptionText.text = (string)creationData["character"][2]["description"];
		}
		else if(id== 3){
			charName.text = "John";
			descriptionText.text = (string)creationData["character"][3]["description"];
		}

		creationData["character"][id]["name"] = charName.text;
		

		
	}
	//See if you cant make these better later!!!!!
	public void strChange(){
		
		if(pointsRemaining !=0){
		pointsRemaining-=1;
		str=str + 1;
		creationData["character"][id]["str"] = str;
	}
		
	}
	public void dexChange(){
		
		if(pointsRemaining !=0){
		dex+=1;
		pointsRemaining-=1;
		creationData["character"][id]["dex"] = dex;
	}
		
	}
	public void intelChange(){
		
		if(pointsRemaining !=0){
		pointsRemaining -=1;
		intel+=1;
		creationData["character"][id]["int"] = intel;
	}
	}
		
	public void agiChange(){
		if(pointsRemaining !=0){
		pointsRemaining -=1;
		agi +=1;
		creationData["character"][id]["agi"] = agi;
		}
	}
	public void Confirmation(){
		if(id < 3){
			id += 1;
			pointsRemaining = 20;
			name();
			str = 0;
			dex= 0;
			intel = 0;
			agi = 0;
		}
		else{
			characterReferencer.characterData = creationData;
			TextUI.gameObject.SetActive(true);
			eventController.gameObject.SetActive(true);
			Destroy(parent);
		}
	}
}
