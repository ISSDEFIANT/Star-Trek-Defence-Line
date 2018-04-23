using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch2 : MonoBehaviour {
	public GameObject Camera1;
	public GameObject Camera2;
	public float Timer;
	public float DarkTimer;
	public float CameraInitialisationTimer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (Timer > 0) {
			if (CameraInitialisationTimer > 0) {
				CameraInitialisationTimer -= Time.deltaTime;
				gameObject.GetComponent<ScreenFader> ().fadeState = ScreenFader.FadeState.InEnd;
			} else {
				Camera2.SetActive (false);
				Camera1.SetActive (true);
				gameObject.GetComponent<ScreenFader> ().fadeState = ScreenFader.FadeState.Out;
			}
		}
	}
	void Update () {
		if (CameraInitialisationTimer < 0) {
			if (Timer > 0) {
				Timer -= Time.deltaTime;
			} else {
				gameObject.GetComponent<ScreenFader> ().fadeState = ScreenFader.FadeState.In;
				if (DarkTimer > 0) {
					DarkTimer -= Time.deltaTime;
				} else {
					Camera1.SetActive (false);
					Camera2.SetActive (true);
					gameObject.GetComponent<ScreenFader> ().fadeState = ScreenFader.FadeState.Out;
				}
			}
		}
	}
}
