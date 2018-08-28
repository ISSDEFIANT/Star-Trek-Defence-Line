using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
public class ShipAI : MonoBehaviour {	
	private Stats _st;
	private MoveComponent _agent;
	private GlobalDB _GDB;
	private ActiveState _AS;

	private GlobalAI _GAI;

	public bool BattleShip;

	public bool AttackFleetShip;
	public bool AttackFleet2Ship;
	public bool AttackFleet3Ship;
	public bool DefenceFleetShip;
	public bool ScoutFleetShip;

	public bool Builder;
	public bool TitaniumMiner;
	public bool DilithiumMiner;

	public int AttackFleetNum;

	//private float StopTime = 1;
	private float BuildCulDawn;

	public Transform BuildingPlacePosition;

	public float Reaction;
	private bool StopReaction;
	private bool DilithiumMinerActive;

	private float ScoutTimer;
	private bool Scouting;
	private Vector3 ScoutVec;
	// Use this for initialization
	private List<Collider> colls;
	[HideInInspector]
	public bool DontReaction;
	[HideInInspector]
	public bool ChangeArea;


	int EnemyPrioritet;
	int fleet1Pr;
	int fleet2Pr;
	int fleet3Pr;


	private SensorModule _es;
	void Start () {
		_st = gameObject.GetComponent<Stats> ();
		_es = gameObject.GetComponent<SensorModule> ();
		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
		_AS = gameObject.GetComponent<ActiveState> ();
		_agent = gameObject.GetComponent<MoveComponent> ();

		Reaction = Random.Range (3, 5);

		if (!_st.miner) {
			BattleShip = true;
		}
	}
	// Update is called once per frame
	void Update () {
		if (_st.Owner != null) {
			_GAI = _st.Owner.GetComponent<GlobalAI> ();
		}

		if (ScoutTimer > 0) {
			ScoutTimer -= Time.deltaTime;
		}
		if (Reaction > 0) {
			Reaction -= Time.deltaTime;
		} else {
			Reaction = Random.Range (3, 5);
			if (!BattleShip) {
				DilithiumMinerActive = !DilithiumMinerActive;
				StopReaction = false;
			}
			UpdateOrders ();
		}
		if (TitaniumMiner) {
			gameObject.GetComponent<Miner> ().enabled = true;

			gameObject.GetComponent<Miner> ().Titanium = true;
			gameObject.GetComponent<Miner> ().Dilithium = false;

			gameObject.GetComponent<Miner> ().TargetTitanium = true;
			gameObject.GetComponent<Miner> ().TargetDilithium = false;
			if (!FindInTitaniumMinersList (gameObject)) {
				_GAI.TitaniumMiners.Add (gameObject);
			}
		}
		if (DilithiumMiner) {
			gameObject.GetComponent<Miner> ().enabled = true;

			gameObject.GetComponent<Miner> ().Titanium = false;
			gameObject.GetComponent<Miner> ().Dilithium = true;

			gameObject.GetComponent<Miner> ().TargetTitanium = false;
			gameObject.GetComponent<Miner> ().TargetDilithium = true;
			if (!FindInDilithiumMinersList (gameObject)) {
				_GAI.DilithiumMiners.Add (gameObject);
			}
		}

		if (DefenceFleetShip) {
			if (!FindInDefenceFleetList (gameObject)) {
				_GAI.DefenceFleet.Add (gameObject);
			}
		}
		if (ScoutFleetShip) {
			if (!FindInScoutFleetList (gameObject)) {
				_GAI.ScoutFleet.Add (gameObject);
			}
		}
		if (AttackFleetShip) {
			if (!FindInAttackFleetList (gameObject)) {
				_GAI.AttackFleet.Add (gameObject);
			}
		}
		if (AttackFleet2Ship) {
			if (!FindInAttackFleet2List (gameObject)) {
				_GAI.AttackFleet2.Add (gameObject);
			}
		}
		if (AttackFleet3Ship) {
			if (!FindInAttackFleet3List (gameObject)) {
				_GAI.AttackFleet3.Add (gameObject);
			}
		}

		colls = Physics.OverlapSphere (transform.position, _es.VisionRadius).ToList ();
		for (int i = 0; i < colls.Count; i++) {
			if (colls [i] != null) {
				if (_GAI.AI) {
					if (colls [i].tag == "Dwarf" || colls [i].tag == "Freand") {
						if (_GAI != null) {
							if (_GAI.EnemyObjectsIsVisible.Count > 0) {
								if (!FindInEnemyList (colls [i].gameObject)) {
									_GAI.EnemyObjectsIsVisible.Add (colls [i].gameObject);
								}
							} else {
								_GAI.EnemyObjectsIsVisible.Add (colls [i].gameObject);
							}
						}
						if (colls [i].GetComponent<Stats> ()) {
							if (ScoutFleetShip) {		
								_GAI.ScoutFindEnemyShip (colls [i].gameObject, gameObject);
							}
							if (AttackFleetShip) {
								EnemyPrioritet += colls [i].gameObject.GetComponent<Stats> ().приоритет;
								for (int F1i = 0; F1i < _GAI.AttackFleet.Count; F1i++) {
									fleet1Pr += _GAI.AttackFleet [F1i].GetComponent<Stats> ().приоритет;
								}
							}
							if (AttackFleet2Ship) {
								EnemyPrioritet += colls [i].gameObject.GetComponent<Stats> ().приоритет;
								for (int F2i = 0; F2i < _GAI.AttackFleet2.Count; F2i++) {
									fleet2Pr += _GAI.AttackFleet [F2i].GetComponent<Stats> ().приоритет;
								}
							}
							if (AttackFleet3Ship) {
								EnemyPrioritet += colls [i].gameObject.GetComponent<Stats> ().приоритет;
								for (int F3i = 0; F3i < _GAI.AttackFleet.Count; F3i++) {
									fleet3Pr += _GAI.AttackFleet [F3i].GetComponent<Stats> ().приоритет;
								}
							}
						} else {
							if (ScoutFleetShip) {		
								if (Scouting) {
									if (ScoutTimer > 0) {
										ScoutTimer -= Time.deltaTime;
									} else {
										_GAI.EnemyBaseScoutingCompleat (colls [i].gameObject, gameObject);
										Scouting = false;
									}
								} else {
									_GAI.ScoutFindEnemyBase (colls [i].gameObject, gameObject);
								}
							}
						}
					}
				}
				if (_GAI.FreandAI) {
					if (colls [i].tag == "Enemy") {
						if (_GAI != null) {
							if (_GAI.EnemyObjectsIsVisible.Count > 0) {
								if (!FindInEnemyList (colls [i].gameObject)) {
									_GAI.EnemyObjectsIsVisible.Add (colls [i].gameObject);
								}
							} else {
								_GAI.EnemyObjectsIsVisible.Add (colls [i].gameObject);
							}
						}
						if (colls [i].GetComponent<Stats> ()) {
							if (ScoutFleetShip) {		
								_GAI.ScoutFindEnemyShip (colls [i].gameObject, gameObject);
							}
							if (AttackFleetShip) {
								EnemyPrioritet += colls [i].gameObject.GetComponent<Stats> ().приоритет;
								for (int F1i = 0; F1i < _GAI.AttackFleet.Count; F1i++) {
									fleet1Pr += _GAI.AttackFleet [F1i].GetComponent<Stats> ().приоритет;
								}
							}
							if (AttackFleet2Ship) {
								EnemyPrioritet += colls [i].gameObject.GetComponent<Stats> ().приоритет;
								for (int F2i = 0; F2i < _GAI.AttackFleet2.Count; F2i++) {
									fleet2Pr += _GAI.AttackFleet [F2i].GetComponent<Stats> ().приоритет;
								}
							}
							if (AttackFleet3Ship) {
								EnemyPrioritet += colls [i].gameObject.GetComponent<Stats> ().приоритет;
								for (int F3i = 0; F3i < _GAI.AttackFleet.Count; F3i++) {
									fleet3Pr += _GAI.AttackFleet [F3i].GetComponent<Stats> ().приоритет;
								}
							}
						} else {
							if (Scouting) {
								if (ScoutTimer > 0) {
									ScoutTimer -= Time.deltaTime;
								} else {
									_GAI.EnemyBaseScoutingCompleat (colls [i].gameObject, gameObject);
									Scouting = false;
								}
							} else {
								_GAI.ScoutFindEnemyBase (colls [i].gameObject, gameObject);
							}
						}
					}
				}
			}
		}
	}
	void LateUpdate(){
		if (BuildCulDawn > 0) {
			BuildCulDawn -= Time.deltaTime;
		}
	}

