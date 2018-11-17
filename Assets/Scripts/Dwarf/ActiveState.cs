using UnityEngine;
using System.Collections;
//using Boo.Lang;
using System.Collections.Generic;
using  System.Linq;

public class ActiveState : MonoBehaviour
{
	public GameObject Tractor;
	public GameObject Ship;
	public bool CanAttack;
	public int radiuse;

	public float timer = 1;

	private HealthModule _h;
	private Stats _st;
	private Shell _sl;

	private MoveComponent _agent;

	//private float _timerDown = 0;
	public bool Build;
	public bool isFlag;
	public bool isAttack;

	public bool Agrass;
	public bool Protact;
	public bool Idle;

    public enum AttackType
    {
        NormalAttack,
        ImpulseSystemAttack,
        LifeSupportSystemAttack,
        PrimaryWeaponSystemAttack,
        SensorsSystemAttack,
        TractorBeamSystemAttack,
        WarpEngingSystemAttack,
        WarpCoreAttack,
        SecondaryWeaponSystemAttack
    }

    public AttackType TargetingAt;


    public float SctiptLockTimer = 0.1f;
	public GameObject SB;

	private GlobalDB _GDB;

	public bool GizmosActive;

	private SensorModule _es;

	private bool setwaytotarget;

	public int DefenceNumber;

	private float correct = 0;

    [HideInInspector]
    public bool ForcedFix;
    [HideInInspector]
    public GameObject curShipYard;
	// Use this for initialization
	void Start()
	{
		_es = gameObject.GetComponent<SensorModule>();
		_h = gameObject.GetComponent<HealthModule>();
		_st = gameObject.GetComponent<Stats>();
		_agent = gameObject.GetComponent<MoveComponent>();
		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
	}

	// Update is called once per frame
	void LateUpdate()
	{

	}

	void OnDrawGizmosSelected()
	{
		if (GizmosActive)
		{
			Gizmos.color = new Color32(255, 0, 0, 100);
			Gizmos.DrawSphere(transform.position, radiuse);

			Gizmos.color = new Color32(255, 255, 255, 100);
			Gizmos.DrawSphere(transform.position, gameObject.GetComponent<HealthModule>().ShipRadius);

			Gizmos.color = new Color32(0, 255, 0, 100);
			Gizmos.DrawSphere(transform.position, _es.VisionRadius);
		}
	}
	void GuartShip()
	{
		bool IsShip;

		if (_st.GuartTarget.GetComponent<MoveComponent>())
		{
			IsShip = true;
		}
		else
		{
			IsShip = false;
		}

		if (IsShip)
		{
			HealthModule _gthm = _st.GuartTarget.GetComponent<HealthModule>();

			if (_st.targetTransform == null)
			{
				Quaternion rotation = _st.GuartTarget.transform.rotation;
				Vector3 rotateVector = _st.GuartTarget.transform.position + (rotation * new Vector3(_gthm.ProtectPosition[DefenceNumber].x, 0, _gthm.ProtectPosition[DefenceNumber].y));

				if (Vector3.Distance(gameObject.transform.position, _st.GuartTarget.transform.position) > 1)
				{
					_agent.Movement(new Vector3(rotateVector.x, _st.GuartTarget.transform.position.y, rotateVector.z));
				}

				if (!_agent.Move)
				{
					if (correct < 1)
					{
						correct += 0.01f;
					}
					else
					{
						correct = 1;
					}
				}
				else
				{
					correct = 0;
				}
			}
			else
			{
				if (Vector3.Distance(gameObject.transform.position, _st.GuartTarget.transform.position) > (_h.NormalSensors / 4) * 2)
				{
					setwaytotarget = true;
					_agent.Movement(new Vector3(_st.GuartTarget.transform.position.x + _gthm.ProtectPosition[DefenceNumber].x, _st.GuartTarget.transform.position.y, _st.GuartTarget.transform.position.z + _gthm.ProtectPosition[DefenceNumber].y));
				}
				else
				{
					if (setwaytotarget)
					{
						_agent.Stop();
						setwaytotarget = false;
					}
				}
			}
		}
	}

