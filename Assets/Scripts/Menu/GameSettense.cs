using UnityEngine;
using System.Collections;

public class GameSettense : MonoBehaviour {
	public static bool Fastest;
	public static bool Faster;
	public static bool Simple;
	public static bool Good;
	public static bool Beautiful;
	public static bool Fantastic;
	public static bool AudioOn;
	public static bool AudioOff;

	public bool Fastest1;
	public bool Faster1;
	public bool Simple1;
	public bool Good1;
	public bool Beautiful1;
	public bool Fantastic1;
	public bool AudioOn1;
	public bool AudioOff1;

	private float Timer;
	private bool SaveLoad;
	private int GraphicLevel;
	private float AudioLevel;
	// Use this for initialization
	void Start () {
		Timer = 0;
		SaveLoad = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F5)) {
			GameSettense.AudioOn = !GameSettense.AudioOn;
			GameSettense.AudioOff = !GameSettense.AudioOff;
		}


		Fastest1 = Fastest;
		Faster1 = Faster;
		Simple1 = Simple;
		Good1 = Good;
		Beautiful1 = Beautiful;
		Fantastic1 = Fantastic;
		AudioOff1 = AudioOff;
		AudioOn1 = AudioOn;
		if(Fastest){
			//QualitySettings.currentLevel = QualityLevel.Simple;
			QualitySettings.SetQualityLevel(0, true);
			PlayerPrefs.SetInt ("Graphic", 0);
		}
		if(Faster){
			//QualitySettings.currentLevel = QualityLevel.Simple;
			QualitySettings.SetQualityLevel(1, true);
			PlayerPrefs.SetInt ("Graphic", 1);
		}
		if(Simple){
			//QualitySettings.currentLevel = QualityLevel.Simple;
			QualitySettings.SetQualityLevel(2, true);
			PlayerPrefs.SetInt ("Graphic", 2);
		}
		if(Good){
			//QualitySettings.currentLevel = QualityLevel.Good;
			QualitySettings.SetQualityLevel(3, true);
			PlayerPrefs.SetInt ("Graphic", 3);
		}
		if(Beautiful){
			//QualitySettings.currentLevel = QualityLevel.Beautiful;
			QualitySettings.SetQualityLevel(4, true);
			PlayerPrefs.SetInt ("Graphic", 4);
		}
		if(Fantastic){
			//QualitySettings.currentLevel = QualityLevel.Fantastic;
			QualitySettings.SetQualityLevel(5, true);
			PlayerPrefs.SetInt ("Graphic", 5);
		}
		if(AudioOn){
			AudioListener.volume = 1;
			PlayerPrefs.SetFloat ("Audio", 1);
		}
		if(AudioOff){
			AudioListener.volume = 0;
			PlayerPrefs.SetFloat ("Audio", 0);
		}
		if (Timer > 0) {
			Timer -= Time.deltaTime;
		} else {
			if (SaveLoad) {
				SaveLoad = false;
			} else {
				GraphicLevel = PlayerPrefs.GetInt ("Graphic");
				AudioLevel = PlayerPrefs.GetFloat ("Audio");
				if (GraphicLevel == 0) {
					QualitySettings.SetQualityLevel(0, true);
					Fastest = true;
					Faster = false;
					Simple = false;
					Good = false;
					Beautiful = false;
					Fantastic = false;
				}
				if (GraphicLevel == 1) {
					QualitySettings.SetQualityLevel(1, true);
					Fastest = false;
					Faster = true;
					Simple = false;
					Good = false;
					Beautiful = false;
					Fantastic = false;
				}
				if (GraphicLevel == 2) {
					QualitySettings.SetQualityLevel(2, true);
					Fastest = false;
					Faster = false;
					Simple = true;
					Good = false;
					Beautiful = false;
					Fantastic = false;
				}
				if (GraphicLevel == 3) {
					QualitySettings.SetQualityLevel(3, true);
					Fastest = false;
					Faster = false;
					Simple = false;
					Good = true;
					Beautiful = false;
					Fantastic = false;
				}
				if (GraphicLevel == 4) {
					QualitySettings.SetQualityLevel(4, true);
					Fastest = false;
					Faster = false;
					Simple = false;
					Good = false;
					Beautiful = true;
					Fantastic = false;
				}
				if (GraphicLevel == 5) {
					QualitySettings.SetQualityLevel(5, true);
					Fastest = false;
					Faster = false;
					Simple = false;
					Good = false;
					Beautiful = false;
					Fantastic = true;
				}
				if (AudioLevel == 0) {
					AudioListener.volume = 0;
					AudioOn = false;
					AudioOff = true;
				}
				if (AudioLevel == 1) {
					AudioListener.volume = 1;
					AudioOn = true;
					AudioOff = false;
				}
				SaveLoad = true;
			}
			Timer = 0;
		}
	}
}
