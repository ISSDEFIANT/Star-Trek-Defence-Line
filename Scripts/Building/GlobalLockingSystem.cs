using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLockingSystem : MonoBehaviour {
	public bool IsFederationStarBase;
	public int FederationStarBaseMaxLvl;

	public bool IsFederationMiningStation;
	public bool IsFederationDock1;
	public bool IsFederationDock2;
	public bool IsFederationSciStation;
	public bool IsFederationDefenceStation;
	public bool IsFederationTradingStation;

	public bool IsBorgNexus;
	public int BorgNexusMaxLvl;

	public bool IsBorgMiningStation;
	public bool IsBorgDock1;
	public bool IsBorgDock2;
	public bool IsBorgSciStation;
	public bool IsBorgDefenceStation;
	public bool IsBorgTorpedoTurret;
	public bool IsBorgCutterTurret;

	private float timer = 1;
	// Use this for initialization
	void Start () {
		Check ();
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0) {
			timer -= Time.deltaTime;
		} else {
			timer = 3;
		}
	}
	public void Check(){
		IsFederationStarBase = false;
		IsFederationMiningStation = false;
		IsFederationDock1 = false;
		IsFederationDock2 = false;
		IsFederationSciStation = false;
		IsFederationDefenceStation = false;
		IsFederationTradingStation = false;

		IsBorgNexus = false;
		IsBorgMiningStation = false;
		IsBorgDock1 = false;
		IsBorgDock2 = false;
		IsBorgSciStation = false;
		IsBorgDefenceStation = false;
		IsBorgTorpedoTurret = false;
		IsBorgCutterTurret = false;
	}
}
