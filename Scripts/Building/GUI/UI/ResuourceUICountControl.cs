using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResuourceUICountControl : MonoBehaviour {
	public bool Crew;
	public bool Dilithium;
	public bool Titanium;

	public int FontSizer = 40;

	private UnityEngine.UI.Text Target;
	private GlobalDB _GDB;
	// Use this for initialization
	void Start () {
		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
		Target = gameObject.GetComponent<UnityEngine.UI.Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		Target.fontSize = Screen.height/FontSizer;

		if (Crew) {
			Target.text = "Crew: " + (int)_GDB.Humans;
		}
		if (Dilithium) {
			Target.text = "Dilithium: " + (int)_GDB.Dilithium;
		}
		if (Titanium) {
			Target.text = "Titanium: " + (int)_GDB.Titanium;
		}
	}
}