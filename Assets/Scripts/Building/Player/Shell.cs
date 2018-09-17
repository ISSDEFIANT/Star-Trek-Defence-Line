using UnityEngine;
using System.Collections;
using System.Linq;

public class Shell : MonoBehaviour
{
	public Transform target;
	public GameObject Ship;
	public int damage;
	public int moveSpeed = 1;

	public bool ImpulseSystemAttack;
	public bool LifeSupportSystemAttack;
	public bool PrimaryWeaponSystemAttack;
	public bool SensorsSystemAttack;
	public bool TractorBeamSystemAttack;
	public bool WarpEngingSystemAttack;
	public bool WarpCoreAttack;
	public bool SecondaryWeaponSystemAttack;

	private RaycastHit hit;
	public GameObject Explod;

	public float Timer = 1;

	public bool NotAIM;

	public float Radius;
	public float Fuild;
	private float MaxFuild;
	// Use this for initialization
	void Awake()
	{
		MaxFuild = Fuild;
	}
	void Start()
	{
		Fuild = 2;
	}

	// Update is called once per frame
	void Update()
	{
		if (checkVisible() == false)
		{
			foreach (Transform child in transform)
			{
				child.gameObject.SetActive(false);
			}
		}
		else
		{
			foreach (Transform child in transform)
			{
				child.gameObject.SetActive(true);
			}
		}
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
		if (target != null)
		{
			if (Fuild > 0)
			{
				Vector3 LookVector = (target.transform.position - this.transform.position);
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(LookVector), 0.05f);
				Fuild -= Time.deltaTime;
			}
		}

