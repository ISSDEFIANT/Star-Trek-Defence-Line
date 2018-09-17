using UnityEngine;
using System.Collections;

public class TopidoLounser : MonoBehaviour
{
	public bool Torpidos;
	public bool Disraptors;

	public GameObject Owner;

	public float radiuse;
	public float TorpidoRange;
	public float timer;
	public bool Active;
	public GameObject Shell;
	public int maxTorpidos;
	public int curTorpidos;
	public float ReloadTime;
	public float RTimer;

	private HealthModule _h;

	private bool IsShip;
	private Stats _st;
	private ActiveState _as;
	private Maneuvers _man;

	private bool IsStation;
	private Station _sb;
	private WeaponModule _sbwm;

	private Shell _sl;


	public bool moove;

	public float Delay;
	private float SelfDelay;

	public GameObject[] AllTorpedose;
	private int poolSize;

	private HealthModule _hp2;

	public bool InvertX;

	public float MinX;
	public float MaxX;

	public bool InvertY;

	public float MinY;
	public float MaxY;

	public bool CanFireMirror;
	// Use this for initialization
	void Awake()
	{
		poolSize = maxTorpidos;
	}

	void Start()
	{
		SelfDelay = Delay;
		_h = Owner.GetComponent<HealthModule>();
		if (Owner.GetComponent<Stats>())
		{
			_st = Owner.GetComponent<Stats>();
			_as = Owner.GetComponent<ActiveState>();
			_man = Owner.GetComponent<Maneuvers>();
			IsShip = true;
			IsStation = false;
		}
		if (Owner.GetComponent<Station>())
		{
			_sb = Owner.GetComponent<Station>();
			_sbwm = Owner.GetComponent<WeaponModule>();
			IsShip = false;
			IsStation = true;
		}
		moove = true;

		AllTorpedose = new GameObject[poolSize];
		for (int i = 0; i < poolSize; i++)
		{
			AllTorpedose[i] = Instantiate(Shell) as GameObject;
			AllTorpedose[i].SetActive(false);
		}
	}
	// Update is called once per frame
	void Update()
	{
		CanFireMirror = CanFire();

		if (IsShip)
		{
			if (Disraptors)
			{
				if (curTorpidos == maxTorpidos)
				{
					_man.WeaponIsReady = true;
				}
				if (_man.OpenTheFire)
				{
					if (curTorpidos == 0)
					{
						_man.FireCompleate = true;
					}
				}
			}
			if (_as.isAttack && _st.targetTransform != null)
			{
				if (Vector3.Distance(_st.targetTransform.position, gameObject.transform.position) > (radiuse))
				{
					Active = false;
				}
				if (Vector3.Distance(_st.targetTransform.position, gameObject.transform.position) <= (radiuse))
				{
					Active = true;
				}
			}
			if (_st.targetTransform == null)
			{
				Active = false;
			}
			if (CanFire())
			{
				if (curTorpidos > 0)
				{
					if (_h.maxCrew > 0)
					{
						if (_h.curSecondaryWeaponSystemHealth > _h.maxSecondaryWeaponSystemHealth * 0.125f && !_h.ActiveSecondaryWeapon && _h.curCrew > 0)
						{
							if (timer > 0)
							{
								timer -= Time.deltaTime;
							}
							if (timer <= 0)
							{
								if (Active)
								{
									if (Torpidos)
									{
										if (Vector3.Distance(_st.targetTransform.position, gameObject.transform.position) <= radiuse)
										{
											InstantiateAlternative();
											curTorpidos -= 1;
											timer = TorpidoRange;
										}
									}
									if (Disraptors)
									{
										if (Vector3.Distance(_st.targetTransform.position, gameObject.transform.position) <= radiuse)
										{
											if (Delay > 0 && curTorpidos == maxTorpidos)
											{
												Delay -= Time.deltaTime;
											}
											else
											{
												if (_man.OpenTheFire)
												{
													InstantiateAlternative();
													curTorpidos -= 1;
													timer = TorpidoRange;

													Delay = SelfDelay;
												}
											}
										}
									}
								}
							}
						}
					}
					else
					{
						if (_h.curSecondaryWeaponSystemHealth > _h.maxSecondaryWeaponSystemHealth * 0.125f && !_h.ActiveSecondaryWeapon)
						{
							if (timer > 0)
							{
								timer -= Time.deltaTime;
							}
							if (timer <= 0)
							{
								if (Active)
								{
									if (Torpidos)
									{
										if (Vector3.Distance(_st.targetTransform.position, gameObject.transform.position) <= radiuse)
										{
											InstantiateAlternative();
											curTorpidos -= 1;
											timer = TorpidoRange;
										}
									}
									if (Disraptors)
									{
										if (Vector3.Distance(_st.targetTransform.position, gameObject.transform.position) <= radiuse)
										{
											if (Delay > 0 && curTorpidos == maxTorpidos)
											{
												Delay -= Time.deltaTime;
											}
											else
											{
												if (_man.OpenTheFire)
												{
													InstantiateAlternative();
													curTorpidos -= 1;
													timer = TorpidoRange;

													Delay = SelfDelay;
												}
											}
										}
									}
								}
							}
						}
					}
				}
				if (curTorpidos <= 0)
				{
					if (RTimer > 0)
					{
						RTimer -= Time.deltaTime;
					}
					if (RTimer <= 0)
					{
						curTorpidos = maxTorpidos;
						RTimer = ReloadTime;
					}
				}
			}
			if (_st.targetTransform != null)
			{
				_hp2 = _st.targetTransform.gameObject.GetComponent<HealthModule>();
				radiuse = _h.ShipRadius + _as.radiuse + _hp2.ShipRadius + 50;
			}
		}

		if (IsStation)
		{
			if (_sbwm.isAttack && _sbwm.target != null)
			{
				if (Vector3.Distance(_sbwm.target.transform.position, gameObject.transform.position) > (radiuse))
				{
					Active = false;
				}
				if (Vector3.Distance(_sbwm.target.transform.position, gameObject.transform.position) <= (radiuse))
				{
					Active = true;
				}
			}
			if (_sbwm.target == null)
			{
				Active = false;
			}
			if (CanFire())
			{
				if (curTorpidos > 0)
				{
					if (_h.maxCrew > 0)
					{
						if (_h.curSecondaryWeaponSystemHealth > _h.maxSecondaryWeaponSystemHealth * 0.125f && !_h.ActiveSecondaryWeapon && _h.curCrew > 0)
						{
							if (timer > 0)
							{
								timer -= Time.deltaTime;
							}
							if (timer <= 0)
							{
								
								if (Active)
								{
									if (Torpidos)
									{
										if (Vector3.Distance(_sbwm.target.transform.position, gameObject.transform.position) <= radiuse)
										{
											InstantiateAlternative();
											curTorpidos -= 1;
											timer = TorpidoRange;
										}
									}
									if (Disraptors)
									{
										if (Vector3.Distance(_sbwm.target.transform.position, gameObject.transform.position) <= radiuse)
										{
											if (Delay > 0 && curTorpidos == maxTorpidos)
											{
												Delay -= Time.deltaTime;
											}
											else
											{
												if (_man.OpenTheFire)
												{
													InstantiateAlternative();
													curTorpidos -= 1;
													timer = TorpidoRange;

													Delay = SelfDelay;
												}
											}
										}
									}
								}
							}
						}
					}
					else
					{
						if (_h.curSecondaryWeaponSystemHealth > _h.maxSecondaryWeaponSystemHealth * 0.125f && !_h.ActiveSecondaryWeapon)
						{
							if (timer > 0)
							{
								timer -= Time.deltaTime;
							}
							if (timer <= 0)
							{
								if (Active)
								{
									if (Torpidos)
									{
										if (Vector3.Distance(_sbwm.target.transform.position, gameObject.transform.position) <= radiuse)
										{
											InstantiateAlternative();
											curTorpidos -= 1;
											timer = TorpidoRange;
										}
									}
									if (Disraptors)
									{
										if (Vector3.Distance(_sbwm.target.transform.position, gameObject.transform.position) <= radiuse)
										{
											if (Delay > 0 && curTorpidos == maxTorpidos)
											{
												Delay -= Time.deltaTime;
											}
											else
											{
												if (_man.OpenTheFire)
												{
													InstantiateAlternative();
													curTorpidos -= 1;
													timer = TorpidoRange;

													Delay = SelfDelay;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if (curTorpidos <= 0)
			{
				if (RTimer > 0)
				{
					RTimer -= Time.deltaTime;
				}
				if (RTimer <= 0)
				{
					curTorpidos = maxTorpidos;
					RTimer = ReloadTime;
				}
			}
			if (_sbwm.target != null)
			{
				_hp2 = _sbwm.target.GetComponent<HealthModule>();
				radiuse = _h.ShipRadius + _sbwm.radiuse + _hp2.ShipRadius + 50;
			}
		}

		if (moove)
		{
			if (IsShip)
			{
				if (_st.targetTransform != null)
				{
					if (_hp2.Ship)
					{
						if (Torpidos)
						{
							if (_st.targetTransform.gameObject.GetComponent<MoveComponent>())
							{
								Vector3 LookVector = ((_st.targetTransform.position + (_st.targetTransform.gameObject.GetComponent<Rigidbody>().velocity * Shell.GetComponent<Shell>().moveSpeed / 3)) - this.transform.position);
								this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(LookVector), 360);
							}
							else
							{
								Vector3 LookVector = ((_st.targetTransform.position + (_st.targetTransform.gameObject.GetComponent<Rigidbody>().velocity * Shell.GetComponent<Shell>().moveSpeed / 3)) - this.transform.position);
								this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(LookVector), 360);
							}
						}
						if (Disraptors)
						{
							if (_st.targetTransform.gameObject.GetComponent<MoveComponent>())
							{
								Vector3 LookVector = ((_st.targetTransform.position + (_st.targetTransform.gameObject.GetComponent<Rigidbody>().velocity * Shell.GetComponent<Disrapter>().speed / 3)) - this.transform.position);
								this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(LookVector), 360);
							}
							else
							{
								Vector3 LookVector = ((_st.targetTransform.position + (_st.targetTransform.gameObject.GetComponent<Rigidbody>().velocity * Shell.GetComponent<Disrapter>().speed / 3)) - this.transform.position);
								this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(LookVector), 360);
							}
						}
					}
					else
					{
						if (_st.targetTransform != null)
						{
							Vector3 LookVector = (_st.targetTransform.position - this.transform.position);
							this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(LookVector), 360);
						}
					}
				}
			}
			if (IsStation)
			{
				if (_sbwm.target != null)
				{
					if (_hp2.Ship)
					{
						if (Torpidos)
						{
							if (_sbwm.target.GetComponent<MoveComponent>())
							{
								Vector3 LookVector = ((_sbwm.target.transform.position + (_sbwm.target.GetComponent<Rigidbody>().velocity * Shell.GetComponent<Shell>().moveSpeed / 3)) - this.transform.position);
								this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(LookVector), 360);
							}
							else
							{
								Vector3 LookVector = ((_sbwm.target.transform.position + (_sbwm.target.GetComponent<Rigidbody>().velocity * Shell.GetComponent<Shell>().moveSpeed / 3)) - this.transform.position);
								this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(LookVector), 360);
							}
						}
						if (Disraptors)
						{
							if (_st.targetTransform.gameObject.GetComponent<MoveComponent>())
							{
								Vector3 LookVector = ((_sbwm.target.transform.position + (_sbwm.target.GetComponent<Rigidbody>().velocity * Shell.GetComponent<Disrapter>().speed / 3)) - this.transform.position);
								this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(LookVector), 360);
							}
							else
							{
								Vector3 LookVector = ((_sbwm.target.transform.position + (_sbwm.target.GetComponent<Rigidbody>().velocity * Shell.GetComponent<Disrapter>().speed / 3)) - this.transform.position);
								this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(LookVector), 360);
							}
						}
					}
					else
					{
						if (_sbwm.target != null)
						{
							Vector3 LookVector = (_sbwm.target.transform.position - this.transform.position);
							this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(LookVector), 360);
						}
					}
				}
			}
		}
	}

