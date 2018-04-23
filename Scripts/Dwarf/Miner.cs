using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Miner : MonoBehaviour {
	public float maxAs;
	public float curAs;
	private MoveComponent _agent;
	public bool OnMine;
	public bool OnBase;

	public bool Titanium;
	public bool Dilithium;
	public bool Human;

	public bool TargetTitanium;
	public bool TargetDilithium;
	public bool TargetHuman;

	private GlobalDB _GDB;

	public GameObject CMine;
	public GameObject CMineBase;

	public List<GameObject> MineBase;
	GameObject closestminebase;

	public List<GameObject> Mine;
	GameObject closestmine;

	private int MineSelect;
	private bool MineWasSelected;

	public Texture DilitiumTex;
	public Texture TitaniumTex;
	public Texture HumanTex;

	private float ListReloadTimer = 0.1f;

	void Start () {
		_agent = gameObject.GetComponent<MoveComponent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (OnMine) {
			if (FindClosestMine () != null) {
				_agent.Movement (FindClosestMine ().transform.position);
				gameObject.GetComponent<Stats> ().targetVector = FindClosestMine ().transform.position;
				if (Vector3.Distance (gameObject.transform.position, FindClosestMine ().transform.position) < 6) {
					curAs += Time.deltaTime * 10;
					Titanium = FindClosestMine ().GetComponent<As> ().Titanium;
					Dilithium = FindClosestMine ().GetComponent<As> ().Dilithium;
					Human = FindClosestMine ().GetComponent<As> ().Human;
					FindClosestMine ().GetComponent<As> ().Ass -= Time.deltaTime * 10;
				}
			}
		}
		if (ListReloadTimer > 0) {
			ListReloadTimer -= Time.deltaTime;
		} else {
			Mine.Clear ();
			MineBase.Clear ();
			ListReloadTimer = 0.1f;	
		}
		CMine = FindClosestMine ();
		CMineBase = FindClosestMineBase ();
		if (Mine.Count == 0) {
			if (TargetDilithium) {
				foreach (GameObject Test in GameObject.FindGameObjectsWithTag ("DilitiumMine")) {
					Mine.Add(Test);
				}
			}
			if (TargetHuman) {
				foreach (GameObject Test in GameObject.FindGameObjectsWithTag ("HumanCrashShip")) {
					Mine.Add(Test);
				}
			}
			if (TargetTitanium) {
				foreach (GameObject Test in GameObject.FindGameObjectsWithTag ("TitaniumMine")) {
					Mine.Add(Test);
				}
			}
		}
		if (MineBase.Count == 0) {
			if (gameObject.GetComponent<Stats> ().AI) {
				foreach (GameObject Test in GameObject.FindGameObjectsWithTag ("EnemySB")) {
					MineBase.Add (Test);
				}
			}
			if (gameObject.GetComponent<Stats> ().FreandAI) {
				foreach (GameObject Test in GameObject.FindGameObjectsWithTag ("FreandSB")) {
					MineBase.Add (Test);
				}
			}
			if (!gameObject.GetComponent<Stats> ().AI && !gameObject.GetComponent<Stats> ().FreandAI) {
				foreach (GameObject Test in GameObject.FindGameObjectsWithTag ("SB")) {
					MineBase.Add (Test);
				}
			}
		}
		if (curAs <= 0) {
			OnMine = true;
			OnBase = false;
			curAs = 0;
			if (!gameObject.GetComponent<Stats> ().AI) {
				if (!gameObject.GetComponent<Stats> ().FreandAI) {
					//FindClosestMineBase().GetComponent<Miner2> ().As = 0;
				}
				if (gameObject.GetComponent<Stats> ().FreandAI) {
					if (FindClosestMineBase() != null) {
					//	FindClosestMineBase().GetComponent<Miner2> ().As = 0;
					}
				}
			}
			if (gameObject.GetComponent<Stats> ().AI) {
				if (FindClosestMineBase() != null) {
					//FindClosestMineBase().GetComponent<Miner2> ().As = 0;
				}
			}
		}
		if (curAs >= maxAs) {
			OnMine = false;
			OnBase = true;
		}
		if (OnBase) {
			if (!gameObject.GetComponent<Stats> ().AI) {
				if (!gameObject.GetComponent<Stats> ().FreandAI) {
					_agent.Movement (FindClosestMineBase ().transform.parent.transform.position);
					gameObject.GetComponent<Stats> ().targetVector = FindClosestMineBase ().transform.parent.transform.position;
					if (Vector3.Distance (gameObject.transform.position, FindClosestMineBase ().transform.parent.transform.position) < FindClosestMineBase ().transform.parent.transform.gameObject.GetComponent<HealthModule>().ShipRadius + 10) {
						if (FindClosestMineBase ().GetComponent<Miner2> ().currentMiner == null || FindClosestMineBase ().GetComponent<Miner2> ().currentMiner == gameObject) {
							FindClosestMineBase ().GetComponent<Miner2> ().currentMiner = gameObject;
							curAs -= Time.deltaTime * 20;
							FindClosestMineBase ().GetComponent<Miner2> ().Titanium = Titanium;
							FindClosestMineBase ().GetComponent<Miner2> ().Dilithium = Dilithium;
							FindClosestMineBase ().GetComponent<Miner2> ().Human = Human;
							FindClosestMineBase ().GetComponent<Miner2> ().As += Time.deltaTime * 20;
						}
					}
				}
			}
			if (!gameObject.GetComponent<Stats> ().AI) {
				if (gameObject.GetComponent<Stats> ().FreandAI) {
					_agent.Movement (FindClosestMineBase ().transform.parent.transform.position);
					gameObject.GetComponent<Stats> ().targetVector = FindClosestMineBase ().transform.parent.transform.position;
					if (Vector3.Distance (gameObject.transform.position, FindClosestMineBase ().transform.parent.transform.position) < FindClosestMineBase ().transform.parent.transform.gameObject.GetComponent<HealthModule>().ShipRadius + 10) {
						if (FindClosestMineBase ().GetComponent<Miner2> ().currentMiner == null || FindClosestMineBase ().GetComponent<Miner2> ().currentMiner == gameObject) {
							FindClosestMineBase ().GetComponent<Miner2> ().currentMiner = gameObject;
							curAs -= Time.deltaTime * 20;
							FindClosestMineBase ().GetComponent<Miner2> ().Titanium = Titanium;
							FindClosestMineBase ().GetComponent<Miner2> ().Dilithium = Dilithium;
							FindClosestMineBase ().GetComponent<Miner2> ().Human = Human;
							FindClosestMineBase ().GetComponent<Miner2> ().As += Time.deltaTime * 20;
						}
					}
				}
			}
			if (gameObject.GetComponent<Stats> ().AI) {
				if (FindClosestMineBase () != null) {
					_agent.Movement (FindClosestMineBase ().transform.parent.transform.position);
					gameObject.GetComponent<Stats> ().targetVector = FindClosestMineBase ().transform.parent.transform.position;
					if (Vector3.Distance (gameObject.transform.position, FindClosestMineBase ().transform.parent.transform.position) < FindClosestMineBase ().transform.parent.transform.gameObject.GetComponent<HealthModule>().ShipRadius + 10) {
						if (FindClosestMineBase ().GetComponent<Miner2> ().currentMiner == null || FindClosestMineBase ().GetComponent<Miner2> ().currentMiner == gameObject) {
							FindClosestMineBase ().GetComponent<Miner2> ().currentMiner = gameObject;
							curAs -= Time.deltaTime * 20;
							FindClosestMineBase ().GetComponent<Miner2> ().Titanium = Titanium;
							FindClosestMineBase ().GetComponent<Miner2> ().Dilithium = Dilithium;
							FindClosestMineBase ().GetComponent<Miner2> ().Human = Human;
							FindClosestMineBase ().GetComponent<Miner2> ().As += Time.deltaTime * 20;
						}
					}
				}
			}
		}
		if (gameObject.GetComponent<Stats> ().WasSelect) {
			if (OnMine) {
				if (Input.GetMouseButtonDown (1)) {
					gameObject.GetComponent<Miner> ().enabled = false;
				}
			} else if (OnBase) {
				if (Input.GetMouseButtonDown (1)) {
					gameObject.GetComponent<Miner> ().enabled = false;
				}
			}
		}
	}
	public GameObject FindClosestMineBase()
	{           
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		if(MineBase != null){
			foreach (GameObject gomb in MineBase) {
				float curDistance = Vector3.Distance (gomb.transform.position, position);  
				if (curDistance < distance) {
					closestminebase = gomb;
					distance = curDistance;
				}
			}
			return closestminebase;
		} else {
			return null;
		}
	}
	public GameObject FindClosestMine()
	{
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		if (Mine != null) {
			foreach (GameObject gom in Mine) {
				float curDistance = Vector3.Distance (gom.transform.position, position);  
				if (curDistance < distance) {
					closestmine = gom;
					distance = curDistance;
				}
			}
			return closestmine;
		} else {
			return null;
		}
	}
}