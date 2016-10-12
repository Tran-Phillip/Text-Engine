using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using LitJson;

public class characterReferencer : MonoBehaviour {
	/* Data obtained from character creation is stored here as well as a statCheck function
	to see whether or not the stat of a certain character passes a skill check */
	private string jsonText;
	public static JsonData characterData;
	public List<string> names;
	public List<int> stats;
	public int id = 0;	
	private string jsontext;

	public static characterReferencer instance;



	// Use this for initialization
	void Start () {
		
	


	
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			print(characterData["character"][2]["str"]);
		}
} 
	public static void statCheck(string stat, int limit, int id){
		int skilllevel = (int) characterData["character"][id][stat];
		print(characterData["character"][id][stat]);
		if(skilllevel >=limit){
			callEvent.pass = true;
			callEvent.fail = false;
		}
		else{
			callEvent.fail = true;
			callEvent.pass = false;
		}
	}

}
