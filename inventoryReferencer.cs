using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using LitJson;
public class inventoryReferencer : MonoBehaviour {
	private string jsonText;
	private JsonData inventory;
	public static List<string> templist;
	public static List<string> currentInventory;
	public static bool itemExist;

	/*References the inventory WIP WIP WIP*/
	// Use this for initialization
	void Start () {
	List<string> templist = new List<string>();
	jsonText = File.ReadAllText(Application.dataPath + "/Resources/Databases/itemDatabase.json");
	inventory = JsonMapper.ToObject(jsonText);
	AddItem("Pizaa");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public static void CheckItem(string name){
		for(int i = 0; i< currentInventory.Count;i++){
			if(currentInventory[i] == name){
				itemExist = true;
			}
			else{
				itemExist = false;
				print("Does not exist");
				
			}
		}
	}
	public static void AddItem(string item){
		List<string> templist = new List<string>();
		templist.Add(item);
		currentInventory=templist;
	}	
}

