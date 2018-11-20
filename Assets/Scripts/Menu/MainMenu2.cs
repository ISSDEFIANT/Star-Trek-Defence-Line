using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu2 : MonoBehaviour {
	public GameObject PlaneX;
	public GameObject NextPlane;
	public bool Button;
	public bool Exit;
	public bool Faster;
	public bool Good;
	public bool Beautiful;
	public bool Fantastic;
	public bool AudioOn;
	public bool AudioOff;
	public bool Play;
	public bool Load;
	public int Level;
	public Color Color1;
	public Color Color2;
	public AudioClip clip1;
	public AudioClip clip2;
	public bool X;
	public bool Down;
	private float Timer;
	private bool TimerSet;

	public GameObject LoadScreen;
	public GameObject GameName;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color2;
	}
	
	// Update is called once per frame
	void Update () {
		Time.timeScale = 1;
		if(Down){
			if(TimerSet){
				Timer += Time.deltaTime;
			}
		if(Timer > 0.8f){
				TimerSet = false;
				Timer = 0;
		if(Exit){
			Application.Quit();
		}
		if(Button){
			NextPlane.SetActive(true);
			PlaneX.SetActive(false);
		}
		if(Faster){
					GameSettense.Faster = true;
					GameSettense.Good = false;
					GameSettense.Beautiful = false;
					GameSettense.Fantastic = false;
		}
		if(Good){
					GameSettense.Faster = false;
					GameSettense.Good = true;
					GameSettense.Beautiful = false;
					GameSettense.Fantastic = false;
		}
		if(Beautiful){
					GameSettense.Faster = false;
					GameSettense.Good = false;
					GameSettense.Beautiful = true;
					GameSettense.Fantastic = false;
		}
		if(Fantastic){
					GameSettense.Faster = false;
					GameSettense.Good = false;
					GameSettense.Beautiful = false;
					GameSettense.Fantastic = true;
		}
		if(AudioOn){
					GameSettense.AudioOn = true;
					GameSettense.AudioOff = false;
		}
		if(AudioOff){
					GameSettense.AudioOn = false;
					GameSettense.AudioOff = true;
		}
		if(Play){
			LoadScreen.SetActive (true);
			GameName.SetActive (false);
			SceneManager.LoadScene(Level);
		}
	}
		}
	}

	public void PanelSwinch(){
		NextPlane.SetActive(true);
		PlaneX.SetActive(false);
	}
	public void ExitButton(){
		Application.Quit();
	}
	public void FastestButton(){
		GameSettense.Fastest = true;
		GameSettense.Faster = false;
		GameSettense.Simple = false;
		GameSettense.Good = false;
		GameSettense.Beautiful = false;
		GameSettense.Fantastic = false;
	}
	public void FasterButton(){
		GameSettense.Fastest = false;
		GameSettense.Faster = true;
		GameSettense.Simple = false;
		GameSettense.Good = false;
		GameSettense.Beautiful = false;
		GameSettense.Fantastic = false;
	}
	public void SimpleButton(){
		GameSettense.Fastest = false;
		GameSettense.Faster = false;
		GameSettense.Simple = true;
		GameSettense.Good = false;
		GameSettense.Beautiful = false;
		GameSettense.Fantastic = false;
	}
	public void GoodButton(){
		GameSettense.Fastest = false;
		GameSettense.Faster = false;
		GameSettense.Simple = false;
		GameSettense.Good = true;
		GameSettense.Beautiful = false;
		GameSettense.Fantastic = false;
	}
	public void BeautifulButton(){
		GameSettense.Fastest = false;
		GameSettense.Faster = false;
		GameSettense.Simple = false;
		GameSettense.Good = false;
		GameSettense.Beautiful = true;
		GameSettense.Fantastic = false;
	}
	public void FantasticButton(){
		GameSettense.Fastest = false;
		GameSettense.Faster = false;
		GameSettense.Simple = false;
		GameSettense.Good = false;
		GameSettense.Beautiful = false;
		GameSettense.Fantastic = true;
	}
	public void AudioOnButton(){
		GameSettense.AudioOn = true;
		GameSettense.AudioOff = false;
	}
	public void AudioOffButton(){
		GameSettense.AudioOn = false;
		GameSettense.AudioOff = true;
	}
	public void PlayButton(){
		LoadScreen.SetActive (true);
		GameName.SetActive (false);
	    SceneManager.LoadScene(Level);
	}

	void OnMouseEnter(){
		if (gameObject.GetComponent<Renderer> ()) {
			gameObject.GetComponent<Renderer> ().material.color = Color1;
		}
		if(X){
		AudioSource.PlayClipAtPoint(clip1, gameObject.transform.position);
		}
	}
	void OnMouseExit(){
		if (gameObject.GetComponent<Renderer> ()) {
			gameObject.GetComponent<Renderer> ().material.color = Color2;
		}
	}
	void OnMouseDown(){
		if(X){
			AudioSource.PlayClipAtPoint(clip2, gameObject.transform.position);
		}
		TimerSet = true;
		Down = true;
		if (gameObject.GetComponent<Renderer> ()) {
			gameObject.GetComponent<Renderer> ().material.color = Color2;
		}
	}
	}