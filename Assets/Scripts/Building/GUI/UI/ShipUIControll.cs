using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipUIControll : MonoBehaviour
{
    public enum UIRenderType
    {
        None,
        ShipIcon,
        ResourcesBar,
        ShildBar,
        HealthBar,
        EnergyBar,
        Blueprint,
        ShipName,
        ShipClass,
        Operator,
        ResourcesCount,
        CrewIcon,
        CrewCount,
        PrimaryWeaponIcon,
        PrimaryWeaponBar,
        SecondaryWeaponIcon,
        SecondaryWeaponBar,
        ImpulsIcon,
        ImpulsBar,
        WarpEngIcon,
        WarpEngBar,
        WarpCoreIcon,
        WarpCoreBar,
        LifeSupportIcon,
        LifeSupportBar,
        SensorIcon,
        SensorBar,
        TractorIcon,
        TractorBar,
        SystemAttackIcon,
        RedAlertButton,
        YellowAlertButton,
        GreenAlertButton,
        SelfDestructionButton,
        GuardOrder,
        SelfDestructionBar,
        FixButton
    }
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

    public bool BlockImage;
	public bool BlockText;
	public bool BlockToggle;

	public GameObject Ship;

    public UIRenderType ShipUIRenderType;

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

		    switch (ShipUIRenderType)
		    {
                //MSD Menu

                case UIRenderType.ShipIcon:
                    TargetImage.sprite = _st.icon;
                    break;
		        case UIRenderType.ShipName:
		            TargetText.text = _st.Name;
                    break;
		        case UIRenderType.ShipClass:
		            TargetText.text = _st.nameShip;
                    break;

		        case UIRenderType.Operator:
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
                    break;

		        case UIRenderType.Blueprint:
		            TargetImage.sprite = _st.ShipBluePrint;
                    break;
		        case UIRenderType.ShildBar:
		            SHERBar(_h.CurShilds, _h.Shilds);
                    break;
		        case UIRenderType.HealthBar:
		            SHERBar(_h.curHealth, _h.maxHealth);
                    break;
		        case UIRenderType.EnergyBar:
		            SHERBar(_h.curEnergy, _h.maxEnergy);
                    break;

		        case UIRenderType.ResourcesBar:
		            if (_m != null)
		            {
		                SHERBar(_m.curAs, _m.maxAs);
		            }
		            else
		            {
		                TargetImage.fillAmount = 0;
		            }
                    break;

		        case UIRenderType.ResourcesCount:
		            if (_m != null)
		            {
		                TargetText.text = "Resources " + (int)_m.curAs + ":" + _m.maxAs;
		            }
		            else
		            {
		                TargetText.text = string.Empty;
		            }
                    break;

		        case UIRenderType.CrewIcon:
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
                    break;
		        case UIRenderType.CrewCount:
		            if (_h.maxCrew > 0)
		            {
		                TargetText.text = ": " + (int)_h.curCrew;
		            }
		            else
		            {
		                TargetText.text = ": 0";
		            }
                    break;

		        case UIRenderType.PrimaryWeaponBar:
		            SystemBarControl(_h.maxPrimaryWeaponSystemHealth, _h.curPrimaryWeaponSystemHealth, _h.ActivePrimaryWeapon);
                    break;
		        case UIRenderType.PrimaryWeaponIcon:
		            SystemIconControl(_h.maxPrimaryWeaponSystemHealth, _h.curPrimaryWeaponSystemHealth, _h.ActivePrimaryWeapon);
                    break;

		        case UIRenderType.SecondaryWeaponBar:
		            SystemBarControl(_h.maxSecondaryWeaponSystemHealth, _h.curSecondaryWeaponSystemHealth, _h.ActiveSecondaryWeapon);
                    break;
		        case UIRenderType.SecondaryWeaponIcon:
		            SystemIconControl(_h.maxSecondaryWeaponSystemHealth, _h.curSecondaryWeaponSystemHealth, _h.ActiveSecondaryWeapon);
                    break;

		        case UIRenderType.ImpulsBar:
		            SystemBarControl(_h.maxImpulseSystemHealth, _h.curImpulseSystemHealth, _h.ActiveImpulse);
                    break;
		        case UIRenderType.ImpulsIcon:
		            SystemIconControl(_h.maxImpulseSystemHealth, _h.curImpulseSystemHealth, _h.ActiveImpulse);
                    break;

		        case UIRenderType.WarpEngBar:
		            SystemBarControl(_h.maxWarpEngingSystemHealth, _h.curWarpEngingSystemHealth, _h.ActiveWarpEnging);
                    break;
		        case UIRenderType.WarpEngIcon  :
		            SystemIconControl(_h.maxWarpEngingSystemHealth, _h.curWarpEngingSystemHealth, _h.ActiveWarpEnging);
                    break;

		        case UIRenderType.WarpCoreBar:
		            SystemBarControl(_h.maxWarpCoreHealth, _h.curWarpCoreHealth, _h.ActiveWarpCore);
                    break;
		        case UIRenderType.WarpCoreIcon:
		            SystemIconControl(_h.maxWarpCoreHealth, _h.curWarpCoreHealth, _h.ActiveWarpCore);
                    break;

		        case UIRenderType.LifeSupportBar:
		            SystemBarControl(_h.maxLifeSupportSystemHealth, _h.curLifeSupportSystemHealth, _h.ActiveLifeSupport);
                    break;
		        case UIRenderType.LifeSupportIcon:
		            SystemIconControl(_h.maxLifeSupportSystemHealth, _h.curLifeSupportSystemHealth, _h.ActiveLifeSupport);
                    break;

		        case UIRenderType.SensorBar:
		            SystemBarControl(_h.maxSensorsSystemHealth, _h.curSensorsSystemHealth, _h.ActiveSensors);
                    break;
		        case UIRenderType.SensorIcon:
		            SystemIconControl(_h.maxSensorsSystemHealth, _h.curSensorsSystemHealth, _h.ActiveSensors);
                    break;

		        case UIRenderType.TractorBar:
		            SystemBarControl(_h.maxTractorBeamSystemHealth, _h.curTractorBeamSystemHealth, _h.ActiveTractor);
                    break;
		        case UIRenderType.TractorIcon:
		            SystemIconControl(_h.maxTractorBeamSystemHealth, _h.curTractorBeamSystemHealth, _h.ActiveTractor);
                    break;

                    //OrdersMenu

		        case UIRenderType.SystemAttackIcon:
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
                    break;

		        case UIRenderType.RedAlertButton:
		            if (_as.Agrass)
		            {
		                TargetImage.sprite = OrderActive;
		            }
		            else
		            {
		                TargetImage.sprite = OrderDeActive;
		            }
                    break;
		        case UIRenderType.YellowAlertButton:
		            if (_as.Protact)
		            {
		                TargetImage.sprite = OrderActive;
		            }
		            else
		            {
		                TargetImage.sprite = OrderDeActive;
		            }
                    break;
		        case UIRenderType.GreenAlertButton:
		            if (_as.Idle)
		            {
		                TargetImage.sprite = OrderActive;
		            }
		            else
		            {
		                TargetImage.sprite = OrderDeActive;
		            }
                    break;

		        case UIRenderType.SelfDestructionButton:
		            if (_h.SelfDestructActive)
		            {
		                TargetImage.sprite = OrderActive;
		            }
		            else
		            {
		                TargetImage.sprite = OrderDeActive;
		            }
                    break;
		        case UIRenderType.SelfDestructionBar:
		            if (_h.SelfDestructActive)
		            {
		                TargetImage.enabled = true;
                        SHERBar(_h.SelfDestructTimer, 5, true);
		            }
		            else
		            {
		                TargetImage.enabled = false;
                    }
		            break;
                case UIRenderType.GuardOrder:
		            if (_st.GuartTarget != null)
		            {
		                TargetImage.sprite = OrderActive;
		            }
		            else
		            {
		                TargetImage.sprite = OrderDeActive;
		            }
                    break;
		        case UIRenderType.FixButton:
		            if (_as.curShipYard != null)
		            {
		                TargetImage.sprite = OrderActive;
                    }
		            else
		            {
		                TargetImage.sprite = OrderDeActive;
                    }
		            break;
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

    private void SHERBar(float CurVar, float MaxVar, bool Revert = false)
    {
        if (!Revert)
        {
            if (CurVar > 0)
            {
                TargetImage.fillAmount = CurVar / MaxVar;
            }
            else
            {
                TargetImage.fillAmount = 0;
            }
        }
        else
        {
            if (CurVar > 0)
            {
                float tx = CurVar / MaxVar;
                TargetImage.fillAmount = 1-tx;
            }
            else
            {
                TargetImage.fillAmount = 1;
            }
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