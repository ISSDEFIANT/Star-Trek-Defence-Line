using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipBuild : MonoBehaviour
{
	public List<float> Время;
	public List<GameObject> Корабль;
	public List<int> ListofNumbers;
	public List<float> Titanium;
	public List<float> Dilithium;
	public List<float> Humans;
	public float timeBuild;
	public bool Build;
	public GameObject Sensor;
	public bool Dock;
	public bool Station;
	public bool OutPost;

	public bool StartBuild;
	private bool DeleteinList;
	private ShipBuildModule _sbm;
	private Select _sel;
	private Station _sb;
	private GlobalDB _GDB;
	private float ВремяЗадержки = 0.1f;
	private bool Задержка;

	public List<bool> Builder;
	public List<bool> Miner;

	public List<bool> BaseFleet;
	public List<bool> AttackFleet;
	public List<bool> ScoutFleet;

	public List<float> OpenTime;

	public bool HaveAnimation;
	public GameObject AnimObject;
	public float CurOpenTimer;

	private float NavEnableTimer;

	public List<GameObject> NavObsObjects;
	// Use this for initialization
	void Start()
	{
		_sbm = gameObject.GetComponent<ShipBuildModule>();
		_sb = gameObject.GetComponent<Station>();
		_sel = GameObject.FindGameObjectWithTag("MainUI").GetComponent<Select>();

		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
	}

	// Update is called once per frame
	void Update()
	{
		if (HaveAnimation)
		{
			if (CurOpenTimer > 0)
			{
				CurOpenTimer -= Time.deltaTime;
				if (!AnimObject.GetComponent<Animation>().IsPlaying("Open"))
				{
					AnimObject.GetComponent<Animation>().Play("Open");
				}
			}
			else
			{
				if (AnimObject.GetComponent<Animation>().IsPlaying("Open"))
				{
					AnimObject.GetComponent<Animation>().Play("Close");
				}
			}
		}
		if (Корабль.Count <= 0)
		{
			Build = false;
			timeBuild = 1;
			if (Dock)
			{
				_sbm.timeBuild = 0;
			}
		}
		if (Корабль.Count > 0)
		{
			Build = true;
			if (Dock)
			{
				_sbm.timeBuild = timeBuild;
			}
			if (!StartBuild)
			{
				timeBuild = Время[0];
				StartBuild = true;
			}
		}
		if (Build)
		{
			timeBuild -= Time.deltaTime;
		}
		if (timeBuild < 0)
		{
			if (!_sb.AI && !_sb.FreandAI)
			{
				_sel.PlayConstructingEndSound(gameObject);
			}
			GameObject Ship;
			Ship = (GameObject)Instantiate(Корабль[0], _sbm.startPoint.transform.position, _sbm.startPoint.transform.rotation);

			Ship.GetComponent<MoveComponent>().ShipIsBuilt = true;

			if (HaveAnimation)
			{
				CurOpenTimer = OpenTime[0];
			}
			Ship.GetComponent<Stats>().AI = _sb.AI;
			Ship.GetComponent<Stats>().FreandAI = _sb.FreandAI;
			Ship.GetComponent<Stats>().Owner = _sb.Owner;
			Ship.GetComponent<ActiveState>().SB = _sbm.SB;

			if (ListofNumbers[0] == 1)
			{
				_sbm.Ships[0].ShipCount -= 1;
			}
			if (ListofNumbers[0] == 2)
			{
				_sbm.Ships[1].ShipCount -= 1;
			}
			if (ListofNumbers[0] == 3)
			{
				_sbm.Ships[2].ShipCount -= 1;
			}
			if (ListofNumbers[0] == 4)
			{
				_sbm.Ships[3].ShipCount -= 1;
			}
			if (ListofNumbers[0] == 5)
			{
				_sbm.Ships[4].ShipCount -= 1;
			}
			if (ListofNumbers[0] == 6)
			{
				_sbm.Ships[5].ShipCount -= 1;
			}
			if (Ship.name == "U.S.S.EXCALIBUR(Clone)")
			{
				NavEnableTimer = 4;
			}

			Корабль.RemoveAt(0);
			Время.RemoveAt(0);
			ListofNumbers.RemoveAt(0);

			Dilithium.RemoveAt(0);
			Titanium.RemoveAt(0);
			Humans.RemoveAt(0);
			if (HaveAnimation)
			{
				OpenTime.RemoveAt(0);
			}
			if (_sb.AI || _sb.FreandAI)
			{
				Builder.RemoveAt(0);

				Miner.RemoveAt(0);

				BaseFleet.RemoveAt(0);

				AttackFleet.RemoveAt(0);

				ScoutFleet.RemoveAt(0);
			}
			Задержка = true;
			StartBuild = false;
		}
		if (Задержка)
		{
			if (ВремяЗадержки > 0)
			{
				ВремяЗадержки -= Time.deltaTime;
			}
			if (ВремяЗадержки <= 0)
			{
				ВремяЗадержки = 0.1f;
				Задержка = false;
			}
		}
		if (NavEnableTimer > 0)
		{
			foreach (GameObject obj in NavObsObjects)
			{
				if (obj != null && obj.activeSelf != false)
				{
					obj.SetActive(false);
				}
			}
			NavEnableTimer -= Time.deltaTime;
		}
		else
		{
			foreach (GameObject obj in NavObsObjects)
			{
				if (obj != null && obj.activeSelf != true)
				{
					obj.SetActive(true);
				}
			}
		}
	}
	public void DiactivateNavObs()
	{
		NavEnableTimer = 10;
	}
}