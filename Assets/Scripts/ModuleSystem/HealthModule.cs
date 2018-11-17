using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HealthModule : MonoBehaviour
{
    [HideInInspector]
	public GameObject ShildModel;

	public float Shilds;
	public float CurShilds;
	public float ReloadShildTimer;
	public float ShildTimerStarter;

	public float curHealth;
	public float maxHealth;

	public float curEnergy;
	public float maxEnergy;

	public float curCrew;
	public float maxCrew;

	public float curImpulseSystemHealth;
	public float maxImpulseSystemHealth;
	public bool ActiveImpulse;

	public float curLifeSupportSystemHealth;
	public float maxLifeSupportSystemHealth;
	public bool ActiveLifeSupport;

	public float curPrimaryWeaponSystemHealth;
	public float maxPrimaryWeaponSystemHealth;
	public bool ActivePrimaryWeapon;

	public float curSensorsSystemHealth;
	public float maxSensorsSystemHealth;
	public bool ActiveSensors;

	public float curTractorBeamSystemHealth;
	public float maxTractorBeamSystemHealth;
	public bool ActiveTractor;

	public float curWarpEngingSystemHealth;
	public float maxWarpEngingSystemHealth;
	public bool ActiveWarpEnging;

	public float curWarpCoreHealth;
	public float maxWarpCoreHealth;
	public bool ActiveWarpCore;

	public float curSecondaryWeaponSystemHealth;
	public float maxSecondaryWeaponSystemHealth;
	public bool ActiveSecondaryWeapon;

	public float lengthHealth;
	public float timer = 1;
	public GameObject target;

	public bool Station;
	private Station _sb;
	public bool Ship;
	[HideInInspector]
	public Stats _st;

	public bool CM;
	public GameObject X;
	public List<GameObject> DEList;
	public bool DamageEffect1;
	public bool DamageEffect2;
	public bool DamageEffect3;
	public bool DamageEffect4;
	public bool DamageEffect5;

	public GameObject DE1;
	public GameObject DE2;
	public GameObject DE3;
	public GameObject DE4;
	public GameObject DE5;

	public GameObject Explode;
	public GameObject WarpCoreDestroyed;
	public GameObject BorgWarpCoreDestroyed;
	public List<GameObject> DebrisObjects;

	private float ShildTimer = 2;

	private GlobalDB _GDB;
	//private SaveAndLoad SAL;

	public string SaveObject;

	public GameObject Mesh;

    [HideInInspector]
	public bool Catched;

	private float CatchTimer = 2;

	public float ShipRadius;

	public bool Team0;
	public bool Team1;
	public bool Team2;
	public bool Team3;
	public bool Team4;
	public bool Team5;
	public bool Team6;
	public bool Team7;
	public bool Team8;
	public bool Team9;

	

	private List<Collider> colls;

	private ShipEffects WarpCoreDisable;

	public float NormalSensors;
	private ActiveState _as;
	private SensorModule _es;

    [HideInInspector]
	public float SelfDestructTimer = 5;
	[HideInInspector]
	public bool SelfDestructActive;

	public Vector2[] ProtectPosition;
	public List<GameObject> ShipsForDefence;

	private List<float> radius;
	public float MaxDefenceRadius;

	// Use this for initialization
	void Start()
	{
	    ShildModel = gameObject.GetComponentInChildren<Forcefield>().gameObject;

		ProtectPosition = new Vector2[12];
		radius = new List<float>();

		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
		_es = gameObject.GetComponent<SensorModule>();
		if (Ship)
		{
			_st = gameObject.GetComponent<Stats>();
			_as = gameObject.GetComponent<ActiveState>();
		}
		if (Station)
		{
			_sb = gameObject.GetComponent<Station>();
		}
		timer = Random.Range(0.01f, 10);
	}
	void LateUpdate()
	{
		if (CatchTimer > 0)
		{
			CatchTimer -= Time.deltaTime;
		}
		else
		{
			Catched = false;
			CatchTimer = 2;
		}
	}

	void Update()
    {
        if (gameObject.GetComponent<MoveComponent>())
        {
            if (gameObject.GetComponent<MoveComponent>().enabled == false)
            {
                gameObject.GetComponent<Rigidbody>().drag = 0;
            }
        }

        if (curImpulseSystemHealth > 0)
        {
            RegenerateSystem(curImpulseSystemHealth, maxImpulseSystemHealth);
        }

        if (curLifeSupportSystemHealth > 0)
        {
            RegenerateSystem(curLifeSupportSystemHealth, maxLifeSupportSystemHealth);
        }

        if (curPrimaryWeaponSystemHealth > 0)
        {
            RegenerateSystem(curPrimaryWeaponSystemHealth, maxPrimaryWeaponSystemHealth);
        }

        if (curSensorsSystemHealth > 0)
        {
            RegenerateSystem(curSensorsSystemHealth, maxSensorsSystemHealth);
        }

        if (curTractorBeamSystemHealth > 0)
        {
            RegenerateSystem(curTractorBeamSystemHealth, maxTractorBeamSystemHealth);
        }

        if (curWarpEngingSystemHealth > 0)
        {
            RegenerateSystem(curWarpEngingSystemHealth, maxWarpEngingSystemHealth);
        }

        if (curWarpCoreHealth > 0)
        {
            RegenerateSystem(curWarpCoreHealth, maxWarpCoreHealth);
        }

        if (curSecondaryWeaponSystemHealth > 0)
        {
            RegenerateSystem(curSecondaryWeaponSystemHealth, maxSecondaryWeaponSystemHealth);
        }


        if (curEnergy < maxEnergy)
        {
            curEnergy += Time.deltaTime;
        }
        else if (curEnergy > maxEnergy)
        {
            curEnergy = maxEnergy;
        }
        ShildsWorkingEvent();

        if (curHealth <= 0)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            if (Ship)
            {
                _GDB.selectList.Remove(gameObject);
                _GDB.dwarfList.Remove(gameObject);
            }

            curImpulseSystemHealth = 0;
            curPrimaryWeaponSystemHealth = 0;
            curSecondaryWeaponSystemHealth = 0;
            curWarpEngingSystemHealth = 0;
            if (timer < 0)
            {
                ExplodeAndCreateDebrisObjects();
                Destroy(gameObject);
            }
        }
        ObjectNeutralisationEvent();

        if (curHealth <= maxHealth / 2)
        {
            if (!DamageEffect1)
            {
                DE1 = DEList[Random.Range(0, 4)];
                DE1.SetActive(true);
                DamageEffect1 = true;
            }
        }
        if (curHealth > maxHealth / 2)
        {
            if (DamageEffect1)
            {
                DE1.SetActive(false);
                DE1 = null;
                DamageEffect1 = false;
            }
        }
        if (curHealth <= maxHealth / 3)
        {
            if (!DamageEffect2)
            {
                DE2 = DEList[Random.Range(0, 4)];
                DE2.SetActive(true);
                DamageEffect2 = true;
            }
        }
        if (curHealth > maxHealth / 3)
        {
            if (DamageEffect2)
            {
                DE2.SetActive(false);
                DE2 = null;
                DamageEffect2 = false;
            }
        }
        if (curHealth <= maxHealth / 4)
        {
            if (!DamageEffect3)
            {
                DE3 = DEList[Random.Range(0, 4)];
                DE3.SetActive(true);
                DamageEffect3 = true;
            }
        }
        if (curHealth > maxHealth / 4)
        {
            if (DamageEffect3)
            {
                DE3.SetActive(false);
                DE3 = null;
                DamageEffect3 = false;
            }
        }
        if (curHealth <= maxHealth / 5)
        {
            if (!DamageEffect4)
            {
                DE4 = DEList[Random.Range(0, 4)];
                DE4.SetActive(true);
                DamageEffect4 = true;
            }
        }
        if (curHealth > maxHealth / 5)
        {
            if (DamageEffect4)
            {
                DE4.SetActive(false);
                DE4 = null;
                DamageEffect4 = false;
            }
        }
        if (curHealth <= maxHealth / 6)
        {
            if (!DamageEffect5)
            {
                DE5 = DEList[Random.Range(0, 4)];
                DE5.SetActive(true);
                DamageEffect5 = true;
            }
        }
        if (curHealth > maxHealth / 6)
        {
            if (DamageEffect5)
            {
                DE5.SetActive(false);
                DE5 = null;
                DamageEffect5 = false;
            }
        }
        if (curWarpCoreHealth <= 0)
        {
            curHealth = 0;
        }

        SystemEffects();
        SelfDestructionEvent();
        DefenceMachine();
    }

    private void ShildsWorkingEvent()
    {
        if (Shilds > 0)
        {
            if (CurShilds < Shilds & CurShilds > 0)
            {
                CurShilds += Time.deltaTime / 3;
            }
            if (CurShilds <= 0)
            {
                ReloadShildTimer -= Time.deltaTime;
                CurShilds = 0;
            }
            if (ReloadShildTimer <= 0)
            {
                CurShilds = 1;
                ReloadShildTimer = ShildTimerStarter;
            }
        }

        if (ShildModel != null)
        {
            if (ShildModel.GetComponent<Renderer>().enabled == true)
            {
                ShildTimer -= Time.deltaTime;
            }
        }
        if (ShildTimer <= 0)
        {
            ShildModel.GetComponent<Renderer>().enabled = false;
            ShildTimer = 2;
        }
    }

    private void ObjectNeutralisationEvent()
    {
        if (Ship)
        {
            if (maxCrew > 0)
            {
                if (curCrew <= 0)
                {
                    _st.AI = false;
                    _st.FreandAI = false;
                    _st.NeutralAgrass = false;
                    _st.Neutral = true;

                    _st.targetTransform = null;
                    if (curCrew < 0)
                    {
                        curCrew = 0;
                    }
                }
            }
        }
        if (Station)
        {
            if (maxCrew > 0)
            {
                if (curCrew <= 0)
                {
                    _sb.AI = false;
                    _sb.FreandAI = false;
                    _sb.NeutralAgrass = false;
                    _sb.Neutral = true;

                    if (_sb.WeaponModule)
                    {
                        WeaponModule _wm = gameObject.GetComponent<WeaponModule>();
                        _wm.target = null;
                    }
                    if (curCrew < 0)
                    {
                        curCrew = 0;
                    }
                }
            }
        }
    }

    private void SelfDestructionEvent()
    {
        if (SelfDestructActive)
        {
            if (SelfDestructTimer > 0)
            {
                SelfDestructTimer -= Time.deltaTime;
            }
            else
            {
                curHealth = 0;
                timer = -1;
            }
        }
        else
        {
            if (SelfDestructTimer < 5)
            {
                SelfDestructTimer = 5;
            }
        }
    }

    private void SystemEffects()
    {
        if (curWarpCoreHealth <= maxWarpCoreHealth * 0.125f || ActiveWarpCore)
        {
            curEnergy = 0;
            if (WarpCoreDisable == null)
            {
                WarpCoreDisable = gameObject.AddComponent<ShipEffects>();
                WarpCoreDisable.Effect = ShipEffects.ShipEffect.WarpCoreDisable;
            }
        }
        if (curWarpCoreHealth > maxWarpCoreHealth * 0.125f && !ActiveWarpCore)
        {
            if (WarpCoreDisable != null)
            {
                Destroy(WarpCoreDisable);
                WarpCoreDisable = null;
            }
        }

        if (curSensorsSystemHealth <= maxSensorsSystemHealth * 0.125f || ActiveSensors)
        {
            _es.VisionRadius = NormalSensors / 5;
        }
        else
        {
            _es.VisionRadius = NormalSensors;
        }


        if (curLifeSupportSystemHealth <= maxLifeSupportSystemHealth * 0.125f || ActiveLifeSupport)
        {
            if (curCrew > 0)
            {
                if (curCrew > 15)
                {
                    float T9;
                    T9 = maxCrew / 100;
                    curCrew -= T9 / 50;
                }
                if (curCrew < 15)
                {
                    float T9;
                    T9 = maxCrew / 1000;
                    curCrew -= T9 / 50;
                }
            }
            else
            {
                curCrew = 0;
            }
        }
        if (curEnergy < maxEnergy)
        {
            curEnergy += Time.deltaTime;
        }
        if (maxCrew > 0)
        {
            if (curImpulseSystemHealth <= maxImpulseSystemHealth * 0.125f || ActiveImpulse || curCrew <= 0)
            {
                if (Ship)
                {
                    gameObject.GetComponent<MoveComponent>().enabled = false;
                }
            }
        }
        else if (maxCrew <= 0)
        {
            if (curImpulseSystemHealth <= maxImpulseSystemHealth * 0.125f || ActiveImpulse)
            {
                if (Ship)
                {
                    gameObject.GetComponent<MoveComponent>().enabled = false;
                }
            }
        }
        if (maxCrew > 0)
        {
            if (curImpulseSystemHealth > maxImpulseSystemHealth * 0.125f && !ActiveImpulse && curCrew > 0)
            {
                if (Ship)
                {
                    gameObject.GetComponent<MoveComponent>().enabled = true;
                }
            }
        }
        else
        {
            if (curImpulseSystemHealth > maxImpulseSystemHealth * 0.125f && !ActiveImpulse)
            {
                if (Ship)
                {
                    gameObject.GetComponent<MoveComponent>().enabled = true;
                }
            }
        }
    }

    private void DefenceMachine()
    {
        List<GameObject> OldDefenceList = new List<GameObject>();

        float OldMaxRadius = 0;

        if (_GDB.selectList.Count == 0)
        {
            if (radius.Count > 0)
            {
                radius.Clear();
            }
        }
        if (OldDefenceList != _GDB.selectList)
        {
            radius.Clear();
            OldDefenceList = _GDB.selectList;
        }
        if (radius.Count < _GDB.selectList.Count)
        {
            foreach (GameObject obj in ShipsForDefence)
            {
                radius.Add(obj.GetComponent<HealthModule>().ShipRadius);
            }
        }
        if (radius.Count != 0)
        {
            MaxDefenceRadius = radius.Max();
        }

        if ((int)OldMaxRadius != (int)MaxDefenceRadius)
        {
            SetProtectPosition();

            OldMaxRadius = MaxDefenceRadius;
        }

        if (ShipsForDefence.Count > 0)
        {
            foreach (GameObject obj in ShipsForDefence)
            {
                if (obj.GetComponent<Stats>().GuartTarget != gameObject.transform)
                {
                    ShipsForDefence.Remove(obj);
                }
            }
        }
    }

    private void ExplodeAndCreateDebrisObjects()
    {
        Instantiate(Explode, Mesh.transform.position, Mesh.transform.rotation);
        if (curWarpCoreHealth <= 0)
        {
            if (Ship)
            {
                if (_st.Assimilated)
                {
                    Instantiate(BorgWarpCoreDestroyed, Mesh.transform.position, Mesh.transform.rotation);
                }
                else
                {
                    Instantiate(WarpCoreDestroyed, Mesh.transform.position, Mesh.transform.rotation);
                }
            }
            if (Station)
            {
                if (_sb.Assimilated)
                {
                    Instantiate(BorgWarpCoreDestroyed, Mesh.transform.position, Mesh.transform.rotation);
                }
                else
                {
                    Instantiate(WarpCoreDestroyed, Mesh.transform.position, Mesh.transform.rotation);
                }
            }
        }
        if (DebrisObjects.Count != 0)
        {
            int i = Random.Range(0, DebrisObjects.Count);
            DebrisObjects[i].SetActive(true);
            DebrisObjects[i].transform.parent = null;

            Component[] DebrisObjectsCom = DebrisObjects[i].GetComponentsInChildren(typeof(Rigidbody));

            foreach (Rigidbody _drg in DebrisObjectsCom)
            {
                _drg.velocity = gameObject.GetComponent<Rigidbody>().velocity;
            }
        }
    }

    private void RegenerateSystem(float CurSystemHealth, float MaxSystemHealth)
    {
        if (CurSystemHealth < MaxSystemHealth)
        {
            CurSystemHealth += Time.deltaTime;
        }
        else if (CurSystemHealth > MaxSystemHealth)
        {
            CurSystemHealth = MaxSystemHealth;
        }
    }

    private void SetProtectPosition()
    {
        ProtectPosition[0].x = 0;
        ProtectPosition[0].y = (MaxDefenceRadius * 10) + ShipRadius;

        ProtectPosition[1].x = 0;
        ProtectPosition[1].y = -1 * ((MaxDefenceRadius * 10) + ShipRadius);

        ProtectPosition[2].x = (MaxDefenceRadius * 8) + ShipRadius;
        ProtectPosition[2].y = 0;

        ProtectPosition[3].x = -1 * ((MaxDefenceRadius * 8) + ShipRadius);
        ProtectPosition[3].y = 0;

        ProtectPosition[4].x = ProtectPosition[0].x - (MaxDefenceRadius * 3f);
        ProtectPosition[4].y = ProtectPosition[0].y - (MaxDefenceRadius * 3f);

        ProtectPosition[5].x = ProtectPosition[0].x + (MaxDefenceRadius * 3f);
        ProtectPosition[5].y = ProtectPosition[0].y - (MaxDefenceRadius * 3f);

        ProtectPosition[6].x = ProtectPosition[1].x - (MaxDefenceRadius * 3f);
        ProtectPosition[6].y = ProtectPosition[1].y - (MaxDefenceRadius * 3f);

        ProtectPosition[7].x = ProtectPosition[1].x + (MaxDefenceRadius * 3f);
        ProtectPosition[7].y = ProtectPosition[1].y - (MaxDefenceRadius * 3f);

        ProtectPosition[8].x = ProtectPosition[2].x;
        ProtectPosition[8].y = ProtectPosition[2].y + (MaxDefenceRadius * 5);

        ProtectPosition[9].x = ProtectPosition[3].x;
        ProtectPosition[9].y = ProtectPosition[3].y + (MaxDefenceRadius * 5);

        ProtectPosition[10].x = ProtectPosition[2].x;
        ProtectPosition[10].y = ProtectPosition[2].y - (MaxDefenceRadius * 5);

        ProtectPosition[11].x = ProtectPosition[3].x;
        ProtectPosition[11].y = ProtectPosition[3].y - (MaxDefenceRadius * 5);
    }

    void OnDestroy()
	{
		if (timer > 0)
		{
			timer -= Time.deltaTime;
		}
		if (Ship)
		{
			_GDB.selectList.Remove(gameObject);
			_GDB.dwarfList.Remove(gameObject);
		}
		if (Ship)
		{
			if (!_st.AI)
			{
				_GDB.СилаИгрока -= _st.приоритет;
			}
			if (_st.AI)
			{
				_GDB.СилаВрага -= _st.приоритет;
			}
		}
		if (timer < 0)
		{
			if (Ship)
			{
				CtrlNum _CNC = _GDB.gameObject.GetComponent<CtrlNum>();
				if (Team0)
				{
					_CNC.Num0.Remove(gameObject);
				}
				if (Team1)
				{
					_CNC.Num1.Remove(gameObject);
				}
				if (Team2)
				{
					_CNC.Num2.Remove(gameObject);
				}
				if (Team3)
				{
					_CNC.Num3.Remove(gameObject);
				}
				if (Team4)
				{
					_CNC.Num4.Remove(gameObject);
				}
				if (Team5)
				{
					_CNC.Num5.Remove(gameObject);
				}
				if (Team6)
				{
					_CNC.Num6.Remove(gameObject);
				}
				if (Team7)
				{
					_CNC.Num7.Remove(gameObject);
				}
				if (Team8)
				{
					_CNC.Num8.Remove(gameObject);
				}
				if (Team9)
				{
					_CNC.Num9.Remove(gameObject);
				}
			}
		}
	}
	public void TeamActivate()
	{
		if (Station)
		{
			_sb.visible = true;
			GameObject.FindGameObjectWithTag("MainUI").GetComponent<BackgroudUI>().pictureSelectObject = gameObject.GetComponent<Station>().tex;
		}
	}

	public void ResetTeam() {
		Team0 = false;
		Team1 = false;
		Team2 = false;
		Team3 = false;
		Team4 = false;
		Team5 = false;
		Team6 = false;
		Team7 = false;
		Team8 = false;
		Team9 = false;
	}
}