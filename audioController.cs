using UnityEngine;
using System.Collections;

public class audioController : MonoBehaviour {
	public AudioClip soundEffect;
	public AudioSource source;
	public AudioSource backgroundSource;
	public AudioClip backGround;
	// Use this for initialization
	/*Adds background music or plays an audio clip during a conversation*/
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void callAudio(string AudioClip)
	{

	soundEffect =Resources.Load<AudioClip>(AudioClip);
	source = this.GetComponent<AudioSource>();
	source.clip = soundEffect;
	
	source.Play();
	}

	public void callBackground(string AudioClip)
	{
		backGround = Resources.Load<AudioClip>(AudioClip);

		backgroundSource.clip = backGround;
		backgroundSource.Play();

	}
}
