using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
	public bool AI;
	public bool FreandAI;
	public bool NeutralAgrass;
	public bool Neutral;

	public bool visible = false;

	public Sprite tex;

	public string StationName;
	public string StationClass;

	public GameObject Owner;

	public bool MSDActive;
	public bool OrdersActive;
	public bool SpecialActive;
	public bool BuildActive;

	public Sprite ShipBluePrint;

	private GlobalLockingSystem _GLS;

	public bool UpgradeModule;
	public bool FixModule;
	public bool ShipBuildModule;
	public bool WeaponModule;
	public bool SciModule;
	public bool TradeModule;
	public bool MineModule;

	[HideInInspector]
	public bool DoorModule;
	[HideInInspector]
	public DoorModule _dm;

	public bool Assimilated;

	public bool ResPlus;
	public float CrewPS;
	public float DilithiumPS;
	public float TiteniumPS;

	private GlobalDB _GDB;
	private Select _SEL;

	public GameObject SensorsLine;
	private CircleRenderer _scr;
	private EnterSelectPlaneActive _sespa;
	public GameObject WeaponLine;
	private CircleRenderer _wcr;
	private EnterSelectPlaneActive _wespa;

	public bool StationOutLineActive;
	public GameObject StationOutLine;

	[HideInInspector]
	public SensorModule _sbsm;

	[HideInInspector]
	public ObjectTypeVisible _sbotv;
	[HideInInspector]
	public bool Hovering;

	// Use this for initialization

	void Awake()
	{
		_sbsm = gameObject.GetComponent<SensorModule>();
		_sbotv = gameObject.GetComponent<ObjectTypeVisible>();

		GameObject SenObj = Instantiate(SensorsLine, gameObject.transform.position, gameObject.transform.rotation);
		SensorsLine = SenObj;
		_scr = SenObj.GetComponent<CircleRenderer>();
		_sespa = SenObj.GetComponent<EnterSelectPlaneActive>();
		GameObject WepObj = Instantiate(WeaponLine, gameObject.transform.position, gameObject.transform.rotation);
		WeaponLine = WepObj;
		_wcr = WepObj.GetComponent<CircleRenderer>();
		_wespa = WepObj.GetComponent<EnterSelectPlaneActive>();

		_sespa.Owner = gameObject;

		_wespa.Owner = gameObject;
	}

	void Start()
	{
		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
		_SEL = GameObject.FindGameObjectWithTag("MainUI").GetComponent<Select>();
	}

	// Update is called once per frame
	void Update()
	{
		if (StationOutLineActive)
		{
			if (_sbotv.IsVisible)
			{
				StationOutLine.SetActive(true);
			}
		}
		else
		{
			StationOutLine.SetActive(false);
		}

		if (!visible && _GDB.activeObjectInterface == gameObject)
		{
			_GDB.activeObjectInterface = null;
		}

		if (_GDB.selectList.Count > 0 || _GDB.activeObjectInterface != gameObject || !_sbotv.IsVisible)
		{
			visible = false;
		}

		if (AI)
		{
			gameObject.tag = "Enemy";
		}
		if (FreandAI)
		{
			gameObject.tag = "Freand";
		}
		if (Neutral)
		{
			gameObject.tag = "Neutral";
		}
		if (NeutralAgrass)
		{
			gameObject.tag = "NeutralAgrass";
		}
		if (!AI && !FreandAI && !Neutral && !NeutralAgrass)
		{
			gameObject.tag = "Dwarf";
		}

		if (Neutral)
		{
			if (Owner != null)
			{
				Owner = null;
			}
		}
		if (ResPlus)
		{
			if (!AI && !FreandAI)
			{
				_GDB.Humans += CrewPS;
				_GDB.Dilithium += DilithiumPS;
				_GDB.Titanium += TiteniumPS;
			}
			if (AI)
			{
				Owner.GetComponent<GlobalAI>().Crew += CrewPS;
				Owner.GetComponent<GlobalAI>().Dilithium += DilithiumPS;
				Owner.GetComponent<GlobalAI>().Titanium += TiteniumPS;
			}
			if (!AI && FreandAI)
			{
				Owner.GetComponent<GlobalAI>().Crew += CrewPS;
				Owner.GetComponent<GlobalAI>().Dilithium += DilithiumPS;
				Owner.GetComponent<GlobalAI>().Titanium += TiteniumPS;
			}
		}

		SensorsLine.transform.position = gameObject.transform.position;
		_scr.radius = _sbsm.VisionRadius;

		if (WeaponModule)
		{
			WeaponLine.transform.position = gameObject.transform.position;
			_wcr.radius = gameObject.GetComponent<WeaponModule>().radiuse;
		}
		else
		{
			_wcr.radius = 0;
		}
		if (visible)
		{
			StationOutLineActive = true;
		}
		else
		{
			if (!Hovering)
			{
				StationOutLineActive = false;
			}
		}
	}
	void OnMouseOver()
	{
		Hovering = true;
		if (_sbotv.IsVisible)
		{
			StationOutLineActive = true;
		}
	}

	void OnMouseExit()
	{
		Hovering = false;
		if (!visible)
		{
			StationOutLineActive = false;
		}
	}
}