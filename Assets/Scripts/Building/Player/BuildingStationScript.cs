using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingStationScript : MonoBehaviour
{
	public string Name;
	public GUISkin mainSkin;
	public int numDepth = 1;
	public bool visible;
	public List<GameObject> Builders;
	public GameObject ReadyBuilding;
	public float Timer;
	private GlobalDB _GDB;
	private float Timer1 = 0.01f;
	private float Timer2 = 0.01f;

	public bool AI;
	public bool FreandAI;
	public bool NeutralAgrass;
	public bool Neutral;

	public float Second = 0.01f;
	private bool Ready;
	public float BuildDistance;
	public GameObject BuildAnimMesh;

	public bool Castle;
	public bool Mining;
	public bool Shop;
	public bool Magic;
	public bool Tower;
	public bool PhaserTower;
	public bool Sensor;
	public bool ShopStation;
	public bool DefenceBase;
	public bool RStation;

	public GameObject Owner;
	public GameObject LocalAI;

	public float Делитель;

	private Select _SEL;

	public int Humans;
	public int Dilithium;
	public int Titanium;

	public bool LocalAIBuild;

	private float AIReationTimer = 60;
	// Use this for initialization
	void Start()
	{
		BuildAnimMesh.GetComponent<Animator>().speed = 0;
		_SEL = GameObject.FindGameObjectWithTag("MainUI").GetComponent<Select>();
		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
		if (AI)
		{
			gameObject.tag = "BuildingBuildingEnemy";
		}
		if (FreandAI)
		{
			gameObject.tag = "BuildingBuildingFreand";
		}
		gameObject.name = "BuildingStation(Clone)";
	}
	void LateUpdate()
	{
		if (Second <= 0)
		{
			Destroy(gameObject);
		}
	}
	// Update is called once per frame
	void Update()
	{
		if (_GDB.selectList.Count > 0)
		{
			visible = false;
			_GDB.activeObjectInterface = null;
		}
		Timer -= Time.deltaTime * Builders.Count;
		BuildAnimMesh.GetComponent<Animator>().speed = Builders.Count / Делитель;
		if (Timer <= 0)
		{
			if (!AI && !FreandAI)
			{
				_SEL.PlayConstructingEndSound(gameObject);
			}
			if (!Ready)
			{
				foreach (GameObject obj in Builders)
				{
					obj.GetComponent<Builder>().Order = null;
				}
				GameObject Building = (GameObject)Instantiate(ReadyBuilding, gameObject.transform.position, gameObject.transform.rotation);
				if (AI || FreandAI)
				{
					Building.GetComponent<Station>().Owner = Owner;
					Building.GetComponent<Station>().AI = AI;
					Building.GetComponent<Station>().FreandAI = FreandAI;


					if (Magic)
					{
					}
					Owner.GetComponent<GlobalAI>().NeedStarBaseLock = false;
					Owner.GetComponent<GlobalAI>().NeedMinesLock = false;
					Owner.GetComponent<GlobalAI>().NeedDock1Lock = false;
					Owner.GetComponent<GlobalAI>().NeedDock2Lock = false;
					Owner.GetComponent<GlobalAI>().NeedSciStationLock = false;
					Owner.GetComponent<GlobalAI>().NeedSensorsLock = false;
					Owner.GetComponent<GlobalAI>().NeedPhaserTurretsLock = false;
					Owner.GetComponent<GlobalAI>().NeedTorpedoTurretsLock = false;
					Owner.GetComponent<GlobalAI>().NeedTradingStationLock = false;
					Owner.GetComponent<GlobalAI>().NeedDefenceStationLock = false;
				}
				Ready = true;
			}
		}
		if (Ready)
		{
			if (Second > 0)
			{
				Second -= Time.deltaTime;
			}
		}
		if (AI || FreandAI)
		{
			if (AIReationTimer > 0)
			{
				if (Builders.Count == 0)
				{
					AIReationTimer -= Time.deltaTime;
				}
			}
			else
			{
				AIReationTimer = 60;
				Owner.GetComponent<GlobalAI>().AIBuilderTargeting(gameObject);
			}
		}
	}

	void OnMouseDown()
	{
		_SEL.ClearSelect();
		if (_GDB.activeObjectInterface != null)
			_GDB.deactivationInterface();
		_GDB.activeObjectInterface = gameObject;
		visible = true;
	}
	public void CancelBuilding()
	{
		_GDB.Humans += Humans;
		_GDB.Dilithium += Dilithium;
		_GDB.Titanium += Titanium;
		Destroy(gameObject);
		_SEL.PlayConstructingCanseledSound(gameObject);
	}
}