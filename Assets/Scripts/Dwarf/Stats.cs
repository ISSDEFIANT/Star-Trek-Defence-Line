using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
	public bool AI;
	public bool FreandAI;
	public bool NeutralAgrass;
	public bool Neutral;

	public int protect;
	public int damage;


	public bool AttackStations;
	public bool DefenceStations;
	public bool NatralStations;

	public Texture ASTexture;
	public Texture DSTexture;
	public Texture NSTexture;

	public Sprite icon;
	public int приоритет;
	public string nameShip;
	public string Name;
	public GameObject shell;
	public Transform startPoint;
	public GameObject target;

	public GameObject MeshFOW;
	public enum enInstruction
	{
		idle,
		move,
		attack,
		derth
	}
	public enInstruction instruction;
	public Transform targetTransform;
	public Transform GuartTarget;
	public Vector3 targetVector;

	public bool Transport;
	public bool miner;

	private GlobalDB _GDB;
	//private Enemy _ey;
	private ActiveState _AS;
	private Select _select;
	public bool HoverCursore;
	private GameObject Box;
	private MoveComponent _agent;

	public bool Selected;
	public GameObject Camera;
	public GameObject Proector;
	public bool isSelect;
	public float Timer = 0.1f;
	public bool TimerDown;
	public bool WasSelect;
	public bool BoxSelected;
	public bool SelectLock;

	public bool WarpEffect;
	private float WarpEffectDelay = 1;
	public bool Warp;
	public float WarpSpeed;
	public float NormalSpeed;

	public int ShipTipe;

	public bool InMaskito;

	private bool TorpidoUp;

	public bool Assimilated;

	public GameObject Owner;

	private Ray _ray;
	private RaycastHit _hit;

	private float orderstoptimer = 0.1f;

	public Sprite ShipBluePrint;

	public int NameNumber;

	public string classname;

	public GameObject CountPrefeb;

	public string CountTag;

	public bool ShadowProjectorActive;

	public bool StopOrder;

	public bool IsFix;

	private HealthModule _HP;

	public GameObject StartLocation;
	public bool NonPhysicalMovement;


	private ObjectTypeVisible _otv;
	private SensorModule _es;

	public GameObject SensorsLine;
	private CircleRenderer _scr;
	private EnterSelectPlaneActive _sespa;
	public GameObject WeaponLine;
	private CircleRenderer _wcr;
	private EnterSelectPlaneActive _wespa;

	public bool Order;
	// Use this for initialization
	void Awake()
	{
		//CountTag = CountPrefeb.name;
		CountPrefeb = GameObject.Find(CountTag);
		if (gameObject.GetComponent<ObjectTypeVisible>())
		{
			_otv = gameObject.GetComponent<ObjectTypeVisible>();
		}
		_HP = gameObject.GetComponent<HealthModule>();
		if (!NonPhysicalMovement)
		{
			_agent = gameObject.GetComponent<MoveComponent>();
		}
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

		_es = gameObject.GetComponent<SensorModule>();

		if (StartLocation == null)
		{
			targetVector = transform.position;
		}
		else
		{
			_agent.Movement(StartLocation.transform.position);
		}
	}
	void Start()
	{
		Camera = GameObject.FindGameObjectWithTag("MainCamera");
		Box = GameObject.FindGameObjectWithTag("UnclickedBox");
		_select = GameObject.FindGameObjectWithTag("MainUI").GetComponent<Select>();
		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
		_GDB.dwarfList.Add(gameObject);

		_AS = gameObject.GetComponent<ActiveState>();
		if (GameObject.FindGameObjectWithTag("Enemy"))
		{
			//_ey = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
		}
		//if (AI) {
		//	gameObject.AddComponent<ShipAI>();
		//}
		if (_AS.Build)
		{
			if (CountPrefeb != null)
			{
				CountPrefeb.GetComponent<NameCounter>().CurShips -= 1;
			}
			else
			{
				CountPrefeb = GameObject.FindGameObjectWithTag(CountTag);
			}
		}
		_agent.MovementSpeed = NormalSpeed;
	}
	// Update is called once per frame
	void LateUpdate()
	{
		if (FindInSelectList(gameObject))
		{
			WasSelect = true;
			isSelect = true;
			if (Proector.active == false)
			{
				Proector.SetActive(true);
			}
		}
		else
		{
			WasSelect = false;
			isSelect = false;
			Proector.SetActive(false);
			if (BoxSelected)
			{
				BoxSelected = false;
			}
		}
	}
	void Update()
	{
		if (IsFix)
		{
			_agent.SensorBlocking = true;
		}
		else
		{
			_agent.SensorBlocking = false;
		}
		if (Neutral)
		{
			if (Owner != null)
			{
				gameObject.GetComponent<ShipAI>().ShipIsСaptured();
				Owner = null;
			}
		}
		if (Assimilated)
		{
			gameObject.GetComponent<Captan>().Race = "Borg";
			gameObject.GetComponent<Captan>().CaptanNum = 0;
		}
		if (FindInSelectList(gameObject))
		{
			Proector.GetComponent<MeshRenderer>().enabled = true;
		}
		else
		{
			Proector.GetComponent<MeshRenderer>().enabled = false;
		}
		if (orderstoptimer > 0)
		{
			orderstoptimer -= Time.deltaTime;
		}
		else
		{
			StopOrder = false;
			orderstoptimer = 0.5f;
		}
		if (AI || FreandAI)
		{
			gameObject.GetComponent<ShipAI>().enabled = true;
		}
		else
		{
			gameObject.GetComponent<ShipAI>().enabled = false;
		}
		if (targetTransform == gameObject.transform)
		{
			targetTransform = null;
		}
		if (!SelectLock)
		{

			if (TimerDown)
			{
				if (Timer > 0)
				{
					Timer -= Time.deltaTime;
				}
				if (Timer <= 0)
				{
					if (isSelect)
					{
						WasSelect = true;
					}
					Timer = 0.1f;
					TimerDown = false;
				}
			}
			if (Selected)
			{
				Camera.GetComponent<CameraRay>().Locker = false;
				//_select.Lock = false;
			}
			else
			{
				Camera.GetComponent<CameraRay>().Locker = true;
				//elect.Lock = true;
			}
			if (HoverCursore)
			{
				GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().IdleBool = false;
				GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().HoverBool = true;
				GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().GoBool = false;
				GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().AttackBool = false;
				GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().BaseBool = false;
			}
		}
		else
		{
			//BoxSelected = false;
			//MeshLock = false;
		}
		if (AI)
		{
			gameObject.tag = "Enemy";
		}
		if (!AI)
		{
			if (FreandAI && !Neutral && !NeutralAgrass)
			{
				gameObject.tag = "Freand";
			}
			if (!FreandAI && !Neutral && !NeutralAgrass)
			{
				gameObject.tag = "Dwarf";
			}
		}
		if (Neutral)
		{
			gameObject.tag = "Neutral";
		}
		if (NeutralAgrass)
		{
			gameObject.tag = "NeutralAgrass";
		}
		if (_HP.curTractorBeamSystemHealth > 0)
		{
			if (!_AS.Tractor.GetComponent<Tractor>().Use)
			{
				if (_HP.maxCrew > 0)
				{
					if (_HP.curImpulseSystemHealth > _HP.maxImpulseSystemHealth * 0.125f && !_HP.ActiveImpulse && _HP.curCrew > 0)
					{
						if (Vector3.Distance(gameObject.transform.position, targetVector) > 400)
						{
							if (_HP.curWarpEngingSystemHealth > _HP.maxWarpEngingSystemHealth * 0.125 && !_HP.ActiveWarpEnging && _HP.curWarpCoreHealth > _HP.maxWarpCoreHealth * 0.125 && !_HP.ActiveWarpCore)
							{
								if (!_agent.CUBETYPE)
								{
									if (!_agent.ForwardBlocked && Vector3.Distance(_agent.RightSensor.transform.position, _agent.TargetVector) < Vector3.Distance(_agent.LeftSensor.transform.position, _agent.TargetVector) + (0.1f * _agent.RotationSpeed) && Vector3.Distance(_agent.LeftSensor.transform.position, _agent.TargetVector) < Vector3.Distance(_agent.RightSensor.transform.position, _agent.TargetVector) + (0.1f * _agent.RotationSpeed) && Vector3.Distance(_agent.UpSensor.transform.position, _agent.TargetVector) < Vector3.Distance(_agent.DownSensor.transform.position, _agent.TargetVector) + (0.1f * _agent.RotationSpeed) && Vector3.Distance(_agent.DownSensor.transform.position, _agent.TargetVector) < Vector3.Distance(_agent.UpSensor.transform.position, _agent.TargetVector) + (0.1f * _agent.RotationSpeed))
									{
										if (WarpEffectDelay > 0)
										{
											WarpEffect = true;
											WarpEffectDelay -= Time.deltaTime;
										}
										else
										{
											Warp = true;
											WarpEffect = false;
											_agent.Warp = false;
										}
									}
									else
									{
										Warp = false;
										WarpEffect = false;
										_agent.Warp = false;
										WarpEffectDelay = 0.5f;
									}
								}
								else
								{
									if (WarpEffectDelay > 0)
									{
										WarpEffect = true;
										WarpEffectDelay -= Time.deltaTime;
									}
									else
									{
										Warp = true;
										WarpEffect = false;
										_agent.Warp = false;
									}
								}
							}
							else
							{
								if (Warp)
								{
									Warp = false;
									WarpEffect = false;
									_agent.Warp = false;
									WarpEffectDelay = 0.5f;
								}
							}
						}
						if (Vector3.Distance(gameObject.transform.position, targetVector) <= 200)
						{
							if (Warp)
							{
								Warp = false;
								WarpEffect = false;
								_agent.Warp = false;
								WarpEffectDelay = 0.5f;
							}
						}
						if (Warp)
						{
							if (_HP.curWarpEngingSystemHealth <= _HP.maxWarpEngingSystemHealth / 2)
							{
								_agent.Warp = true;
								_agent.WarpMovementSpeed = WarpSpeed / 2;
							}
							if (_HP.curWarpEngingSystemHealth > _HP.maxWarpEngingSystemHealth / 2)
							{
								_agent.Warp = true;
								_agent.WarpMovementSpeed = WarpSpeed;
							}
						}
						else
						{
							if (_HP.curImpulseSystemHealth <= _HP.maxImpulseSystemHealth / 3)
							{
								Warp = false;
								_agent.WarpMovementSpeed = 0;
							}
							if (_HP.curImpulseSystemHealth > _HP.maxImpulseSystemHealth / 3)
							{
								Warp = false;
								_agent.WarpMovementSpeed = 0;
							}
						}
					}
					else
					{
						Warp = false;
						WarpEffect = false;
						WarpEffectDelay = 0.5f;
						_agent.Warp = false;
						_agent.WarpMovementSpeed = 0;
						//	gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * _agent.MovementSpeed;
					}
				}
				else
				{
					if (_HP.curImpulseSystemHealth > _HP.maxImpulseSystemHealth * 0.125f && !_HP.ActiveImpulse)
					{
						if (Vector3.Distance(gameObject.transform.position, targetVector) > 400)
						{
							if (_HP.curWarpEngingSystemHealth > _HP.maxWarpEngingSystemHealth * 0.125 || !_HP.ActiveWarpEnging)
							{
								Warp = true;
							}
							else
							{
								if (Warp)
								{
									Warp = false;
								}
							}
						}
						if (Vector3.Distance(gameObject.transform.position, targetVector) <= 200)
						{
							if (Warp)
							{
								Warp = false;
							}
						}
						if (Warp)
						{
							if (_HP.curWarpEngingSystemHealth <= _HP.maxWarpEngingSystemHealth / 2)
							{
								_agent.MovementSpeed = WarpSpeed / 2;
							}
							if (_HP.curWarpEngingSystemHealth > _HP.maxWarpEngingSystemHealth / 2)
							{
								_agent.MovementSpeed = WarpSpeed;
							}
						}
						else
						{
							if (_HP.curImpulseSystemHealth <= _HP.maxImpulseSystemHealth / 3)
							{
								_agent.MovementSpeed = NormalSpeed / 2;
							}
							if (_HP.curImpulseSystemHealth > _HP.maxImpulseSystemHealth / 3)
							{
								_agent.MovementSpeed = NormalSpeed;
							}
						}
					}
					else
					{
						Warp = false;
						_agent.Warp = false;
						_agent.WarpMovementSpeed = 0;
						gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * _agent.MovementSpeed;
					}
				}
			}
		}
		else
		{
			if (_HP.maxCrew > 0)
			{
				if (_HP.curImpulseSystemHealth > _HP.maxImpulseSystemHealth * 0.125f && !_HP.ActiveImpulse && _HP.curCrew > 0)
				{
					if (Vector3.Distance(gameObject.transform.position, targetVector) > 400)
					{
						if (_HP.curWarpEngingSystemHealth > _HP.maxWarpEngingSystemHealth * 0.125 && !_HP.ActiveWarpEnging && _HP.curWarpCoreHealth > _HP.maxWarpCoreHealth * 0.125 && !_HP.ActiveWarpCore)
						{
							if (!_agent.CUBETYPE)
							{
								if (!_agent.ForwardBlocked && Vector3.Distance(_agent.RightSensor.transform.position, _agent.TargetVector) < Vector3.Distance(_agent.LeftSensor.transform.position, _agent.TargetVector) + (0.1f * _agent.RotationSpeed) && Vector3.Distance(_agent.LeftSensor.transform.position, _agent.TargetVector) < Vector3.Distance(_agent.RightSensor.transform.position, _agent.TargetVector) + (0.1f * _agent.RotationSpeed) && Vector3.Distance(_agent.UpSensor.transform.position, _agent.TargetVector) < Vector3.Distance(_agent.DownSensor.transform.position, _agent.TargetVector) + (0.1f * _agent.RotationSpeed) && Vector3.Distance(_agent.DownSensor.transform.position, _agent.TargetVector) < Vector3.Distance(_agent.UpSensor.transform.position, _agent.TargetVector) + (0.1f * _agent.RotationSpeed))
								{
									if (WarpEffectDelay > 0)
									{
										WarpEffect = true;
										WarpEffectDelay -= Time.deltaTime;
									}
									else
									{
										Warp = true;
										WarpEffect = false;
										_agent.Warp = false;
									}
								}
								else
								{
									Warp = false;
									WarpEffect = false;
									_agent.Warp = false;
									WarpEffectDelay = 0.5f;
								}
							}
							else
							{
								if (WarpEffectDelay > 0)
								{
									WarpEffect = true;
									WarpEffectDelay -= Time.deltaTime;
								}
								else
								{
									Warp = true;
									WarpEffect = false;
									_agent.Warp = false;
								}
							}
						}
						else
						{
							if (Warp)
							{
								Warp = false;
								WarpEffect = false;
								_agent.Warp = false;
								WarpEffectDelay = 0.5f;
							}
						}
					}
					if (Vector3.Distance(gameObject.transform.position, targetVector) <= 200)
					{
						if (Warp)
						{
							Warp = false;
							WarpEffect = false;
							_agent.Warp = false;
							WarpEffectDelay = 0.5f;
						}
					}
					if (Warp)
					{
						if (_HP.curWarpEngingSystemHealth <= _HP.maxWarpEngingSystemHealth / 2)
						{
							_agent.Warp = true;
							_agent.WarpMovementSpeed = WarpSpeed / 2;
						}
						if (_HP.curWarpEngingSystemHealth > _HP.maxWarpEngingSystemHealth / 2)
						{
							_agent.Warp = true;
							_agent.WarpMovementSpeed = WarpSpeed;
						}
					}
					else
					{
						if (_HP.curImpulseSystemHealth <= _HP.maxImpulseSystemHealth / 3)
						{
							Warp = false;
							_agent.WarpMovementSpeed = 0;
						}
						if (_HP.curImpulseSystemHealth > _HP.maxImpulseSystemHealth / 3)
						{
							Warp = false;
							_agent.WarpMovementSpeed = 0;
						}
					}
				}
				else
				{
					Warp = false;
					WarpEffect = false;
					WarpEffectDelay = 0.5f;
					_agent.Warp = false;
					_agent.WarpMovementSpeed = 0;
					//	gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * _agent.MovementSpeed;
				}
			}
			else
			{
				if (_HP.curImpulseSystemHealth > _HP.maxImpulseSystemHealth * 0.125f && !_HP.ActiveImpulse)
				{
					if (Vector3.Distance(gameObject.transform.position, targetVector) > 400)
					{
						if (_HP.curWarpEngingSystemHealth > _HP.maxWarpEngingSystemHealth * 0.125 || !_HP.ActiveWarpEnging)
						{
							Warp = true;
						}
						else
						{
							if (Warp)
							{
								Warp = false;
							}
						}
					}
					if (Vector3.Distance(gameObject.transform.position, targetVector) <= 200)
					{
						if (Warp)
						{
							Warp = false;
						}
					}
					if (Warp)
					{
						if (_HP.curWarpEngingSystemHealth <= _HP.maxWarpEngingSystemHealth / 2)
						{
							_agent.MovementSpeed = WarpSpeed / 2;
						}
						if (_HP.curWarpEngingSystemHealth > _HP.maxWarpEngingSystemHealth / 2)
						{
							_agent.MovementSpeed = WarpSpeed;
						}
					}
					else
					{
						if (_HP.curImpulseSystemHealth <= _HP.maxImpulseSystemHealth / 3)
						{
							_agent.MovementSpeed = NormalSpeed / 2;
						}
						if (_HP.curImpulseSystemHealth > _HP.maxImpulseSystemHealth / 3)
						{
							_agent.MovementSpeed = NormalSpeed;
						}
					}
				}
				else
				{
					Warp = false;
					_agent.Warp = false;
					_agent.WarpMovementSpeed = 0;
					gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * _agent.MovementSpeed;
				}
			}
		}

		if (!isSelect)
		{
			AttackStations = false;
			DefenceStations = false;
			NatralStations = false;
		}
		if (Name == System.String.Empty)
		{
			if (classname == "Galactica")
			{
				Name = _GDB.gameObject.GetComponent<NameSelectScript>().GalactiucaNames[Random.Range(0, _GDB.gameObject.GetComponent<NameSelectScript>().GalactiucaNames.Count)];
				_GDB.gameObject.GetComponent<NameSelectScript>().GalactiucaNames.Remove(Name);
			}

			if (classname == "Defiant")
			{
				Name = _GDB.gameObject.GetComponent<NameSelectScript>().DefiantNames[Random.Range(0, _GDB.gameObject.GetComponent<NameSelectScript>().DefiantNames.Count)];
				_GDB.gameObject.GetComponent<NameSelectScript>().DefiantNames.Remove(Name);
			}
			if (classname == "Nova")
			{
				Name = _GDB.gameObject.GetComponent<NameSelectScript>().NovaNames[Random.Range(0, _GDB.gameObject.GetComponent<NameSelectScript>().NovaNames.Count)];
				_GDB.gameObject.GetComponent<NameSelectScript>().NovaNames.Remove(Name);
			}
			if (classname == "Saber")
			{
				Name = _GDB.gameObject.GetComponent<NameSelectScript>().SaberNames[Random.Range(0, _GDB.gameObject.GetComponent<NameSelectScript>().SaberNames.Count)];
				_GDB.gameObject.GetComponent<NameSelectScript>().SaberNames.Remove(Name);
			}

			if (classname == "Akira")
			{
				Name = _GDB.gameObject.GetComponent<NameSelectScript>().AkiraNames[Random.Range(0, _GDB.gameObject.GetComponent<NameSelectScript>().AkiraNames.Count)];
				_GDB.gameObject.GetComponent<NameSelectScript>().AkiraNames.Remove(Name);
			}
			if (classname == "Intrepid")
			{
				Name = _GDB.gameObject.GetComponent<NameSelectScript>().IntrepidNames[Random.Range(0, _GDB.gameObject.GetComponent<NameSelectScript>().IntrepidNames.Count)];
				_GDB.gameObject.GetComponent<NameSelectScript>().IntrepidNames.Remove(Name);
			}
			if (classname == "SteamRunner")
			{
				Name = _GDB.gameObject.GetComponent<NameSelectScript>().SteamRunnerNames[Random.Range(0, _GDB.gameObject.GetComponent<NameSelectScript>().SteamRunnerNames.Count)];
				_GDB.gameObject.GetComponent<NameSelectScript>().SteamRunnerNames.Remove(Name);
			}

			if (classname == "Luna")
			{
				Name = _GDB.gameObject.GetComponent<NameSelectScript>().LunaNames[Random.Range(0, _GDB.gameObject.GetComponent<NameSelectScript>().LunaNames.Count)];
				_GDB.gameObject.GetComponent<NameSelectScript>().LunaNames.Remove(Name);
			}
			if (classname == "Prometheus")
			{
				Name = _GDB.gameObject.GetComponent<NameSelectScript>().PrometheuseNames[Random.Range(0, _GDB.gameObject.GetComponent<NameSelectScript>().PrometheuseNames.Count)];
				_GDB.gameObject.GetComponent<NameSelectScript>().PrometheuseNames.Remove(Name);
			}
			if (classname == "Nebula")
			{
				Name = _GDB.gameObject.GetComponent<NameSelectScript>().NebulaNames[Random.Range(0, _GDB.gameObject.GetComponent<NameSelectScript>().NebulaNames.Count)];
				_GDB.gameObject.GetComponent<NameSelectScript>().NebulaNames.Remove(Name);
			}
			if (classname == "Galaxy")
			{
				Name = _GDB.gameObject.GetComponent<NameSelectScript>().GalaxyNames[Random.Range(0, _GDB.gameObject.GetComponent<NameSelectScript>().GalaxyNames.Count)];
				_GDB.gameObject.GetComponent<NameSelectScript>().GalaxyNames.Remove(Name);
			}
			if (classname == "Sovereign")
			{
				Name = _GDB.gameObject.GetComponent<NameSelectScript>().SovereignNames[Random.Range(0, _GDB.gameObject.GetComponent<NameSelectScript>().SovereignNames.Count)];
				_GDB.gameObject.GetComponent<NameSelectScript>().SovereignNames.Remove(Name);
			}
			if (classname == "Excalibur")
			{
				Name = _GDB.gameObject.GetComponent<NameSelectScript>().ExcaliburNames[Random.Range(0, _GDB.gameObject.GetComponent<NameSelectScript>().ExcaliburNames.Count)];
				_GDB.gameObject.GetComponent<NameSelectScript>().ExcaliburNames.Remove(Name);
			}
		}
		SensorsLine.transform.position = gameObject.transform.position;
		_scr.radius = _es.VisionRadius;

		WeaponLine.transform.position = gameObject.transform.position;
		_wcr.radius = _AS.radiuse;
	}
	bool FindInSelectList(GameObject obj)
	{
		foreach (GameObject selObj in _GDB.selectList)
		{
			if (selObj == obj)
				return true;
		}
		return false;
	}

	void OnDestroy()
	{
		if (_HP.curHealth <= 0)
		{
			CountPrefeb.GetComponent<NameCounter>().CurShips += 1;
		}
		Destroy(SensorsLine);
		Destroy(WeaponLine);
	}
}