using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlingonHelp : MonoBehaviour {
	public List<GameObject> Ships;
	public bool Stop;
	public GameObject AI;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Stop) {
			foreach (GameObject obj in Ships) {
				obj.GetComponent<Stats> ().Neutral = false;
				obj.GetComponent<Stats> ().FreandAI = true;
				obj.GetComponent<Stats> ().Owner = AI;

				obj.GetComponent<Skill> ().InMaskShild = true;
				Stop = false;
				//obj.GetComponent<Stats> ().FreandAI = true;
				//obj.GetComponent<Stats> ().FreandAI = true;
			}
		}
		if (Vector3.Distance (gameObject.transform.position, GameObject.Find ("U.S.S.BABYLON").transform.position) <= 100) {
			foreach (GameObject obj in Ships) {
				obj.GetComponent<Stats> ().Neutral = false;
				obj.GetComponent<Stats> ().FreandAI = true;
				obj.GetComponent<Stats> ().Owner = AI;

				obj.GetComponent<Skill> ().InMaskShild = true;
				Stop = false;
			}
			gameObject.GetComponent<KlingonHelp> ().enabled = false;
		}
	}
}
