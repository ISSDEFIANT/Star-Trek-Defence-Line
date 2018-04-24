using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipUIControll : MonoBehaviour
{
	public bool BlockImage;
	public bool BlockText;
	public bool BlockToggle;

	public GameObject Ship;

	public bool ShipIcon;

	public bool ResourcesBar;

	public bool ShildBar;
	public bool HealthBar;
	public bool EnergyBar;

	public bool Blueprint;

	public bool ShipName;
	public bool ShipClass;
	public bool Operator;
	public bool ResourcesCount;

	public bool CrewIcon;
	public bool CrewCount;

	public bool PrimaryWeaponIcon;
	public bool PrimaryWeaponBar;

	public bool SecondaryWeaponIcon;
	public bool SecondaryWeaponBar;

	public bool ImpulsIcon;
	public bool ImpulsBar;

	public bool WarpEngIcon;
	public bool WarpEngBar;

	public bool WarpCoreIcon;
	public bool WarpCoreBar;

	public bool LifeSupportIcon;
	public bool LifeSupportBar;

	public bool SensorIcon;
	public bool SensorBar;

	public bool TractorIcon;
	public bool TractorBar;

	private Select _sel;
	private GlobalDB _GDB;
	private GlobalLockingSystem _GLS;

	private Stats _st;
	private ActiveState _as;
	private HealthModule _h;
	private Miner _m;

	private Image TargetImage;
	private Text TargetText;
	private Toggle TargetToggle;

	private float ColorChangeTime;
	private Color32 DeactiveSystemColor;

	public bool SystemAttackIcon;
	public Sprite HullAttack;
	public Sprite PrimaryAttack;
	public Sprite SecondaryAttack;
	public Sprite ImpulsAttack;
	public Sprite WarpEAttack;
	public Sprite WarpCAttack;
	public Sprite LifeAttack;
	public Sprite SensorEAttack;
	public Sprite TractorAttack;

	public Sprite OrderActive;
	public Sprite OrderDeActive;

	public bool RedAlertButton;

	public bool YellowAlertButton;

	public bool GreenAlertButton;

	public bool SelfDestructionButton;
	// Use this for initialization
	void Start()
	{
		GameObject MainUI = GameObject.FindGameObjectWithTag("MainUI");

		_sel = MainUI.GetComponent<Select>();
		_GDB = MainUI.GetComponent<GlobalDB>();
		_GLS = MainUI.GetComponent<GlobalLockingSystem>();


		if (gameObject.GetComponent<Text>())
		{
			TargetText = gameObject.GetComponent<Text>();
		}
		if (gameObject.GetComponent<Image>())
		{
			TargetImage = gameObject.GetComponent<Image>();
		}
		if (gameObject.GetComponent<Toggle>())
		{
			TargetToggle = gameObject.GetComponent<Toggle>();
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Ship != null)
		{
			if (ColorChangeTime <= 0)
			{
				if (DeactiveSystemColor.ToString() == new Color32(255, 174, 0, 255).ToString())
				{
					DeactiveSystemColor = new Color32(255, 255, 255, 255);
				}
				else
				{
					DeactiveSystemColor = new Color32(255, 174, 0, 255);
				}
				ColorChangeTime = 1;
			}
			else
			{
				ColorChangeTime -= Time.deltaTime;
			}

			_st = Ship.GetComponent<Stats>();
			_as = Ship.GetComponent<ActiveState>();
			_h = Ship.GetComponent<HealthModule>();
			if (Ship.GetComponent<Miner>())
			{
				_m = Ship.GetComponent<Miner>();
			}
			else
			{
				_m = null;
			}

			if (ShipIcon)
			{
				TargetImage.sprite = _st.icon;
			}
			if (ShipName)
			{
				TargetText.text = _st.Name;
			}
			if (ShipClass)
			{
				TargetText.text = _st.nameShip;
			}
			if (Operator)
			{
				if (!_st.AI && !_st.FreandAI && !_st.Neutral && !_st.NeutralAgrass)
				{
					TargetText.text = "Operator: " + _GDB.PlayerName;
				}
				if (_st.AI || _st.FreandAI)
				{
					TargetText.text = "Operator: " + _st.Owner.name;
				}
				if (_st.Neutral)
				{
					TargetText.text = "Operator: Neutral";
				}
			}
			if (Blueprint)
			{
				TargetImage.sprite = _st.ShipBluePrint;
			}
			if (ShildBar)
			{
				if (_h.CurСилаПоля > 0)
				{
					TargetImage.fillAmount = _h.CurСилаПоля / _h.СилаПоля;
				}
				else
				{
					TargetImage.fillAmount = 0;
				}
			}
			if (HealthBar)
			{
				if (_h.curHealth > 0)
				{
					TargetImage.fillAmount = _h.curHealth / _h.maxHealth;
				}
				else
				{
					TargetImage.fillAmount = 0;
				}
			}
			if (EnergyBar)
			{
				if (_h.curEnergy > 0)
				{
					TargetImage.fillAmount = _h.curEnergy / _h.maxEnergy;
				}
				else
				{
					TargetImage.fillAmount = 0;
				}
			}
			if (ResourcesBar)
			{
				if (_m != null)
				{
					if (_m.curAs > 0)
					{
						TargetImage.fillAmount = _m.curAs / _m.maxAs;
					}
					else
					{
						TargetImage.fillAmount = 0;
					}
				}
				else
				{
					TargetImage.fillAmount = 0;
				}
			}
			if (ResourcesCount)
			{
				if (_m != null)
				{
					TargetText.text = "Resources " + (int)_m.curAs + ":" + _m.maxAs;
				}
				else
				{
					TargetText.text = string.Empty;
				}
			}
			if (CrewIcon)
			{
				if (_h.maxCrew > 0)
				{
					if (_h.curCrew > _h.maxCrew / 2)
					{
						TargetImage.color = Color.green;
					}
					if (_h.curCrew < _h.maxCrew / 2 && _h.curCrew > _h.maxCrew / 4)
					{
						TargetImage.color = Color.yellow;
					}
					if (_h.curCrew < _h.maxCrew / 4 && _h.curCrew > 0)
					{
						TargetImage.color = Color.red;
					}
					if (_h.curCrew <= 0)
					{
						TargetImage.color = Color.black;
					}
				}
				else
				{
					TargetImage.color = Color.green;
				}
			}
			if (CrewCount)
			{
				if (_h.maxCrew > 0)
				{
					TargetText.text = ": " + (int)_h.curCrew;
				}
				else
				{
					TargetText.text = ": 0";
				}
			}
			if (PrimaryWeaponBar)
			{
				if (_h.maxPrimaryWeaponSystemHealth > 0)
				{
					TargetImage.fillAmount = _h.curPrimaryWeaponSystemHealth / _h.maxPrimaryWeaponSystemHealth;
					if (!_h.ActivePrimaryWeapon)
					{
						if (_h.curPrimaryWeaponSystemHealth > _h.maxPrimaryWeaponSystemHealth / 2)
						{
							TargetImage.color = Color.green;
						}
						if (_h.curPrimaryWeaponSystemHealth < _h.maxPrimaryWeaponSystemHealth / 2 && _h.curPrimaryWeaponSystemHealth > _h.maxPrimaryWeaponSystemHealth / 4)
						{
							TargetImage.color = Color.yellow;
						}
						if (_h.curPrimaryWeaponSystemHealth < _h.maxPrimaryWeaponSystemHealth / 4 && _h.curPrimaryWeaponSystemHealth > _h.maxPrimaryWeaponSystemHealth / 8)
						{
							TargetImage.color = new Color32(255, 174, 0, 255);
						}
					}
				}
				else
				{
					TargetImage.color = Color.grey;
					TargetImage.fillAmount = 1;
				}
			}
			if (PrimaryWeaponIcon)
			{
				if (_h.maxPrimaryWeaponSystemHealth > 0)
				{
					if (_h.ActivePrimaryWeapon)
					{
						TargetImage.color = DeactiveSystemColor;
					}
					else if (_h.curPrimaryWeaponSystemHealth > _h.maxPrimaryWeaponSystemHealth / 2)
					{
						TargetImage.color = Color.green;
					}
					if (_h.curPrimaryWeaponSystemHealth < _h.maxPrimaryWeaponSystemHealth / 2 && _h.curPrimaryWeaponSystemHealth > _h.maxPrimaryWeaponSystemHealth / 4)
					{
						TargetImage.color = Color.yellow;
					}
					if (_h.curPrimaryWeaponSystemHealth < _h.maxPrimaryWeaponSystemHealth / 4 && _h.curPrimaryWeaponSystemHealth > _h.maxPrimaryWeaponSystemHealth / 8)
					{
						TargetImage.color = new Color32(255, 174, 0, 255);
					}
					if (_h.curPrimaryWeaponSystemHealth < _h.maxPrimaryWeaponSystemHealth / 8)
					{
						TargetImage.color = Color.red;
					}
					if (_h.curPrimaryWeaponSystemHealth <= 0)
					{
						TargetImage.color = Color.grey;
					}
				}
				else
				{
					TargetImage.color = Color.grey;
				}
			}
			if (SecondaryWeaponBar)
			{
				if (_h.maxSecondaryWeaponSystemHealth > 0)
				{
					TargetImage.fillAmount = _h.curSecondaryWeaponSystemHealth / _h.maxSecondaryWeaponSystemHealth;
					if (!_h.ActiveSecondaryWeapon)
					{
						if (_h.curSecondaryWeaponSystemHealth > _h.maxSecondaryWeaponSystemHealth / 2)
						{
							TargetImage.color = Color.green;
						}
						if (_h.curSecondaryWeaponSystemHealth < _h.maxSecondaryWeaponSystemHealth / 2 && _h.curSecondaryWeaponSystemHealth > _h.maxSecondaryWeaponSystemHealth / 4)
						{
							TargetImage.color = Color.yellow;
						}
						if (_h.curSecondaryWeaponSystemHealth < _h.maxSecondaryWeaponSystemHealth / 4 && _h.curSecondaryWeaponSystemHealth > _h.maxSecondaryWeaponSystemHealth / 8)
						{
							TargetImage.color = new Color32(255, 174, 0, 255);
						}
					}
				}
				else
				{
					TargetImage.color = Color.grey;
					TargetImage.fillAmount = 1;
				}
			}
			if (SecondaryWeaponIcon)
			{
				if (_h.maxSecondaryWeaponSystemHealth > 0)
				{
					if (_h.ActiveSecondaryWeapon)
					{
						TargetImage.color = DeactiveSystemColor;
					}
					else
					{
						if (_h.curSecondaryWeaponSystemHealth > _h.maxSecondaryWeaponSystemHealth / 2)
						{
							TargetImage.color = Color.green;
						}
						if (_h.curSecondaryWeaponSystemHealth < _h.maxSecondaryWeaponSystemHealth / 2 && _h.curSecondaryWeaponSystemHealth > _h.maxSecondaryWeaponSystemHealth / 4)
						{
							TargetImage.color = Color.yellow;
						}
						if (_h.curSecondaryWeaponSystemHealth < _h.maxSecondaryWeaponSystemHealth / 4 && _h.curSecondaryWeaponSystemHealth > _h.maxSecondaryWeaponSystemHealth / 8)
						{
							TargetImage.color = new Color32(255, 174, 0, 255);
						}
						if (_h.curSecondaryWeaponSystemHealth < _h.maxSecondaryWeaponSystemHealth / 8)
						{
							TargetImage.color = Color.red;
						}
						if (_h.curSecondaryWeaponSystemHealth <= 0)
						{
							TargetImage.color = Color.grey;
						}
					}
				}
				else
				{
					TargetImage.color = Color.grey;
				}
			}
			if (ImpulsBar)
			{
				if (_h.maxImpulseSystemHealth > 0)
				{
					TargetImage.fillAmount = _h.curImpulseSystemHealth / _h.maxImpulseSystemHealth;
					if (!_h.ActiveImpulse)
					{
						if (_h.curImpulseSystemHealth > _h.maxImpulseSystemHealth / 2)
						{
							TargetImage.color = Color.green;
						}
						if (_h.curImpulseSystemHealth < _h.maxImpulseSystemHealth / 2 && _h.curImpulseSystemHealth > _h.maxImpulseSystemHealth / 4)
						{
							TargetImage.color = Color.yellow;
						}
						if (_h.curImpulseSystemHealth < _h.maxImpulseSystemHealth / 4 && _h.curImpulseSystemHealth > _h.maxImpulseSystemHealth / 8)
						{
							TargetImage.color = new Color32(255, 174, 0, 255);
						}
					}
				}
				else
				{
					TargetImage.color = Color.grey;
					TargetImage.fillAmount = 1;
				}
			}
			if (ImpulsIcon)
			{
				if (_h.maxImpulseSystemHealth > 0)
				{
					if (_h.ActiveImpulse)
					{
						TargetImage.color = DeactiveSystemColor;
					}
					else
					{
						if (_h.curImpulseSystemHealth > _h.maxImpulseSystemHealth / 2)
						{
							TargetImage.color = Color.green;
						}
						if (_h.curImpulseSystemHealth < _h.maxImpulseSystemHealth / 2 && _h.curImpulseSystemHealth > _h.maxImpulseSystemHealth / 4)
						{
							TargetImage.color = Color.yellow;
						}
						if (_h.curImpulseSystemHealth < _h.maxImpulseSystemHealth / 4 && _h.curImpulseSystemHealth > _h.maxImpulseSystemHealth / 8)
						{
							TargetImage.color = new Color32(255, 174, 0, 255);
						}
						if (_h.curImpulseSystemHealth < _h.maxImpulseSystemHealth / 8)
						{
							TargetImage.color = Color.red;
						}
						if (_h.curImpulseSystemHealth <= 0)
						{
							TargetImage.color = Color.grey;
						}
					}
				}
				else
				{
					TargetImage.color = Color.grey;
				}
			}

			if (WarpEngBar)
			{
				if (_h.maxWarpEngingSystemHealth > 0)
				{
					TargetImage.fillAmount = _h.curWarpEngingSystemHealth / _h.maxWarpEngingSystemHealth;
					if (!_h.ActiveWarpEnging)
					{
						if (_h.curWarpEngingSystemHealth > _h.maxWarpEngingSystemHealth / 2)
						{
							TargetImage.color = Color.green;
						}
						if (_h.curWarpEngingSystemHealth < _h.maxWarpEngingSystemHealth / 2 && _h.curWarpEngingSystemHealth > _h.maxWarpEngingSystemHealth / 4)
						{
							TargetImage.color = Color.yellow;
						}
						if (_h.curWarpEngingSystemHealth < _h.maxWarpEngingSystemHealth / 4 && _h.curWarpEngingSystemHealth > _h.maxWarpEngingSystemHealth / 8)
						{
							TargetImage.color = new Color32(255, 174, 0, 255);
						}
					}
				}
				else
				{
					TargetImage.color = Color.grey;
					TargetImage.fillAmount = 1;
				}
			}
			if (WarpEngIcon)
			{
				if (_h.maxWarpEngingSystemHealth > 0)
				{
					if (_h.ActiveWarpEnging)
					{
						TargetImage.color = DeactiveSystemColor;
					}
					else
					{
						if (_h.curWarpEngingSystemHealth > _h.maxWarpEngingSystemHealth / 2)
						{
							TargetImage.color = Color.green;
						}
						if (_h.curWarpEngingSystemHealth < _h.maxWarpEngingSystemHealth / 2 && _h.curWarpEngingSystemHealth > _h.maxWarpEngingSystemHealth / 4)
						{
							TargetImage.color = Color.yellow;
						}
						if (_h.curWarpEngingSystemHealth < _h.maxWarpEngingSystemHealth / 4 && _h.curWarpEngingSystemHealth > _h.maxWarpEngingSystemHealth / 8)
						{
							TargetImage.color = new Color32(255, 174, 0, 255);
						}
						if (_h.curWarpEngingSystemHealth < _h.maxWarpEngingSystemHealth / 8)
						{
							TargetImage.color = Color.red;
						}
						if (_h.curWarpEngingSystemHealth <= 0)
						{
							TargetImage.color = Color.grey;
						}
					}
				}
				else
				{
					TargetImage.color = Color.grey;
				}
			}
			if (WarpCoreBar)
			{
				if (_h.maxWarpCoreHealth > 0)
				{
					TargetImage.fillAmount = _h.curWarpCoreHealth / _h.maxWarpCoreHealth;
					if (!_h.ActiveWarpCore)
					{
						if (_h.curWarpCoreHealth > _h.maxWarpCoreHealth / 2)
						{
							TargetImage.color = Color.green;
						}
						if (_h.curWarpCoreHealth < _h.maxWarpCoreHealth / 2 && _h.curWarpCoreHealth > _h.maxWarpCoreHealth / 4)
						{
							TargetImage.color = Color.yellow;
						}
						if (_h.curWarpCoreHealth < _h.maxWarpCoreHealth / 4 && _h.curWarpCoreHealth > _h.maxWarpCoreHealth / 8)
						{
							TargetImage.color = new Color32(255, 174, 0, 255);
						}
					}
				}
				else
				{
					TargetImage.color = Color.grey;
					TargetImage.fillAmount = 1;
				}
			}
			if (WarpCoreIcon)
			{
				if (_h.maxWarpCoreHealth > 0)
				{
					if (_h.ActiveWarpCore)
					{
						TargetImage.color = DeactiveSystemColor;
					}
					else
					{
						if (_h.curWarpCoreHealth > _h.maxWarpCoreHealth / 2)
						{
							TargetImage.color = Color.green;
						}
						if (_h.curWarpCoreHealth < _h.maxWarpCoreHealth / 2 && _h.curWarpCoreHealth > _h.maxWarpCoreHealth / 4)
						{
							TargetImage.color = Color.yellow;
						}
						if (_h.curWarpCoreHealth < _h.maxWarpCoreHealth / 4 && _h.curWarpCoreHealth > _h.maxWarpCoreHealth / 8)
						{
							TargetImage.color = new Color32(255, 174, 0, 255);
						}
						if (_h.curWarpCoreHealth < _h.maxWarpCoreHealth / 8)
						{
							TargetImage.color = Color.red;
						}
						if (_h.curWarpCoreHealth <= 0)
						{
							TargetImage.color = Color.grey;
						}
					}
				}
				else
				{
					TargetImage.color = Color.grey;
				}
			}
			if (LifeSupportBar)
			{
				if (_h.maxLifeSupportSystemHealth > 0)
				{
					TargetImage.fillAmount = _h.curLifeSupportSystemHealth / _h.maxLifeSupportSystemHealth;
					if (!_h.ActiveLifeSupport)
					{
						if (_h.curLifeSupportSystemHealth > _h.maxLifeSupportSystemHealth / 2)
						{
							TargetImage.color = Color.green;
						}
						if (_h.curLifeSupportSystemHealth < _h.maxLifeSupportSystemHealth / 2 && _h.curLifeSupportSystemHealth > _h.maxLifeSupportSystemHealth / 4)
						{
							TargetImage.color = Color.yellow;
						}
						if (_h.curLifeSupportSystemHealth < _h.maxLifeSupportSystemHealth / 4 && _h.curLifeSupportSystemHealth > _h.maxLifeSupportSystemHealth / 8)
						{
							TargetImage.color = new Color32(255, 174, 0, 255);
						}
					}
				}
				else
				{
					TargetImage.color = Color.grey;
					TargetImage.fillAmount = 1;
				}
			}
			if (LifeSupportIcon)
			{
				if (_h.maxLifeSupportSystemHealth > 0)
				{
					if (_h.ActiveLifeSupport)
					{
						TargetImage.color = DeactiveSystemColor;
					}
					else
					{
						if (_h.curLifeSupportSystemHealth > _h.maxLifeSupportSystemHealth / 2)
						{
							TargetImage.color = Color.green;
						}
						if (_h.curLifeSupportSystemHealth < _h.maxLifeSupportSystemHealth / 2 && _h.curLifeSupportSystemHealth > _h.maxLifeSupportSystemHealth / 4)
						{
							TargetImage.color = Color.yellow;
						}
						if (_h.curLifeSupportSystemHealth < _h.maxLifeSupportSystemHealth / 4 && _h.curLifeSupportSystemHealth > _h.maxLifeSupportSystemHealth / 8)
						{
							TargetImage.color = new Color32(255, 174, 0, 255);
						}
						if (_h.curLifeSupportSystemHealth < _h.maxLifeSupportSystemHealth / 8)
						{
							TargetImage.color = Color.red;
						}
						if (_h.curLifeSupportSystemHealth <= 0)
						{
							TargetImage.color = Color.grey;
						}
					}
				}
				else
				{
					TargetImage.color = Color.grey;
				}
			}
			if (SensorBar)
			{
				if (_h.maxSensorsSystemHealth > 0)
				{
					TargetImage.fillAmount = _h.curSensorsSystemHealth / _h.maxSensorsSystemHealth;
					if (!_h.ActiveSensors)
					{
						if (_h.curSensorsSystemHealth > _h.maxSensorsSystemHealth / 2)
						{
							TargetImage.color = Color.green;
						}
						if (_h.curSensorsSystemHealth < _h.maxSensorsSystemHealth / 2 && _h.curSensorsSystemHealth > _h.maxSensorsSystemHealth / 4)
						{
							TargetImage.color = Color.yellow;
						}
						if (_h.curSensorsSystemHealth < _h.maxSensorsSystemHealth / 4 && _h.curSensorsSystemHealth > _h.maxSensorsSystemHealth / 8)
						{
							TargetImage.color = new Color32(255, 174, 0, 255);
						}
					}
				}
				else
				{
					TargetImage.color = Color.grey;
					TargetImage.fillAmount = 1;
				}
			}
			if (SensorIcon)
			{
				if (_h.maxSensorsSystemHealth > 0)
				{
					if (_h.ActiveSensors)
					{
						TargetImage.color = DeactiveSystemColor;
					}
					else
					{
						if (_h.curSensorsSystemHealth > _h.maxSensorsSystemHealth / 2)
						{
							TargetImage.color = Color.green;
						}
						if (_h.curSensorsSystemHealth < _h.maxSensorsSystemHealth / 2 && _h.curSensorsSystemHealth > _h.maxSensorsSystemHealth / 4)
						{
							TargetImage.color = Color.yellow;
						}
						if (_h.curSensorsSystemHealth < _h.maxSensorsSystemHealth / 4 && _h.curSensorsSystemHealth > _h.maxSensorsSystemHealth / 8)
						{
							TargetImage.color = new Color32(255, 174, 0, 255);
						}
						if (_h.curSensorsSystemHealth < _h.maxSensorsSystemHealth / 8)
						{
							TargetImage.color = Color.red;
						}
						if (_h.curSensorsSystemHealth <= 0)
						{
							TargetImage.color = Color.grey;
						}
					}
				}
				else
				{
					TargetImage.color = Color.grey;
				}
			}
			if (TractorBar)
			{
				if (_h.maxTractorBeamSystemHealth > 0)
				{
					TargetImage.fillAmount = _h.curTractorBeamSystemHealth / _h.maxTractorBeamSystemHealth;
					if (!_h.ActiveTractor)
					{
						if (_h.curTractorBeamSystemHealth > _h.maxTractorBeamSystemHealth / 2)
						{
							TargetImage.color = Color.green;
						}
						if (_h.curTractorBeamSystemHealth < _h.maxTractorBeamSystemHealth / 2 && _h.curTractorBeamSystemHealth > _h.maxTractorBeamSystemHealth / 4)
						{
							TargetImage.color = Color.yellow;
						}
						if (_h.curTractorBeamSystemHealth < _h.maxTractorBeamSystemHealth / 4 && _h.curTractorBeamSystemHealth > _h.maxTractorBeamSystemHealth / 8)
						{
							TargetImage.color = new Color32(255, 174, 0, 255);
						}
					}
				}
				else
				{
					TargetImage.color = Color.grey;
					TargetImage.fillAmount = 1;
				}
			}
			if (TractorIcon)
			{
				if (_h.maxTractorBeamSystemHealth > 0)
				{
					if (_h.ActiveTractor)
					{
						TargetImage.color = DeactiveSystemColor;
					}
					else
					{
						if (_h.curTractorBeamSystemHealth > _h.maxTractorBeamSystemHealth / 2)
						{
							TargetImage.color = Color.green;
						}
						if (_h.curTractorBeamSystemHealth < _h.maxTractorBeamSystemHealth / 2 && _h.curTractorBeamSystemHealth > _h.maxTractorBeamSystemHealth / 4)
						{
							TargetImage.color = Color.yellow;
						}
						if (_h.curTractorBeamSystemHealth < _h.maxTractorBeamSystemHealth / 4 && _h.curTractorBeamSystemHealth > _h.maxTractorBeamSystemHealth / 8)
						{
							TargetImage.color = new Color32(255, 174, 0, 255);
						}
						if (_h.curTractorBeamSystemHealth < _h.maxTractorBeamSystemHealth / 8)
						{
							TargetImage.color = Color.red;
						}
						if (_h.curTractorBeamSystemHealth <= 0)
						{
							TargetImage.color = Color.grey;
						}
					}
				}
				else
				{
					TargetImage.color = Color.grey;
				}
			}
			if (SystemAttackIcon)
			{
				if (!_as.WarpCoreAttack && !_as.ImpulseSystemAttack && !_as.SensorsSystemAttack && !_as.WarpEngingSystemAttack && !_as.TractorBeamSystemAttack && !_as.LifeSupportSystemAttack && !_as.PrimaryWeaponSystemAttack && !_as.SecondaryWeaponSystemAttack)
				{
					TargetImage.sprite = HullAttack;
				}
				else
				{
					if (_as.PrimaryWeaponSystemAttack)
					{
						TargetImage.sprite = PrimaryAttack;
					}
					if (_as.SecondaryWeaponSystemAttack)
					{
						TargetImage.sprite = SecondaryAttack;
					}
					if (_as.ImpulseSystemAttack)
					{
						TargetImage.sprite = ImpulsAttack;
					}
					if (_as.WarpEngingSystemAttack)
					{
						TargetImage.sprite = WarpEAttack;
					}
					if (_as.WarpCoreAttack)
					{
						TargetImage.sprite = WarpCAttack;
					}
					if (_as.LifeSupportSystemAttack)
					{
						TargetImage.sprite = LifeAttack;
					}
					if (_as.SensorsSystemAttack)
					{
						TargetImage.sprite = SensorEAttack;
					}
					if (_as.TractorBeamSystemAttack)
					{
						TargetImage.sprite = TractorAttack;
					}
				}
			}
			if (RedAlertButton)
			{
				if (_as.Agrass)
				{
					TargetImage.sprite = OrderActive;
				}
				else
				{
					TargetImage.sprite = OrderDeActive;
				}
			}
			if (YellowAlertButton)
			{
				if (_as.Protact)
				{
					TargetImage.sprite = OrderActive;
				}
				else
				{
					TargetImage.sprite = OrderDeActive;
				}
			}
			if (GreenAlertButton)
			{
				if (_as.Idle)
				{
					TargetImage.sprite = OrderActive;
				}
				else
				{
					TargetImage.sprite = OrderDeActive;
				}
			}
			if (SelfDestructionButton)
			{
				if (_h.SelfDestructActive)
				{
					TargetImage.sprite = OrderActive;
				}
				else
				{
					TargetImage.sprite = OrderDeActive;
				}
			}



			if (!BlockImage)
			{
				if (TargetImage != null)
				{
					if (TargetImage.enabled == false)
					{
						TargetImage.enabled = true;
					}
				}
			}
			if (!BlockText)
			{
				if (TargetText != null)
				{
					if (TargetText.enabled == false)
					{
						TargetText.enabled = true;
					}
				}
			}
			if (!BlockToggle)
			{
				if (TargetToggle != null)
				{
					if (TargetToggle.enabled == false)
					{
						TargetToggle.enabled = true;
					}
				}
			}
		}
		else
		{
			if (!BlockImage)
			{
				if (TargetImage != null)
				{
					if (TargetImage.enabled == true)
					{
						TargetImage.enabled = false;
					}
				}
			}
			if (!BlockText)
			{
				if (TargetText != null)
				{
					if (TargetText.enabled == true)
					{
						TargetText.enabled = false;
					}
				}
			}
			if (!BlockToggle)
			{
				if (TargetToggle != null)
				{
					if (TargetToggle.enabled == true)
					{
						TargetToggle.enabled = false;
					}
				}
			}
		}
	}
}