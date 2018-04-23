using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanelActivator : MonoBehaviour {
	public string Message;
	public int Size;

	[HideInInspector]
	public bool Active;
	private InfoPlane _IP;
	// Use this for initialization
	void Start () {
		_IP = GameObject.FindGameObjectWithTag("MainUI").GetComponent<InfoPlane>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Active) {
			_IP.UpdateText(Message,Size);
		}
	}
	public void ActivaActive(){
		Active = true;
	}
	public void ActivaDeActive(){
		Active = false;
		_IP.Stop = true;
	}
}