	public void InstantiateAlternative()
	{
		if (IsShip)
		{
			if (Torpidos)
			{
				for (int i = 0; i < poolSize; i++)
				{
					if (AllTorpedose[i].activeInHierarchy == false)
					{
						AllTorpedose[i].SetActive(true);
						AllTorpedose[i].transform.position = gameObject.transform.position;
						AllTorpedose[i].transform.rotation = gameObject.transform.rotation;
						Shell _s = AllTorpedose[i].GetComponent<Shell>();
						_s.target = _st.targetTransform.transform;
						_s.Ship = Owner;

					    switch (_as.TargetingAt)
					    {
					        case ActiveState.AttackType.PrimaryWeaponSystemAttack:
					            _s.PrimaryWeaponSystemAttack = true;
                                break;
					        case ActiveState.AttackType.SecondaryWeaponSystemAttack:
					            _s.SecondaryWeaponSystemAttack = true;
                                break;
					        case ActiveState.AttackType.ImpulseSystemAttack:
					            _s.ImpulseSystemAttack = true;
                                break;
					        case ActiveState.AttackType.WarpEngingSystemAttack:
					            _s.WarpEngingSystemAttack = true;
                                break;
					        case ActiveState.AttackType.WarpCoreAttack:
					            _s.WarpCoreAttack = true;
                                break;
					        case ActiveState.AttackType.LifeSupportSystemAttack:
					            _s.LifeSupportSystemAttack = true;
                                break;
					        case ActiveState.AttackType.SensorsSystemAttack:
					            _s.SensorsSystemAttack = true;
                                break;
					        case ActiveState.AttackType.TractorBeamSystemAttack:
					            _s.TractorBeamSystemAttack = true;
                                break;
					    }
						return;
					}
				}
			}
			if (Disraptors)
			{
				for (int i = 0; i < poolSize; i++)
				{
					if (AllTorpedose[i].activeInHierarchy == false)
					{
						AllTorpedose[i].SetActive(true);
						AllTorpedose[i].transform.position = gameObject.transform.position;
						AllTorpedose[i].transform.rotation = gameObject.transform.rotation;
						Disrapter _d = AllTorpedose[i].GetComponent<Disrapter>();
						_d.target = _st.targetTransform.transform;
						_d.Ship = Owner;
					    switch (_as.TargetingAt)
					    {
					        case ActiveState.AttackType.PrimaryWeaponSystemAttack:
					            _d.PrimaryWeaponSystemAttack = true;
					            break;
					        case ActiveState.AttackType.SecondaryWeaponSystemAttack:
					            _d.SecondaryWeaponSystemAttack = true;
					            break;
					        case ActiveState.AttackType.ImpulseSystemAttack:
					            _d.ImpulseSystemAttack = true;
					            break;
					        case ActiveState.AttackType.WarpEngingSystemAttack:
					            _d.WarpEngingSystemAttack = true;
					            break;
					        case ActiveState.AttackType.WarpCoreAttack:
					            _d.WarpCoreAttack = true;
					            break;
					        case ActiveState.AttackType.LifeSupportSystemAttack:
					            _d.LifeSupportSystemAttack = true;
					            break;
					        case ActiveState.AttackType.SensorsSystemAttack:
					            _d.SensorsSystemAttack = true;
					            break;
					        case ActiveState.AttackType.TractorBeamSystemAttack:
					            _d.TractorBeamSystemAttack = true;
					            break;
					    }
                        return;
					}
				}
			}
		}
		if (IsStation)
		{
			if (Torpidos)
			{
				for (int i = 0; i < poolSize; i++)
				{
					if (AllTorpedose[i].activeInHierarchy == false)
					{
						AllTorpedose[i].SetActive(true);
						AllTorpedose[i].transform.position = gameObject.transform.position;
						AllTorpedose[i].transform.rotation = gameObject.transform.rotation;
						Shell _s = AllTorpedose[i].GetComponent<Shell>();
						_s.target = _sbwm.target.transform;
						_s.Ship = Owner;
						if (_sbwm.ImpulseSystemAttack)
						{
							_s.ImpulseSystemAttack = true;
						}
						if (_sbwm.LifeSupportSystemAttack)
						{
							_s.LifeSupportSystemAttack = true;
						}
						if (_sbwm.PrimaryWeaponSystemAttack)
						{
							_s.PrimaryWeaponSystemAttack = true;
						}
						if (_sbwm.SensorsSystemAttack)
						{
							_s.SensorsSystemAttack = true;
						}
						if (_sbwm.TractorBeamSystemAttack)
						{
							_s.TractorBeamSystemAttack = true;
						}
						if (_sbwm.WarpEngingSystemAttack)
						{
							_s.WarpEngingSystemAttack = true;
						}
						if (_sbwm.WarpCoreAttack)
						{
							_s.WarpCoreAttack = true;
						}
						if (_sbwm.SecondaryWeaponSystemAttack)
						{
							_s.SecondaryWeaponSystemAttack = true;
						}
						return;
					}
				}
			}
			if (Disraptors)
			{
				for (int i = 0; i < poolSize; i++)
				{
					if (AllTorpedose[i].activeInHierarchy == false)
					{
						AllTorpedose[i].SetActive(true);
						AllTorpedose[i].transform.position = gameObject.transform.position;
						AllTorpedose[i].transform.rotation = gameObject.transform.rotation;
						Disrapter _d = AllTorpedose[i].GetComponent<Disrapter>();
						_d.target = _sbwm.target.transform;
						_d.Ship = Owner;
						if (_sbwm.ImpulseSystemAttack)
						{
							_d.ImpulseSystemAttack = true;
						}
						if (_sbwm.LifeSupportSystemAttack)
						{
							_d.LifeSupportSystemAttack = true;
						}
						if (_sbwm.PrimaryWeaponSystemAttack)
						{
							_d.PrimaryWeaponSystemAttack = true;
						}
						if (_sbwm.SensorsSystemAttack)
						{
							_d.SensorsSystemAttack = true;
						}
						if (_sbwm.TractorBeamSystemAttack)
						{
							_d.TractorBeamSystemAttack = true;
						}
						if (_sbwm.WarpEngingSystemAttack)
						{
							_d.WarpEngingSystemAttack = true;
						}
						if (_sbwm.WarpCoreAttack)
						{
							_d.WarpCoreAttack = true;
						}
						if (_sbwm.SecondaryWeaponSystemAttack)
						{
							_d.SecondaryWeaponSystemAttack = true;
						}
						return;
					}
				}
			}
		}
	}

