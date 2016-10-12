using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class eventController : MonoBehaviour {
	/* Prototype states machine that controls the pace of the game and checks the resources the player has 
	progressPhase -> reduces distance to next city
	eventPhase -> Play random event
	checkPhase -> Check food and water
	getPhase -> obtain food or water
	*/

	public static int distanceTo;
	public string nextCity; 
	public Text textBox;
	private JsonData locationDatabase;
	private int id =0;
	public static bool progressPhase;
	public static bool eventPhase;
	public static bool checkPhase;
	public static bool getPhase;
	public bool first;
	public static int food; 
	public static int water;
	public bool coolDown;
	public static string resource;
	public static int amount;
	private audioController audio;
	private callImage image;

	// Use this for initialization
	void Start () {
	coolDown = true;
	food = 20;
	water = 20;
	audio = this.GetComponent<audioController>();
	image = this.GetComponent<callImage>();
	eventPhase = false;
	string jsonText = File.ReadAllText(Application.dataPath + "/Resources/Databases/locationDatabase.json");
	locationDatabase = JsonMapper.ToObject(jsonText);
	nextCity = (string) locationDatabase["Locations"][id]["name"];
	distanceTo = (int) locationDatabase["Locations"][id]["distance"];
	progressPhase = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(progressPhase == true && Input.GetKeyDown(KeyCode.Space) && coolDown == true){
			coolDown = false;
			ProgressPhase();


		}
		else if(eventPhase == true && Input.GetKeyDown(KeyCode.Space)&& coolDown == true){
			
			EventPhase();
		}
		else if(getPhase == true && coolDown == true){
			GetPhase();
		}
		else if(checkPhase == true&& Input.GetKeyDown(KeyCode.Space)&& coolDown == true){
			coolDown=false;
			CheckPhase();
		}
	
	}

	public void ProgressPhase(){
		audio.callAudio("Driving");
		textBox.text = "Traveling to "+nextCity+"\n"+nextCity+" is "+distanceTo+" miles away";
		distanceTo -= 10;
		Invoke("resetCooldown",1f);
		progressPhase = false;
		eventPhase = true;
		first = true;
		
		if(distanceTo < 0)
		{	
			print("You have arrived in " +nextCity);
			id +=1;
			nextCity = (string) locationDatabase["Locations"][id]["name"];
			distanceTo = (int) locationDatabase["Locations"][id]["distance"];
			

		}
	}

	public void EventPhase(){
		if(first == true){
		
		this.GetComponent<callEvent>().doEvent();
		Invoke("resetCooldown",1f);
		first = false;
		
	}
}
	public void GetPhase(){
		image.removeImage();
		if(amount !=0){
		gainResource(resource,amount);
		Invoke("resetCooldown",1f);
		getPhase = false;
		checkPhase= true;
	}	
		else{
		getPhase = false;
		checkPhase= true;
		CheckPhase();
		}

	
	}
	public void CheckPhase(){
		food -=4;
		water -=4;
		Invoke("resetCooldown",1f);
		textBox.text = "You have "+food+" amounts of food left.\n You have " +water+" bottles of water left.";
		checkPhase = false;
		progressPhase = true;
	

	}

	public void resetCooldown(){
		coolDown = true;
	}

	public void gainResource(string resource, int amount){
		if(resource == "food"){
			eventController.food += amount;
		}
		if(resource == "water"){
			eventController.water +=amount;
		}
		textBox.text = "You found "+amount+" units of "+resource; 
	}
}