	void Update()
    {
        if (_st.GuartTarget != null)
        {
            GuartShip();

            SetDefenceNumber();
        }

        if (_st.Warp)
        {
            Agrass = false;
            Protact = false;
        }
        if (gameObject.GetComponent<Stats>().InMaskito)
        {
            CanAttack = false;
        }
        else
        {
            CanAttack = true;
        }
        if (SctiptLockTimer > 0)
        {
            SctiptLockTimer -= Time.deltaTime;
        }
        if (SctiptLockTimer <= 0)
        {
            _st.enabled = true;
        }

        OnConstruction();

        if (_st.targetTransform != null)
        {
            if (_st.targetTransform.GetComponent<HealthModule>().curHealth <= 0)
            {
                if (_st.Order)
                {
                    _st.Order = false;
                }
            }
            if (!_st.targetTransform.GetComponent<ObjectTypeVisible>().IsVisible)
            {
                isAttack = false;
                _st.targetTransform = null;
                if (_st.Order)
                {
                    _st.Order = false;
                }
            }
        }
        if (Idle)
        {
            GreenAlert();
        }
        if (Protact)
        {
            YellowAlert();
        }
        if (Agrass)
        {
            RedAlert();
        }
    }

    private void OnConstruction()
    {
        if (!isFlag)
        {
            if (_st.AI)
            {
                gameObject.GetComponent<ShipAI>().enabled = true;
                if (!_st.miner)
                {
                    if (!_st.Warp)
                    {
                        Agrass = true;
                    }
                }
            }
            if (_st.FreandAI)
            {
                gameObject.GetComponent<ShipAI>().enabled = true;
                if (!_st.miner)
                {
                    if (!_st.Warp)
                    {
                        Agrass = true;
                    }
                }
            }
        }
        else
        {
            if (_st.AI)
            {
                gameObject.GetComponent<ShipAI>().enabled = false;
            }
            if (_st.FreandAI)
            {
                gameObject.GetComponent<ShipAI>().enabled = false;
            }
        }
        if (!Build)
        {
            if (SB != null)
            {
                _agent.Movement(SB.transform.position);
                if (Vector3.Distance(gameObject.transform.position, SB.transform.position) <= 3)
                {
                    Build = true;
                }
            }
        }
        if (Build)
        {
            if (isFlag)
            {
                if (SB.GetComponent<SP>().flag != null)
                {
                    _agent.Movement(SB.GetComponent<SP>().flag.transform.position);
                    isFlag = false;
                }
                else
                {
                    isFlag = false;
                }
            }
        }
        else if (_st.instruction == Stats.enInstruction.move)
        {
            if (Vector3.Distance(_st.targetVector, gameObject.transform.position) > 1)
            {
                if (_st.targetTransform == null)
                {
                    _agent.Movement(_st.targetVector);
                }
            }
            else
            {
                //_agent.Stop();
                _st.Order = false;
                _st.instruction = Stats.enInstruction.idle;
            }
        }
    }

    private void RedAlert()
    {
        if (_st.targetTransform != null)
        {
            if (_st.Order)
            {
                if (Vector3.Distance(_st.targetTransform.position, gameObject.transform.position) > radiuse + _h.ShipRadius + _st.targetTransform.gameObject.GetComponent<HealthModule>().ShipRadius)
                {
                    if (!gameObject.GetComponent<Maneuvers>())
                    {
                        _agent.Movement(_st.targetTransform.position);
                    }
                    isAttack = false;
                }
                else
                {
                    if (CanAttack)
                    {
                        isAttack = true;
                    }
                }
            }
            else
            {
                if (_es.Target != null)
                {
                    if (Vector3.Distance(gameObject.transform.position, _st.targetTransform.position) < _es.VisionRadius + _h.ShipRadius + _es.Target.GetComponent<HealthModule>().ShipRadius)
                    {
                        if (Vector3.Distance(_st.targetTransform.position, gameObject.transform.position) > radiuse + _h.ShipRadius + _st.targetTransform.gameObject.GetComponent<HealthModule>().ShipRadius)
                        {
                            if (!gameObject.GetComponent<Maneuvers>())
                            {
                                _agent.Movement(_st.targetTransform.position);
                            }
                            isAttack = false;
                        }
                        else
                        {
                            if (CanAttack)
                            {
                                isAttack = true;
                            }
                        }
                    }
                    else
                    {
                        isAttack = false;
                        _st.targetTransform = null;
                    }
                }
            }
        }
        else
        {
            if (!_st.Order)
            {
                if (_es.Target != null)
                {
                    if (Vector3.Distance(gameObject.transform.position, _es.Target.transform.position) < _es.VisionRadius + _h.ShipRadius + _es.Target.GetComponent<HealthModule>().ShipRadius)
                    {
                        _st.targetTransform = _es.Target.transform;
                    }
                }
            }
        }
    }