	void OnDestroy(){
		if (FindInTitaniumMinersList (gameObject)) {
			_GAI.TitaniumMiners.Remove (gameObject);
		}
		if (FindInDilithiumMinersList (gameObject)) {
			_GAI.DilithiumMiners.Remove (gameObject);
		}

		if (FindInDefenceFleetList (gameObject)) {
			_GAI.DefenceFleet.Remove (gameObject);
		}
		if (FindInScoutFleetList (gameObject)) {
			_GAI.ScoutFleet.Remove (gameObject);
		}
		if (FindInAttackFleetList (gameObject)) {
			_GAI.AttackFleet.Remove (gameObject);
		}
		if (FindInAttackFleet2List (gameObject)) {
			_GAI.AttackFleet2.Remove (gameObject);
		}
		if (FindInAttackFleet3List (gameObject)) {
			_GAI.AttackFleet3.Remove (gameObject);
		}
	}

	public void UpdateOrders(){
		if (!StopReaction) {
			if (BattleShip) {
				if (!DefenceFleetShip && !AttackFleetShip && !AttackFleet2Ship && !AttackFleet3Ship && !ScoutFleetShip) {
					if (_GAI.NeedAttackFleet3) {
						DefenceFleetShip = false;
						ScoutFleetShip = false;
						AttackFleetShip = false;
						AttackFleet2Ship = false;
						AttackFleet3Ship = true;

						StopReaction = true;
					}
					if (_GAI.NeedAttackFleet2) {
						DefenceFleetShip = false;
						ScoutFleetShip = false;
						AttackFleetShip = false;
						AttackFleet2Ship = true;
						AttackFleet3Ship = false;

						StopReaction = true;
					}
					if (_GAI.NeedAttackFleet) {
						DefenceFleetShip = false;
						ScoutFleetShip = false;
						AttackFleetShip = true;
						AttackFleet2Ship = false;
						AttackFleet3Ship = false;

						StopReaction = true;
					}

					if (_GAI.NeedScoutFleet) {
						DefenceFleetShip = false;
						ScoutFleetShip = true;
						AttackFleetShip = false;
						AttackFleet2Ship = false;
						AttackFleet3Ship = false;

						StopReaction = true;
					}
					if (_GAI.NeedDefenceFleet) {
						DefenceFleetShip = true;
						ScoutFleetShip = false;
						AttackFleetShip = false;
						AttackFleet2Ship = false;
						AttackFleet3Ship = false;

						StopReaction = true;
					}
				}
			}
			if (!BattleShip) {
				if (!Builder && !DilithiumMiner && !TitaniumMiner) {
					if (DilithiumMinerActive) {
						if (_GAI.NeedDilithiumMiners) {
							DilithiumMiner = true;
							StopReaction = true;
						}
					} else {
						if (_GAI.NeedTitaniumMiners) {
							TitaniumMiner = true;
							StopReaction = true;
						}
					}
					if (_GAI.NeedBuilders) {
						Builder = true;
						StopReaction = true;
					}
				}
			}
			if (Scouting) {
				_agent.Movement (ScoutVec);
			}
		}
		if (!DontReaction) {
			if (ScoutFleetShip) {
				if (!Scouting) {
					if (ChangeArea) {
						gameObject.GetComponent<MoveComponent> ().Movement (_GAI.gameObject.transform.position + new Vector3 (Random.Range (-1000, 1000), 0, Random.Range (-1000, 1000)));
					} else {
						gameObject.GetComponent<MoveComponent> ().Movement (gameObject.transform.position + new Vector3 (Random.Range (-200, 200), 0, Random.Range (-200, 200)));
					}
				}
			}
			if (DefenceFleetShip) {
				if (_st.targetTransform == null) {
					if (_GAI.StarBases.Count > 0) {
						gameObject.GetComponent<MoveComponent> ().Movement (_GAI.StarBases [0].transform.position + new Vector3 (Random.Range (-200, 200), 0, Random.Range (-200, 200)));
					}
				}
			}
			if (AttackFleetShip || AttackFleet2Ship || AttackFleet3Ship) {
				if (_st.targetTransform == null) {
					gameObject.GetComponent<MoveComponent> ().Movement (_GAI.StarBases [0].transform.position + new Vector3 (Random.Range (-200, 200), 0, Random.Range (-200, 200)));
				}
			}
		} else {
			DontReaction = false;
		}
		if (AttackFleetShip) {
			if (EnemyPrioritet > fleet1Pr) {
				_GAI.NeedHelp (_st.targetTransform.gameObject);
			}
		}
		if (AttackFleetShip || AttackFleet2Ship || AttackFleet3Ship) {
			if (EnemyPrioritet > fleet1Pr + fleet2Pr + fleet3Pr) {
				_GAI.MustGoAway ();
			}
		}
	}

