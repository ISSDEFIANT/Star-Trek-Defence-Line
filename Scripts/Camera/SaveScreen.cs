using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class SaveScreen : MonoBehaviour {
	public bool showText = true;
	string mainPath = "";
	string cName;
	public int i;

	void Start() {
		mainPath = Path.Combine(Application.dataPath, "Data/Screenshots/");


		if(!Directory.Exists(mainPath)) {			
			Directory.CreateDirectory(mainPath);			
		}
		i = Directory.GetFiles(mainPath).Length;
	}

	void Update() {


		if(Input.GetKeyDown(KeyCode.F3)) {
			i++;
			cName = "Screen_"+i;
			ScreenCapture.CaptureScreenshot (Path.Combine(mainPath, "Screen_" + i + ".jpg"));
		}
	}
}