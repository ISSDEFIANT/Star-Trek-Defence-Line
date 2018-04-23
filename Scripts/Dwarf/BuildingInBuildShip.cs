using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingInBuildShip : MonoBehaviour {
	public Sprite Station;
	public int costStationTitanium;
	public int costStationDilithium;
	public int costStationHumans;
	public GameObject station;

	private GlobalDB _GDB;

	public GUISkin mainSkin;
	public int numDepth = 1;

	public float XPosition = 220;
	public float YPosition = 70;
	public float XScale = 100;
	public float YScale = 100;

	public bool AttackStation;
	public bool DefenceStation;
	public bool NatrealStation;

	public GameObject OwnerShip;

	public string Приказ;
	public string CurПриказ;

	private Vector3 Transform;
	public bool Stop;
	private float StopTimer = 1;
	private GameObject AIStation;

	public bool IsLock;
	[HideInInspector]
	public bool Locking;

	private GlobalLockingSystem _GLS;

	public bool IsFederationStarBase;
	public int FederationStarBaseLvl;

	public bool IsFederationMiningStation;
	public bool IsFederationDock1;
	public bool IsFederationDock2;
	public bool IsFederationSciStation;
	public bool IsFederationDefenceStation;
	public bool IsFederationTradingStation;

	public bool IsBorgNexus;
	public int BorgNexusLvl = 0;

	public bool IsBorgMiningStation;
	public bool IsBorgDock1;
	public bool IsBorgDock2;
	public bool IsBorgSciStation;
	public bool IsBorgDefenceStation;
	public bool IsBorgTorpedoTurret;
	public bool IsBorgCutterTurret;

	private float LockingUpdateTimer = 0.1f;

	private bool FederationStarBase;
	private int LateFederationStarBaseLvl = 0;

	private bool FederationMiningStation;
	private bool FederationDock1;
	private bool FederationDock2;
	private bool FederationSciStation;
	private bool FederationDefenceStation;
	private bool FederationTradingStation;

	private bool BorgNexus;
	private int LateBorgNexusLvl = 0;

	private bool BorgMiningStation;
	private bool BorgDock1;
	private bool BorgDock2;
	private bool BorgSciStation;
	private bool BorgDefenceStation;
	private bool BorgTorpedoTurret;
	private bool BorgCutterTurret;

	private bool IsCheckedFederationStarBase;
	private bool IsCheckedFederationMiningStation;
	private bool IsCheckedFederationDock1;
	private bool IsCheckedFederationDock2;
	private bool IsCheckedFederationSciStation;
	private bool IsCheckedFederationDefenceStation;
	private bool IsCheckedFederationTradingStation;

	private bool IsCheckedBorgNexus;
	private bool IsCheckedBorgMiningStation;
	private bool IsCheckedBorgDock1;
	private bool IsCheckedBorgDock2;
	private bool IsCheckedBorgSciStation;
	private bool IsCheckedBorgDefenceStation;
	private bool IsCheckedBorgTorpedoTurret;
	private bool IsCheckedBorgCutterTurret;

	public bool check;

	public bool AIError;
	// Use this for initialization
	void Start () {
		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
		AIStation = station.GetComponent<GhostBuilding> ().Building;
		check = true;

		AIError = true;
	}
	
	// Update is called once per frame
	void LateUpdate(){
		if (StopTimer > 0) {
			StopTimer -= Time.deltaTime;
		} else {
			Stop = false;
			StopTimer = 1;
		}
	}
	void Update () {
		CurПриказ = OwnerShip.GetComponent<Builder> ().Order;
		if (OwnerShip.GetComponent<Stats> ().AI || OwnerShip.GetComponent<Stats> ().FreandAI) {
			if (!OwnerShip.GetComponent<Stats> ().Neutral) {
				_GLS = OwnerShip.GetComponent<Stats> ().Owner.GetComponent<GlobalLockingSystem> ();
			} else {
				if (!gameObject.GetComponent<GlobalLockingSystem> ()) {
					gameObject.AddComponent<GlobalLockingSystem> ();
				} else {
					_GLS = gameObject.GetComponent<GlobalLockingSystem> ();
				}
			}
		} else {
			_GLS = GameObject.FindGameObjectWithTag ("MainUI").GetComponent<GlobalLockingSystem> ();
		}
		if (_GLS != null) {
			if (_GLS.IsFederationStarBase != FederationStarBase) {
				check = true;
				FederationStarBase = _GLS.IsFederationStarBase;
			}
			if (_GLS.IsFederationMiningStation != FederationMiningStation) {
				check = true;
				FederationMiningStation = _GLS.IsFederationMiningStation;
			}
			if (_GLS.IsFederationDock1 != FederationDock1) {
				check = true;
				FederationDock1 = _GLS.IsFederationDock1;
			}
			if (_GLS.IsFederationDock2 != FederationDock2) {
				check = true;
				FederationDock2 = _GLS.IsFederationDock2;
			}
			if (_GLS.IsFederationSciStation != FederationSciStation) {
				check = true;
				FederationSciStation = _GLS.IsFederationSciStation;
			}
			if (_GLS.IsFederationDefenceStation != FederationDefenceStation) {
				check = true;
				FederationDefenceStation = _GLS.IsFederationDefenceStation;
			}
			if (_GLS.IsFederationTradingStation != FederationTradingStation) {
				check = true;
				FederationTradingStation = _GLS.IsFederationTradingStation;
			}

			if (_GLS.IsBorgNexus != BorgNexus) {
				check = true;
				BorgNexus = _GLS.IsBorgNexus;
			}
			if (_GLS.IsBorgMiningStation != BorgMiningStation) {
				check = true;
				BorgMiningStation = _GLS.IsBorgMiningStation;
			}
			if (_GLS.IsBorgDock1 != BorgDock1) {
				check = true;
				BorgDock1 = _GLS.IsBorgDock1;
			}
			if (_GLS.IsBorgDock2 != BorgDock2) {
				check = true;
				BorgDock2 = _GLS.IsBorgDock2;
			}
			if (_GLS.IsBorgSciStation != BorgSciStation) {
				check = true;
				BorgSciStation = _GLS.IsBorgSciStation;
			}
			if (_GLS.IsBorgDefenceStation != BorgDefenceStation) {
				check = true;
				BorgDefenceStation = _GLS.IsBorgDefenceStation;
			}
			if (_GLS.IsBorgTorpedoTurret != BorgTorpedoTurret) {
				check = true;
				BorgTorpedoTurret = _GLS.IsBorgTorpedoTurret;
			}
			if (_GLS.IsBorgCutterTurret != BorgCutterTurret) {
				check = true;
				BorgCutterTurret = _GLS.IsBorgCutterTurret;
			}

			if (LateFederationStarBaseLvl != _GLS.FederationStarBaseMaxLvl) {
				check = true;
				LateFederationStarBaseLvl = _GLS.FederationStarBaseMaxLvl;
			}
			if (LateBorgNexusLvl != _GLS.BorgNexusMaxLvl) {
				check = true;
				LateBorgNexusLvl = _GLS.BorgNexusMaxLvl;
			}

			if (check) {
				if (IsFederationStarBase) {
					if (!_GLS.IsFederationStarBase) {
						Locking = true;
						check = false;
					} else {
						if (FederationStarBaseLvl <= _GLS.FederationStarBaseMaxLvl) {
							Locking = false;
							IsCheckedFederationStarBase = true;
						} else {
							Locking = true;
							check = false;
						}
					}
				} else {
					IsCheckedFederationStarBase = true;
				}
				if (IsCheckedFederationStarBase) {
					if (IsFederationMiningStation) {
						if (!_GLS.IsFederationMiningStation) {
							Locking = true;
							check = false;
						} else {
							Locking = false;
							IsCheckedFederationMiningStation = true;
						}
					} else {
						IsCheckedFederationMiningStation = true;
					}
				}
				if (IsCheckedFederationMiningStation) {
					if (IsFederationDock1) {
						if (!_GLS.IsFederationDock1) {
							Locking = true;
							check = false;
						} else {
							Locking = false;
							IsCheckedFederationDock1 = true;
						}
					} else {
						IsCheckedFederationDock1 = true;
					}
				}
				if (IsCheckedFederationDock1) {
					if (IsFederationDock2) {
						if (!_GLS.IsFederationDock2) {
							Locking = true;
							check = false;
						} else {
							Locking = false;
							IsCheckedFederationDock2 = true;
						}
					} else {
						IsCheckedFederationDock2 = true;
					}
				}
				if (IsCheckedFederationDock2) {
					if (IsFederationSciStation) {
						if (!_GLS.IsFederationSciStation) {
							Locking = true;
							check = false;
						} else {
							Locking = false;
							IsCheckedFederationSciStation = true;
						}
					} else {
						IsCheckedFederationSciStation = true;
					}
				}
				if (IsCheckedFederationSciStation) {
					if (IsFederationDefenceStation) {
						if (!_GLS.IsFederationDefenceStation) {
							Locking = true;
							check = false;
						} else {
							Locking = false;
							IsCheckedFederationDefenceStation = true;
						}
					} else {
						IsCheckedFederationDefenceStation = true;
					}
				}
				if (IsCheckedFederationDefenceStation) {
					if (IsFederationTradingStation) {
						if (!_GLS.IsFederationTradingStation) {
							Locking = true;
							check = false;
						} else {
							Locking = false;
							IsCheckedFederationTradingStation = true;
						}
					} else {
						IsCheckedFederationTradingStation = true;
					}
				}

				if (IsCheckedFederationTradingStation) {
					if (IsBorgNexus) {
						if (!_GLS.IsBorgNexus) {
							Locking = true;
							check = false;
						} else {
							if (BorgNexusLvl <= _GLS.BorgNexusMaxLvl) {
								Locking = false;
								IsCheckedBorgNexus = true;
							} else {
								Locking = true;
								check = false;
							}
						}
					} else {
						IsCheckedBorgNexus = true;
					}
				}
				if (IsCheckedBorgNexus) {
					if (IsBorgMiningStation) {
						if (!_GLS.IsBorgMiningStation) {
							Locking = true;
							check = false;
						} else {
							Locking = false;
							IsCheckedBorgMiningStation = true;
						}
					} else {
						IsCheckedBorgMiningStation = true;
					}
				}
				if (IsCheckedBorgMiningStation) {
					if (IsBorgDock1) {
						if (!_GLS.IsBorgDock1) {
							Locking = true;
							check = false;
						} else {
							Locking = false;
							IsCheckedBorgDock1 = true;
						}
					} else {
						IsCheckedBorgDock1 = true;
					}
				}
				if (IsCheckedBorgDock1) {
					if (IsBorgDock2) {
						if (!_GLS.IsBorgDock2) {
							Locking = true;
							check = false;
						} else {
							Locking = false;
							IsCheckedBorgDock2 = true;
						}
					} else {
						IsCheckedBorgDock2 = true;
					}
				}
				if (IsCheckedBorgDock2) {
					if (IsBorgSciStation) {
						if (!_GLS.IsBorgSciStation) {
							Locking = true;
							check = false;
						} else {
							Locking = false;
							IsCheckedBorgSciStation = true;
						}
					} else {
						IsCheckedBorgSciStation = true;
					}
				}
				if (IsCheckedBorgSciStation) {
					if (IsBorgDefenceStation) {
						if (!_GLS.IsBorgDefenceStation) {
							Locking = true;
							check = false;
						} else {
							Locking = false;
							IsCheckedBorgDefenceStation = true;
						}
					} else {
						IsCheckedBorgDefenceStation = true;
					}
				}
				if (IsCheckedBorgDefenceStation) {
					if (IsBorgTorpedoTurret) {
						if (!_GLS.IsBorgTorpedoTurret) {
							Locking = true;
							check = false;
						} else {
							Locking = false;
							IsCheckedBorgTorpedoTurret = true;
						}
					} else {
						IsCheckedBorgTorpedoTurret = true;
					}
				}
				if (IsCheckedBorgTorpedoTurret) {
					if (IsBorgCutterTurret) {
						if (!_GLS.IsBorgCutterTurret) {
							Locking = true;
							check = false;
						} else {
							Locking = false;
							IsCheckedBorgCutterTurret = true;
						}
					} else {
						IsCheckedBorgCutterTurret = true;
					}
				}
			} else {
				IsCheckedFederationStarBase = false;
				IsCheckedFederationMiningStation = false;
				IsCheckedFederationDock1 = false;
				IsCheckedFederationDock2 = false;
				IsCheckedFederationSciStation = false;
				IsCheckedFederationDefenceStation = false;
				IsCheckedFederationTradingStation = false;

				IsCheckedBorgNexus = false;
				IsCheckedBorgMiningStation = false;
				IsCheckedBorgDock1 = false;
				IsCheckedBorgDock2 = false;
				IsCheckedBorgSciStation = false;
				IsCheckedBorgDefenceStation = false;
				IsCheckedBorgTorpedoTurret = false;
				IsCheckedBorgCutterTurret = false;
			}
			if (IsCheckedFederationStarBase && IsCheckedFederationMiningStation &&
			   IsCheckedFederationDock1 && IsCheckedFederationDock2 &&
			   IsCheckedFederationSciStation && IsCheckedFederationDefenceStation &&
			   IsCheckedFederationTradingStation && IsCheckedBorgNexus &&
			   IsCheckedBorgMiningStation && IsCheckedBorgDock1 &&
			   IsCheckedBorgDock2 && IsCheckedBorgSciStation &&
				IsCheckedBorgDefenceStation && IsCheckedBorgTorpedoTurret &&
				IsCheckedBorgCutterTurret) {
				check = false;
			}
		}
		if (OwnerShip.GetComponent<Stats> ().AI || OwnerShip.GetComponent<Stats> ().FreandAI) {
			if (!AIError) {
				if (CurПриказ == Приказ) {
					if (OwnerShip.GetComponent<Builder> ().BuilderTarget == null) {						
						if (!Stop) {
							if (OwnerShip.GetComponent<Stats> ().AI) {
								if (costStationHumans < OwnerShip.GetComponent<Stats> ().Owner.GetComponent<GlobalAI> ().Crew && costStationDilithium < OwnerShip.GetComponent<Stats> ().Owner.GetComponent<GlobalAI> ().Dilithium && costStationTitanium < OwnerShip.GetComponent<Stats> ().Owner.GetComponent<GlobalAI> ().Titanium) {
									OwnerShip.GetComponent<Stats> ().Owner.GetComponent<GlobalAI> ().Crew -= costStationHumans;
									OwnerShip.GetComponent<Stats> ().Owner.GetComponent<GlobalAI> ().Dilithium -= costStationDilithium;
									OwnerShip.GetComponent<Stats> ().Owner.GetComponent<GlobalAI> ().Titanium -= costStationTitanium;

									GameObject Build = (GameObject)Instantiate (AIStation, gameObject.transform.position, new Quaternion(0, Random.Range(-360, 360), 0, 0));
									Build.tag = "BuildingBuildingEnemy";
									OwnerShip.GetComponent<Builder> ().BuilderTarget = Build;

									Build.GetComponent <BuildingStationScript> ().AI = OwnerShip.GetComponent<Stats> ().AI;
									Build.GetComponent <BuildingStationScript> ().FreandAI = OwnerShip.GetComponent<Stats> ().FreandAI;
									Stop = true;
								}
							} else {
								if (costStationHumans < OwnerShip.GetComponent<Stats> ().Owner.GetComponent<GlobalAI> ().Crew && costStationDilithium < OwnerShip.GetComponent<Stats> ().Owner.GetComponent<GlobalAI> ().Dilithium && costStationTitanium < OwnerShip.GetComponent<Stats> ().Owner.GetComponent<GlobalAI> ().Titanium) {
									OwnerShip.GetComponent<Stats> ().Owner.GetComponent<GlobalAI> ().Crew -= costStationHumans;
									OwnerShip.GetComponent<Stats> ().Owner.GetComponent<GlobalAI> ().Dilithium -= costStationDilithium;
									OwnerShip.GetComponent<Stats> ().Owner.GetComponent<GlobalAI> ().Titanium -= costStationTitanium;

									GameObject Build = (GameObject)Instantiate (AIStation, gameObject.transform.position, new Quaternion(0, Random.Range(-360, 360), 0, 0));
									Build.tag = "BuildingBuildingFreand";
									OwnerShip.GetComponent<Builder> ().BuilderTarget = Build;

									Build.GetComponent <BuildingStationScript> ().AI = OwnerShip.GetComponent<Stats> ().AI;
									Build.GetComponent <BuildingStationScript> ().FreandAI = OwnerShip.GetComponent<Stats> ().FreandAI;
									Stop = true;
								}
							}
						}
					}
				}
			}

			Transform = OwnerShip.transform.position;
			Collider[] colls = Physics.OverlapSphere (gameObject.transform.position, station.GetComponent<GhostBuilding>().Radius+5);
			foreach (Collider coll in colls) {
				if (coll != null) {
					if (coll.tag == "Dwarf" || coll.tag == "Enemy" || coll.tag == "Freand" || coll.tag == "Ship" || coll.tag == "Untagged" || coll.tag == "Neutral") {
						AIError = true;
					} 
					if (coll.tag != "Dwarf" && coll.tag != "Enemy" && coll.tag != "Freand" && coll.tag != "Ship" && coll.tag != "Untagged" && coll.tag != "Neutral") {
						AIError = false;
					}
				} else {
					AIError = false;
				}
			}
		}
		gameObject.transform.position = new Vector3(Random.Range (Transform.x - 70, Transform.x + 70), -2, Random.Range (Transform.z - 70, Transform.z + 70));
	}
	public void TryBuildStation(){
		if (!Locking && !IsLock) {
			if (OwnerShip.GetComponent<Stats> ().isSelect && _GDB.selectList.Count == 1) {
				if (_GDB.Dilithium >= costStationDilithium & _GDB.Humans >= costStationHumans & _GDB.Titanium >= costStationTitanium) {
					Camera.main.GetComponent<BuildManager> ().Ship = OwnerShip;
					Camera.main.GetComponent<BuildManager> ().setBuild (station);
				}
			}
		}
	}
}