	public void ShipIsСaptured(){
		if (FindInTitaniumMinersList (gameObject)) {
			_GAI.TitaniumMiners.Remove (gameObject);
		}
		if (FindInDilithiumMinersList (gameObject)) {
			_GAI.DilithiumMiners.Remove (gameObject);
		}

		if (FindInDefenceFleetList (gameObject)) {
			_GAI.DefenceFleet.Remove (gameObject);
		}
		if (FindInScoutFleetList (gameObject)) {
			_GAI.ScoutFleet.Remove (gameObject);
		}
		if (FindInAttackFleetList (gameObject)) {
			_GAI.AttackFleet.Remove (gameObject);
		}
		if (FindInAttackFleet2List (gameObject)) {
			_GAI.AttackFleet2.Remove (gameObject);
		}
		if (FindInAttackFleet3List (gameObject)) {
			_GAI.AttackFleet3.Remove (gameObject);
		}
	}

	bool FindInSelectListEnemy (GameObject obj)
	{
		foreach(GameObject selObj in GameObject.FindGameObjectWithTag("BuildingBuildingEnemy").GetComponent<BuildingStationScript>().Builders)
		{
			if(selObj == obj)
				return true;
		}
		return false;
	}
	bool FindInSelectListFreand (GameObject obj)
	{
		foreach(GameObject selObj in GameObject.FindGameObjectWithTag("BuildingBuildingFreand").GetComponent<BuildingStationScript>().Builders)
		{
			if(selObj == obj)
				return true;
		}
		return false;
	}

