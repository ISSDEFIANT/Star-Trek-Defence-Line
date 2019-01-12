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

	public ShipInInterface[] Ships;

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

    void OnDestroyShipRecover(int number)
    {
        if (number == 1)
        {
            Ships[0].ShipCount -= 1;
            ReInitForShip(Ships[0].shipclassname);
        }
        if (number == 2)
        {
            Ships[1].ShipCount -= 1;
            ReInitForShip(Ships[1].shipclassname);
        }
        if (number == 3)
        {
            Ships[2].ShipCount -= 1;
            ReInitForShip(Ships[2].shipclassname);
        }
        if (number == 4)
        {
            Ships[3].ShipCount -= 1;
            ReInitForShip(Ships[3].shipclassname);
        }
        if (number == 5)
        {
            Ships[4].ShipCount -= 1;
            ReInitForShip(Ships[4].shipclassname);
        }
        if (number == 6)
        {
            Ships[5].ShipCount -= 1;
            ReInitForShip(Ships[5].shipclassname);
        }
    }

    void OnDestroy()
	{
		if (_SB.Корабль.Count >= 1)
		{
			OnDestroyShipRecover(_SB.ListofNumbers[0]);
		}
		if (_SB.Корабль.Count >= 2)
		{
		    OnDestroyShipRecover(_SB.ListofNumbers[1]);
		}
		if (_SB.Корабль.Count >= 3)
		{
		    OnDestroyShipRecover(_SB.ListofNumbers[2]);
        }
		if (_SB.Корабль.Count >= 4)
		{
		    OnDestroyShipRecover(_SB.ListofNumbers[3]);
        }
		if (_SB.Корабль.Count == 5)
		{
		    OnDestroyShipRecover(_SB.ListofNumbers[4]);
        }
	}

	public void CansledShip(int SlotNumber)
	{
		_SEL.PlayComputerSound(_GDB.PlayerRace, "constructingCanseled");

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
		    ReInitForShip(Ships[0].shipclassname);
        }
		if (_SB.ListofNumbers[SlotNumber-1] == 2)
		{
			Ships[1].ShipCount -= 1;
		    ReInitForShip(Ships[1].shipclassname);
        }
		if (_SB.ListofNumbers[SlotNumber-1] == 3)
		{
			Ships[2].ShipCount -= 1;
		    ReInitForShip(Ships[2].shipclassname);
        }
		if (_SB.ListofNumbers[SlotNumber-1] == 4)
		{
			Ships[3].ShipCount -= 1;
		    ReInitForShip(Ships[3].shipclassname);
        }
		if (_SB.ListofNumbers[SlotNumber-1] == 5)
		{
			Ships[4].ShipCount -= 1;
		    ReInitForShip(Ships[4].shipclassname);
        }
		if (_SB.ListofNumbers[SlotNumber-1] == 6)
		{
			Ships[5].ShipCount -= 1;
		    ReInitForShip(Ships[5].shipclassname);
        }

		_SB.ListofNumbers.RemoveAt(SlotNumber-1);
	}

    public void BuildStarShip(int ShipNumber)
    {
        if (!Fixing)
        {
            if (ShipAvaible(Ships[ShipNumber - 1].shipclassname))
            {
                if (_SB.Корабль.Count < 5)
                {
                    if (_GDB.Titanium >= Ships[ShipNumber - 1].TitaniumCost &
                        _GDB.Dilithium >= Ships[ShipNumber - 1].DilithiumCost &
                        _GDB.Humans >= Ships[ShipNumber - 1].CrewCost)
                    {
                        _GDB.Titanium -= Ships[ShipNumber - 1].TitaniumCost;
                        _GDB.Dilithium -= Ships[ShipNumber - 1].DilithiumCost;
                        _GDB.Humans -= Ships[ShipNumber - 1].CrewCost;
                        _SB.Корабль.Add(Ships[ShipNumber - 1].Ship);
                        _SB.Время.Add(Ships[ShipNumber - 1].BuildTime);
                        _SB.ListofNumbers.Add(ShipNumber);

                        PreInitForShip(Ships[ShipNumber - 1].shipclassname);

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
    }

    bool ShipAvaible(string classname)
    {
        switch (classname)
        {
            case "Galactica":
                if (NameCounter.Galactica.CurShips > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case "Defiant":
                if (NameCounter.Defiant.CurShips > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "Nova":
                if (NameCounter.Nova.CurShips > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "Saber":
                if (NameCounter.Saber.CurShips > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case "Akira":
                if (NameCounter.Akira.CurShips > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "Intrepid":
                if (NameCounter.Intrepid.CurShips > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "SteamRunner":
                if (NameCounter.Steamrunner.CurShips > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case "Luna":
                if (NameCounter.Luna.CurShips > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "Prometheus":
                if (NameCounter.Prometheuse.CurShips > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "Nebula":
                if (NameCounter.Nebula.CurShips > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "Galaxy":
                if (NameCounter.Galaxy.CurShips > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "Sovereign":
                if (NameCounter.Sovereign.CurShips > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "Excalibur":
                if (NameCounter.Excalibur.CurShips > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        return true;
    }

    void PreInitForShip(string classname)
    {
        switch (classname)
        {
            case "Galactica":
              //  NameCounter.Galactica.CurShips -= 1;
                break;

            case "Defiant":
                NameCounter.Defiant.CurShips -= 1;
                break;
            case "Nova":
                NameCounter.Nova.CurShips -= 1;
                break;
            case "Saber":
                NameCounter.Saber.CurShips -= 1;
                break;

            case "Akira":
                NameCounter.Akira.CurShips -= 1;
                break;
            case "Intrepid":
                NameCounter.Intrepid.CurShips -= 1;
                break;
            case "SteamRunner":
                NameCounter.Steamrunner.CurShips -= 1;
                break;

            case "Luna":
                NameCounter.Luna.CurShips -= 1;
                break;
            case "Prometheus":
                NameCounter.Prometheuse.CurShips -= 1;
                break;
            case "Nebula":
                NameCounter.Nebula.CurShips -= 1;
                break;
            case "Galaxy":
                NameCounter.Galaxy.CurShips -= 1;
                break;
            case "Sovereign":
                NameCounter.Sovereign.CurShips -= 1;
                break;
            case "Excalibur":
                NameCounter.Excalibur.CurShips -= 1;
                break;
        }
    }
    void ReInitForShip(string classname)
    {
        switch (classname)
        {
            case "Galactica":
                NameCounter.Galactica.CurShips += 1;
                break;

            case "Defiant":
                NameCounter.Defiant.CurShips += 1;
                break;
            case "Nova":
                NameCounter.Nova.CurShips += 1;
                break;
            case "Saber":
                NameCounter.Saber.CurShips += 1;
                break;

            case "Akira":
                NameCounter.Akira.CurShips += 1;
                break;
            case "Intrepid":
                NameCounter.Intrepid.CurShips += 1;
                break;
            case "SteamRunner":
                NameCounter.Steamrunner.CurShips += 1;
                break;

            case "Luna":
                NameCounter.Luna.CurShips += 1;
                break;
            case "Prometheus":
                NameCounter.Prometheuse.CurShips += 1;
                break;
            case "Nebula":
                NameCounter.Nebula.CurShips += 1;
                break;
            case "Galaxy":
                NameCounter.Galaxy.CurShips += 1;
                break;
            case "Sovereign":
                NameCounter.Sovereign.CurShips += 1;
                break;
            case "Excalibur":
                NameCounter.Excalibur.CurShips += 1;
                break;
        }
    }

    [System.Serializable]
    public class ShipInInterface
    {
        public GameObject Ship;
        public float BuildTime = 1;
        public int CrewCost;
        public int DilithiumCost;
        public int TitaniumCost;
        public int ShipCount;
        public Sprite Icon;
        public string Info;
        public string shipclassname;
        public float OpenTime;

        public bool ShipLock;
    }
}