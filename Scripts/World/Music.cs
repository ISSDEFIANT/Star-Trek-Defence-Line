using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {
	public AudioClip MusicObj;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<AudioSource> ().clip = MusicObj;
		gameObject.GetComponent<AudioSource> ().Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}