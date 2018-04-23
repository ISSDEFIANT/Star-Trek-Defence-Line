using UnityEngine;
using System.Collections;

public class Phaser : MonoBehaviour
{
	private ArcReactor_Launcher _PGS;

	public GameObject Owner;
	public Transform target;
	public float ReloadTime;
	public float ReloadTimer;
	public bool DamageToTarget;
	public float Damage;
	public float DamageTimer;
	public float DamageTime;

	private HealthModule _hm;

	private Stats _st;
	private ActiveState _as;

	private WeaponModule _sbwm;

	private bool starship;
	private bool starbase;

	public GameObject Hit;

	public GameObject RayStart;

	public AudioClip Sound;
	private AudioSource _a;

	public bool InvertX;

	public float MinX;
	public float MaxX;

	public bool InvertY;

	public float MinY;
	public float MaxY;

	public bool CanFireMirror;

	public bool DebugActive;

	// Use this for initialization
	void Start()
	{
		_PGS = gameObject.GetComponent<ArcReactor_Launcher>();

		_hm = Owner.GetComponent<HealthModule>();
		if (Owner.GetComponent<WeaponModule>())
		{
			_sbwm = Owner.GetComponent<WeaponModule>();
			starship = false;
			starbase = true;
		}
		if (Owner.GetComponent<Stats>())
		{
			_st = Owner.GetComponent<Stats>();
			_as = Owner.GetComponent<ActiveState>();
			starship = true;
			starbase = false;
		}
		if (gameObject.GetComponent<AudioSource>())
		{
			_a = gameObject.GetComponent<AudioSource>();
			_a.maxDistance = 1000;
			_a.spatialBlend = 0.8f;
		}
	}

	// Update is called once per frame
	void Update()
	{
		CanFireMirror = CanFire();

		if (starbase)
		{
			if (ReloadTimer > 0)
			{
				ReloadTimer -= Time.deltaTime;
			}



			if (_sbwm.target != null)
			{
				target = _sbwm.target.transform;

				HealthModule EnemyHM = _sbwm.target.GetComponent<HealthModule>();

				Vector3 LookVector = (EnemyHM.Mesh.transform.position - this.transform.position);

				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(LookVector), 360);

				if (CanFire())
				{
					if (_hm.maxCrew > 0)
					{
						if (_hm.curPrimaryWeaponSystemHealth > _hm.maxPrimaryWeaponSystemHealth * 0.125f && _hm.curCrew > 0)
						{
							if (_sbwm.isAttack)
							{
								if (ReloadTimer <= 0)
								{
									RaycastHit hit;
									RayStart = gameObject;
									if (Physics.Raycast(_sbwm.target.transform.position, _sbwm.transform.position, out hit, 1000))
									{
									}
									if (EnemyHM.CurСилаПоля > 0)
									{
										EnemyHM.Поле.GetComponent<Renderer>().enabled = true;
										EnemyHM.Поле.GetComponent<Forcefield>().Shot = true;
										EnemyHM.Поле.GetComponent<Forcefield>().PhaserHit = hit;
									}
									_PGS.PhaserFire(EnemyHM.Mesh.transform);
									_a.clip = Sound;
									_a.Play();
									DamageToTarget = true;
									ReloadTimer = ReloadTime;
								}
							}
							if (DamageToTarget)
							{
								if (DamageTimer > 0)
								{
									DamageTimer -= Time.deltaTime;
									if (EnemyHM.CurСилаПоля <= 0)
									{
										EnemyHM.curHealth -= Time.deltaTime * Damage;
									}
									else
									{
										EnemyHM.CurСилаПоля -= Time.deltaTime * Damage;
									}
								}
								if (DamageTimer <= 0)
								{
									DamageTimer = DamageTime;
									DamageToTarget = false;
								}
							}
						}
					}
					else
					{
						if (_hm.curPrimaryWeaponSystemHealth > _hm.maxPrimaryWeaponSystemHealth * 0.125f)
						{
							if (_sbwm.isAttack)
							{
								if (ReloadTimer <= 0)
								{
									RaycastHit hit;
									RayStart = gameObject;
									if (Physics.Raycast(_sbwm.target.transform.position, _sbwm.target.transform.position, out hit, 1000))
									{
									}
									if (EnemyHM.CurСилаПоля > 0)
									{
										EnemyHM.Поле.GetComponent<Renderer>().enabled = true;
										EnemyHM.Поле.GetComponent<Forcefield>().Shot = true;
										EnemyHM.Поле.GetComponent<Forcefield>().PhaserHit = hit;
									}
									_PGS.PhaserFire(EnemyHM.Mesh.transform);

									if (Sound != null)
									{
										_a.clip = Sound;
										_a.Play();
									}
									DamageToTarget = true;
									ReloadTimer = ReloadTime;
								}
							}
							if (DamageToTarget)
							{
								if (DamageTimer > 0)
								{
									DamageTimer -= Time.deltaTime;
									if (EnemyHM.CurСилаПоля <= 0)
									{
										EnemyHM.curHealth -= Time.deltaTime * Damage;
									}
									else
									{
										EnemyHM.CurСилаПоля -= Time.deltaTime * Damage;
									}
								}
								if (DamageTimer <= 0)
								{
									DamageTimer = DamageTime;
									DamageToTarget = false;
								}
							}
						}
					}
				}
			}
		}