	bool CanFire()
	{
		if (InvertX && !InvertY)
		{
			if ((gameObject.transform.localRotation.eulerAngles.x < MinX || gameObject.transform.localRotation.eulerAngles.x > MaxX) && gameObject.transform.localRotation.eulerAngles.y > MinY && gameObject.transform.localRotation.eulerAngles.y < MaxY)
			{
				return true;
			}
			return false;
		}
		if (InvertY && !InvertX)
		{
			if (gameObject.transform.localRotation.eulerAngles.x > MinX && gameObject.transform.localRotation.eulerAngles.x < MaxX && (gameObject.transform.localRotation.eulerAngles.y < MinY || gameObject.transform.localRotation.eulerAngles.y > MaxY))
			{
				return true;
			}
			return false;
		}
		if (InvertX && InvertY)
		{
			if ((gameObject.transform.localRotation.eulerAngles.x < MinX || gameObject.transform.localRotation.eulerAngles.x > MaxX) && (gameObject.transform.localRotation.eulerAngles.y < MinY || gameObject.transform.localRotation.eulerAngles.y > MaxY))
			{
				return true;
			}
			return false;
		}
		if (!InvertX && !InvertY)
		{
			if (gameObject.transform.localRotation.eulerAngles.x > MinX && gameObject.transform.localRotation.eulerAngles.x < MaxX && gameObject.transform.localRotation.eulerAngles.y > MinY && gameObject.transform.localRotation.eulerAngles.y < MaxY)
			{
				return true;
			}
			return false;
		}
		return false;
	}
}