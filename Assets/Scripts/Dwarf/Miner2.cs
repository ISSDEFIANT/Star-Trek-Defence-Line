using UnityEngine;
using System.Collections;

public class Miner2 : MonoBehaviour {
	public GameObject Miner;
	public float As;

	public bool Titanium;
	public bool Dilithium;
	public bool Human;

	public GameObject currentMiner;

	public float timer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Miner.GetComponent<Mining> ().CurrentMiner = currentMiner;
		if (timer <= 0) {
			currentMiner = null;
			timer = 1;
		} else {
			timer -= Time.deltaTime;
		}
		if(As > 0){
			Miner.GetComponent<Mining>().As +=Time.deltaTime * 20;
			Miner.GetComponent<Mining> ().Titanium = Titanium;
			Miner.GetComponent<Mining> ().Human = Human;
			Miner.GetComponent<Mining> ().Dilithium = Dilithium;
			As -= Time.deltaTime * 20;
		}
		if (Miner.GetComponent<Mining>().AI) {
			gameObject.tag = "EnemySB";
		}
		if (!Miner.GetComponent<Mining>().AI) {
			if(!Miner.GetComponent<Mining>().FreandAI){
			gameObject.tag = "SB";
			}
			if(Miner.GetComponent<Mining>().FreandAI){
				gameObject.tag = "FreandSB";
			}
		}
	}
}