		if (starship)
		{
			if (_st.targetTransform != null)
			{
				target = _st.targetTransform;

				HealthModule EnemyHM = _st.targetTransform.gameObject.GetComponent<HealthModule>();

				Vector3 LookVector = (EnemyHM.Mesh.transform.position - this.transform.position);

				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(LookVector), 360);

				if (CanFire())
				{
					if (_hm.maxCrew > 0)
					{
						if (_hm.curPrimaryWeaponSystemHealth > _hm.maxPrimaryWeaponSystemHealth * 0.125f && !_hm.ActivePrimaryWeapon && _hm.curCrew > 0)
						{
							if (_as.isAttack)
							{
								if (ReloadTimer > 0)
								{
									ReloadTimer -= Time.deltaTime;
								}
								if (ReloadTimer <= 0)
								{
									RaycastHit hit = new RaycastHit();
									if (_st.targetTransform != null)
									{
										if (EnemyHM.CurСилаПоля > 0)
										{
											EnemyHM.Поле.GetComponent<Renderer>().enabled = true;
											EnemyHM.Поле.GetComponent<Forcefield>().Shot = true;
											EnemyHM.Поле.GetComponent<Forcefield>().PhaserHit = hit;
										}
									}
									_PGS.PhaserFire(EnemyHM.Mesh.transform);
									_a.clip = Sound;
									_a.Play();
									DamageToTarget = true;
									ReloadTimer = ReloadTime;
								}
							}
							if (DamageToTarget)
							{
								if (DamageTimer > 0)
								{
									DamageTimer -= Time.deltaTime;
									if (_st.targetTransform != null)
									{
										if (EnemyHM.CurСилаПоля <= 0)
										{
											EnemyHM.curHealth -= Time.deltaTime * Damage;
										}
										else
										{
											EnemyHM.CurСилаПоля -= Time.deltaTime * Damage;
										}
									}
								}
								if (DamageTimer <= 0)
								{
									DamageTimer = DamageTime;
									DamageToTarget = false;
								}
							}
						}
					}
					else
					{
						if (_hm.curPrimaryWeaponSystemHealth > _hm.maxPrimaryWeaponSystemHealth * 0.125f && !_hm.ActivePrimaryWeapon)
						{
							if (_as.isAttack)
							{
								if (ReloadTimer > 0)
								{
									ReloadTimer -= Time.deltaTime;
								}
								if (ReloadTimer <= 0)
								{
									RaycastHit hit;
									RayStart = gameObject;
									if (Physics.Raycast(_st.targetTransform.position, _st.targetTransform.position, out hit, gameObject.GetComponent<Distanser>().DistanceToTarget + 1000))
									{
									}
									if (EnemyHM.CurСилаПоля > 0)
									{
										EnemyHM.Поле.GetComponent<Renderer>().enabled = true;
										EnemyHM.Поле.GetComponent<Forcefield>().Shot = true;
										EnemyHM.Поле.GetComponent<Forcefield>().PhaserHit = hit;
									}
									_PGS.PhaserFire(EnemyHM.Mesh.transform);
									_a.clip = Sound;
									_a.Play();
									DamageToTarget = true;
									ReloadTimer = ReloadTime;
								}
							}
							if (DamageToTarget)
							{
								if (DamageTimer > 0)
								{
									DamageTimer -= Time.deltaTime;
									if (EnemyHM.CurСилаПоля <= 0)
									{
										EnemyHM.curHealth -= Time.deltaTime * Damage;
									}
									else
									{
										EnemyHM.CurСилаПоля -= Time.deltaTime * Damage;
									}
								}
								if (DamageTimer <= 0)
								{
									DamageTimer = DamageTime;
									DamageToTarget = false;
								}
							}
						}
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