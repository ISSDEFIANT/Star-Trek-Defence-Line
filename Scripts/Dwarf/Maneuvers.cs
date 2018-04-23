using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
using System.Linq;

public class Maneuvers : MonoBehaviour
{
	public float ManeuversSpeed;

	public bool Type1;
	public bool Type2;
	public bool Type3;

	public bool BPType;

	public bool CType;

	public GameObject MoveTarget;

	//AttackType1
	public GameObject BPObject;

	//	[HideInInspector]
	public bool WeaponIsReady;
	//	[HideInInspector]
	public bool OpenTheFire;
	//	[HideInInspector]
	public bool FireCompleate;
	//
	//AttackType2
	public GameObject RoundObject;
	//
	//AttackType3
	public GameObject FlowerObject;
	//

	private Stats _st;
	private MoveComponent _mc;
	private WaypointProgressTracker _WPT;
	private HealthModule _h;
	private ActiveState _as;

	private float TypeChangerTimer;
	public string CurMane;

	int type = 0;

	public float WayChangeTimer;
	private float StartWCT;
	public bool BlockManSys;

	RaycastHit _rh;

	private float Delay = 3;
	// Use this for initialization
	void Awake()
	{
		StartWCT = TypeChangerTimer;
	}

	void Start()
	{
		_st = gameObject.GetComponent<Stats>();
		_mc = gameObject.GetComponent<MoveComponent>();
		_WPT = gameObject.GetComponent<WaypointProgressTracker>();
		_h = gameObject.GetComponent<HealthModule>();
		_as = gameObject.GetComponent<ActiveState>();
		TypeChangerTimer = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if (TypeChangerTimer > 0)
		{
			TypeChangerTimer -= Time.deltaTime;

			type = 0;
		}
		else
		{
			if (type == 0)
			{
				if (BPType)
				{
					type = Random.Range(1, 2);
				}
				if (CType)
				{
					type = 4;
				}
				if (Type1)
				{
					type = Random.Range(1, 3);
				}
				if (Type2)
				{
					type = Random.Range(1, 4);
				}
				if (Type3)
				{
					type = Random.Range(2, 4);
				}
			}
			else
			{
				if (BPType)
				{
					if (type == 1)
					{
						TypeChangerTimer = Random.Range(30, 300);
					}
					if (type == 2)
					{
						TypeChangerTimer = Random.Range(10, 100);
					}
				}
				if (CType)
				{
					TypeChangerTimer = Random.Range(30, 300);
				}
				if (Type1)
				{
					if (type == 1)
					{
						TypeChangerTimer = Random.Range(5, 60);
					}
					if (type == 2)
					{
						TypeChangerTimer = Random.Range(20, 200);
					}
					if (type == 3)
					{
						TypeChangerTimer = Random.Range(30, 300);
					}
				}
				if (Type2)
				{
					if (type == 1)
					{
						TypeChangerTimer = Random.Range(5, 60);
					}
					if (type == 2)
					{
						TypeChangerTimer = Random.Range(20, 200);
					}
					if (type == 3)
					{
						TypeChangerTimer = Random.Range(30, 300);
					}
					if (type == 4)
					{
						TypeChangerTimer = Random.Range(5, 60);
					}
				}
				if (Type3)
				{
					if (type == 2)
					{
						TypeChangerTimer = Random.Range(10, 100);
					}
					if (type == 3)
					{
						TypeChangerTimer = Random.Range(20, 200);
					}
					if (type == 4)
					{
						TypeChangerTimer = Random.Range(30, 300);
					}
				}
			}
		}
		if (type == 1)
		{
			CurMane = "DefiantImpulseAttack";
		}
		if (type == 2)
		{
			CurMane = "AroundTarget";
		}
		if (type == 3)
		{
			CurMane = "Flower";
		}
		if (type == 4)
		{
			CurMane = "DONOTMOVE";
		}
		if ((_as.Protact && _st.Order) || _as.Agrass || (_as.Idle && _st.Order))
		{
			if (_st.targetTransform != null)
			{
				if (BPType)
				{
					if (WeaponIsReady)
					{
						BlockManSys = true;
						_mc.Movement(_st.targetTransform.position);
						ManeuversSpeed = _mc.RotationSpeed;

						if (Delay > 0)
						{
							Delay -= Time.deltaTime;
						}
						else
						{
							OpenTheFire = true;
							Delay = 3;
						}
					}
					if (OpenTheFire)
					{
						if (FireCompleate)
						{
							BlockManSys = false;
							WeaponIsReady = false;
							FireCompleate = false;
							OpenTheFire = false;
						}
					}
				}
				if (!BlockManSys)
				{
					if (CurMane == "DefiantImpulseAttack")
					{
						ManeuversSpeed = _mc.RotationSpeed / BPObject.transform.lossyScale.x;
						_WPT.enabled = true;
						_WPT.circuit = BPObject.GetComponent<WaypointCircuit>();
						if (_st.targetTransform != null)
						{
							HealthModule _ehm = _st.targetTransform.gameObject.GetComponent<HealthModule>();

							if (Vector3.Distance(gameObject.transform.position, _st.targetTransform.position) > (_as.radiuse + _h.ShipRadius + _ehm.ShipRadius))
							{
								_mc.Movement(_st.targetTransform.position);
							}
							else
							{
								BPObject.transform.localScale = new Vector3(_ehm.ShipRadius, _ehm.ShipRadius, _ehm.ShipRadius);
								BPObject.transform.parent = null;
								BPObject.transform.position = _st.targetTransform.position;
								if (WayChangeTimer > 0)
								{
									WayChangeTimer -= Time.deltaTime;
								}
								else
								{
									_mc.Movement(_WPT.target.position);
									WayChangeTimer = StartWCT;
								}
							}
						}
						else
						{
							BPObject.transform.parent = gameObject.transform;
						}

						RoundObject.transform.parent = gameObject.transform;
						FlowerObject.transform.parent = gameObject.transform;
					}
					if (CurMane == "AroundTarget")
					{
						ManeuversSpeed = _mc.RotationSpeed / RoundObject.transform.lossyScale.x;
						_WPT.enabled = true;
						_WPT.circuit = RoundObject.GetComponent<WaypointCircuit>();
						if (_st.targetTransform != null)
						{
							HealthModule _ehm = _st.targetTransform.gameObject.GetComponent<HealthModule>();

							if (Vector3.Distance(gameObject.transform.position, _st.targetTransform.position) > (_as.radiuse + _h.ShipRadius + _ehm.ShipRadius))
							{
								_mc.Movement(_st.targetTransform.position);
							}
							else
							{
								RoundObject.transform.localScale = new Vector3(_ehm.ShipRadius, _ehm.ShipRadius, _ehm.ShipRadius);
								RoundObject.transform.parent = null;
								RoundObject.transform.position = _st.targetTransform.position;
								if (WayChangeTimer > 0)
								{
									WayChangeTimer -= Time.deltaTime;
								}
								else
								{
									_mc.Movement(_WPT.target.position);
									WayChangeTimer = StartWCT;
								}
							}
						}
						else
						{
							RoundObject.transform.parent = gameObject.transform;
						}

						BPObject.transform.parent = gameObject.transform;
						FlowerObject.transform.parent = gameObject.transform;
					}
					if (CurMane == "Flower")
					{
						ManeuversSpeed = _mc.RotationSpeed / FlowerObject.transform.lossyScale.x;
						_WPT.enabled = true;
						_WPT.circuit = FlowerObject.GetComponent<WaypointCircuit>();
						if (_st.targetTransform != null)
						{
							HealthModule _ehm = _st.targetTransform.gameObject.GetComponent<HealthModule>();

							if (Vector3.Distance(gameObject.transform.position, _st.targetTransform.position) > (_as.radiuse + _h.ShipRadius + _ehm.ShipRadius))
							{
								_mc.Movement(_st.targetTransform.position);
							}
							else
							{
								FlowerObject.transform.localScale = new Vector3(_ehm.ShipRadius, _ehm.ShipRadius, _ehm.ShipRadius);
								FlowerObject.transform.parent = null;
								FlowerObject.transform.position = _st.targetTransform.position;
								if (WayChangeTimer > 0)
								{
									WayChangeTimer -= Time.deltaTime;
								}
								else
								{
									_mc.Movement(_WPT.target.position);
									WayChangeTimer = StartWCT;
								}
							}
						}
						else
						{
							FlowerObject.transform.parent = gameObject.transform;
						}

						BPObject.transform.parent = gameObject.transform;
						RoundObject.transform.parent = gameObject.transform;
					}
					if (CurMane == "DONOTMOVE")
					{
						_WPT.enabled = false;
						_WPT.circuit = RoundObject.GetComponent<WaypointCircuit>();
						if (Vector3.Distance(gameObject.transform.position, _st.targetTransform.position) > (_as.radiuse + _h.ShipRadius + _st.targetTransform.gameObject.GetComponent<HealthModule>().ShipRadius))
						{
							_mc.Movement(_st.targetTransform.position);
						}
						else
						{
							_mc.Stop();
						}

						BPObject.transform.parent = gameObject.transform;
						RoundObject.transform.parent = gameObject.transform;
						FlowerObject.transform.parent = gameObject.transform;
					}
				}
			}
			else
			{
				_WPT.enabled = false;
				_WPT.circuit = RoundObject.GetComponent<WaypointCircuit>();

				BPObject.transform.parent = gameObject.transform;
				RoundObject.transform.parent = gameObject.transform;
				FlowerObject.transform.parent = gameObject.transform;
			}
		}
		else
		{
			_WPT.enabled = false;
			_WPT.circuit = RoundObject.GetComponent<WaypointCircuit>();

			BPObject.transform.parent = gameObject.transform;
			RoundObject.transform.parent = gameObject.transform;
			FlowerObject.transform.parent = gameObject.transform;
		}
	}
}