		if (Timer > 0)
		{
			Timer -= Time.deltaTime;
		}
		else
		{
			if (target != null)
			{
				HealthModule _hp = target.GetComponent<HealthModule>();
				if (_hp.Ship)
				{
					Collider[] colls = Physics.OverlapSphere(transform.position, Radius);
					foreach (Collider coll in colls)
					{
						if (coll.GetComponent<HealthModule>())
						{
							if (_hp.CurShilds <= 0)
							{
								_hp.curHealth -= (damage);
								if (!ImpulseSystemAttack & !LifeSupportSystemAttack & !PrimaryWeaponSystemAttack & !SensorsSystemAttack & !TractorBeamSystemAttack & !WarpEngingSystemAttack & !WarpCoreAttack & !SecondaryWeaponSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(10, damage);
									_hp.curLifeSupportSystemHealth -= Random.Range(10, damage);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(10, damage);
									_hp.curSensorsSystemHealth -= Random.Range(10, damage);
									_hp.curTractorBeamSystemHealth -= Random.Range(10, damage);
									_hp.curWarpEngingSystemHealth -= Random.Range(10, damage);
									_hp.curWarpCoreHealth -= Random.Range(10, damage);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(10, damage);
									if (_hp.curCrew > 0)
									{
										_hp.curCrew -= Random.Range(1, damage / 3);
									}
									else
									{
										_hp.curCrew = 0;
									}
								}
								if (ImpulseSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(20, damage);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage);
									_hp.curWarpCoreHealth -= Random.Range(0, damage);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage);
									if (_hp.curCrew > 0)
									{
										_hp.curCrew -= Random.Range(1, damage / 3);
									}
									else
									{
										_hp.curCrew = 0;
									}
								}
								if (LifeSupportSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage);
									_hp.curLifeSupportSystemHealth -= Random.Range(20, damage);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage);
									_hp.curWarpCoreHealth -= Random.Range(0, damage);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage);
									if (_hp.curCrew > 0)
									{
										_hp.curCrew -= Random.Range(1, damage / 3);
									}
									else
									{
										_hp.curCrew = 0;
									}
								}
								if (PrimaryWeaponSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(20, damage);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage);
									_hp.curWarpCoreHealth -= Random.Range(0, damage);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage);
									if (_hp.curCrew > 0)
									{
										_hp.curCrew -= Random.Range(1, damage / 3);
									}
									else
									{
										_hp.curCrew = 0;
									}
								}
								if (SensorsSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage);
									_hp.curSensorsSystemHealth -= Random.Range(20, damage);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage);
									_hp.curWarpCoreHealth -= Random.Range(0, damage);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage);
									if (_hp.curCrew > 0)
									{
										_hp.curCrew -= Random.Range(1, damage / 3);
									}
									else
									{
										_hp.curCrew = 0;
									}
								}
								if (TractorBeamSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage);
									_hp.curTractorBeamSystemHealth -= Random.Range(20, damage);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage);
									_hp.curWarpCoreHealth -= Random.Range(0, damage);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage);
									if (_hp.curCrew > 0)
									{
										_hp.curCrew -= Random.Range(1, damage / 3);
									}
									else
									{
										_hp.curCrew = 0;
									}
								}
								if (WarpEngingSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage);
									_hp.curWarpEngingSystemHealth -= Random.Range(20, damage);
									_hp.curWarpCoreHealth -= Random.Range(0, damage);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage);
									if (_hp.curCrew > 0)
									{
										_hp.curCrew -= Random.Range(1, damage / 3);
									}
									else
									{
										_hp.curCrew = 0;
									}
								}
								if (WarpCoreAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage);
									_hp.curWarpCoreHealth -= Random.Range(20, damage);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage);
									if (_hp.curCrew > 0)
									{
										_hp.curCrew -= Random.Range(1, damage / 3);
									}
									else
									{
										_hp.curCrew = 0;
									}
								}
								if (SecondaryWeaponSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage);
									_hp.curWarpCoreHealth -= Random.Range(0, damage);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(20, damage);
									if (_hp.curCrew > 0)
									{
										_hp.curCrew -= Random.Range(1, damage / 3);
									}
									else
									{
										_hp.curCrew = 0;
									}
								}
								if (_hp.ActiveImpulse || _hp.curImpulseSystemHealth <= _hp.maxImpulseSystemHealth * 0.125f || _hp.curCrew <= 0)
								{
									if (!_hp.Catched)
									{
										if (target.gameObject.GetComponent<Rigidbody>())
										{
											//	target.gameObject.GetComponent<Rigidbody> ().velocity += gameObject.GetComponent<Rigidbody> ().velocity / 2;
										}
									}
								}
								Instantiate(Explod, transform.position, transform.rotation);
								DestroyAlternative();
							}
							if (_hp.CurShilds > 0)
							{
								_hp.curHealth -= (damage / 5);
								_hp.CurShilds -= (damage / 2);
								if (!ImpulseSystemAttack & !LifeSupportSystemAttack & !PrimaryWeaponSystemAttack & !SensorsSystemAttack & !TractorBeamSystemAttack & !WarpEngingSystemAttack & !WarpCoreAttack & !SecondaryWeaponSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(1, damage / 5);
									_hp.curLifeSupportSystemHealth -= Random.Range(1, damage / 5);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(1, damage / 5);
									_hp.curSensorsSystemHealth -= Random.Range(1, damage / 5);
									_hp.curTractorBeamSystemHealth -= Random.Range(1, damage / 5);
									_hp.curWarpEngingSystemHealth -= Random.Range(1, damage / 5);
									_hp.curWarpCoreHealth -= Random.Range(1, damage / 5);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(1, damage / 5);
								}
								if (ImpulseSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(2, damage / 5);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage / 5);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage / 5);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage / 5);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpCoreHealth -= Random.Range(0, damage / 5);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage / 5);
								}
								if (LifeSupportSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage / 5);
									_hp.curLifeSupportSystemHealth -= Random.Range(2, damage / 5);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage / 5);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage / 5);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpCoreHealth -= Random.Range(0, damage / 5);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage / 5);
								}
								if (PrimaryWeaponSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage / 5);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage / 5);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(2, damage / 5);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage / 5);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpCoreHealth -= Random.Range(0, damage / 5);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage / 5);
								}
								if (SensorsSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage / 5);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage / 5);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage / 5);
									_hp.curSensorsSystemHealth -= Random.Range(2, damage / 5);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpCoreHealth -= Random.Range(0, damage / 5);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage / 5);
								}
								if (TractorBeamSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage / 5);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage / 5);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage / 5);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage / 5);
									_hp.curTractorBeamSystemHealth -= Random.Range(2, damage / 5);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpCoreHealth -= Random.Range(0, damage / 5);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage / 5);
								}
								if (WarpEngingSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage / 5);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage / 5);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage / 5);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage / 5);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpEngingSystemHealth -= Random.Range(2, damage / 5);
									_hp.curWarpCoreHealth -= Random.Range(0, damage / 5);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage / 5);
								}
								if (WarpCoreAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage / 5);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage / 5);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage / 5);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage / 5);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpCoreHealth -= Random.Range(2, damage / 5);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage / 5);
								}
								if (SecondaryWeaponSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage / 5);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage / 5);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage / 5);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage / 5);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpCoreHealth -= Random.Range(0, damage / 5);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(2, damage / 5);
								}
								Instantiate(Explod, transform.position, transform.rotation);
								if (Physics.Raycast(transform.position, transform.position, out hit, 10))
								{
									_hp.ShildModel.GetComponent<Renderer>().enabled = true;
									_hp.ShildModel.GetComponent<Forcefield>().Shot = true;
									_hp.ShildModel.GetComponent<Forcefield>().PhaserHit = hit;
								}
								DestroyAlternative();
							}
						}
					}
				}
				else
				{
					Collider[] colls = Physics.OverlapSphere(transform.position, Radius);
					var healths = colls.Select(col => col.GetComponent<HealthModule>());
					foreach (HealthModule hp in healths)
					{
						if (hp != null)
						{
							if (_hp.CurShilds <= 0)
							{
								_hp.curHealth -= (damage);
								if (!ImpulseSystemAttack & !LifeSupportSystemAttack & !PrimaryWeaponSystemAttack & !SensorsSystemAttack & !TractorBeamSystemAttack & !WarpEngingSystemAttack & !WarpCoreAttack & !SecondaryWeaponSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(10, damage);
									_hp.curLifeSupportSystemHealth -= Random.Range(10, damage);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(10, damage);
									_hp.curSensorsSystemHealth -= Random.Range(10, damage);
									_hp.curTractorBeamSystemHealth -= Random.Range(10, damage);
									_hp.curWarpEngingSystemHealth -= Random.Range(10, damage);
									_hp.curWarpCoreHealth -= Random.Range(10, damage);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(10, damage);
									if (_hp.curCrew > 0)
									{
										_hp.curCrew -= Random.Range(1, damage / 3);
									}
									else
									{
										_hp.curCrew = 0;
									}
								}
								if (ImpulseSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(20, damage);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage);
									_hp.curWarpCoreHealth -= Random.Range(0, damage);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage);
									if (_hp.curCrew > 0)
									{
										_hp.curCrew -= Random.Range(1, damage / 3);
									}
									else
									{
										_hp.curCrew = 0;
									}
								}
								if (LifeSupportSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage);
									_hp.curLifeSupportSystemHealth -= Random.Range(20, damage);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage);
									_hp.curWarpCoreHealth -= Random.Range(0, damage);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage);
									if (_hp.curCrew > 0)
									{
										_hp.curCrew -= Random.Range(1, damage / 3);
									}
									else
									{
										_hp.curCrew = 0;
									}
								}
								if (PrimaryWeaponSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(20, damage);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage);
									_hp.curWarpCoreHealth -= Random.Range(0, damage);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage);
									if (_hp.curCrew > 0)
									{
										_hp.curCrew -= Random.Range(1, damage / 3);
									}
									else
									{
										_hp.curCrew = 0;
									}
								}
								if (SensorsSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage);
									_hp.curSensorsSystemHealth -= Random.Range(20, damage);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage);
									_hp.curWarpCoreHealth -= Random.Range(0, damage);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage);
									if (_hp.curCrew > 0)
									{
										_hp.curCrew -= Random.Range(1, damage / 3);
									}
									else
									{
										_hp.curCrew = 0;
									}
								}
								if (TractorBeamSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage);
									_hp.curTractorBeamSystemHealth -= Random.Range(20, damage);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage);
									_hp.curWarpCoreHealth -= Random.Range(0, damage);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage);
									if (_hp.curCrew > 0)
									{
										_hp.curCrew -= Random.Range(1, damage / 3);
									}
									else
									{
										_hp.curCrew = 0;
									}
								}
								if (WarpEngingSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage);
									_hp.curWarpEngingSystemHealth -= Random.Range(20, damage);
									_hp.curWarpCoreHealth -= Random.Range(0, damage);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage);
									if (_hp.curCrew > 0)
									{
										_hp.curCrew -= Random.Range(1, damage / 3);
									}
									else
									{
										_hp.curCrew = 0;
									}
								}
								if (WarpCoreAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage);
									_hp.curWarpCoreHealth -= Random.Range(20, damage);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage);
									if (_hp.curCrew > 0)
									{
										_hp.curCrew -= Random.Range(1, damage / 3);
									}
									else
									{
										_hp.curCrew = 0;
									}
								}
								if (SecondaryWeaponSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage);
									_hp.curWarpCoreHealth -= Random.Range(0, damage);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(20, damage);
									if (_hp.curCrew > 0)
									{
										_hp.curCrew -= Random.Range(1, damage / 3);
									}
									else
									{
										_hp.curCrew = 0;
									}
								}
								if (_hp.ActiveImpulse || _hp.curImpulseSystemHealth <= _hp.maxImpulseSystemHealth * 0.125f || _hp.curCrew <= 0)
								{
									if (!_hp.Catched)
									{
										if (target.gameObject.GetComponent<Rigidbody>())
										{
											//	target.gameObject.GetComponent<Rigidbody> ().velocity += gameObject.GetComponent<Rigidbody> ().velocity / 2;
										}
									}
								}
								Instantiate(Explod, transform.position, transform.rotation);
								DestroyAlternative();
							}
							if (_hp.CurShilds > 0)
							{
								_hp.curHealth -= (damage / 5);
								_hp.CurShilds -= (damage / 2);
								if (!ImpulseSystemAttack & !LifeSupportSystemAttack & !PrimaryWeaponSystemAttack & !SensorsSystemAttack & !TractorBeamSystemAttack & !WarpEngingSystemAttack & !WarpCoreAttack & !SecondaryWeaponSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(1, damage / 5);
									_hp.curLifeSupportSystemHealth -= Random.Range(1, damage / 5);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(1, damage / 5);
									_hp.curSensorsSystemHealth -= Random.Range(1, damage / 5);
									_hp.curTractorBeamSystemHealth -= Random.Range(1, damage / 5);
									_hp.curWarpEngingSystemHealth -= Random.Range(1, damage / 5);
									_hp.curWarpCoreHealth -= Random.Range(1, damage / 5);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(1, damage / 5);
								}
								if (ImpulseSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(2, damage / 5);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage / 5);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage / 5);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage / 5);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpCoreHealth -= Random.Range(0, damage / 5);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage / 5);
								}
								if (LifeSupportSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage / 5);
									_hp.curLifeSupportSystemHealth -= Random.Range(2, damage / 5);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage / 5);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage / 5);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpCoreHealth -= Random.Range(0, damage / 5);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage / 5);
								}
								if (PrimaryWeaponSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage / 5);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage / 5);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(2, damage / 5);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage / 5);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpCoreHealth -= Random.Range(0, damage / 5);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage / 5);
								}
								if (SensorsSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage / 5);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage / 5);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage / 5);
									_hp.curSensorsSystemHealth -= Random.Range(2, damage / 5);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpCoreHealth -= Random.Range(0, damage / 5);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage / 5);
								}
								if (TractorBeamSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage / 5);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage / 5);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage / 5);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage / 5);
									_hp.curTractorBeamSystemHealth -= Random.Range(2, damage / 5);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpCoreHealth -= Random.Range(0, damage / 5);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage / 5);
								}
								if (WarpEngingSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage / 5);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage / 5);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage / 5);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage / 5);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpEngingSystemHealth -= Random.Range(2, damage / 5);
									_hp.curWarpCoreHealth -= Random.Range(0, damage / 5);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage / 5);
								}
								if (WarpCoreAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage / 5);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage / 5);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage / 5);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage / 5);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpCoreHealth -= Random.Range(2, damage / 5);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(0, damage / 5);
								}
								if (SecondaryWeaponSystemAttack)
								{
									_hp.curImpulseSystemHealth -= Random.Range(0, damage / 5);
									_hp.curLifeSupportSystemHealth -= Random.Range(0, damage / 5);
									_hp.curPrimaryWeaponSystemHealth -= Random.Range(0, damage / 5);
									_hp.curSensorsSystemHealth -= Random.Range(0, damage / 5);
									_hp.curTractorBeamSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpEngingSystemHealth -= Random.Range(0, damage / 5);
									_hp.curWarpCoreHealth -= Random.Range(0, damage / 5);
									_hp.curSecondaryWeaponSystemHealth -= Random.Range(2, damage / 5);
								}
								if (_hp.ActiveImpulse || _hp.curImpulseSystemHealth <= _hp.maxImpulseSystemHealth * 0.125f || _hp.curCrew <= 0)
								{
									if (!_hp.Catched)
									{
										if (target.gameObject.GetComponent<Rigidbody>())
										{
											//		target.gameObject.GetComponent<Rigidbody> ().velocity += gameObject.GetComponent<Rigidbody> ().velocity / 2;
										}
									}
								}
								Instantiate(Explod, transform.position, transform.rotation);
								if (Physics.Raycast(transform.position, transform.position, out hit, 10))
								{
									_hp.ShildModel.GetComponent<Renderer>().enabled = true;
									_hp.ShildModel.GetComponent<Forcefield>().Shot = true;
									_hp.ShildModel.GetComponent<Forcefield>().PhaserHit = hit;
								}
								DestroyAlternative();
							}
						}
					}
				}
			}
		}
	}
	public bool checkVisible()
	{
		if (Camera.main != null)
		{
			return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), transform.gameObject.GetComponent<Collider>().bounds);
		}
		else
		{
			return false;
		}
	}
	void DestroyAlternative()
	{
		DiactivateObject _d = gameObject.GetComponent<DiactivateObject>();
		Timer = 1;
		Fuild = MaxFuild;
		_d.Diactivate();
	}
}