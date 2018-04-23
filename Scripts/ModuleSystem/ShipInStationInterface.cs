using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInStationInterface : MonoBehaviour {
	public GameObject Ship;
	public float BuildTime = 1;
	public int CrewCost;
	public int DilithiumCost;
	public int TitaniumCost;
	public int ShipCount;
	public Sprite Icon;
	public string Info;
	public GameObject CountPrefeb;
	public float OpenTime;

	public bool ShipLock;

	// Use this for initialization
	void Start () {
		CountPrefeb = GameObject.Find(Ship.GetComponent<Stats>().CountTag);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
