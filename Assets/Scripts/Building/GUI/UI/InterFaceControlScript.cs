using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class InterFaceControlScript : MonoBehaviour {
	public GameObject OneShipPlane;

	public GameObject MSDOneShipPlane;

	//MSDSystems
	public Button OneShipMSDButton;
	public Button OneShipOrdersButton;
	public Button OneShipSpecialButton;
	public Button OneShipBuildButton;

	public List<ShipUIControll> OneShipElements;

	private HealthModule OneShipHealth;
	//End of MSDSystems

	public GameObject OrdersOneShipPlane;
	//Orders
	//End of orders

	public GameObject SpecialOneShipPlane;

	public GameObject MineDilithiumButton;
	public GameObject MineTitaniumButton;

	public GameObject TradeDilithiumButton;
	public GameObject TradeTitaniumButton;
	//SkillSystems
	public List<Skill> OneShipSkillList;

	public Image Skill1B;
	public Image CoolDown1;

	public Image Skill2B;
	public Image CoolDown2;

	public Image Skill3B;
	public Image CoolDown3;

	public Image Skill4B;
	public Image CoolDown4;

	public Image Skill5B;
	public Image CoolDown5;

	public Image Skill6B;
	public Image CoolDown6;

	public Image Skill7B;
	public Image CoolDown7;

	public Image Skill8B;
	public Image CoolDown8;
	//End of SkillSystmes

	//BuildSystem
	public GameObject BuildOneShipPlane;

	public GameObject GlobalBuildPlane;
	public GameObject PassiveBuildPlane;
	public GameObject BuildBuildPlane;
	public GameObject DefenceBuildPlane;

	public List<BuildingInBuildShip> OneShipBuildingsList;

	public List<BuildingInBuildShip> OneShipPassiveBuildingsList;
	public List<BuildingInBuildShip> OneShipBuildBuildingsList;
	public List<BuildingInBuildShip> OneShipDefenceBuildingsList;

	public GameObject PassiveStation1;
	public GameObject PassiveStation2;
	public GameObject PassiveStation3;
	public GameObject PassiveStation4;
	public GameObject PassiveStation5;
	public GameObject PassiveStation6;
	public GameObject PassiveStation7;
	public GameObject PassiveStation8;

	public GameObject BuildStation1;
	public GameObject BuildStation2;
	public GameObject BuildStation3;
	public GameObject BuildStation4;
	public GameObject BuildStation5;
	public GameObject BuildStation6;
	public GameObject BuildStation7;
	public GameObject BuildStation8;

	public GameObject DefenceStation1;
	public GameObject DefenceStation2;
	public GameObject DefenceStation3;
	public GameObject DefenceStation4;
	public GameObject DefenceStation5;
	public GameObject DefenceStation6;
	public GameObject DefenceStation7;
	public GameObject DefenceStation8;
	//End of BuildSystem

	public GameObject FleetPlane;
	//MSD fleet systems
	public GameObject MSDFleetPlane;

	public List<List<ShipUIControll>> FleetShipsElemets = new List<List<ShipUIControll>>();

	public List<ShipUIControll> Ship1Elemets;
	public List<ShipUIControll> Ship2Elemets;
	public List<ShipUIControll> Ship3Elemets;
	public List<ShipUIControll> Ship4Elemets;
	public List<ShipUIControll> Ship5Elemets;
	public List<ShipUIControll> Ship6Elemets;
	public List<ShipUIControll> Ship7Elemets;
	public List<ShipUIControll> Ship8Elemets;
	public List<ShipUIControll> Ship9Elemets;
	public List<ShipUIControll> Ship10Elemets;
	public List<ShipUIControll> Ship11Elemets;
	public List<ShipUIControll> Ship12Elemets;

	//end of MSD fleet systems

	public GameObject OrdersFleetPlane;
	public GameObject SpecialFleetPlane;
	public GameObject BuildFleetPlane;

	private Select _sel;
	private GlobalDB _GDB;

	private float ColorChangeTime;
	private Color32 DeactiveSystemColor;

	public GameObject StationPlane;

	//STATIONS
	private bool StationMSDActive;
	private bool StationOrdersActive;
	private bool StationSpecialActive;
	private bool StationBuildActive;
	private bool StationTradeActive;
	private bool StationResearchActive;

	public GameObject StationMSDButton;
	public GameObject StationOrdersButton;
	public GameObject StationSpecialButton;
	public GameObject StationBuildButton;

	public GameObject StationTradeButton;
	public GameObject StationResearchButton;

	public GameObject StationMSDPlane;
	public GameObject StationOrdersPlane;
	public GameObject StationSpecialPlane;
	public GameObject StationBuildPlane;

	public GameObject StationTradePlane;
	public GameObject StationResearchPlane;

	public Text StationName;
	public Text StationOperatop;
	public Text StationLvl;

	public Image StationBluePrint;

	public Image StationHealthBar;
	public Image StationShildBar;
	public Image StationEnergyBar;

	public Image StationCrewI;
	public Text StationCrewC;

	public Image StationPrimaryWeaponI;
	public Image StationPrimaryWeaponC;

	public Image StationSecondaryWeaponI;
	public Image StationSecondaryWeaponC;

	public Image StationImpulseEngineI;
	public Image StationImpulseEngineC;

	public Image StationWarpEngineI;
	public Image StationWarpEngineC;

	public Image StationWarpCoreI;
	public Image StationWarpCoreC;

	public Image StationLifeSupportI;
	public Image StationLifeSupportC;

	public Image StationSensorsI;
	public Image StationSensorsC;

	public Image StationTractorI;
	public Image StationTractorC;

	[HideInInspector]
	public GameObject oldShip;

	private string SName;
	private string SOperator;
	private string SLVL;
	private Sprite SBluePrint;
	[HideInInspector]
	public HealthModule SH;

	public GameObject UpButton;
	public Text UpCost;

	//BuildSystem
	public Button Ship1BuildButton;
	public Button Ship2BuildButton;
	public Button Ship3BuildButton;
	public Button Ship4BuildButton;
	public Button Ship5BuildButton;
	public Button Ship6BuildButton;

	public Button Ship1BuildCancelButton;
	public Button Ship2BuildCancelButton;
	public Button Ship3BuildCancelButton;
	public Button Ship4BuildCancelButton;
	public Button Ship5BuildCancelButton;

	public Image ProgressBar;
	public Text RemainingTime;

	private GlobalLockingSystem _GLS;

	//Research
	public GameObject GlobalResPlane;
	public GameObject ShipTecPlane;
	public GameObject StationTecPlane;
	public GameObject GlobalTecPlane;

	public List<Research> TecList;

	public List<Research> ShipTecList;
	public List<Research> StationTecList;
	public List<Research> GlobalTecList;

	public GameObject ShipTec1;
	public GameObject ShipTec2;
	public GameObject ShipTec3;
	public GameObject ShipTec4;
	public GameObject ShipTec5;
	public GameObject ShipTec6;
	public GameObject ShipTec7;
	public GameObject ShipTec8;

	public GameObject StationTec1;
	public GameObject StationTec2;
	public GameObject StationTec3;
	public GameObject StationTec4;
	public GameObject StationTec5;
	public GameObject StationTec6;
	public GameObject StationTec7;
	public GameObject StationTec8;

	public GameObject GlobalTec1;
	public GameObject GlobalTec2;
	public GameObject GlobalTec3;
	public GameObject GlobalTec4;
	public GameObject GlobalTec5;
	public GameObject GlobalTec6;
	public GameObject GlobalTec7;
	public GameObject GlobalTec8;

	public GameObject CurTec;
	public Image CurTecProgressBar;
	public Text CurTecTime;

	public GameObject CancelConstructionButton;
	public GameObject SelfDestructionButton;

	// Use this for initialization
	void Start () {
		GameObject MainUI = GameObject.FindGameObjectWithTag ("MainUI");

		_sel = MainUI.GetComponent<Select>();
		_GDB = MainUI.GetComponent<GlobalDB>();
		_GLS = MainUI.GetComponent<GlobalLockingSystem>();

		FleetShipsElemets.Add (Ship1Elemets);
		FleetShipsElemets.Add (Ship2Elemets);
		FleetShipsElemets.Add (Ship3Elemets);
		FleetShipsElemets.Add (Ship4Elemets);
		FleetShipsElemets.Add (Ship5Elemets);
		FleetShipsElemets.Add (Ship6Elemets);
		FleetShipsElemets.Add (Ship7Elemets);
		FleetShipsElemets.Add (Ship8Elemets);
		FleetShipsElemets.Add (Ship9Elemets);
		FleetShipsElemets.Add (Ship10Elemets);
		FleetShipsElemets.Add (Ship11Elemets);
		FleetShipsElemets.Add (Ship12Elemets);
	}
	
	// Update is called once per frame
	void Update () {
		if (ColorChangeTime <= 0) {
			if (DeactiveSystemColor.ToString() == new Color32 (255, 174, 0, 255).ToString()) {
				DeactiveSystemColor = new Color32 (255, 255, 255, 255);
			} else {
				DeactiveSystemColor = new Color32 (255, 174, 0, 255);
			}
			ColorChangeTime = 1;
		} else {
			ColorChangeTime -= Time.deltaTime;
		}

		if (_GDB.selectList.Count == 0) {
			OneShipSkillList.Clear ();

			OneShipBuildingsList.Clear ();
			OneShipPassiveBuildingsList.Clear ();
			OneShipBuildBuildingsList.Clear ();

			OneShipDefenceBuildingsList.Clear ();

			OneShipHealth = null;
		}

		if (_GDB.selectList.Count != 0) {
			if (_GDB.selectList.Count > 1) {
				FleetPlane.SetActive (true);
				OneShipPlane.SetActive (false);
				if (_sel.MSDActive) {
					MSDFleetPlane.SetActive (true);
					OrdersFleetPlane.SetActive (false);
					SpecialFleetPlane.SetActive (false);
					BuildFleetPlane.SetActive (false);

					MSDOneShipPlane.SetActive (false);
					OrdersOneShipPlane.SetActive (false);
					SpecialOneShipPlane.SetActive (false);
					BuildOneShipPlane.SetActive (false);

					for (int i = 0; i < _GDB.selectList.Count; ++i) {
						foreach (ShipUIControll _SUI in FleetShipsElemets[i]) {
							_SUI.Ship = _GDB.selectList [i];
						}
					}
					for (int i = _GDB.selectList.Count; i < 12; ++i) {
						foreach (ShipUIControll _SUI in FleetShipsElemets[i]) {
							_SUI.Ship = null;
						}
					}
				}
				if(_sel.OrdersActive){
					MSDFleetPlane.SetActive (false);
					OrdersFleetPlane.SetActive (true);
					SpecialFleetPlane.SetActive (false);
					BuildFleetPlane.SetActive (false);

					MSDOneShipPlane.SetActive (false);
					OrdersOneShipPlane.SetActive (false);
					SpecialOneShipPlane.SetActive (false);
					BuildOneShipPlane.SetActive (false);
				}
				if(_sel.SpecialActive){
					MSDFleetPlane.SetActive (false);
					OrdersFleetPlane.SetActive (false);
					SpecialFleetPlane.SetActive (true);
					BuildFleetPlane.SetActive (false);

					MSDOneShipPlane.SetActive (false);
					OrdersOneShipPlane.SetActive (false);
					SpecialOneShipPlane.SetActive (false);
					BuildOneShipPlane.SetActive (false);
				}
				if(_sel.BuildActive){
					MSDFleetPlane.SetActive (false);
					OrdersFleetPlane.SetActive (false);
					SpecialFleetPlane.SetActive (false);
					BuildFleetPlane.SetActive (true);

					MSDOneShipPlane.SetActive (false);
					OrdersOneShipPlane.SetActive (false);
					SpecialOneShipPlane.SetActive (false);
					BuildOneShipPlane.SetActive (false);
				}
			}
			if (_GDB.selectList.Count == 1) {
				OneShipHealth = _GDB.selectList [0].GetComponent<HealthModule> ();

				FleetPlane.SetActive (false);
				OneShipPlane.SetActive (true);

				OneShipSkillList = _GDB.selectList [0].GetComponents<Skill> ().ToList ();

				OneShipBuildingsList = _GDB.selectList [0].GetComponentsInChildren<BuildingInBuildShip> ().ToList ();
				for (int i = 0; i < OneShipBuildingsList.Count; i++) {
					if (OneShipBuildingsList [i].NatrealStation) {
						if (!FindInPassiveBuildingsList (OneShipBuildingsList [i])) {
							OneShipPassiveBuildingsList.Add (OneShipBuildingsList [i]);
						}
					}
					if (OneShipBuildingsList [i].AttackStation) {
						if (!FindInBuildBuildingsList (OneShipBuildingsList [i])) {
							OneShipBuildBuildingsList.Add (OneShipBuildingsList [i]);
						}
					}
					if (OneShipBuildingsList [i].DefenceStation) {
						if (!FindInDefenceBuildingsList (OneShipBuildingsList [i])) {
							OneShipDefenceBuildingsList.Add (OneShipBuildingsList [i]);
						}
					}
				}
				if (!_GDB.selectList [0].GetComponent<Stats> ().AI && !_GDB.selectList [0].GetComponent<Stats> ().FreandAI && !_GDB.selectList [0].GetComponent<Stats> ().Neutral) {
					OneShipOrdersButton.interactable = true;
					OneShipSpecialButton.interactable = true;
					OneShipBuildButton.interactable = true;
				} else {
					OneShipOrdersButton.interactable = false;
					OneShipSpecialButton.interactable = false;
					OneShipBuildButton.interactable = false;
				}

				if (_sel.MSDActive) {
					MSDFleetPlane.SetActive (false);
					OrdersFleetPlane.SetActive (false);
					SpecialFleetPlane.SetActive (false);
					BuildFleetPlane.SetActive (false);

					MSDOneShipPlane.SetActive (true);
					OrdersOneShipPlane.SetActive (false);
					SpecialOneShipPlane.SetActive (false);
					BuildOneShipPlane.SetActive (false);

					foreach (ShipUIControll _sUI in OneShipElements) {
						_sUI.Ship = _GDB.selectList [0];
					}
				}
				if (_sel.OrdersActive) {
					MSDFleetPlane.SetActive (false);
					OrdersFleetPlane.SetActive (false);
					SpecialFleetPlane.SetActive (false);
					BuildFleetPlane.SetActive (false);

					MSDOneShipPlane.SetActive (false);
					OrdersOneShipPlane.SetActive (true);
					SpecialOneShipPlane.SetActive (false);
					BuildOneShipPlane.SetActive (false);

					foreach (ShipUIControll _sUI in OneShipElements) {
						_sUI.Ship = _GDB.selectList[0];
					}

					if (_GDB.selectList [0].GetComponent<Stats> ().miner) {
						MineTitaniumButton.SetActive (true);
						MineDilithiumButton.SetActive (true);
					} else {
						MineTitaniumButton.SetActive (false);
						MineDilithiumButton.SetActive (false);
					}
					if (_GDB.selectList [0].GetComponent<Stats> ().Transport) {
						TradeTitaniumButton.SetActive (true);
						TradeDilithiumButton.SetActive (true);
					} else {
						TradeTitaniumButton.SetActive (false);
						TradeDilithiumButton.SetActive (false);
					}
				}

				if (_sel.SpecialActive) {
					MSDFleetPlane.SetActive (false);
					OrdersFleetPlane.SetActive (false);
					SpecialFleetPlane.SetActive (false);
					BuildFleetPlane.SetActive (false);

					MSDOneShipPlane.SetActive (false);
					OrdersOneShipPlane.SetActive (false);
					SpecialOneShipPlane.SetActive (true);
					BuildOneShipPlane.SetActive (false);

					if (OneShipSkillList.Count >= 1) {
						Skill1B.color = new Color32(255, 255, 255, 255);
						Skill1B.sprite = OneShipSkillList [0].Icon;
						if (OneShipSkillList [0].CoolDownTimer > 0) {
							CoolDown1.fillAmount = OneShipSkillList [0].CoolDownTimer / OneShipSkillList [0].CoolDown; 
						} else {
							CoolDown1.fillAmount = 0;
						}
					} else {
						Skill1B.color = new Color32 (0, 0, 0, 0);
						CoolDown1.fillAmount = 0;
					}
					if (OneShipSkillList.Count >= 2) {
						Skill2B.color = new Color32(255, 255, 255, 255);
						Skill2B.sprite = OneShipSkillList [1].Icon;
						if (OneShipSkillList [1].CoolDownTimer > 0) {
							CoolDown2.fillAmount = OneShipSkillList [1].CoolDownTimer / OneShipSkillList [1].CoolDown; 
						} else {
							CoolDown2.fillAmount = 0;
						}
					} else {
						Skill2B.color = new Color32 (0, 0, 0, 0);
						CoolDown2.fillAmount = 0;
					}
					if (OneShipSkillList.Count >= 3) {
						Skill3B.color = new Color32(255, 255, 255, 255);
						Skill3B.sprite = OneShipSkillList [2].Icon;
						if (OneShipSkillList [2].CoolDownTimer > 0) {
							CoolDown3.fillAmount = OneShipSkillList [2].CoolDownTimer / OneShipSkillList [2].CoolDown; 
						} else {
							CoolDown3.fillAmount = 0;
						}
					} else {
						Skill3B.color = new Color32 (0, 0, 0, 0);
						CoolDown3.fillAmount = 0;
					}
					if (OneShipSkillList.Count >= 4) {
						Skill4B.color = new Color32(255, 255, 255, 255);
						Skill4B.sprite = OneShipSkillList [3].Icon;
						if (OneShipSkillList [3].CoolDownTimer > 0) {
							CoolDown4.fillAmount = OneShipSkillList [3].CoolDownTimer / OneShipSkillList [3].CoolDown; 
						} else {
							CoolDown4.fillAmount = 0;
						}
					} else {
						Skill4B.color = new Color32 (0, 0, 0, 0);
						CoolDown4.fillAmount = 0;
					}
					if (OneShipSkillList.Count >= 5) {
						Skill5B.color = new Color32(255, 255, 255, 255);
						Skill5B.sprite = OneShipSkillList [4].Icon;
						if (OneShipSkillList [4].CoolDownTimer > 0) {
							CoolDown5.fillAmount = OneShipSkillList [4].CoolDownTimer / OneShipSkillList [4].CoolDown; 
						} else {
							CoolDown5.fillAmount = 0;
						}
					} else {
						Skill5B.color = new Color32 (0, 0, 0, 0);
						CoolDown5.fillAmount = 0;
					}
					if (OneShipSkillList.Count >= 6) {
						Skill6B.color = new Color32(255, 255, 255, 255);
						Skill6B.sprite = OneShipSkillList [5].Icon;
						if (OneShipSkillList [5].CoolDownTimer > 0) {
							CoolDown6.fillAmount = OneShipSkillList [5].CoolDownTimer / OneShipSkillList [5].CoolDown; 
						} else {
							CoolDown6.fillAmount = 0;
						}
					} else {
						Skill6B.color = new Color32 (0, 0, 0, 0);
						CoolDown6.fillAmount = 0;
					}
					if (OneShipSkillList.Count >= 7) {
						Skill7B.color = new Color32(255, 255, 255, 255);
						Skill7B.sprite = OneShipSkillList [6].Icon;
						if (OneShipSkillList [6].CoolDownTimer > 0) {
							CoolDown7.fillAmount = OneShipSkillList [6].CoolDownTimer / OneShipSkillList [6].CoolDown; 
						} else {
							CoolDown7.fillAmount = 0;
						}
					} else {
						Skill7B.color = new Color32 (0, 0, 0, 0);
						CoolDown7.fillAmount = 0;
					}
					if (OneShipSkillList.Count >= 8) {
						Skill8B.color = new Color32(255, 255, 255, 255);
						Skill8B.sprite = OneShipSkillList [7].Icon;
						if (OneShipSkillList [7].CoolDownTimer > 0) {
							CoolDown8.fillAmount = OneShipSkillList [7].CoolDownTimer / OneShipSkillList [7].CoolDown; 
						} else {
							CoolDown8.fillAmount = 0;
						}
					} else {
						Skill8B.color = new Color32 (0, 0, 0, 0);
						CoolDown8.fillAmount = 0;
					}
				}

				if (_sel.BuildActive) {
					if (oldShip != _GDB.selectList [0]) {
						OneShipSkillList.Clear ();

						OneShipBuildingsList.Clear ();
						OneShipPassiveBuildingsList.Clear ();
						OneShipBuildBuildingsList.Clear ();
						OneShipDefenceBuildingsList.Clear ();

						oldShip = _GDB.selectList [0];
					}

					MSDFleetPlane.SetActive (false);
					OrdersFleetPlane.SetActive (false);
					SpecialFleetPlane.SetActive (false);
					BuildFleetPlane.SetActive (false);

					MSDOneShipPlane.SetActive (false);
					OrdersOneShipPlane.SetActive (false);
					SpecialOneShipPlane.SetActive (false);
					BuildOneShipPlane.SetActive (true);

					if (_GDB.selectList [0].GetComponent<Stats> ().miner) {
						if (!_GDB.selectList [0].GetComponent<Stats> ().AttackStations && !_GDB.selectList [0].GetComponent<Stats> ().DefenceStations & !_GDB.selectList [0].GetComponent<Stats> ().NatralStations) {
							GlobalBuildPlane.SetActive (true);
							PassiveBuildPlane.SetActive (false);
							BuildBuildPlane.SetActive (false);
							DefenceBuildPlane.SetActive (false);
						} else {
							if (_GDB.selectList [0].GetComponent<Stats> ().NatralStations) {
								GlobalBuildPlane.SetActive (false);
								PassiveBuildPlane.SetActive (true);
								BuildBuildPlane.SetActive (false);
								DefenceBuildPlane.SetActive (false);

								if (OneShipPassiveBuildingsList.Count >= 1) {
									PassiveStation1.SetActive (true);
									PassiveStation1.GetComponent<Image> ().sprite = OneShipPassiveBuildingsList [0].Station;
									if (OneShipPassiveBuildingsList [0].Locking) {
										PassiveStation1.GetComponent<Button> ().interactable = false;
									} else {
										PassiveStation1.GetComponent<Button> ().interactable = true;
									}

									if (OneShipPassiveBuildingsList.Count >= 2) {
										PassiveStation2.SetActive (true);
										PassiveStation2.GetComponent<Image> ().sprite = OneShipPassiveBuildingsList [1].Station;
										if (OneShipPassiveBuildingsList [1].Locking) {
											PassiveStation2.GetComponent<Button> ().interactable = false;
										} else {
											PassiveStation2.GetComponent<Button> ().interactable = true;
										}

										if (OneShipPassiveBuildingsList.Count >= 3) {
											PassiveStation3.SetActive (true);
											PassiveStation3.GetComponent<Image> ().sprite = OneShipPassiveBuildingsList [2].Station;
											if (OneShipPassiveBuildingsList [2].Locking) {
												PassiveStation3.GetComponent<Button> ().interactable = false;
											} else {
												PassiveStation3.GetComponent<Button> ().interactable = true;
											}

											if (OneShipPassiveBuildingsList.Count >= 4) {
												PassiveStation4.SetActive (true);
												PassiveStation4.GetComponent<Image> ().sprite = OneShipPassiveBuildingsList [3].Station;
												if (OneShipPassiveBuildingsList [3].Locking) {
													PassiveStation4.GetComponent<Button> ().interactable = false;
												} else {
													PassiveStation4.GetComponent<Button> ().interactable = true;
												}

												if (OneShipPassiveBuildingsList.Count >= 5) {
													PassiveStation5.SetActive (true);
													PassiveStation5.GetComponent<Image> ().sprite = OneShipPassiveBuildingsList [4].Station;
													if (OneShipPassiveBuildingsList [4].Locking) {
														PassiveStation5.GetComponent<Button> ().interactable = false;
													} else {
														PassiveStation5.GetComponent<Button> ().interactable = true;
													}

													if (OneShipPassiveBuildingsList.Count >= 6) {
														PassiveStation6.SetActive (true);
														PassiveStation6.GetComponent<Image> ().sprite = OneShipPassiveBuildingsList [5].Station;
														if (OneShipPassiveBuildingsList [5].Locking) {
															PassiveStation6.GetComponent<Button> ().interactable = false;
														} else {
															PassiveStation6.GetComponent<Button> ().interactable = true;
														}

														if (OneShipPassiveBuildingsList.Count >= 7) {
															PassiveStation7.SetActive (true);
															PassiveStation7.GetComponent<Image> ().sprite = OneShipPassiveBuildingsList [6].Station;
															if (OneShipPassiveBuildingsList [6].Locking) {
																PassiveStation7.GetComponent<Button> ().interactable = false;
															} else {
																PassiveStation7.GetComponent<Button> ().interactable = true;
															}

															if (OneShipPassiveBuildingsList.Count >= 8) {
																PassiveStation8.SetActive (true);
																PassiveStation8.GetComponent<Image> ().sprite = OneShipPassiveBuildingsList [7].Station;
																if (OneShipPassiveBuildingsList [7].Locking) {
																	PassiveStation8.GetComponent<Button> ().interactable = false;
																} else {
																	PassiveStation8.GetComponent<Button> ().interactable = true;
																}
															} else {
																PassiveStation8.SetActive (false);
															}
														} else {
															PassiveStation7.SetActive (false);
															PassiveStation8.SetActive (false);
														}
													} else {
														PassiveStation6.SetActive (false);
														PassiveStation7.SetActive (false);
														PassiveStation8.SetActive (false);
													}
												} else {
													PassiveStation5.SetActive (false);
													PassiveStation6.SetActive (false);
													PassiveStation7.SetActive (false);
													PassiveStation8.SetActive (false);
												}
											} else {
												PassiveStation4.SetActive (false);
												PassiveStation5.SetActive (false);
												PassiveStation6.SetActive (false);
												PassiveStation7.SetActive (false);
												PassiveStation8.SetActive (false);
											}
										} else {
											PassiveStation3.SetActive (false);
											PassiveStation4.SetActive (false);
											PassiveStation5.SetActive (false);
											PassiveStation6.SetActive (false);
											PassiveStation7.SetActive (false);
											PassiveStation8.SetActive (false);
										}
									} else {
										PassiveStation2.SetActive (false);
										PassiveStation3.SetActive (false);
										PassiveStation4.SetActive (false);
										PassiveStation5.SetActive (false);
										PassiveStation6.SetActive (false);
										PassiveStation7.SetActive (false);
										PassiveStation8.SetActive (false);
									}
								} else {
									PassiveStation1.SetActive (false);
									PassiveStation2.SetActive (false);
									PassiveStation3.SetActive (false);
									PassiveStation4.SetActive (false);
									PassiveStation5.SetActive (false);
									PassiveStation6.SetActive (false);
									PassiveStation7.SetActive (false);
									PassiveStation8.SetActive (false);
								}
							}
							if (_GDB.selectList [0].GetComponent<Stats> ().AttackStations) {
								GlobalBuildPlane.SetActive (false);
								PassiveBuildPlane.SetActive (false);
								BuildBuildPlane.SetActive (true);
								DefenceBuildPlane.SetActive (false);

								if (OneShipBuildBuildingsList.Count >= 1) {
									BuildStation1.SetActive (true);
									BuildStation1.GetComponent<Image> ().sprite = OneShipBuildBuildingsList [0].Station;
									if (OneShipBuildBuildingsList [0].Locking) {
										BuildStation1.GetComponent<Button> ().interactable = false;
									} else {
										BuildStation1.GetComponent<Button> ().interactable = true;
									}

									if (OneShipBuildBuildingsList.Count >= 2) {
										BuildStation2.SetActive (true);
										BuildStation2.GetComponent<Image> ().sprite = OneShipBuildBuildingsList [1].Station;
										if (OneShipBuildBuildingsList [1].Locking) {
											BuildStation2.GetComponent<Button> ().interactable = false;
										} else {
											BuildStation2.GetComponent<Button> ().interactable = true;
										}

										if (OneShipBuildBuildingsList.Count >= 3) {
											BuildStation3.SetActive (true);
											BuildStation3.GetComponent<Image> ().sprite = OneShipBuildBuildingsList [2].Station;
											if (OneShipBuildBuildingsList [2].Locking) {
												BuildStation3.GetComponent<Button> ().interactable = false;
											} else {
												BuildStation3.GetComponent<Button> ().interactable = true;
											}

											if (OneShipBuildBuildingsList.Count >= 4) {
												BuildStation4.SetActive (true);
												BuildStation4.GetComponent<Image> ().sprite = OneShipBuildBuildingsList [3].Station;
												if (OneShipBuildBuildingsList [3].Locking) {
													BuildStation4.GetComponent<Button> ().interactable = false;
												} else {
													BuildStation4.GetComponent<Button> ().interactable = true;
												}

												if (OneShipBuildBuildingsList.Count >= 5) {
													BuildStation5.SetActive (true);
													BuildStation5.GetComponent<Image> ().sprite = OneShipBuildBuildingsList [4].Station;
													if (OneShipBuildBuildingsList [4].Locking) {
														BuildStation5.GetComponent<Button> ().interactable = false;
													} else {
														BuildStation5.GetComponent<Button> ().interactable = true;
													}

													if (OneShipBuildBuildingsList.Count >= 6) {
														BuildStation6.SetActive (true);
														BuildStation6.GetComponent<Image> ().sprite = OneShipBuildBuildingsList [5].Station;
														if (OneShipBuildBuildingsList [5].Locking) {
															BuildStation6.GetComponent<Button> ().interactable = false;
														} else {
															BuildStation6.GetComponent<Button> ().interactable = true;
														}

														if (OneShipBuildBuildingsList.Count >= 7) {
															BuildStation7.SetActive (true);
															BuildStation7.GetComponent<Image> ().sprite = OneShipBuildBuildingsList [6].Station;
															if (OneShipBuildBuildingsList [6].Locking) {
																BuildStation7.GetComponent<Button> ().interactable = false;
															} else {
																BuildStation7.GetComponent<Button> ().interactable = true;
															}

															if (OneShipBuildBuildingsList.Count >= 8) {
																BuildStation8.SetActive (true);
																BuildStation8.GetComponent<Image> ().sprite = OneShipBuildBuildingsList [7].Station;
																if (OneShipPassiveBuildingsList [7].Locking) {
																	BuildStation8.GetComponent<Button> ().interactable = false;
																} else {
																	BuildStation8.GetComponent<Button> ().interactable = true;
																}
															} else {
																BuildStation8.SetActive (false);
															}
														} else {
															BuildStation7.SetActive (false);
															BuildStation8.SetActive (false);
														}
													} else {
														BuildStation6.SetActive (false);
														BuildStation7.SetActive (false);
														BuildStation8.SetActive (false);
													}
												} else {
													BuildStation5.SetActive (false);
													BuildStation6.SetActive (false);
													BuildStation7.SetActive (false);
													BuildStation8.SetActive (false);
												}
											} else {
												BuildStation4.SetActive (false);
												BuildStation5.SetActive (false);
												BuildStation6.SetActive (false);
												BuildStation7.SetActive (false);
												BuildStation8.SetActive (false);
											}
										} else {
											BuildStation3.SetActive (false);
											BuildStation4.SetActive (false);
											BuildStation5.SetActive (false);
											BuildStation6.SetActive (false);
											BuildStation7.SetActive (false);
											BuildStation8.SetActive (false);
										}
									} else {
										BuildStation2.SetActive (false);
										BuildStation3.SetActive (false);
										BuildStation4.SetActive (false);
										BuildStation5.SetActive (false);
										BuildStation6.SetActive (false);
										BuildStation7.SetActive (false);
										BuildStation8.SetActive (false);
									}
								} else {
									BuildStation1.SetActive (false);
									BuildStation2.SetActive (false);
									BuildStation3.SetActive (false);
									BuildStation4.SetActive (false);
									BuildStation5.SetActive (false);
									BuildStation6.SetActive (false);
									BuildStation7.SetActive (false);
									BuildStation8.SetActive (false);
								}
							}
							if (_GDB.selectList [0].GetComponent<Stats> ().DefenceStations) {
								GlobalBuildPlane.SetActive (false);
								PassiveBuildPlane.SetActive (false);
								BuildBuildPlane.SetActive (false);
								DefenceBuildPlane.SetActive (true);

								if (OneShipDefenceBuildingsList.Count >= 1) {
									DefenceStation1.SetActive (true);
									DefenceStation1.GetComponent<Image> ().sprite = OneShipDefenceBuildingsList [0].Station;
									if (OneShipDefenceBuildingsList [0].Locking) {
										DefenceStation1.GetComponent<Button> ().interactable = false;
									} else {
										DefenceStation1.GetComponent<Button> ().interactable = true;
									}

									if (OneShipDefenceBuildingsList.Count >= 2) {
										DefenceStation2.SetActive (true);
										DefenceStation2.GetComponent<Image> ().sprite = OneShipDefenceBuildingsList [1].Station;
										if (OneShipDefenceBuildingsList [1].Locking) {
											DefenceStation2.GetComponent<Button> ().interactable = false;
										} else {
											DefenceStation2.GetComponent<Button> ().interactable = true;
										}

										if (OneShipDefenceBuildingsList.Count >= 3) {
											DefenceStation3.SetActive (true);
											DefenceStation3.GetComponent<Image> ().sprite = OneShipDefenceBuildingsList [2].Station;
											if (OneShipDefenceBuildingsList [2].Locking) {
												DefenceStation3.GetComponent<Button> ().interactable = false;
											} else {
												DefenceStation3.GetComponent<Button> ().interactable = true;
											}

											if (OneShipDefenceBuildingsList.Count >= 4) {
												DefenceStation4.SetActive (true);
												DefenceStation4.GetComponent<Image> ().sprite = OneShipDefenceBuildingsList [3].Station;
												if (OneShipDefenceBuildingsList [3].Locking) {
													DefenceStation4.GetComponent<Button> ().interactable = false;
												} else {
													DefenceStation4.GetComponent<Button> ().interactable = true;
												}

												if (OneShipDefenceBuildingsList.Count >= 5) {
													DefenceStation5.SetActive (true);
													DefenceStation5.GetComponent<Image> ().sprite = OneShipDefenceBuildingsList [4].Station;
													if (OneShipDefenceBuildingsList [4].Locking) {
														DefenceStation5.GetComponent<Button> ().interactable = false;
													} else {
														DefenceStation5.GetComponent<Button> ().interactable = true;
													}

													if (OneShipDefenceBuildingsList.Count >= 6) {
														DefenceStation6.SetActive (true);
														DefenceStation6.GetComponent<Image> ().sprite = OneShipDefenceBuildingsList [5].Station;
														if (OneShipDefenceBuildingsList [5].Locking) {
															DefenceStation6.GetComponent<Button> ().interactable = false;
														} else {
															DefenceStation6.GetComponent<Button> ().interactable = true;
														}

														if (OneShipDefenceBuildingsList.Count >= 7) {
															DefenceStation7.SetActive (true);
															DefenceStation7.GetComponent<Image> ().sprite = OneShipDefenceBuildingsList [6].Station;
															if (OneShipDefenceBuildingsList [6].Locking) {
																DefenceStation7.GetComponent<Button> ().interactable = false;
															} else {
																DefenceStation7.GetComponent<Button> ().interactable = true;
															}

															if (OneShipDefenceBuildingsList.Count >= 8) {
																DefenceStation8.SetActive (true);
																DefenceStation8.GetComponent<Image> ().sprite = OneShipDefenceBuildingsList [7].Station;
																if (OneShipDefenceBuildingsList [7].Locking) {
																	DefenceStation8.GetComponent<Button> ().interactable = false;
																} else {
																	DefenceStation8.GetComponent<Button> ().interactable = true;
																}
															} else {
																DefenceStation8.SetActive (false);
															}
														} else {
															DefenceStation7.SetActive (false);
															DefenceStation8.SetActive (false);
														}
													} else {
														DefenceStation6.SetActive (false);
														DefenceStation7.SetActive (false);
														DefenceStation8.SetActive (false);
													}
												} else {
													DefenceStation5.SetActive (false);
													DefenceStation6.SetActive (false);
													DefenceStation7.SetActive (false);
													DefenceStation8.SetActive (false);
												}
											} else {
												DefenceStation4.SetActive (false);
												DefenceStation5.SetActive (false);
												DefenceStation6.SetActive (false);
												DefenceStation7.SetActive (false);
												DefenceStation8.SetActive (false);
											}
										} else {
											DefenceStation3.SetActive (false);
											DefenceStation4.SetActive (false);
											DefenceStation5.SetActive (false);
											DefenceStation6.SetActive (false);
											DefenceStation7.SetActive (false);
											DefenceStation8.SetActive (false);
										}
									} else {
										DefenceStation2.SetActive (false);
										DefenceStation3.SetActive (false);
										DefenceStation4.SetActive (false);
										DefenceStation5.SetActive (false);
										DefenceStation6.SetActive (false);
										DefenceStation7.SetActive (false);
										DefenceStation8.SetActive (false);
									}
								} else {
									DefenceStation1.SetActive (false);
									DefenceStation2.SetActive (false);
									DefenceStation3.SetActive (false);
									DefenceStation4.SetActive (false);
									DefenceStation5.SetActive (false);
									DefenceStation6.SetActive (false);
									DefenceStation7.SetActive (false);
									DefenceStation8.SetActive (false);
								}
							}
						}
					}
				} else {
					GlobalBuildPlane.SetActive (false);
					PassiveBuildPlane.SetActive (false);
					BuildBuildPlane.SetActive (false);
					DefenceBuildPlane.SetActive (false);
				}
			}
		} else {
			FleetPlane.SetActive (false);
			OneShipPlane.SetActive (false);

			MSDFleetPlane.SetActive (false);
			OrdersFleetPlane.SetActive (false);
			SpecialFleetPlane.SetActive (false);
			BuildFleetPlane.SetActive (false);

			MSDOneShipPlane.SetActive (false);
			OrdersOneShipPlane.SetActive (false);
			SpecialOneShipPlane.SetActive (false);
			BuildOneShipPlane.SetActive (false);
		}
		if (_GDB.activeObjectInterface != null) {
			StationPlane.SetActive (true);

			if (!_GDB.activeObjectInterface.GetComponent<BuildingStationScript> ()) {
				SH = _GDB.activeObjectInterface.GetComponent<HealthModule> ();
			}
			Station _sb = _GDB.activeObjectInterface.GetComponent<Station>();
			SName = _sb.StationName;
				if (!_sb.AI && !_sb.FreandAI && !_sb.Neutral) {
					SOperator = "Operator: " + _GDB.PlayerName;
				} else {
					if (_sb.Neutral) {
						SOperator = "Operator: Neutral";
					} else {
						SOperator = "Operator: " + _sb.Owner.name;
					}
				}
				SBluePrint = _sb.ShipBluePrint;
			if (_sb.UpgradeModule)
			{
				SLVL = _GDB.activeObjectInterface.GetComponent<UpgradeModule>().lvl.ToString();
			}

				StationMSDButton.SetActive (true);
				StationOrdersButton.SetActive (true);
				StationSpecialButton.SetActive (true);
			if (_sb.ShipBuildModule)
			{
				StationBuildButton.SetActive(true);
			}
			if (_sb.TradeModule)
			{
				StationTradeButton.SetActive(false);
			}
			if (_sb.SciModule)
			{
				StationResearchButton.SetActive(false);
			}

				if (!_sb.AI && !_sb.FreandAI && !_sb.Neutral) {
					StationOrdersButton.GetComponent<Button> ().interactable = true;
					StationSpecialButton.GetComponent<Button> ().interactable = true;
					StationBuildButton.GetComponent<Button> ().interactable = true;

					StationTradeButton.GetComponent<Button> ().interactable = true;
					StationResearchButton.GetComponent<Button> ().interactable = true;
				} else {
					StationOrdersButton.GetComponent<Button> ().interactable = false;
					StationSpecialButton.GetComponent<Button> ().interactable = false;
					StationBuildButton.GetComponent<Button> ().interactable = false;

					StationTradeButton.GetComponent<Button> ().interactable = false;
					StationResearchButton.GetComponent<Button> ().interactable = false;
				}


			if (_GDB.activeObjectInterface.name == "BuildingStation(Clone)") {
				BuildingStationScript _bs = _GDB.activeObjectInterface.GetComponent<BuildingStationScript> ();
				if (_bs.ReadyBuilding.GetComponent<Station> ()) {
					SName = _bs.ReadyBuilding.GetComponent<Station> ().name;
					SBluePrint = _bs.ReadyBuilding.GetComponent<Station> ().ShipBluePrint;
				}

				if (!_bs.AI && !_bs.FreandAI && !_bs.Neutral) {
					SOperator = "Operator: " + _GDB.PlayerName;
				} else {
					if (_bs.Neutral) {
						SOperator = "Operator: Neutral";
					} else {
						SOperator = "Operator: " + _bs.Owner.name;
					}
				}
				SLVL = string.Empty;

				StationMSDButton.SetActive (true);
				StationOrdersButton.SetActive (true);
				StationSpecialButton.SetActive (false);
				StationBuildButton.SetActive (false);

				StationTradeButton.SetActive (false);
				StationResearchButton.SetActive (false);

				if (!_bs.AI && !_bs.FreandAI && !_bs.Neutral) {
					StationOrdersButton.GetComponent<Button> ().interactable = true;
					StationSpecialButton.GetComponent<Button> ().interactable = true;
				} else {
					StationOrdersButton.GetComponent<Button> ().interactable = false;
					StationSpecialButton.GetComponent<Button> ().interactable = false;
				}
			}

			if (StationMSDActive) {
				StationMSDPlane.SetActive (true);
				StationOrdersPlane.SetActive (false);
				StationSpecialPlane.SetActive (false);
				StationBuildPlane.SetActive (false);

				StationTradePlane.SetActive (false);
				StationResearchPlane.SetActive (false);

				StationName.text = SName;
				StationLvl.text = SLVL;

				StationOperatop.text = SOperator;

				StationBluePrint.sprite = SBluePrint;
				if (SH != null) {
					if (SH.CurShilds > 0) {
						StationShildBar.fillAmount = SH.CurShilds / SH.Shilds;
					} else {
						StationShildBar.fillAmount = 0;
					}
					if (SH.curHealth > 0) {
						StationHealthBar.fillAmount = SH.curHealth / SH.maxHealth;
					} else {
						StationHealthBar.fillAmount = 0;
					}
					if (SH.curEnergy > 0) {
						StationEnergyBar.fillAmount = SH.curEnergy / SH.maxEnergy;
					} else {
						StationEnergyBar.fillAmount = 0;
					}

					if (SH.maxCrew > 0) {
						if (SH.curCrew > SH.maxCrew / 2) {
							StationCrewI.color = Color.green;
						}
						if (SH.curCrew < SH.maxCrew / 2 && SH.curCrew > SH.maxCrew / 4) {
							StationCrewI.color = Color.yellow;
						}
						if (SH.curCrew < SH.maxCrew / 4 && SH.curCrew != 0) {
							StationCrewI.color = Color.red;
						}
						if (SH.curCrew <= 0) {
							StationCrewI.color = Color.black;
						}
						StationCrewC.text = ": " + (int)SH.curCrew;
					} else {
						StationCrewI.color = Color.green;
						StationCrewC.text = ": 0";
					}
					if (SH.maxPrimaryWeaponSystemHealth > 0) {
						StationPrimaryWeaponC.fillAmount = SH.curPrimaryWeaponSystemHealth / SH.maxPrimaryWeaponSystemHealth;
						if (SH.ActivePrimaryWeapon) {
							StationPrimaryWeaponI.color = DeactiveSystemColor;
						} else {
							if (SH.curPrimaryWeaponSystemHealth > SH.maxPrimaryWeaponSystemHealth / 2) {
								StationPrimaryWeaponI.color = Color.green;
								StationPrimaryWeaponC.color = Color.green;
							}
							if (SH.curPrimaryWeaponSystemHealth < SH.maxPrimaryWeaponSystemHealth / 2 && SH.curPrimaryWeaponSystemHealth > SH.maxPrimaryWeaponSystemHealth / 4) {
								StationPrimaryWeaponI.color = Color.yellow;
								StationPrimaryWeaponC.color = Color.yellow;
							}
							if (SH.curPrimaryWeaponSystemHealth < SH.maxPrimaryWeaponSystemHealth / 4 && SH.curPrimaryWeaponSystemHealth > SH.maxPrimaryWeaponSystemHealth / 8) {
								StationPrimaryWeaponI.color = new Color32 (255, 174, 0, 255);
								StationPrimaryWeaponC.color = new Color32 (255, 174, 0, 255);
							}
							if (SH.curPrimaryWeaponSystemHealth < SH.maxPrimaryWeaponSystemHealth / 8) {
								StationPrimaryWeaponI.color = Color.red;
							}
							if (SH.curPrimaryWeaponSystemHealth <= 0) {
								StationPrimaryWeaponI.color = Color.grey;
							}
						}
					} else {
						StationPrimaryWeaponI.color = Color.grey;
						StationPrimaryWeaponC.color = Color.grey;
						StationPrimaryWeaponC.fillAmount = 1;
					}
					if (SH.maxSecondaryWeaponSystemHealth > 0) {
						StationSecondaryWeaponC.fillAmount = SH.curSecondaryWeaponSystemHealth / SH.maxSecondaryWeaponSystemHealth;
						if (SH.ActiveSecondaryWeapon) {
							StationSecondaryWeaponI.color = DeactiveSystemColor;
						} else {
							if (SH.curSecondaryWeaponSystemHealth > SH.maxSecondaryWeaponSystemHealth / 2) {
								StationSecondaryWeaponI.color = Color.green;
								StationSecondaryWeaponC.color = Color.green;
							}
							if (SH.curSecondaryWeaponSystemHealth < SH.maxSecondaryWeaponSystemHealth / 2 && SH.curSecondaryWeaponSystemHealth > SH.maxSecondaryWeaponSystemHealth / 4) {
								StationSecondaryWeaponI.color = Color.yellow;
								StationSecondaryWeaponC.color = Color.yellow;
							}
							if (SH.curSecondaryWeaponSystemHealth < SH.maxSecondaryWeaponSystemHealth / 4 && SH.curSecondaryWeaponSystemHealth > SH.maxSecondaryWeaponSystemHealth / 8) {
								StationSecondaryWeaponI.color = new Color32 (255, 174, 0, 255);
								StationSecondaryWeaponC.color = new Color32 (255, 174, 0, 255);
							}
							if (SH.curSecondaryWeaponSystemHealth < SH.maxSecondaryWeaponSystemHealth / 8) {
								StationSecondaryWeaponI.color = Color.red;
							}
							if (SH.curSecondaryWeaponSystemHealth <= 0) {
								StationSecondaryWeaponI.color = Color.grey;
							}
						}
					} else {
						StationSecondaryWeaponI.color = Color.grey;
						StationSecondaryWeaponC.color = Color.grey;
						StationSecondaryWeaponC.fillAmount = 1;
					}
					if (SH.maxImpulseSystemHealth > 0) {
						StationImpulseEngineC.fillAmount = SH.curImpulseSystemHealth / SH.maxImpulseSystemHealth;
						if (SH.ActiveImpulse) {
							StationImpulseEngineI.color = DeactiveSystemColor;
						} else {
							if (SH.curImpulseSystemHealth > SH.maxImpulseSystemHealth / 2) {
								StationImpulseEngineI.color = Color.green;
								StationImpulseEngineC.color = Color.green;
							}
							if (SH.curImpulseSystemHealth < SH.maxImpulseSystemHealth / 2 && SH.curImpulseSystemHealth > SH.maxImpulseSystemHealth / 4) {
								StationImpulseEngineI.color = Color.yellow;
								StationImpulseEngineC.color = Color.yellow;
							}
							if (SH.curImpulseSystemHealth < SH.maxImpulseSystemHealth / 4 && SH.curImpulseSystemHealth > SH.maxImpulseSystemHealth / 8) {
								StationImpulseEngineI.color = new Color32 (255, 174, 0, 255);
								StationImpulseEngineC.color = new Color32 (255, 174, 0, 255);
							}
							if (SH.curImpulseSystemHealth < SH.maxImpulseSystemHealth / 8) {
								StationImpulseEngineI.color = Color.red;
							}
							if (SH.curImpulseSystemHealth <= 0) {
								StationImpulseEngineI.color = Color.grey;
							}
						}
					} else {
						StationImpulseEngineI.color = Color.grey;
						StationImpulseEngineC.color = Color.grey;
						StationImpulseEngineC.fillAmount = 1;
					}
					if (SH.maxWarpEngingSystemHealth > 0) {
						StationWarpEngineC.fillAmount = SH.curWarpEngingSystemHealth / SH.maxWarpEngingSystemHealth;
						if (SH.ActiveWarpEnging) {
							StationWarpEngineI.color = DeactiveSystemColor;
						} else {
							if (SH.curWarpEngingSystemHealth > SH.maxWarpEngingSystemHealth / 2) {
								StationWarpEngineI.color = Color.green;
								StationWarpEngineC.color = Color.green;
							}
							if (SH.curWarpEngingSystemHealth < SH.maxWarpEngingSystemHealth / 2 && SH.curWarpEngingSystemHealth > SH.maxWarpEngingSystemHealth / 4) {
								StationWarpEngineI.color = Color.yellow;
								StationWarpEngineC.color = Color.yellow;
							}
							if (SH.curWarpEngingSystemHealth < SH.maxWarpEngingSystemHealth / 4 && SH.curWarpEngingSystemHealth > SH.maxWarpEngingSystemHealth / 8) {
								StationWarpEngineI.color = new Color32 (255, 174, 0, 255);
								StationWarpEngineC.color = new Color32 (255, 174, 0, 255);
							}
							if (SH.curWarpEngingSystemHealth < SH.maxWarpEngingSystemHealth / 8) {
								StationWarpEngineI.color = Color.red;
							}
							if (SH.curWarpEngingSystemHealth <= 0) {
								StationWarpEngineI.color = Color.grey;
							}
						}
					} else {
						StationWarpEngineI.color = Color.grey;
						StationWarpEngineC.color = Color.grey;
						StationWarpEngineC.fillAmount = 1;
					}
					if (SH.maxWarpCoreHealth > 0) {
						StationWarpCoreC.fillAmount = SH.curWarpCoreHealth / SH.maxWarpCoreHealth;
						if (SH.ActiveWarpCore) {
							StationWarpCoreI.color = DeactiveSystemColor;
						} else {
							if (SH.curWarpCoreHealth > SH.maxWarpCoreHealth / 2) {
								StationWarpCoreI.color = Color.green;
								StationWarpCoreC.color = Color.green;
							}
							if (SH.curWarpCoreHealth < SH.maxWarpCoreHealth / 2 && SH.curWarpCoreHealth > SH.maxWarpCoreHealth / 4) {
								StationWarpCoreI.color = Color.yellow;
								StationWarpCoreC.color = Color.yellow;
							}
							if (SH.curWarpCoreHealth < SH.maxWarpCoreHealth / 4 && SH.curWarpCoreHealth > SH.maxWarpCoreHealth / 8) {
								StationWarpCoreI.color = new Color32 (255, 174, 0, 255);
								StationWarpCoreC.color = new Color32 (255, 174, 0, 255);
							}
							if (SH.curWarpCoreHealth < SH.maxWarpCoreHealth / 8) {
								StationWarpCoreI.color = Color.red;
							}
							if (SH.curWarpCoreHealth <= 0) {
								StationWarpCoreI.color = Color.grey;
							}
						}
					} else {
						StationWarpCoreI.color = Color.grey;
						StationWarpCoreC.color = Color.grey;
						StationWarpCoreC.fillAmount = 1;
					}
					if (SH.maxLifeSupportSystemHealth > 0) {
						StationLifeSupportC.fillAmount = SH.curLifeSupportSystemHealth / SH.maxLifeSupportSystemHealth;
						if (SH.ActiveLifeSupport) {
							StationLifeSupportI.color = DeactiveSystemColor;
						} else {
							if (SH.curLifeSupportSystemHealth > SH.maxLifeSupportSystemHealth / 2) {
								StationLifeSupportI.color = Color.green;
								StationLifeSupportC.color = Color.green;
							}
							if (SH.curLifeSupportSystemHealth < SH.maxLifeSupportSystemHealth / 2 && SH.curLifeSupportSystemHealth > SH.maxLifeSupportSystemHealth / 4) {
								StationLifeSupportI.color = Color.yellow;
								StationLifeSupportC.color = Color.yellow;
							}
							if (SH.curLifeSupportSystemHealth < SH.maxLifeSupportSystemHealth / 4 && SH.curLifeSupportSystemHealth > SH.maxLifeSupportSystemHealth / 8) {
								StationLifeSupportI.color = new Color32 (255, 174, 0, 255);
								StationLifeSupportC.color = new Color32 (255, 174, 0, 255);
							}
							if (SH.curLifeSupportSystemHealth < SH.maxLifeSupportSystemHealth / 8) {
								StationLifeSupportI.color = Color.red;
							}
							if (SH.curLifeSupportSystemHealth <= 0) {
								StationLifeSupportI.color = Color.grey;
							}
						}
					} else {
						StationLifeSupportI.color = Color.grey;
						StationLifeSupportC.color = Color.grey;
						StationLifeSupportC.fillAmount = 1;
					}
					if (SH.maxSensorsSystemHealth > 0) {
						StationSensorsC.fillAmount = SH.curSensorsSystemHealth / SH.maxSensorsSystemHealth;
						if (SH.ActiveSensors) {
							StationSensorsI.color = DeactiveSystemColor;
						} else {
							if (SH.curSensorsSystemHealth > SH.maxSensorsSystemHealth / 2) {
								StationSensorsI.color = Color.green;
								StationSensorsC.color = Color.green;
							}
							if (SH.curSensorsSystemHealth < SH.maxSensorsSystemHealth / 2 && SH.curSensorsSystemHealth > SH.maxSensorsSystemHealth / 4) {
								StationSensorsI.color = Color.yellow;
								StationSensorsC.color = Color.yellow;
							}
							if (SH.curSensorsSystemHealth < SH.maxSensorsSystemHealth / 4 && SH.curSensorsSystemHealth > SH.maxSensorsSystemHealth / 8) {
								StationSensorsI.color = new Color32 (255, 174, 0, 255);
								StationSensorsC.color = new Color32 (255, 174, 0, 255);
							}
							if (SH.curSensorsSystemHealth < SH.maxSensorsSystemHealth / 8) {
								StationSensorsI.color = Color.red;
							}
							if (SH.curSensorsSystemHealth <= 0) {
								StationSensorsI.color = Color.grey;
							}
						}
					} else {
						StationSensorsI.color = Color.grey;
						StationSensorsC.color = Color.grey;
						StationSensorsC.fillAmount = 1;
					}
					if (SH.maxTractorBeamSystemHealth > 0) {
						StationTractorC.fillAmount = SH.curTractorBeamSystemHealth / SH.maxTractorBeamSystemHealth;
						if (SH.ActiveTractor) {
							StationTractorI.color = DeactiveSystemColor;
						} else {
							if (SH.curTractorBeamSystemHealth > SH.maxTractorBeamSystemHealth / 2) {
								StationTractorI.color = Color.green;
								StationTractorC.color = Color.green;
							}
							if (SH.curTractorBeamSystemHealth < SH.maxTractorBeamSystemHealth / 2 && SH.curTractorBeamSystemHealth > SH.maxTractorBeamSystemHealth / 4) {
								StationTractorI.color = Color.yellow;
								StationTractorC.color = Color.yellow;
							}
							if (SH.curTractorBeamSystemHealth < SH.maxTractorBeamSystemHealth / 4 && SH.curTractorBeamSystemHealth > SH.maxTractorBeamSystemHealth / 8) {
								StationTractorI.color = new Color32 (255, 174, 0, 255);
								StationTractorC.color = new Color32 (255, 174, 0, 255);
							}
							if (SH.curTractorBeamSystemHealth < SH.maxTractorBeamSystemHealth / 8) {
								StationTractorI.color = Color.red;
							}
							if (SH.curTractorBeamSystemHealth <= 0) {
								StationTractorI.color = Color.grey;
							}
						}
					} else {
						StationTractorI.color = Color.grey;
						StationTractorC.color = Color.grey;
						StationTractorC.fillAmount = 1;
					}
				} else {
					StationShildBar.fillAmount = 0;
					StationHealthBar.fillAmount = 0;
					StationEnergyBar.fillAmount = 0;
					StationCrewC.text = ": 0";

					StationPrimaryWeaponI.color = Color.grey;
					StationPrimaryWeaponC.fillAmount = 0;

					StationSecondaryWeaponI.color = Color.grey;
					StationSecondaryWeaponC.fillAmount = 0;

					StationImpulseEngineI.color = Color.grey;
					StationImpulseEngineC.fillAmount = 0;

					StationWarpEngineI.color = Color.grey;
					StationWarpEngineC.fillAmount = 0;

					StationWarpCoreI.color = Color.grey;
					StationWarpCoreC.fillAmount = 0;

					StationTractorI.color = Color.grey;
					StationTractorC.fillAmount = 0;

					StationSensorsI.color = Color.grey;
					StationSensorsC.fillAmount = 0;

					StationLifeSupportI.color = Color.grey;
					StationLifeSupportC.fillAmount = 0;
				}
			}
			if (StationOrdersActive) {
				StationMSDPlane.SetActive (false);
				StationOrdersPlane.SetActive (true);
				StationSpecialPlane.SetActive (false);
				StationBuildPlane.SetActive (false);

				StationTradePlane.SetActive (false);
				StationResearchPlane.SetActive (false);

				if (_sb.UpgradeModule)
				{
					UpButton.SetActive(true);
					UpCost.gameObject.SetActive(true);
					UpgradeModule _sbum = _sb.gameObject.GetComponent<UpgradeModule>();
					if (_sbum.lvl < _sbum.MaxLvl)
					{
						UpButton.GetComponent<Button>().interactable = true;
						UpCost.text = "Dilithium: "+ _sbum.costUp[_sbum.lvl];
					}
					else
					{
						UpButton.GetComponent<Button>().interactable = false;
					}
				}
				else
				{
					UpButton.SetActive(false);
					UpCost.gameObject.SetActive(false);
				}

				if (_GDB.activeObjectInterface.GetComponent<BuildingStationScript> ()) {
					CancelConstructionButton.SetActive (true);
					SelfDestructionButton.SetActive (false);
				} else {
					CancelConstructionButton.SetActive (false);
					SelfDestructionButton.SetActive (true);
				}
			}
			if (StationSpecialActive) {
				StationMSDPlane.SetActive (false);
				StationOrdersPlane.SetActive (false);
				StationSpecialPlane.SetActive (true);
				StationBuildPlane.SetActive (false);

				StationTradePlane.SetActive (false);
				StationResearchPlane.SetActive (false);
			}
			if (StationBuildActive) {
				StationMSDPlane.SetActive (false);
				StationOrdersPlane.SetActive (false);
				StationSpecialPlane.SetActive (false);
					
				if (!_sb.AI && !_sb.FreandAI && !_sb.Neutral) {
						StationBuildPlane.SetActive (true);
					}
				if (_sb.ShipBuildModule)
				{
					ShipBuildModule _sbsbm = _sb.gameObject.GetComponent<ShipBuildModule>();
					if (_sbsbm.Ships.Length >= 1)
					{
						Ship1BuildButton.gameObject.SetActive(true);
						Ship1BuildButton.GetComponent<Image>().sprite = _sbsbm.Ships[0].Icon;
						if (!_sbsbm.Ships[0].ShipLock)
						{
							Ship1BuildButton.interactable = true;
						}
						else
						{
							Ship1BuildButton.interactable = false;
						}
					}
					else
					{
						Ship1BuildButton.gameObject.SetActive(false);
					}
					if (_sbsbm.Ships.Length >= 2)
					{
						Ship2BuildButton.gameObject.SetActive(true);
						Ship2BuildButton.GetComponent<Image>().sprite =  _sbsbm.Ships[1].Icon;
						if (!_sbsbm.Ships[1].ShipLock)
						{
							Ship2BuildButton.interactable = true;
						}
						else
						{
							Ship2BuildButton.interactable = false;
						}
					}
					else
					{
						Ship2BuildButton.gameObject.SetActive(false);
					}
					if (_sbsbm.Ships.Length >= 3)
					{
						Ship3BuildButton.gameObject.SetActive(true);
						Ship3BuildButton.GetComponent<Image>().sprite = _sbsbm.Ships[2].Icon;
						if (!_sbsbm.Ships[2].ShipLock)
						{
							Ship3BuildButton.interactable = true;
						}
						else
						{
							Ship3BuildButton.interactable = false;
						}
					}
					else
					{
						Ship3BuildButton.gameObject.SetActive(false);
					}
					if (_sbsbm.Ships.Length >= 4)
					{
						Ship4BuildButton.gameObject.SetActive(true);
						Ship4BuildButton.GetComponent<Image>().sprite = _sbsbm.Ships[3].Icon;
						if (!_sbsbm.Ships[3].ShipLock)
						{
							Ship4BuildButton.interactable = true;
						}
						else
						{
							Ship4BuildButton.interactable = false;
						}
					}
					else
					{
						Ship4BuildButton.gameObject.SetActive(false);
					}
					if (_sbsbm.Ships.Length >= 5)
					{
						Ship5BuildButton.gameObject.SetActive(true);
						Ship5BuildButton.GetComponent<Image>().sprite = _sbsbm.Ships[4].Icon;
						if (!_sbsbm.Ships[4].ShipLock)
						{
							Ship5BuildButton.interactable = true;
						}
						else
						{
							Ship5BuildButton.interactable = false;
						}
					}
					else
					{
						Ship5BuildButton.gameObject.SetActive(false);
					}
					if (_sbsbm.Ships.Length >= 6)
					{
						Ship6BuildButton.gameObject.SetActive(true);
						Ship6BuildButton.GetComponent<Image>().sprite = _sbsbm.Ships[5].Icon;
						if (!_sbsbm.Ships[5].ShipLock)
						{
							Ship6BuildButton.interactable = true;
						}
						else
						{
							Ship6BuildButton.interactable = false;
						}
					}
					else
					{
						Ship6BuildButton.gameObject.SetActive(false);
					}
				}

				if (_GDB.activeObjectInterface.GetComponent<ShipBuild> ().Корабль.Count > 0) {
					ProgressBar.fillAmount = 1 - (_GDB.activeObjectInterface.GetComponent<ShipBuild> ().timeBuild / _GDB.activeObjectInterface.GetComponent<ShipBuild> ().Время [0]);
				} else {
					ProgressBar.fillAmount = 0;
				}

				if (_GDB.activeObjectInterface.GetComponent<ShipBuild> ().Корабль.Count > 0) {
					RemainingTime.text = "Remaining time:" + (int)_GDB.activeObjectInterface.GetComponent<ShipBuild> ().timeBuild;
				} else {
					RemainingTime.text = "Remaining time: 0";
				}
				if (_GDB.activeObjectInterface.GetComponent<ShipBuild> ().Корабль.Count >= 1) {
					Ship1BuildCancelButton.gameObject.SetActive (true);
					Ship1BuildCancelButton.GetComponent<Image> ().sprite = _GDB.activeObjectInterface.GetComponent<ShipBuild> ().Корабль[0].GetComponent<Stats>().icon;
					if (_GDB.activeObjectInterface.GetComponent<ShipBuild> ().Корабль.Count >= 2) {
						Ship2BuildCancelButton.gameObject.SetActive (true);
						Ship2BuildCancelButton.GetComponent<Image> ().sprite = _GDB.activeObjectInterface.GetComponent<ShipBuild> ().Корабль[1].GetComponent<Stats>().icon;
						if (_GDB.activeObjectInterface.GetComponent<ShipBuild> ().Корабль.Count >= 3) {
							Ship3BuildCancelButton.gameObject.SetActive (true);
							Ship3BuildCancelButton.GetComponent<Image> ().sprite = _GDB.activeObjectInterface.GetComponent<ShipBuild> ().Корабль[2].GetComponent<Stats>().icon;
							if (_GDB.activeObjectInterface.GetComponent<ShipBuild> ().Корабль.Count >= 4) {
								Ship4BuildCancelButton.gameObject.SetActive (true);
								Ship4BuildCancelButton.GetComponent<Image> ().sprite = _GDB.activeObjectInterface.GetComponent<ShipBuild> ().Корабль[3].GetComponent<Stats>().icon;
								if (_GDB.activeObjectInterface.GetComponent<ShipBuild> ().Корабль.Count >= 5) {
									Ship5BuildCancelButton.gameObject.SetActive (true);
									Ship5BuildCancelButton.GetComponent<Image> ().sprite = _GDB.activeObjectInterface.GetComponent<ShipBuild> ().Корабль [4].GetComponent<Stats> ().icon;
								}else{
									Ship5BuildCancelButton.gameObject.SetActive (false);
								}
							} else {
								Ship4BuildCancelButton.gameObject.SetActive (false);
								Ship5BuildCancelButton.gameObject.SetActive (false);
							}
						} else {
							Ship3BuildCancelButton.gameObject.SetActive (false);
							Ship4BuildCancelButton.gameObject.SetActive (false);
							Ship5BuildCancelButton.gameObject.SetActive (false);
						}
					} else {
						Ship2BuildCancelButton.gameObject.SetActive (false);
						Ship3BuildCancelButton.gameObject.SetActive (false);
						Ship4BuildCancelButton.gameObject.SetActive (false);
						Ship5BuildCancelButton.gameObject.SetActive (false);
					}
				} else {
					Ship1BuildCancelButton.gameObject.SetActive (false);
					Ship2BuildCancelButton.gameObject.SetActive (false);
					Ship3BuildCancelButton.gameObject.SetActive (false);
					Ship4BuildCancelButton.gameObject.SetActive (false);
					Ship5BuildCancelButton.gameObject.SetActive (false);
				}

				StationTradePlane.SetActive (false);
				StationResearchPlane.SetActive (false);
			}
			if (StationTradeActive) {
				StationMSDPlane.SetActive (false);
				StationOrdersPlane.SetActive (false);
				StationSpecialPlane.SetActive (false);
				StationBuildPlane.SetActive (false);

				if (!_sb.AI && !_sb.FreandAI && !_sb.Neutral) {
					StationTradePlane.SetActive (true);
				}
				StationResearchPlane.SetActive (false);
			}
			if (StationResearchActive) {
				StationMSDPlane.SetActive (false);
				StationOrdersPlane.SetActive (false);
				StationSpecialPlane.SetActive (false);
				StationBuildPlane.SetActive (false);

				StationTradePlane.SetActive (false);
				if (!_GDB.activeObjectInterface.GetComponent<Magic> ().AI && !_GDB.activeObjectInterface.GetComponent<Magic> ().FreandAI && !_GDB.activeObjectInterface.GetComponent<Magic> ().Neutral) {
					StationResearchPlane.SetActive (true);
				}

				if (ShipTecList.Count >= 1) {
					ShipTec1.SetActive (true);
					ShipTec1.GetComponent<Image> ().sprite = ShipTecList [0].Texture;
					if (ShipTecList [0].Locking) {
						ShipTec1.GetComponent<Button> ().interactable = false;
					} else {
						ShipTec1.GetComponent<Button> ().interactable = true;
					}

					if (ShipTecList.Count >= 2) {
						ShipTec2.SetActive (true);
						ShipTec2.GetComponent<Image> ().sprite = ShipTecList [1].Texture;
						if (ShipTecList [1].Locking) {
							ShipTec2.GetComponent<Button> ().interactable = false;
						} else {
							ShipTec2.GetComponent<Button> ().interactable = true;
						}

						if (ShipTecList.Count >= 3) {
							ShipTec3.SetActive (true);
							ShipTec3.GetComponent<Image> ().sprite = ShipTecList [2].Texture;
							if (ShipTecList [2].Locking) {
								ShipTec3.GetComponent<Button> ().interactable = false;
							} else {
								ShipTec3.GetComponent<Button> ().interactable = true;
							}

							if (ShipTecList.Count >= 4) {
								ShipTec4.SetActive (true);
								ShipTec4.GetComponent<Image> ().sprite = ShipTecList [3].Texture;
								if (ShipTecList [3].Locking) {
									ShipTec4.GetComponent<Button> ().interactable = false;
								} else {
									ShipTec4.GetComponent<Button> ().interactable = true;
								}

								if (ShipTecList.Count >= 5) {
									ShipTec5.SetActive (true);
									ShipTec5.GetComponent<Image> ().sprite = ShipTecList [4].Texture;
									if (ShipTecList [4].Locking) {
										ShipTec5.GetComponent<Button> ().interactable = false;
									} else {
										ShipTec5.GetComponent<Button> ().interactable = true;
									}

									if (ShipTecList.Count >= 6) {
										ShipTec6.SetActive (true);
										ShipTec6.GetComponent<Image> ().sprite = ShipTecList [5].Texture;
										if (ShipTecList [5].Locking) {
											ShipTec6.GetComponent<Button> ().interactable = false;
										} else {
											ShipTec6.GetComponent<Button> ().interactable = true;
										}

										if (ShipTecList.Count >= 7) {
											ShipTec7.SetActive (true);
											ShipTec7.GetComponent<Image> ().sprite = ShipTecList [6].Texture;
											if (ShipTecList [6].Locking) {
												ShipTec7.GetComponent<Button> ().interactable = false;
											} else {
												ShipTec7.GetComponent<Button> ().interactable = true;
											}

											if (ShipTecList.Count >= 8) {
												ShipTec8.SetActive (true);
												ShipTec8.GetComponent<Image> ().sprite = ShipTecList [7].Texture;
												if (ShipTecList [7].Locking) {
													ShipTec8.GetComponent<Button> ().interactable = false;
												} else {
													ShipTec8.GetComponent<Button> ().interactable = true;
												}
											} else {
												ShipTec8.SetActive (false);
											}
										} else {
											ShipTec7.SetActive (false);
											ShipTec8.SetActive (false);
										}
									} else {
										ShipTec6.SetActive (false);
										ShipTec7.SetActive (false);
										ShipTec8.SetActive (false);
									}
								} else {
									ShipTec5.SetActive (false);
									ShipTec6.SetActive (false);
									ShipTec7.SetActive (false);
									ShipTec8.SetActive (false);
								}
							} else {
								ShipTec4.SetActive (false);
								ShipTec5.SetActive (false);
								ShipTec6.SetActive (false);
								ShipTec7.SetActive (false);
								ShipTec8.SetActive (false);
							}
						} else {
							ShipTec3.SetActive (false);
							ShipTec4.SetActive (false);
							ShipTec5.SetActive (false);
							ShipTec6.SetActive (false);
							ShipTec7.SetActive (false);
							ShipTec8.SetActive (false);
						}
					} else {
						ShipTec2.SetActive (false);
						ShipTec3.SetActive (false);
						ShipTec4.SetActive (false);
						ShipTec5.SetActive (false);
						ShipTec6.SetActive (false);
						ShipTec7.SetActive (false);
						ShipTec8.SetActive (false);
					}
				} else {
					ShipTec1.SetActive (false);
					ShipTec2.SetActive (false);
					ShipTec3.SetActive (false);
					ShipTec4.SetActive (false);
					ShipTec5.SetActive (false);
					ShipTec6.SetActive (false);
					ShipTec7.SetActive (false);
					ShipTec8.SetActive (false);
				}

				if (StationTecList.Count >= 1) {
					StationTec1.SetActive (true);
					StationTec1.GetComponent<Image> ().sprite = StationTecList [0].Texture;
					if (StationTecList [0].Locking) {
						StationTec1.GetComponent<Button> ().interactable = false;
					} else {
						StationTec1.GetComponent<Button> ().interactable = true;
					}

					if (StationTecList.Count >= 2) {
						StationTec2.SetActive (true);
						StationTec2.GetComponent<Image> ().sprite = StationTecList [1].Texture;
						if (StationTecList [1].Locking) {
							StationTec2.GetComponent<Button> ().interactable = false;
						} else {
							StationTec2.GetComponent<Button> ().interactable = true;
						}

						if (StationTecList.Count >= 3) {
							StationTec3.SetActive (true);
							StationTec3.GetComponent<Image> ().sprite = StationTecList [2].Texture;
							if (StationTecList [2].Locking) {
								StationTec3.GetComponent<Button> ().interactable = false;
							} else {
								StationTec3.GetComponent<Button> ().interactable = true;
							}

							if (StationTecList.Count >= 4) {
								StationTec4.SetActive (true);
								StationTec4.GetComponent<Image> ().sprite = StationTecList [3].Texture;
								if (StationTecList [3].Locking) {
									StationTec4.GetComponent<Button> ().interactable = false;
								} else {
									StationTec4.GetComponent<Button> ().interactable = true;
								}

								if (StationTecList.Count >= 5) {
									StationTec5.SetActive (true);
									StationTec5.GetComponent<Image> ().sprite = StationTecList [4].Texture;
									if (StationTecList [4].Locking) {
										StationTec5.GetComponent<Button> ().interactable = false;
									} else {
										StationTec5.GetComponent<Button> ().interactable = true;
									}

									if (StationTecList.Count >= 6) {
										StationTec6.SetActive (true);
										StationTec6.GetComponent<Image> ().sprite = StationTecList [5].Texture;
										if (StationTecList [5].Locking) {
											StationTec6.GetComponent<Button> ().interactable = false;
										} else {
											StationTec6.GetComponent<Button> ().interactable = true;
										}

										if (StationTecList.Count >= 7) {
											StationTec7.SetActive (true);
											StationTec7.GetComponent<Image> ().sprite = StationTecList [6].Texture;
											if (StationTecList [6].Locking) {
												StationTec7.GetComponent<Button> ().interactable = false;
											} else {
												StationTec7.GetComponent<Button> ().interactable = true;
											}

											if (StationTecList.Count >= 8) {
												StationTec8.SetActive (true);
												StationTec8.GetComponent<Image> ().sprite = StationTecList [7].Texture;
												if (StationTecList [7].Locking) {
													StationTec8.GetComponent<Button> ().interactable = false;
												} else {
													StationTec8.GetComponent<Button> ().interactable = true;
												}
											} else {
												StationTec8.SetActive (false);
											}
										} else {
											StationTec7.SetActive (false);
											StationTec8.SetActive (false);
										}
									} else {
										StationTec6.SetActive (false);
										StationTec7.SetActive (false);
										StationTec8.SetActive (false);
									}
								} else {
									StationTec5.SetActive (false);
									StationTec6.SetActive (false);
									StationTec7.SetActive (false);
									StationTec8.SetActive (false);
								}
							} else {
								StationTec4.SetActive (false);
								StationTec5.SetActive (false);
								StationTec6.SetActive (false);
								StationTec7.SetActive (false);
								StationTec8.SetActive (false);
							}
						} else {
							StationTec3.SetActive (false);
							StationTec4.SetActive (false);
							StationTec5.SetActive (false);
							StationTec6.SetActive (false);
							StationTec7.SetActive (false);
							StationTec8.SetActive (false);
						}
					} else {
						StationTec2.SetActive (false);
						StationTec3.SetActive (false);
						StationTec4.SetActive (false);
						StationTec5.SetActive (false);
						StationTec6.SetActive (false);
						StationTec7.SetActive (false);
						StationTec8.SetActive (false);
					}
				} else {
					StationTec1.SetActive (false);
					StationTec2.SetActive (false);
					StationTec3.SetActive (false);
					StationTec4.SetActive (false);
					StationTec5.SetActive (false);
					StationTec6.SetActive (false);
					StationTec7.SetActive (false);
					StationTec8.SetActive (false);
				}

				if (GlobalTecList.Count >= 1) {
					GlobalTec1.SetActive (true);
					GlobalTec1.GetComponent<Image> ().sprite = GlobalTecList [0].Texture;
					if (GlobalTecList [0].Locking) {
						GlobalTec1.GetComponent<Button> ().interactable = false;
					} else {
						GlobalTec1.GetComponent<Button> ().interactable = true;
					}

					if (GlobalTecList.Count >= 2) {
						GlobalTec2.SetActive (true);
						GlobalTec2.GetComponent<Image> ().sprite = GlobalTecList [1].Texture;
						if (GlobalTecList [1].Locking) {
							GlobalTec2.GetComponent<Button> ().interactable = false;
						} else {
							GlobalTec2.GetComponent<Button> ().interactable = true;
						}

						if (GlobalTecList.Count >= 3) {
							GlobalTec3.SetActive (true);
							GlobalTec3.GetComponent<Image> ().sprite = GlobalTecList [2].Texture;
							if (GlobalTecList [2].Locking) {
								GlobalTec3.GetComponent<Button> ().interactable = false;
							} else {
								GlobalTec3.GetComponent<Button> ().interactable = true;
							}

							if (GlobalTecList.Count >= 4) {
								GlobalTec4.SetActive (true);
								GlobalTec4.GetComponent<Image> ().sprite = GlobalTecList [3].Texture;
								if (GlobalTecList [3].Locking) {
									GlobalTec4.GetComponent<Button> ().interactable = false;
								} else {
									GlobalTec4.GetComponent<Button> ().interactable = true;
								}

								if (GlobalTecList.Count >= 5) {
									GlobalTec5.SetActive (true);
									GlobalTec5.GetComponent<Image> ().sprite = GlobalTecList [4].Texture;
									if (GlobalTecList [4].Locking) {
										GlobalTec5.GetComponent<Button> ().interactable = false;
									} else {
										GlobalTec5.GetComponent<Button> ().interactable = true;
									}

									if (GlobalTecList.Count >= 6) {
										GlobalTec6.SetActive (true);
										GlobalTec6.GetComponent<Image> ().sprite = GlobalTecList [5].Texture;
										if (GlobalTecList [5].Locking) {
											GlobalTec6.GetComponent<Button> ().interactable = false;
										} else {
											GlobalTec6.GetComponent<Button> ().interactable = true;
										}

										if (GlobalTecList.Count >= 7) {
											GlobalTec7.SetActive (true);
											GlobalTec7.GetComponent<Image> ().sprite = GlobalTecList [6].Texture;
											if (GlobalTecList [6].Locking) {
												GlobalTec7.GetComponent<Button> ().interactable = false;
											} else {
												GlobalTec7.GetComponent<Button> ().interactable = true;
											}

											if (GlobalTecList.Count >= 8) {
												GlobalTec8.SetActive (true);
												GlobalTec8.GetComponent<Image> ().sprite = GlobalTecList [7].Texture;
												if (GlobalTecList [7].Locking) {
													GlobalTec8.GetComponent<Button> ().interactable = false;
												} else {
													GlobalTec8.GetComponent<Button> ().interactable = true;
												}
											} else {
												GlobalTec8.SetActive (false);
											}
										} else {
											GlobalTec7.SetActive (false);
											GlobalTec8.SetActive (false);
										}
									} else {
										GlobalTec6.SetActive (false);
										GlobalTec7.SetActive (false);
										GlobalTec8.SetActive (false);
									}
								} else {
									GlobalTec5.SetActive (false);
									GlobalTec6.SetActive (false);
									GlobalTec7.SetActive (false);
									GlobalTec8.SetActive (false);
								}
							} else {
								GlobalTec4.SetActive (false);
								GlobalTec5.SetActive (false);
								GlobalTec6.SetActive (false);
								GlobalTec7.SetActive (false);
								GlobalTec8.SetActive (false);
							}
						} else {
							GlobalTec3.SetActive (false);
							GlobalTec4.SetActive (false);
							GlobalTec5.SetActive (false);
							GlobalTec6.SetActive (false);
							GlobalTec7.SetActive (false);
							GlobalTec8.SetActive (false);
						}
					} else {
						GlobalTec2.SetActive (false);
						GlobalTec3.SetActive (false);
						GlobalTec4.SetActive (false);
						GlobalTec5.SetActive (false);
						GlobalTec6.SetActive (false);
						GlobalTec7.SetActive (false);
						GlobalTec8.SetActive (false);
					}
				} else {
					GlobalTec1.SetActive (false);
					GlobalTec2.SetActive (false);
					GlobalTec3.SetActive (false);
					GlobalTec4.SetActive (false);
					GlobalTec5.SetActive (false);
					GlobalTec6.SetActive (false);
					GlobalTec7.SetActive (false);
					GlobalTec8.SetActive (false);
				}

				if (_GDB.activeObjectInterface.GetComponent<Magic> ()) {
					Magic _m = _GDB.activeObjectInterface.GetComponent<Magic> ();
					if (_m.ShipTecActive) {
						ShipTecPlane.SetActive (true);
						StationTecPlane.SetActive (false);
						GlobalTecPlane.SetActive (false);

						GlobalResPlane.SetActive (false);
					}
					if (_m.StationTecActive) {
						ShipTecPlane.SetActive (false);
						StationTecPlane.SetActive (true);
						GlobalTecPlane.SetActive (false);

						GlobalResPlane.SetActive (false);
					}
					if (_m.GlobalTecActive) {
						ShipTecPlane.SetActive (false);
						StationTecPlane.SetActive (false);
						GlobalTecPlane.SetActive (true);

						GlobalResPlane.SetActive (false);
					}
					if (!_m.ShipTecActive && !_m.StationTecActive && !_m.GlobalTecActive) {
						ShipTecPlane.SetActive (false);
						StationTecPlane.SetActive (false);
						GlobalTecPlane.SetActive (false);

						GlobalResPlane.SetActive (true);
					}

					if (_m.Scient) {
						CurTec.SetActive (true);
						CurTec.GetComponent<Image> ().sprite = _m.CurRes.Texture;
						CurTecProgressBar.fillAmount = _m.TimeScient / _m.CurRes.StartTime;
						CurTecTime.text = "Time: " + (int)_m.CurRes.ScientTime;
					} else {
						CurTec.SetActive (false);
						CurTecProgressBar.fillAmount = 0;
						CurTecTime.text = "Time: 0";
					}
				}
			}

			TecList = _GDB.activeObjectInterface.GetComponentsInChildren<Research> ().ToList ();
			if (TecList.Count > 0) {
				for (int i = 0; i < TecList.Count; i++) {
					if (TecList [i].T1) {
						if (!FindInShipTecList (TecList [i])) {
							ShipTecList.Add (TecList [i]);
						}
					}
					if (TecList [i].T2) {
						if (!FindInStationTecList (TecList [i])) {
							StationTecList.Add (TecList [i]);
						}
					}
					if (TecList [i].T3) {
						if (!FindInGlobalTecList (TecList [i])) {
							GlobalTecList.Add (TecList [i]);
						}
					}
				}
			} else {
				TecList.Clear ();

				ShipTecList.Clear ();
				StationTecList.Clear ();
				GlobalTecList.Clear ();
			}
		} else {
			StationPlane.SetActive (false);

			StationMSDButton.SetActive (false);
			StationOrdersButton.SetActive (false);
			StationSpecialButton.SetActive (false);
			StationBuildButton.SetActive (false);

			StationTradeButton.SetActive (false);
			StationResearchButton.SetActive (false);

			SH = null;
		}
		if (_GDB.activeObjectInterface == null) {
			StationMSDActive = true;
			StationOrdersActive = false;
			StationSpecialActive = false;
			StationBuildActive = false;
			StationTradeActive = false;
			StationResearchActive = false;
		}
	}

	public void MSDActive(){
		_sel.MSDActive = true;
		_sel.OrdersActive = false;
		_sel.SpecialActive = false;
		_sel.BuildActive = false;
	}
	public void OrdersActive(){
		_sel.MSDActive = false;
		_sel.OrdersActive = true;
		_sel.SpecialActive = false;
		_sel.BuildActive = false;
	}
	public void SpecialActive(){
		_sel.MSDActive = false;
		_sel.OrdersActive = false;
		_sel.SpecialActive = true;
		_sel.BuildActive = false;
	}
	public void BuildActive(){
		_sel.MSDActive = false;
		_sel.OrdersActive = false;
		_sel.SpecialActive = false;
		_sel.BuildActive = true;
	}

	public void SelfDestruction()
	{
		foreach (GameObject obj in _GDB.selectList)
		{
			HealthModule targetHealth = obj.GetComponent<HealthModule>();
			targetHealth.SelfDestructActive = !targetHealth.SelfDestructActive;
		}
	}
	public void AttackOrder()
	{
		_sel.SetOrder("Attack");
	}
	public void GuardOrder()
	{
		_sel.SetOrder("Guard");
	}
	public void PatrolOrder()
	{
		_sel.SetOrder("Patrol");
	}
	public void StopOrder()
	{
		_sel.SetOrder("FullStop");
	}
	public void FixOrder()
	{
	    _sel.SetOrder("Fix");
    }
	public void RecycleOrder()
	{
		foreach (GameObject obj in _GDB.selectList)
		{

		}
	}
	public void SearchOrder()
	{
		foreach (GameObject obj in _GDB.selectList)
		{

		}
	}
	public void FaDOrder()
	{
		foreach (GameObject obj in _GDB.selectList)
		{

		}
	}

	public void GreenAlert()
	{
		_sel.SetOrder("GreenAlert");
	}
	public void YellowAlert()
	{
		_sel.SetOrder("YellowAlert");
	}
	public void RedAlert()
	{
		_sel.SetOrder("RedAlert");
	}

	public void LifeSuportAttack()
	{
		_sel.SetOrder("LifeSupportAttack");
	}
	public void ImpulsAttack()
	{
		_sel.SetOrder("ImpulsAttack");
	}
	public void PrimaryWeaponAttack()
	{
		_sel.SetOrder("PrimaryWeaponAttack");
	}
	public void SensorAttack()
	{
		_sel.SetOrder("SensorAttack");
	}
	public void TractorAttack()
	{
		_sel.SetOrder("TractorAttack");
	}
	public void WarpEngingAttack()
	{
		_sel.SetOrder("WarpEngingAttack");
	}
	public void WarpCoreAttack()
	{
		_sel.SetOrder("WarpCoreAttack");
	}
	public void SecondaryWeaponAttack()
	{
		_sel.SetOrder("SecondaryWeaponAttack");
	}

	public void NormalAttack()
	{
		_sel.SetOrder("NormalAttack");
	}

	public void MineTitanium()
	{
		_GDB.selectList[0].GetComponent<Miner>().TargetDilithium = false;
		_GDB.selectList[0].GetComponent<Miner>().TargetTitanium = true;
		_GDB.selectList[0].GetComponent<Miner>().TargetHuman = false;
		_GDB.selectList[0].GetComponent<Miner>().enabled = true;
	}
	public void MineDilithium()
	{
		_GDB.selectList[0].GetComponent<Miner>().TargetDilithium = true;
		_GDB.selectList[0].GetComponent<Miner>().TargetTitanium = false;
		_GDB.selectList[0].GetComponent<Miner>().TargetHuman = false;
		_GDB.selectList[0].GetComponent<Miner>().enabled = true;
	}

	public void TradeTitanium()
	{
		_GDB.selectList[0].GetComponent<Transport>().Dilitium = false;
		_GDB.selectList[0].GetComponent<Transport>().Titanium = true;
		_GDB.selectList[0].GetComponent<Transport>().Humans = false;
	}
	public void TradeDilithium()
	{
		_GDB.selectList[0].GetComponent<Transport>().Dilitium = true;
		_GDB.selectList[0].GetComponent<Transport>().Titanium = false;
		_GDB.selectList[0].GetComponent<Transport>().Humans = false;
	}

	public void OneShipActiveSkill1(){
		if (OneShipSkillList.Count >= 1) {
			OneShipSkillList [0].Active ();
		}
	}
	public void OneShipActiveSkill2(){
		if (OneShipSkillList.Count >= 2) {
			OneShipSkillList [1].Active ();
		}
	}
	public void OneShipActiveSkill3(){
		if (OneShipSkillList.Count >= 3) {
			OneShipSkillList [2].Active ();
		}
	}
	public void OneShipActiveSkill4(){
		if (OneShipSkillList.Count >= 4) {
			OneShipSkillList [3].Active ();
		}
	}
	public void OneShipActiveSkill5(){
		if (OneShipSkillList.Count >= 5) {
			OneShipSkillList [4].Active ();
		}
	}
	public void OneShipActiveSkill6(){
		if (OneShipSkillList.Count >= 6) {
			OneShipSkillList [5].Active ();
		}
	}
	public void OneShipActiveSkill7(){
		if (OneShipSkillList.Count >= 7) {
			OneShipSkillList [6].Active ();
		}
	}
	public void OneShipActiveSkill8(){
		if (OneShipSkillList.Count >= 8) {
			OneShipSkillList [7].Active ();
		}
	}

	public void PassiveStation1Build(){
		OneShipPassiveBuildingsList [0].TryBuildStation ();
	}
	public void PassiveStation2Build(){
		OneShipPassiveBuildingsList [1].TryBuildStation ();
	}
	public void PassiveStation3Build(){
		OneShipPassiveBuildingsList [2].TryBuildStation ();
	}
	public void PassiveStation4Build(){
		OneShipPassiveBuildingsList [3].TryBuildStation ();
	}
	public void PassiveStation5Build(){
		OneShipPassiveBuildingsList [4].TryBuildStation ();
	}
	public void PassiveStation6Build(){
		OneShipPassiveBuildingsList [5].TryBuildStation ();
	}
	public void PassiveStation7Build(){
		OneShipPassiveBuildingsList [6].TryBuildStation ();
	}
	public void PassiveStation8Build(){
		OneShipPassiveBuildingsList [7].TryBuildStation ();
	}

	public void BuildingStation1Build(){
		OneShipBuildBuildingsList [0].TryBuildStation ();
	}
	public void BuildingStation2Build(){
		OneShipBuildBuildingsList [1].TryBuildStation ();
	}
	public void BuildingStation3Build(){
		OneShipBuildBuildingsList [2].TryBuildStation ();
	}
	public void BuildingStation4Build(){
		OneShipBuildBuildingsList [3].TryBuildStation ();
	}
	public void BuildingStation5Build(){
		OneShipBuildBuildingsList [4].TryBuildStation ();
	}
	public void BuildingStation6Build(){
		OneShipBuildBuildingsList [5].TryBuildStation ();
	}
	public void BuildingStation7Build(){
		OneShipBuildBuildingsList [6].TryBuildStation ();
	}
	public void BuildingStation8Build(){
		OneShipBuildBuildingsList [7].TryBuildStation ();
	}

	public void DefenceStation1Build(){
		OneShipDefenceBuildingsList [0].TryBuildStation ();
	}
	public void DefenceStation2Build(){
		OneShipDefenceBuildingsList [1].TryBuildStation ();
	}
	public void DefenceStation3Build(){
		OneShipDefenceBuildingsList [2].TryBuildStation ();
	}
	public void DefenceStation4Build(){
		OneShipDefenceBuildingsList [3].TryBuildStation ();
	}
	public void DefenceStation5Build(){
		OneShipDefenceBuildingsList [4].TryBuildStation ();
	}
	public void DefenceStation6Build(){
		OneShipDefenceBuildingsList [5].TryBuildStation ();
	}
	public void DefenceStation7Build(){
		OneShipDefenceBuildingsList [6].TryBuildStation ();
	}
	public void DefenceStation8Build(){
		OneShipDefenceBuildingsList [7].TryBuildStation ();
	}

	public void PassiveBuildingsActive(){
		_GDB.selectList [0].GetComponent<Stats> ().AttackStations = false;
		_GDB.selectList [0].GetComponent<Stats> ().DefenceStations = false;
		_GDB.selectList [0].GetComponent<Stats> ().NatralStations = true;
	}
	public void BuildBuildingsActive(){
		_GDB.selectList [0].GetComponent<Stats> ().AttackStations = true;
		_GDB.selectList [0].GetComponent<Stats> ().DefenceStations = false;
		_GDB.selectList [0].GetComponent<Stats> ().NatralStations = false;
	}
	public void DefenceBuildingsActive(){
		_GDB.selectList [0].GetComponent<Stats> ().AttackStations = false;
		_GDB.selectList [0].GetComponent<Stats> ().DefenceStations = true;
		_GDB.selectList [0].GetComponent<Stats> ().NatralStations = false;
	}
	public void GlobalBuildPlaneActive(){
		_GDB.selectList [0].GetComponent<Stats> ().AttackStations = false;
		_GDB.selectList [0].GetComponent<Stats> ().DefenceStations = false;
		_GDB.selectList [0].GetComponent<Stats> ().NatralStations = false;
	}

	public void Ship1Select(){
		_sel.SaveObj = _GDB.selectList [0];
		_GDB.selectList.Clear ();
		_GDB.selectList.Add (_sel.SaveObj);
		_GDB.selectList [0].GetComponent<Stats> ().Proector.GetComponent<MeshRenderer> ().enabled = true;
		_GDB.selectList [0].GetComponent<Stats> ().BoxSelected = true;
		_GDB.selectList [0].GetComponent<Stats> ().WasSelect = true;
		_GDB.selectList [0].GetComponent<Stats> ().isSelect = true;
	}
	public void Ship2Select(){
		_sel.SaveObj = _GDB.selectList [1];
		_GDB.selectList.Clear ();
		_GDB.selectList.Add (_sel.SaveObj);
		_GDB.selectList [0].GetComponent<Stats> ().Proector.GetComponent<MeshRenderer> ().enabled = true;
		_GDB.selectList [0].GetComponent<Stats> ().BoxSelected = true;
		_GDB.selectList [0].GetComponent<Stats> ().WasSelect = true;
		_GDB.selectList [0].GetComponent<Stats> ().isSelect = true;
	}
	public void Ship3Select(){
		_sel.SaveObj = _GDB.selectList [2];
		_GDB.selectList.Clear ();
		_GDB.selectList.Add (_sel.SaveObj);
		_GDB.selectList [0].GetComponent<Stats> ().Proector.GetComponent<MeshRenderer> ().enabled = true;
		_GDB.selectList [0].GetComponent<Stats> ().BoxSelected = true;
		_GDB.selectList [0].GetComponent<Stats> ().WasSelect = true;
		_GDB.selectList [0].GetComponent<Stats> ().isSelect = true;
	}
	public void Ship4Select(){
		_sel.SaveObj = _GDB.selectList [3];
		_GDB.selectList.Clear ();
		_GDB.selectList.Add (_sel.SaveObj);
		_GDB.selectList [0].GetComponent<Stats> ().Proector.GetComponent<MeshRenderer> ().enabled = true;
		_GDB.selectList [0].GetComponent<Stats> ().BoxSelected = true;
		_GDB.selectList [0].GetComponent<Stats> ().WasSelect = true;
		_GDB.selectList [0].GetComponent<Stats> ().isSelect = true;
	}
	public void Ship5Select(){
		_sel.SaveObj = _GDB.selectList [4];
		_GDB.selectList.Clear ();
		_GDB.selectList.Add (_sel.SaveObj);
		_GDB.selectList [0].GetComponent<Stats> ().Proector.GetComponent<MeshRenderer> ().enabled = true;
		_GDB.selectList [0].GetComponent<Stats> ().BoxSelected = true;
		_GDB.selectList [0].GetComponent<Stats> ().WasSelect = true;
		_GDB.selectList [0].GetComponent<Stats> ().isSelect = true;
	}
	public void Ship6Select(){
		_sel.SaveObj = _GDB.selectList [5];
		_GDB.selectList.Clear ();
		_GDB.selectList.Add (_sel.SaveObj);
		_GDB.selectList [0].GetComponent<Stats> ().Proector.GetComponent<MeshRenderer> ().enabled = true;
		_GDB.selectList [0].GetComponent<Stats> ().BoxSelected = true;
		_GDB.selectList [0].GetComponent<Stats> ().WasSelect = true;
		_GDB.selectList [0].GetComponent<Stats> ().isSelect = true;
	}
	public void Ship7Select(){
		_sel.SaveObj = _GDB.selectList [6];
		_GDB.selectList.Clear ();
		_GDB.selectList.Add (_sel.SaveObj);
		_GDB.selectList [0].GetComponent<Stats> ().Proector.GetComponent<MeshRenderer> ().enabled = true;
		_GDB.selectList [0].GetComponent<Stats> ().BoxSelected = true;
		_GDB.selectList [0].GetComponent<Stats> ().WasSelect = true;
		_GDB.selectList [0].GetComponent<Stats> ().isSelect = true;
	}
	public void Ship8Select(){
		_sel.SaveObj = _GDB.selectList [7];
		_GDB.selectList.Clear ();
		_GDB.selectList.Add (_sel.SaveObj);
		_GDB.selectList [0].GetComponent<Stats> ().Proector.GetComponent<MeshRenderer> ().enabled = true;
		_GDB.selectList [0].GetComponent<Stats> ().BoxSelected = true;
		_GDB.selectList [0].GetComponent<Stats> ().WasSelect = true;
		_GDB.selectList [0].GetComponent<Stats> ().isSelect = true;
	}
	public void Ship9Select(){
		_sel.SaveObj = _GDB.selectList [8];
		_GDB.selectList.Clear ();
		_GDB.selectList.Add (_sel.SaveObj);
		_GDB.selectList [0].GetComponent<Stats> ().Proector.GetComponent<MeshRenderer> ().enabled = true;
		_GDB.selectList [0].GetComponent<Stats> ().BoxSelected = true;
		_GDB.selectList [0].GetComponent<Stats> ().WasSelect = true;
		_GDB.selectList [0].GetComponent<Stats> ().isSelect = true;
	}
	public void Ship10Select(){
		_sel.SaveObj = _GDB.selectList [9];
		_GDB.selectList.Clear ();
		_GDB.selectList.Add (_sel.SaveObj);
		_GDB.selectList [0].GetComponent<Stats> ().Proector.GetComponent<MeshRenderer> ().enabled = true;
		_GDB.selectList [0].GetComponent<Stats> ().BoxSelected = true;
		_GDB.selectList [0].GetComponent<Stats> ().WasSelect = true;
		_GDB.selectList [0].GetComponent<Stats> ().isSelect = true;
	}
	public void Ship11Select(){
		_sel.SaveObj = _GDB.selectList [10];
		_GDB.selectList.Clear ();
		_GDB.selectList.Add (_sel.SaveObj);
		_GDB.selectList [0].GetComponent<Stats> ().Proector.GetComponent<MeshRenderer> ().enabled = true;
		_GDB.selectList [0].GetComponent<Stats> ().BoxSelected = true;
		_GDB.selectList [0].GetComponent<Stats> ().WasSelect = true;
		_GDB.selectList [0].GetComponent<Stats> ().isSelect = true;
	}
	public void Ship12Select(){
		_sel.SaveObj = _GDB.selectList [11];
		_GDB.selectList.Clear ();
		_GDB.selectList.Add (_sel.SaveObj);
		_GDB.selectList [0].GetComponent<Stats> ().Proector.GetComponent<MeshRenderer> ().enabled = true;
		_GDB.selectList [0].GetComponent<Stats> ().BoxSelected = true;
		_GDB.selectList [0].GetComponent<Stats> ().WasSelect = true;
		_GDB.selectList [0].GetComponent<Stats> ().isSelect = true;
	}

	bool FindInPassiveBuildingsList (BuildingInBuildShip obj)
	{
		foreach (BuildingInBuildShip selObj in OneShipPassiveBuildingsList) {
			if (selObj == obj)
				return true;
		}
		return false;
	}
	bool FindInBuildBuildingsList (BuildingInBuildShip obj)
	{
		foreach (BuildingInBuildShip selObj in OneShipBuildBuildingsList) {
			if (selObj == obj)
				return true;
		}
		return false;
	}
	bool FindInDefenceBuildingsList (BuildingInBuildShip obj)
	{
		foreach (BuildingInBuildShip selObj in OneShipDefenceBuildingsList) {
			if (selObj == obj)
				return true;
		}
		return false;
	}

	bool FindInShipTecList (Research obj)
	{
		foreach (Research selObj in ShipTecList) {
			if (selObj == obj)
				return true;
		}
		return false;
	}
	bool FindInStationTecList (Research obj)
	{
		foreach (Research selObj in StationTecList) {
			if (selObj == obj)
				return true;
		}
		return false;
	}
	bool FindInGlobalTecList (Research obj)
	{
		foreach (Research selObj in GlobalTecList) {
			if (selObj == obj)
				return true;
		}
		return false;
	}

	public void StationMSDActiveEvent(){
		StationMSDActive = true;
		StationOrdersActive = false;
		StationSpecialActive = false;
		StationBuildActive = false;
		StationTradeActive = false;
		StationResearchActive = false;
	}
	public void StationOrdersActiveEvent(){
		StationMSDActive = false;
		StationOrdersActive = true;
		StationSpecialActive = false;
		StationBuildActive = false;
		StationTradeActive = false;
		StationResearchActive = false;
	}
	public void StationSpecialActiveEvent(){
		StationMSDActive = false;
		StationOrdersActive = false;
		StationSpecialActive = true;
		StationBuildActive = false;
		StationTradeActive = false;
		StationResearchActive = false;
	}
	public void StationBuildActiveEvent(){
		StationMSDActive = false;
		StationOrdersActive = false;
		StationSpecialActive = false;
		StationBuildActive = true;
		StationTradeActive = false;
		StationResearchActive = false;
	}
	public void StationTradeActiveEvent(){
		StationMSDActive = false;
		StationOrdersActive = false;
		StationSpecialActive = false;
		StationBuildActive = false;
		StationTradeActive = true;
		StationResearchActive = false;
	}
	public void StationResearchActiveEvent(){
		StationMSDActive = false;
		StationOrdersActive = false;
		StationSpecialActive = false;
		StationBuildActive = false;
		StationTradeActive = false;
		StationResearchActive = true;
	}

	public void SellStation(){
		_GDB.obj.Remove (_GDB.activeObjectInterface);
		Destroy (_GDB.activeObjectInterface);
	}
	public void DestroyStation(){
		SH.curHealth = 0;
	}
	public void BuyTitanium(){
		if (_GDB.Dilithium >= 120) {
			_GDB.Titanium += 100;
			_GDB.Dilithium -= 120;
		}
	}
	public void BuyDilithium(){
		if (_GDB.Titanium >= 120) {
			_GDB.Dilithium += 100;
			_GDB.Titanium -= 120;
		}
	}
	public void SetFlag(){
		if (_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ()) {
			_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ().isFlag = true;
		}
	}

	public void BuildShip1(){
		if (_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ()) {
			_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ().BuildStarShip(1);
		}
	}
	public void BuildShip2(){
		if (_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ()) {
			_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ().BuildStarShip(2);
		}
	}
	public void BuildShip3(){
		if (_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ()) {
			_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ().BuildStarShip(3);
		}
	}
	public void BuildShip4(){
		if (_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ()) {
			_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ().BuildStarShip(4);
		}
	}
	public void BuildShip5(){
		if (_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ()) {
			_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ().BuildStarShip(5);
		}
	}
	public void BuildShip6(){
		if (_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ()) {
			_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ().BuildStarShip(6);
		}
	}

	public void CansledShip1(){
		if (_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ()) {
			_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ().CansledShip(1);
		}
	}
	public void CansledShip2(){
		if (_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ()) {
			_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ().CansledShip(2);
		}
	}
	public void CansledShip3(){
		if (_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ()) {
			_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ().CansledShip(3);
		}
	}
	public void CansledShip4(){
		if (_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ()) {
			_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ().CansledShip(4);
		}
	}
	public void CansledShip5(){
		if (_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ()) {
			_GDB.activeObjectInterface.GetComponent<ShipBuildModule> ().CansledShip(5);
		}
	}
	public void UPStation(){
		if (_GDB.activeObjectInterface.GetComponent<UpgradeModule> ()) {
			_GDB.activeObjectInterface.GetComponent<UpgradeModule> ().Upgrade ();
		}
	}

	public void GlobalSciPlaneActive(){
		_GDB.activeObjectInterface.GetComponent<Magic> ().ShipTecActive = false;
		_GDB.activeObjectInterface.GetComponent<Magic> ().StationTecActive = false;
		_GDB.activeObjectInterface.GetComponent<Magic> ().GlobalTecActive = false;
	}
	public void ShipTecPlaneActive(){
		_GDB.activeObjectInterface.GetComponent<Magic> ().ShipTecActive = true;
		_GDB.activeObjectInterface.GetComponent<Magic> ().StationTecActive = false;
		_GDB.activeObjectInterface.GetComponent<Magic> ().GlobalTecActive = false;
	}
	public void StationTecPlaneActive(){
		_GDB.activeObjectInterface.GetComponent<Magic> ().ShipTecActive = false;
		_GDB.activeObjectInterface.GetComponent<Magic> ().StationTecActive = true;
		_GDB.activeObjectInterface.GetComponent<Magic> ().GlobalTecActive = false;
	}
	public void GlobalTecPlaneActive(){
		_GDB.activeObjectInterface.GetComponent<Magic> ().ShipTecActive = false;
		_GDB.activeObjectInterface.GetComponent<Magic> ().StationTecActive = false;
		_GDB.activeObjectInterface.GetComponent<Magic> ().GlobalTecActive = true;
	}

	public void ShipTecActive1(){
		ShipTecList [0].SciActive ();
	}
	public void ShipTecActive2(){
		ShipTecList [1].SciActive ();
	}
	public void ShipTecActive3(){
		ShipTecList [2].SciActive ();
	}
	public void ShipTecActive4(){
		ShipTecList [3].SciActive ();
	}
	public void ShipTecActive5(){
		ShipTecList [4].SciActive ();
	}
	public void ShipTecActive6(){
		ShipTecList [5].SciActive ();
	}
	public void ShipTecActive7(){
		ShipTecList [6].SciActive ();
	}
	public void ShipTecActive8(){
		ShipTecList [7].SciActive ();
	}

	public void StationTecActive1(){
		StationTecList [0].SciActive ();
	}
	public void StationTecActive2(){
		StationTecList [1].SciActive ();
	}
	public void StationTecActive3(){
		StationTecList [2].SciActive ();
	}
	public void StationTecActive4(){
		StationTecList [3].SciActive ();
	}
	public void StationTecActive5(){
		StationTecList [4].SciActive ();
	}
	public void StationTecActive6(){
		StationTecList [5].SciActive ();
	}
	public void StationTecActive7(){
		StationTecList [6].SciActive ();
	}
	public void StationTecActive8(){
		StationTecList [7].SciActive ();
	}

	public void GlobalTecActive1(){
		GlobalTecList [0].SciActive ();
	}
	public void GlobalTecActive2(){
		GlobalTecList [1].SciActive ();
	}
	public void GlobalTecActive3(){
		GlobalTecList [2].SciActive ();
	}
	public void GlobalTecActive4(){
		GlobalTecList [3].SciActive ();
	}
	public void GlobalTecActive5(){
		GlobalTecList [4].SciActive ();
	}
	public void GlobalTecActive6(){
		GlobalTecList [5].SciActive ();
	}
	public void GlobalTecActive7(){
		GlobalTecList [6].SciActive ();
	}
	public void GlobalTecActive8(){
		GlobalTecList [7].SciActive ();
	}

	public void CansledRes(){
		_GDB.activeObjectInterface.GetComponent<Magic> ().CurRes.SciCansled ();
	}

	public void CanselConstruction(){
		_GDB.activeObjectInterface.GetComponent<BuildingStationScript> ().CancelBuilding ();
	}
}