    private void YellowAlert()
    {
        if (_st.targetTransform != null)
        {
            if (_st.Order)
            {
                if (Vector3.Distance(_st.targetTransform.position, gameObject.transform.position) > radiuse + _h.ShipRadius + _st.targetTransform.gameObject.GetComponent<HealthModule>().ShipRadius)
                {
                    if (!gameObject.GetComponent<Maneuvers>())
                    {
                        _agent.Movement(_st.targetTransform.position);
                    }
                    isAttack = false;
                }
                else
                {
                    if (CanAttack)
                    {
                        isAttack = true;
                    }
                }
            }
            else
            {
                if (Vector3.Distance(_st.targetTransform.position, gameObject.transform.position) < radiuse + _h.ShipRadius + _st.targetTransform.gameObject.GetComponent<HealthModule>().ShipRadius)
                {
                    if (CanAttack)
                    {
                        isAttack = true;
                    }
                }
                else
                {
                    isAttack = false;
                    _st.targetTransform = null;
                }
            }
        }
        else
        {
            if (!_st.Order)
            {
                if (_es.Target != null)
                {
                    if (Vector3.Distance(gameObject.transform.position, _es.Target.transform.position) < radiuse + _h.ShipRadius + _es.Target.GetComponent<HealthModule>().ShipRadius)
                    {
                        _st.targetTransform = _es.Target.transform;
                    }
                }
            }
        }
    }

    private void GreenAlert()
    {
        if (_st.targetTransform != null)
        {
            if (_st.Order)
            {
                if (Vector3.Distance(_st.targetTransform.position, gameObject.transform.position) > radiuse + _h.ShipRadius + _st.targetTransform.gameObject.GetComponent<HealthModule>().ShipRadius)
                {
                    if (!gameObject.GetComponent<Maneuvers>())
                    {
                        _agent.Movement(_st.targetTransform.position);
                    }
                    isAttack = false;
                }
                else
                {
                    if (CanAttack)
                    {
                        isAttack = true;
                    }
                }
            }
            else
            {
                isAttack = false;
                _st.targetTransform = null;
            }
        }
    }

    private void SetDefenceNumber()
    {
        HealthModule _gthm = _st.GuartTarget.GetComponent<HealthModule>();
        for (int i = 0; i <= _gthm.ShipsForDefence.Count - 1; i++)
        {
            if (_gthm.ShipsForDefence[i] == gameObject)
            {
                DefenceNumber = i;
            }
        }
    }

    public GameObject NearestShipYard()
    {
        GameObject nsy = null;

        FixModule[] fm = FindObjectsOfType<FixModule>().ToArray();
        if (fm.Length > 0)
        {
            foreach (FixModule efm in fm)
            {
                if (!efm._sb.AI && !efm._sb.FreandAI && !efm._sb.Neutral && !efm._sb.NeutralAgrass)
                {
                    float distance = Mathf.Infinity;
                    Vector3 position = transform.position;
                    float curDistance = Vector3.Distance(efm.transform.position, position);
                    if (curDistance < distance)
                    {
                        nsy = efm.gameObject;
                        distance = curDistance;
                    }
                }
                else
                {
                    nsy = null;
                }
            }
        }
        else
        {
            nsy = null;
        }
        return nsy;
    }

    public void Fix()
    {
        if (curShipYard == null)
        {
            FixModule _nfx = NearestShipYard().GetComponent<FixModule>();
            curShipYard = _nfx.gameObject;
            if (!STDLCMethods.FindInList(gameObject, _nfx.ShipsToFix)) _nfx.ShipsToFix.Add(gameObject);
            _agent.CurFleet.Clear();
            //_SEL.PlayFixSound(gameObject);
            ForcedFix = true;
        }
        else
        {
            ForcedFix = false;
            curShipYard = null;
            _st.StopOrder = true;
        }
    }
	public void DeAssamble()
	{

	}
}