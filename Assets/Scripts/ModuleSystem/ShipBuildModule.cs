using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuildModule : MonoBehaviour
{
	public GameObject flag;
	public GameObject curFlag;

	public float timeBuild;

	public GameObject startPoint;

	public float timer = 1;
	public bool Build;

	public bool isFlag;

	public GameObject ExitLocation;

	public bool Fixing;

	public ShipInStationInterface[] Ships;

	private GlobalDB _GDB;
	private Select _SEL;

	private ShipBuild _SB;

	private Station _sbm;

	public LayerMask UsingLayer;

	public GameObject SB;
	// Use this for initialization
	void Start()
	{
		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
		_SEL = GameObject.FindGameObjectWithTag("MainUI").GetComponent<Select>();
		_SB = gameObject.GetComponent<ShipBuild>();
		_sbm = gameObject.GetComponent<Station>();
	}

	// Update is called once per frame
	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 10000.0f, UsingLayer))
		{
			if (isFlag)
			{
				curFlag.transform.position = hit.point;
				if (Input.GetMouseButtonDown(0))
				{
					isFlag = false;
				}
			}
		}

		if (_sbm.visible)
		{
			curFlag.GetComponent<Flag>().FlagHalo.SetActive(true);
		}
		else
		{
			curFlag.GetComponent<Flag>().FlagHalo.SetActive(false);
		}

		if (Fixing)
		{
			_SB.enabled = false;
		}
		else
		{
			_SB.enabled = true;
		}
	}

	void OnDestroy()
	{
		if (_SB.Корабль.Count >= 1)
		{
			if (_SB.ListofNumbers[0] == 1)
			{
				Ships[0].ShipCount -= 1;
				Ships[0].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[0] == 2)
			{
				Ships[1].ShipCount -= 1;
				Ships[1].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[0] == 3)
			{
				Ships[2].ShipCount -= 1;
				Ships[2].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[0] == 4)
			{
				Ships[3].ShipCount -= 1;
				Ships[3].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[0] == 5)
			{
				Ships[4].ShipCount -= 1;
				Ships[4].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[0] == 6)
			{
				Ships[5].ShipCount -= 1;
				Ships[5].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
		}
		if (_SB.Корабль.Count >= 2)
		{
			if (_SB.ListofNumbers[1] == 1)
			{
				Ships[0].ShipCount -= 1;
				Ships[0].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[1] == 2)
			{
				Ships[1].ShipCount -= 1;
				Ships[1].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[1] == 3)
			{
				Ships[2].ShipCount -= 1;
				Ships[2].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[1] == 4)
			{
				Ships[3].ShipCount -= 1;
				Ships[3].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[1] == 5)
			{
				Ships[4].ShipCount -= 1;
				Ships[4].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[1] == 6)
			{
				Ships[5].ShipCount -= 1;
				Ships[5].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
		}
		if (_SB.Корабль.Count >= 3)
		{
			if (_SB.ListofNumbers[2] == 1)
			{
				Ships[0].ShipCount -= 1;
				Ships[0].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[2] == 2)
			{
				Ships[1].ShipCount -= 1;
				Ships[1].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[2] == 3)
			{
				Ships[2].ShipCount -= 1;
				Ships[2].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[2] == 4)
			{
				Ships[3].ShipCount -= 1;
				Ships[3].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[2] == 5)
			{
				Ships[4].ShipCount -= 1;
				Ships[4].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[2] == 6)
			{
				Ships[5].ShipCount -= 1;
				Ships[5].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
		}
		if (_SB.Корабль.Count >= 4)
		{
			if (_SB.ListofNumbers[3] == 1)
			{
				Ships[0].ShipCount -= 1;
				Ships[0].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[3] == 2)
			{
				Ships[1].ShipCount -= 1;
				Ships[1].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[3] == 3)
			{
				Ships[2].ShipCount -= 1;
				Ships[2].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[3] == 4)
			{
				Ships[3].ShipCount -= 1;
				Ships[3].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[3] == 5)
			{
				Ships[4].ShipCount -= 1;
				Ships[4].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[3] == 6)
			{
				Ships[5].ShipCount -= 1;
				Ships[5].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
		}
		if (_SB.Корабль.Count == 5)
		{
			if (_SB.ListofNumbers[4] == 1)
			{
				Ships[0].ShipCount -= 1;
				Ships[0].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[4] == 2)
			{
				Ships[1].ShipCount -= 1;
				Ships[1].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[4] == 3)
			{
				Ships[2].ShipCount -= 1;
				Ships[2].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[4] == 4)
			{
				Ships[3].ShipCount -= 1;
				Ships[3].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[4] == 5)
			{
				Ships[4].ShipCount -= 1;
				Ships[4].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
			if (_SB.ListofNumbers[4] == 6)
			{
				Ships[5].ShipCount -= 1;
				Ships[5].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
			}
		}	}

	public void CansledShip(int SlotNumber)
	{
		_SEL.PlayConstructingCanseledSound(gameObject);

		_SB.Корабль.RemoveAt(SlotNumber-1);
		_SB.Время.RemoveAt(SlotNumber-1);
		_SB.StartBuild = false;
		if (_SB.Время.Count != 0)
		{
			_SB.timeBuild = _SB.Время[SlotNumber-1];
		}
		_GDB.Titanium += _SB.Titanium[SlotNumber-1];
		_GDB.Dilithium += _SB.Dilithium[SlotNumber-1];
		_GDB.Humans += _SB.Humans[SlotNumber-1];
		_SB.Titanium.RemoveAt(SlotNumber-1);
		_SB.Dilithium.RemoveAt(SlotNumber-1);
		_SB.Humans.RemoveAt(SlotNumber-1);
		_SB.Builder.RemoveAt(SlotNumber-1);
		_SB.Miner.RemoveAt(SlotNumber-1);
		_SB.BaseFleet.RemoveAt(SlotNumber-1);
		_SB.AttackFleet.RemoveAt(SlotNumber-1);
		_SB.ScoutFleet.RemoveAt(SlotNumber-1);
		_SB.OpenTime.RemoveAt(SlotNumber-1);
		if (_SB.ListofNumbers[SlotNumber-1] == 1)
		{
			Ships[0].ShipCount -= 1;
			Ships[0].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
		}
		if (_SB.ListofNumbers[SlotNumber-1] == 2)
		{
			Ships[1].ShipCount -= 1;
			Ships[1].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
		}
		if (_SB.ListofNumbers[SlotNumber-1] == 3)
		{
			Ships[2].ShipCount -= 1;
			Ships[2].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
		}
		if (_SB.ListofNumbers[SlotNumber-1] == 4)
		{
			Ships[3].ShipCount -= 1;
			Ships[3].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
		}
		if (_SB.ListofNumbers[SlotNumber-1] == 5)
		{
			Ships[4].ShipCount -= 1;
			Ships[4].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
		}
		if (_SB.ListofNumbers[SlotNumber-1] == 6)
		{
			Ships[5].ShipCount -= 1;
			Ships[5].CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
		}

		_SB.ListofNumbers.RemoveAt(SlotNumber-1);
	}

	public void BuildStarShip(int ShipNumber)
	{
		if (!Fixing)
		{
			if (Ships[ShipNumber - 1].CountPrefeb != null)
			{
				if (Ships[ShipNumber - 1].CountPrefeb.GetComponent<NameCounter>().CurShips > 0)
				{
					if (_SB.Корабль.Count < 5)
					{
						if (_GDB.Titanium >= Ships[ShipNumber - 1].TitaniumCost & _GDB.Dilithium >= Ships[ShipNumber - 1].DilithiumCost & _GDB.Humans >= Ships[ShipNumber - 1].CrewCost)
						{
							_GDB.Titanium -= Ships[ShipNumber - 1].TitaniumCost;
							_GDB.Dilithium -= Ships[ShipNumber - 1].DilithiumCost;
							_GDB.Humans -= Ships[ShipNumber - 1].CrewCost;
							_SB.Корабль.Add(Ships[ShipNumber - 1].Ship);
							_SB.Время.Add(Ships[ShipNumber - 1].BuildTime);
							_SB.ListofNumbers.Add(ShipNumber);
							Ships[ShipNumber - 1].CountPrefeb.GetComponent<NameCounter>().CurShips -= 1;

							_SB.Titanium.Add(Ships[ShipNumber - 1].TitaniumCost);
							_SB.Dilithium.Add(Ships[ShipNumber - 1].DilithiumCost);
							_SB.Humans.Add(Ships[ShipNumber - 1].CrewCost);
							_SB.Builder.Add(false);
							_SB.Miner.Add(false);
							_SB.BaseFleet.Add(false);
							_SB.AttackFleet.Add(false);
							_SB.ScoutFleet.Add(false);
							_SB.OpenTime.Add(Ships[ShipNumber - 1].OpenTime);

							Ships[ShipNumber - 1].ShipCount += 1;
						}
					}
				}
			}
			else
			{
				if (_GDB.Titanium >= Ships[ShipNumber - 1].TitaniumCost & _GDB.Dilithium >= Ships[ShipNumber - 1].DilithiumCost & _GDB.Humans >= Ships[ShipNumber - 1].CrewCost)
				{
					_GDB.Titanium -= Ships[ShipNumber - 1].TitaniumCost;
					_GDB.Dilithium -= Ships[ShipNumber - 1].DilithiumCost;
					_GDB.Humans -= Ships[ShipNumber - 1].CrewCost;
					_SB.Корабль.Add(Ships[ShipNumber - 1].Ship);
					_SB.Время.Add(Ships[ShipNumber - 1].BuildTime);
					_SB.ListofNumbers.Add(ShipNumber);

					_SB.Titanium.Add(Ships[ShipNumber - 1].TitaniumCost);
					_SB.Dilithium.Add(Ships[ShipNumber - 1].DilithiumCost);
					_SB.Humans.Add(Ships[ShipNumber - 1].CrewCost);
					_SB.Builder.Add(false);
					_SB.Miner.Add(false);
					_SB.BaseFleet.Add(false);
					_SB.AttackFleet.Add(false);
					_SB.ScoutFleet.Add(false);
					_SB.OpenTime.Add(Ships[ShipNumber - 1].OpenTime);

					Ships[ShipNumber - 1].ShipCount += 1;
				}
			}
		}	}
}