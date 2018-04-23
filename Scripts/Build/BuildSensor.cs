using UnityEngine;
using System.Collections;

public class BuildSensor : MonoBehaviour {
	public GameObject Station;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnTriggerStay (Collider ErrorObj){
		if (ErrorObj.tag == "Dwarf" || ErrorObj.tag == "Enemy" || ErrorObj.tag == "Freand") {
			Station.GetComponent<GhostBuilding> ().Error = true;
		} else {
			Station.GetComponent<GhostBuilding> ().Error = false;
		}
	}
}
