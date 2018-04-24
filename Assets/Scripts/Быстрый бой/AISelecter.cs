using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISelecter : MonoBehaviour {
	public int PlayerNum;
	public List<GameObject> Ships;
	public List<GameObject> StarBases;
	// Use this for initialization
	void Start () {
		if (PlayerNum != 0 || PlayerNum != 1) {
			if (PlayerNum == 2) {
				foreach (GameObject obj in Ships) {
					obj.GetComponent<Stats> ().Owner = GameObject.FindGameObjectWithTag ("MainUI").GetComponent<SystemGenerator> ().AI2;
				}
				foreach (GameObject obj in StarBases) {
					obj.GetComponent<Station> ().Owner = GameObject.FindGameObjectWithTag ("MainUI").GetComponent<SystemGenerator> ().AI2;
				}
			}
			if (PlayerNum == 3) {
				foreach (GameObject obj in Ships) {
					obj.GetComponent<Stats> ().Owner = GameObject.FindGameObjectWithTag ("MainUI").GetComponent<SystemGenerator> ().AI3;
				}
				foreach (GameObject obj in StarBases) {
					obj.GetComponent<Station> ().Owner = GameObject.FindGameObjectWithTag ("MainUI").GetComponent<SystemGenerator> ().AI3;
				}
			}
			if (PlayerNum == 4) {
				foreach (GameObject obj in Ships) {
					obj.GetComponent<Stats> ().Owner = GameObject.FindGameObjectWithTag ("MainUI").GetComponent<SystemGenerator> ().AI4;
				}
				foreach (GameObject obj in StarBases) {
					obj.GetComponent<Station> ().Owner = GameObject.FindGameObjectWithTag ("MainUI").GetComponent<SystemGenerator> ().AI4;
				}
			}
			if (PlayerNum == 5) {
				foreach (GameObject obj in Ships) {
					obj.GetComponent<Stats> ().Owner = GameObject.FindGameObjectWithTag ("MainUI").GetComponent<SystemGenerator> ().AI5;
				}
				foreach (GameObject obj in StarBases) {
					obj.GetComponent<Station> ().Owner = GameObject.FindGameObjectWithTag ("MainUI").GetComponent<SystemGenerator> ().AI5;
				}
			}
			if (PlayerNum == 6) {
				foreach (GameObject obj in Ships) {
					obj.GetComponent<Stats> ().Owner = GameObject.FindGameObjectWithTag ("MainUI").GetComponent<SystemGenerator> ().AI6;
				}
				foreach (GameObject obj in StarBases) {
					obj.GetComponent<Station> ().Owner = GameObject.FindGameObjectWithTag ("MainUI").GetComponent<SystemGenerator> ().AI6;
				}
			}
			if (PlayerNum == 7) {
				foreach (GameObject obj in Ships) {
					obj.GetComponent<Stats> ().Owner = GameObject.FindGameObjectWithTag ("MainUI").GetComponent<SystemGenerator> ().AI7;
				}
				foreach (GameObject obj in StarBases) {
					obj.GetComponent<Station> ().Owner = GameObject.FindGameObjectWithTag ("MainUI").GetComponent<SystemGenerator> ().AI7;
				}
			}
			if (PlayerNum == 8) {
				foreach (GameObject obj in Ships) {
					obj.GetComponent<Stats> ().Owner = GameObject.FindGameObjectWithTag ("MainUI").GetComponent<SystemGenerator> ().AI8;
				}
				foreach (GameObject obj in StarBases) {
					obj.GetComponent<Station> ().Owner = GameObject.FindGameObjectWithTag ("MainUI").GetComponent<SystemGenerator> ().AI8;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