	bool FindInDilithiumMinersList (GameObject obj)
	{
		if (_GAI != null) {
			foreach (GameObject selObj in _GAI.DilithiumMiners) {
				if (selObj == obj)
					return true;
			}
			return false;
		}
		return false;
	}
	bool FindInTitaniumMinersList (GameObject obj)
	{
		if (_GAI != null) {
			foreach (GameObject selObj in _GAI.TitaniumMiners) {
				if (selObj == obj)
					return true;
			}
			return false;
		}
		return false;
	}

	bool FindInEnemyList (GameObject obj)
	{
		if (_GAI != null) {
			foreach (GameObject selObj in _GAI.EnemyObjectsIsVisible) {
				if (selObj == obj)
					return true;
			}
			return false;
		}
		return false;
	}


	bool FindInDefenceFleetList (GameObject obj)
	{
		if (_GAI != null) {
			foreach (GameObject selObj in _GAI.DefenceFleet) {
				if (selObj == obj)
					return true;
			}
			return false;
		}
		return false;
	}
	bool FindInScoutFleetList (GameObject obj)
	{
		if (_GAI != null) {
			foreach (GameObject selObj in _GAI.ScoutFleet) {
				if (selObj == obj)
					return true;
			}
			return false;
		}
		return false;
	}
	bool FindInAttackFleetList (GameObject obj)
	{
		if (_GAI != null) {
			foreach (GameObject selObj in _GAI.AttackFleet) {
				if (selObj == obj)
					return true;
			}
			return false;
		}
		return false;
	}
	bool FindInAttackFleet2List (GameObject obj)
	{
		if (_GAI != null) {
			foreach (GameObject selObj in _GAI.AttackFleet2) {
				if (selObj == obj)
					return true;
			}
			return false;
		}
		return false;
	}
	bool FindInAttackFleet3List (GameObject obj)
	{
		if (_GAI != null) {
			foreach (GameObject selObj in _GAI.AttackFleet3) {
				if (selObj == obj)
					return true;
			}
			return false;
		}
		return false;
	}

	public void Attack(GameObject target){
		_st.targetTransform = target.transform;
		_st.instruction = Stats.enInstruction.attack;
	}
	public void Scout(Vector3 target){
		Scouting = true;
		ScoutVec = target;
		ScoutTimer = 10;
	}
}