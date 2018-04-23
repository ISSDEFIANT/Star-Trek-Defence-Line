using UnityEngine;
using System.Collections;

public class UBox : MonoBehaviour {
	public bool Lock;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnMouseEnter(){
		Lock = false;
	}
	void OnMouseExit(){
		Lock = true;
	}
}
