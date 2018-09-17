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

	public bool GuardOrder;
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
                SHERBar(_h.CurShilds, _h.Shilds);
            }
            if (HealthBar)
			{
			    SHERBar(_h.curHealth, _h.maxHealth);
            }
			if (EnergyBar)
			{
			    SHERBar(_h.curEnergy, _h.maxEnergy);
            }

		    if (ResourcesBar)
		    {
		        if (_m != null)
		        {
		            SHERBar(_m.curAs, _m.maxAs);
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
			    SystemBarControl(_h.maxPrimaryWeaponSystemHealth, _h.curPrimaryWeaponSystemHealth, _h.ActivePrimaryWeapon);
            }
			if (PrimaryWeaponIcon)
			{
			    SystemIconControl(_h.maxPrimaryWeaponSystemHealth, _h.curPrimaryWeaponSystemHealth, _h.ActivePrimaryWeapon);
            }
			if (SecondaryWeaponBar)
			{
			    SystemBarControl(_h.maxSecondaryWeaponSystemHealth, _h.curSecondaryWeaponSystemHealth, _h.ActiveSecondaryWeapon);
            }
			if (SecondaryWeaponIcon)
			{
			    SystemIconControl(_h.maxSecondaryWeaponSystemHealth, _h.curSecondaryWeaponSystemHealth, _h.ActiveSecondaryWeapon);
            }
			if (ImpulsBar)
			{
			    SystemBarControl(_h.maxImpulseSystemHealth, _h.curImpulseSystemHealth, _h.ActiveImpulse);
            }
			if (ImpulsIcon)
			{
			    SystemIconControl(_h.maxImpulseSystemHealth, _h.curImpulseSystemHealth, _h.ActiveImpulse);
            }

			if (WarpEngBar)
			{
			    SystemBarControl(_h.maxWarpEngingSystemHealth, _h.curWarpEngingSystemHealth, _h.ActiveWarpEnging);
            }
			if (WarpEngIcon)
			{
			    SystemIconControl(_h.maxWarpEngingSystemHealth, _h.curWarpEngingSystemHealth, _h.ActiveWarpEnging);
            }
			if (WarpCoreBar)
			{
			    SystemBarControl(_h.maxWarpCoreHealth, _h.curWarpCoreHealth, _h.ActiveWarpCore);
            }
			if (WarpCoreIcon)
			{
			    SystemIconControl(_h.maxWarpCoreHealth, _h.curWarpCoreHealth, _h.ActiveWarpCore);
            }
			if (LifeSupportBar)
			{
			    SystemBarControl(_h.maxLifeSupportSystemHealth, _h.curLifeSupportSystemHealth, _h.ActiveLifeSupport);
            }
			if (LifeSupportIcon)
			{
			    SystemIconControl(_h.maxLifeSupportSystemHealth, _h.curLifeSupportSystemHealth, _h.ActiveLifeSupport);
            }
			if (SensorBar)
			{
			    SystemBarControl(_h.maxSensorsSystemHealth, _h.curSensorsSystemHealth, _h.ActiveSensors);
			}
			if (SensorIcon)
			{
			    SystemIconControl(_h.maxSensorsSystemHealth, _h.curSensorsSystemHealth, _h.ActiveSensors);
            }
			if (TractorBar)
            {
                SystemBarControl(_h.maxTractorBeamSystemHealth, _h.curTractorBeamSystemHealth, _h.ActiveTractor);
            }
            if (TractorIcon)
            {
                SystemIconControl(_h.maxTractorBeamSystemHealth, _h.curTractorBeamSystemHealth, _h.ActiveTractor);
            }
            if (SystemAttackIcon)
			{
			    switch (_as.TargetingAt)
			    {
                    case ActiveState.AttackType.NormalAttack:
                        TargetImage.sprite = HullAttack;
                        break;
			        case ActiveState.AttackType.PrimaryWeaponSystemAttack:
			            TargetImage.sprite = PrimaryAttack;
                        break;
			        case ActiveState.AttackType.SecondaryWeaponSystemAttack:
			            TargetImage.sprite = SecondaryAttack;
                        break;
			        case ActiveState.AttackType.ImpulseSystemAttack:
			            TargetImage.sprite = ImpulsAttack;
                        break;
			        case ActiveState.AttackType.WarpEngingSystemAttack:
			            TargetImage.sprite = WarpEAttack;
                        break;
			        case ActiveState.AttackType.WarpCoreAttack:
			            TargetImage.sprite = WarpCAttack;
                        break;
			        case ActiveState.AttackType.LifeSupportSystemAttack:
			            TargetImage.sprite = LifeAttack;
                        break;
			        case ActiveState.AttackType.SensorsSystemAttack:
			            TargetImage.sprite = SensorEAttack;
                        break;
			        case ActiveState.AttackType.TractorBeamSystemAttack:
			            TargetImage.sprite = TractorAttack;
                        break;
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
			if (GuardOrder)
			{
				if (_st.GuartTarget != null)
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

    private void SHERBar(float CurVar, float MaxVar)
    {
        if (_h.CurShilds > 0)
        {
            TargetImage.fillAmount = CurVar / MaxVar;
        }
        else
        {
            TargetImage.fillAmount = 0;
        }
    }

    private void SystemBarControl(float maxSystemHealth, float curSystemHealth, bool DeActivationBool)
    {
        if (maxSystemHealth > 0)
        {
            TargetImage.fillAmount = curSystemHealth / maxSystemHealth;
            if (!DeActivationBool)
            {
                if (curSystemHealth > maxSystemHealth / 2)
                {
                    TargetImage.color = Color.green;
                }
                if (curSystemHealth < maxSystemHealth / 2 && curSystemHealth > maxSystemHealth / 4)
                {
                    TargetImage.color = Color.yellow;
                }
                if (curSystemHealth < maxSystemHealth / 4 && curSystemHealth > maxSystemHealth / 8)
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

    private void SystemIconControl(float maxSystemHealth, float curSystemHealth, bool DeActivationBool)
    {
        if (maxSystemHealth > 0)
        {
            if (DeActivationBool)
            {
                TargetImage.color = DeactiveSystemColor;
            }
            else
            {
                if (curSystemHealth > maxSystemHealth / 2)
                {
                    TargetImage.color = Color.green;
                }
                if (curSystemHealth < maxSystemHealth / 2 && curSystemHealth > maxSystemHealth / 4)
                {
                    TargetImage.color = Color.yellow;
                }
                if (curSystemHealth < maxSystemHealth / 4 && curSystemHealth > maxSystemHealth / 8)
                {
                    TargetImage.color = new Color32(255, 174, 0, 255);
                }
                if (curSystemHealth < maxSystemHealth / 8)
                {
                    TargetImage.color = Color.red;
                }
                if (curSystemHealth <= 0)
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
}