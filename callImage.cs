using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class callImage : MonoBehaviour {
	public  Sprite backDrop;
	public float fadeSpeed = 1.5f;
	private bool sceneStart;
	public Image FadeImg;


	// Use this for initialization
	/* Calls and removes a background image and adds a fade to it*/
	void Awake(){
	
	}
	void Start(){
		
	}
	// Update is called once per frame
	void Update () 
	{
		
	
	
	}

	public void CallImage(string fileName)
	{
	backDrop=Resources.Load<Sprite>(fileName);
	
	StartCoroutine(FadeIn());
	FadeImg.sprite = backDrop;
	
	
	
	}
	public void removeImage()
	{
		StartCoroutine(FadeOut());
	}
	private IEnumerator FadeIn()
	{
		
		for(float f = 0f; f <= 225; f += 0.01f)
		{
			
			Color c = FadeImg.color;
			c.a =f;
			FadeImg.color = c;
			yield return null;
		}
		
			

	}
	private IEnumerator FadeOut()
	{
		yield return new WaitForSeconds(1);
		for(float f = 1f; f>=0; f-=.01f)
		{
			Color c = FadeImg.color;
			c.a =f;
			FadeImg.color = c;
			yield return null;
		}
		FadeImg.sprite=Resources.Load<Sprite>("black texture");
		

	}

}