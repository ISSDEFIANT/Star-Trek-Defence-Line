using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GhostBuilding : MonoBehaviour {
	public GameObject Building;
	public float Humans;
	public float Dilithium;
	public float Titanium;
	public float Timer = 0.01f;
	public bool Error;
	public GameObject BuilderShip;
	public bool Rotate;
	public float Radius;

	public bool x;
	public bool y;
	public bool z;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.GetComponent<BuildManager> ()._currentBuild = gameObject;
		if (Input.GetMouseButtonDown(0)) {
			if (!Error) {
				GameObject Build = (GameObject)Instantiate (Building, gameObject.transform.position, gameObject.transform.rotation);
				GameObject.FindGameObjectWithTag ("MainUI").GetComponent<GlobalDB> ().Humans -= Humans;
				GameObject.FindGameObjectWithTag ("MainUI").GetComponent<GlobalDB> ().Dilithium -= Dilithium;
				GameObject.FindGameObjectWithTag ("MainUI").GetComponent<GlobalDB> ().Titanium -= Titanium;
				BuilderShip.GetComponent<Builder> ().BuilderTarget = Build;
				if (Timer > 0) {
					Timer -= Time.deltaTime;
				}
			} else {
				if (Timer > 0) {
					Timer -= Time.deltaTime;
				}
			}
		if(Timer <= 0){
				Destroy(gameObject);
		}
	}
		if (Input.GetMouseButtonDown (1)) {
			Destroy(gameObject);
		}
		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			Rotate = true;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift)) {
			Rotate = false;
		}
		if (Rotate) {
			if (x) {
				transform.Rotate (Vector3.right * Input.GetAxis ("Mouse ScrollWheel") * -100);
			}
			if (y) {
				transform.Rotate (Vector3.up * Input.GetAxis ("Mouse ScrollWheel") * -100);
			}
			if (z) {
				transform.Rotate (Vector3.forward * Input.GetAxis ("Mouse ScrollWheel") * -100);
			}
		}
		Collider[] colls = Physics.OverlapSphere (transform.position, Radius+3);
		foreach (Collider coll in colls) {
			if (coll != null) {
				if (coll.tag == "Dwarf" || coll.tag == "Enemy" || coll.tag == "Freand" || coll.tag == "Ship" || coll.tag == "Untagged" || coll.tag == "Neutral") {
					Error = true;
				} else {
					Error = false;
				}
			}
		}
}
}