using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixModule : MonoBehaviour
{
	public GameObject RPoint1;
	public GameObject RPoint2;
	public GameObject RPoint3;

	public bool Repair;

	public bool R1;
	public bool R2;
	public bool R3;

	public bool BuildFroze;

	public ShipBuildModule BuildModule;

	public GameObject FixTarget;

	public GameObject WaitingPoint;

	public List<GameObject> ShipsToFix;

	public float AnimationSpeed;

	private GlobalDB _GDB;
	private Select _SEL;

	private float EnteringProcess;
	private Vector3 MovementStart;
	private bool MSPAccepted;

	private Station _sb;

	// Use this for initialization
	void Start()
	{
		_sb = gameObject.GetComponent<Station>();
		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
		_SEL = GameObject.FindGameObjectWithTag("MainUI").GetComponent<Select>();
	}

	// Update is called once per frame
	void Update()
	{
		if (BuildFroze)
		{
			if (FixTarget != null)
			{
				BuildModule.Fixing = true;
			}
			else
			{
				BuildModule.Fixing = false;
			}
		}
		if (FixTarget != null)
		{
			if (FixTarget.GetComponent<Stats>().StopOrder)
			{
				if (R1)
				{
					R1 = false;
					Repair = false;
					FixTarget = null;
				}
				if (R2)
				{
					R2 = false;
					R3 = true;

					EnteringProcess = 0;
					MSPAccepted = false;
				}
			}
			else
			{
				Repair = true;
				if (!R1 && !R2 && !R3) R1 = true;
			}
		}
		else
		{
			R1 = false;
			R2 = false;
			R3 = false;
			Repair = false;

			EnteringProcess = 0;
			MSPAccepted = false;
		}
		if (ShipsToFix.Count > 0)
		{
			CreateWaitingPoints();

			foreach (GameObject obj in ShipsToFix)
			{
				if (obj.GetComponent<Stats>().StopOrder)
				{
					ShipsToFix.Remove(obj);
				}
			}
		}

		if (!_sb.AI && !_sb.FreandAI && !_sb.Neutral && !_sb.NeutralAgrass)
		{
			if (_sb.Hovering)
			{
				if (Input.GetMouseButtonDown(1))
				{
					FindAndAddShips();
				}
			}
		}

		if (Repair)
		{
			if (FixTarget != null)
			{
				MoveComponent _tmc = FixTarget.GetComponent<MoveComponent>();
				HealthModule _SHM = FixTarget.GetComponent<HealthModule>();
				if (R1)
				{
					
					if (Vector3.Distance(FixTarget.transform.position, RPoint1.transform.position) > (_SHM.ShipRadius / 1.5f) + (_tmc.ForwardSpeed * _tmc.ForwardSpeed / 2) / _tmc.MaxAcceleration)
					{
						_tmc.Movement(RPoint1.transform.position);
						if (Vector3.Distance(_tmc.transform.position, RPoint1.transform.position) <= _SHM.ShipRadius)
						{
							Vector3 rpos = RPoint2.transform.position - _tmc.transform.position;

							Quaternion rotation = Quaternion.LookRotation(rpos);

							_tmc.RotateShipAndMoveToTarget(rotation.eulerAngles, RPoint1.transform.position, true, true);
						}
					}
					else
					{
						R2 = true;
						R1 = false;
					}
					FixTarget.GetComponent<Stats>().IsFix = true;
					if (R3)
					{
						R3 = false;
					}
				}
				if (R2)
				{
					gameObject.GetComponent<ShipBuild>().CurOpenTimer = 1;
					if (Vector3.Distance(FixTarget.transform.position, RPoint2.transform.position) > 1)
					{
						FixTarget.GetComponent<Rigidbody>().isKinematic = true;

						Vector3 LookVector = (RPoint2.transform.position - FixTarget.transform.position);
						FixTarget.transform.rotation = Quaternion.Slerp(FixTarget.transform.rotation, Quaternion.LookRotation(LookVector), 0.03f);

						Vector3 dirFromAtoB = (RPoint2.transform.position - FixTarget.transform.position).normalized;
						float dotProd = Vector3.Dot(dirFromAtoB, FixTarget.transform.forward);

						if (dotProd > 0.95)
						{
							if (!MSPAccepted)
							{
								MovementStart = FixTarget.transform.position;
								MSPAccepted = true;
							}

							if (EnteringProcess < 1)
							{
								EnteringProcess += Time.deltaTime * (1 / FixTarget.GetComponent<MoveComponent>().MovementSpeed * AnimationSpeed);
							}
							else
							{
								EnteringProcess = 1;
							}

							FixTarget.transform.position = Vector3.Lerp(MovementStart, RPoint2.transform.position, EnteringProcess);
						}

						gameObject.GetComponent<ShipBuild>().CurOpenTimer = 1;
					}
					if (Vector3.Distance(FixTarget.transform.position, RPoint2.transform.position) <= 1)
					{
						EnteringProcess = 0;
						MSPAccepted = false;

						Vector3 LookVector = (FixTarget.transform.position - RPoint2.transform.position);
						FixTarget.transform.rotation = Quaternion.Slerp(FixTarget.transform.rotation, Quaternion.LookRotation(LookVector), 0.03f);

						if (_SHM.curHealth < _SHM.maxHealth)
						{
							if (_GDB.Titanium > 0)
							{
								_GDB.Titanium -= Time.deltaTime * 2;
								if (_SHM.curHealth < _SHM.maxHealth)
								{
									_SHM.curHealth += Time.deltaTime * 2;
								}
								else
								{
									_SHM.curHealth = _SHM.maxHealth;
								}

								if (_SHM.curImpulseSystemHealth < _SHM.maxImpulseSystemHealth)
								{
									_SHM.curImpulseSystemHealth += Time.deltaTime * 2;
								}
								else
								{
									_SHM.curImpulseSystemHealth = _SHM.maxImpulseSystemHealth;
								}

								if (_SHM.curLifeSupportSystemHealth < _SHM.maxLifeSupportSystemHealth)
								{
									_SHM.curLifeSupportSystemHealth += Time.deltaTime * 2;
								}
								else
								{
									_SHM.curLifeSupportSystemHealth = _SHM.maxLifeSupportSystemHealth;
								}

								if (_SHM.curPrimaryWeaponSystemHealth < _SHM.maxPrimaryWeaponSystemHealth)
								{
									_SHM.curPrimaryWeaponSystemHealth += Time.deltaTime * 2;
								}
								else
								{
									_SHM.curPrimaryWeaponSystemHealth = _SHM.maxPrimaryWeaponSystemHealth;
								}

								if (_SHM.curSensorsSystemHealth < _SHM.maxSensorsSystemHealth)
								{
									_SHM.curSensorsSystemHealth += Time.deltaTime * 2;
								}
								else
								{
									_SHM.curSensorsSystemHealth = _SHM.maxSensorsSystemHealth;
								}

								if (_SHM.curTractorBeamSystemHealth < _SHM.maxTractorBeamSystemHealth)
								{
									_SHM.curTractorBeamSystemHealth += Time.deltaTime * 2;
								}
								else
								{
									_SHM.curTractorBeamSystemHealth = _SHM.maxTractorBeamSystemHealth;
								}

								if (_SHM.curWarpEngingSystemHealth < _SHM.maxWarpEngingSystemHealth)
								{
									_SHM.curWarpEngingSystemHealth += Time.deltaTime * 2;
								}
								else
								{
									_SHM.curWarpEngingSystemHealth = _SHM.maxWarpEngingSystemHealth;
								}

								if (_SHM.curWarpCoreHealth < _SHM.maxWarpCoreHealth)
								{
									_SHM.curWarpCoreHealth += Time.deltaTime * 2;
								}
								else
								{
									_SHM.curWarpCoreHealth = _SHM.maxWarpCoreHealth;
								}

								if (_SHM.curSecondaryWeaponSystemHealth < _SHM.maxSecondaryWeaponSystemHealth)
								{
									_SHM.curSecondaryWeaponSystemHealth += Time.deltaTime * 2;
								}
								else
								{
									_SHM.curSecondaryWeaponSystemHealth = _SHM.maxSecondaryWeaponSystemHealth;
								}

								if (_SHM.CurСилаПоля < _SHM.СилаПоля)
								{
									_SHM.CurСилаПоля += Time.deltaTime * 20;
								}
								else
								{
									_SHM.CurСилаПоля = _SHM.СилаПоля;
								}

								if (_GDB.Humans > 0)
								{
									if (_SHM.curCrew < _SHM.maxCrew)
									{
										_GDB.Humans -= Time.deltaTime * 4;
										_SHM.curCrew += Time.deltaTime * 4;
									}
									else
									{
										_SHM.curCrew = _SHM.maxCrew;
									}
								}

								if (_SHM.curHealth >= _SHM.maxHealth && _SHM.curImpulseSystemHealth >= _SHM.maxImpulseSystemHealth && _SHM.curLifeSupportSystemHealth >= _SHM.maxLifeSupportSystemHealth && _SHM.curPrimaryWeaponSystemHealth >= _SHM.maxPrimaryWeaponSystemHealth && _SHM.curTractorBeamSystemHealth >= _SHM.maxTractorBeamSystemHealth && _SHM.curWarpEngingSystemHealth >= _SHM.maxWarpEngingSystemHealth && _SHM.curWarpCoreHealth >= _SHM.maxWarpCoreHealth && _SHM.curSecondaryWeaponSystemHealth >= _SHM.maxSecondaryWeaponSystemHealth && _SHM.curCrew >= _SHM.maxCrew && _SHM.CurСилаПоля >= _SHM.СилаПоля)
								{
									R3 = true;
									R2 = false;
								}
							}
							else
							{
								R3 = true;
								R2 = false;
							}
						}
					}
					FixTarget.GetComponent<Stats>().IsFix = true;
				}
				if (R3)
				{
					if (Vector3.Distance(FixTarget.transform.position, RPoint3.transform.position) > 1)
					{
						Vector3 LookVector = (RPoint3.transform.position - FixTarget.transform.position);
						FixTarget.transform.rotation = Quaternion.Slerp(FixTarget.transform.rotation, Quaternion.LookRotation(LookVector), 0.03f);

						Vector3 dirFromAtoB = (RPoint3.transform.position - FixTarget.transform.position).normalized;
						float dotProd = Vector3.Dot(dirFromAtoB, FixTarget.transform.forward);

						if (dotProd > 0.95)
						{
							if (!MSPAccepted)
							{
								MovementStart = FixTarget.transform.position;
								MSPAccepted = true;
							}

							if (EnteringProcess < 1)
							{
								EnteringProcess += Time.deltaTime * (1 / FixTarget.GetComponent<MoveComponent>().MovementSpeed * AnimationSpeed);
							}
							else
							{
								EnteringProcess = 1;
							}

							FixTarget.transform.position = Vector3.Lerp(MovementStart, RPoint3.transform.position, EnteringProcess);
						}

						gameObject.GetComponent<ShipBuild>().CurOpenTimer = 1;
					}
					if (Vector3.Distance(FixTarget.transform.position, RPoint3.transform.position) <= 1)
					{
						EnteringProcess = 0;
						MSPAccepted = false;

						FixTarget.GetComponent<Rigidbody>().isKinematic = false;

						FixTarget.GetComponent<Stats>().SelectLock = false;
						if (FindInFixList(FixTarget)) ShipsToFix.Remove(FixTarget);
						FixTarget = null;
						Repair = false;
						R3 = false;
					}
					FixTarget.GetComponent<Stats>().IsFix = false;
				}
			}
			else
			{
				Repair = false;
			}
		}
	}
	bool FindInFixList(GameObject obj)
	{
		foreach (GameObject selObj in ShipsToFix)
		{
			if (selObj == obj)
				return true;
		}
		return false;
	}

	void CreateWaitingPoints()
	{
		if (FixTarget == null)
		{
			FixTarget = ShipsToFix[0];
		}

		Vector3 waitingPointsOrigin = WaitingPoint.transform.position;                  // начальная точка, откуда начинается очередь
		Vector3 rotationVector = WaitingPoint.transform.rotation * Vector3.forward;     // нормаль, вдоль которой будут строиться корабли (длина 1 метр/единица)
		Vector3 waitingPointOffset = waitingPointsOrigin;                                   // временная точка которая как раз и поможет выстраивать корабли, при этом не нужно лишний раз лезть к предыдущему кораблю

		for (int i = 1; i < ShipsToFix.Count; i++)      //цикл по кораблям - можно переписать на foreach
		{
			float shipRadius = ShipsToFix[i].GetComponent<HealthModule>().ShipRadius;   // нам нужен только радиус корабля

			if (i == 1)                                 // если это первый в ожидании корабль
			{                                           // то говорим тчо он должен стоять на начальной точке
				waitingPointOffset = waitingPointsOrigin;
			}
			else                                        // иначе
			{                                           // от предыдущей границы мы отобдвинемся на радиус корабля
				waitingPointOffset = waitingPointOffset + rotationVector * (shipRadius + 5);
			}

			MoveComponent _mc = ShipsToFix[i].GetComponent<MoveComponent>();


			_mc.Movement(new Vector3(waitingPointOffset.x, gameObject.transform.position.y, waitingPointOffset.z)); // говорим лететь

			if (Vector3.Distance(_mc.transform.position, new Vector3(waitingPointOffset.x, gameObject.transform.position.y, waitingPointOffset.z)) <= ShipsToFix[i].GetComponent<HealthModule>().ShipRadius)
			{
				if (i == 1)
				{
					Vector3 rpos = RPoint1.transform.position - _mc.transform.position;

					Quaternion rotation = Quaternion.LookRotation(rpos);

					_mc.RotateShipAndMoveToTarget(rotation.eulerAngles, new Vector3(waitingPointOffset.x, gameObject.transform.position.y, waitingPointOffset.z), true, true);
				}
				else
				{
					Vector3 rpos = WaitingPoint.transform.position - _mc.transform.position;

					Quaternion rotation = Quaternion.LookRotation(rpos);

					_mc.RotateShipAndMoveToTarget(rotation.eulerAngles, new Vector3(waitingPointOffset.x, gameObject.transform.position.y, waitingPointOffset.z), true, true);
				}
			}

			waitingPointOffset = waitingPointOffset + rotationVector * (shipRadius + 5);    // отодвигаем границу ещё на один радиус ткущего корабля
		}	}

	void FindAndAddShips()
	{
		if (_GDB.selectList.Count > 0)
		{
			foreach (GameObject obj in _GDB.selectList)
			{
				HealthModule _SHM = obj.GetComponent<HealthModule>();
				if (!_SHM._st.AI && !_SHM._st.FreandAI && !_SHM._st.Neutral && !_SHM._st.NeutralAgrass)
				{
					if (_SHM.curHealth < _SHM.maxHealth || _SHM.curImpulseSystemHealth < _SHM.maxImpulseSystemHealth || _SHM.curLifeSupportSystemHealth < _SHM.maxLifeSupportSystemHealth || _SHM.curPrimaryWeaponSystemHealth < _SHM.maxPrimaryWeaponSystemHealth || _SHM.curTractorBeamSystemHealth < _SHM.maxTractorBeamSystemHealth || _SHM.curWarpEngingSystemHealth < _SHM.maxWarpEngingSystemHealth || _SHM.curWarpCoreHealth < _SHM.maxWarpCoreHealth || _SHM.curSecondaryWeaponSystemHealth < _SHM.maxSecondaryWeaponSystemHealth || _SHM.curCrew < _SHM.maxCrew || _SHM.CurСилаПоля < _SHM.СилаПоля)
					{
						if (!FindInFixList(obj)) ShipsToFix.Add(obj);
						obj.GetComponent<MoveComponent>().CurFleet.Clear();
						_SEL.PlayFixSound(gameObject);
					}
				}
			}
		}
	}
}