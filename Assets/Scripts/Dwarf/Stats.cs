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

    public string Race;
    public int VoiceNum;

    public bool AttackStations;
    public bool DefenceStations;
    public bool NatralStations;

    public Sprite icon;
    public int приоритет;
    public string nameShip;
    public string Name;
    public GameObject target;

    public enum enInstruction
    {
        idle,
        move,
        attack
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
    public bool HoverCursore;
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
    public float NormalSpeed;

    public bool InMaskito;

    private bool TorpidoUp;

    public bool Assimilated;

    public GameObject Owner;

    private Ray _ray;
    private RaycastHit _hit;

    private float orderstoptimer = 0.1f;

    public Sprite ShipBluePrint;

    public string classname;

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
        _GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
        _GDB.dwarfList.Add(gameObject);

        _AS = gameObject.GetComponent<ActiveState>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (STDLCMethods.FindInList(gameObject, _GDB.selectList))
        {
            WasSelect = true;
            isSelect = true;
            if (Proector.activeSelf == false)
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
        if (Warp)
        {
            _agent.Warp = true;
        }
        else
        {
            _agent.Warp = false;
        }

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
            VoiceIndex _vi = gameObject.GetComponent<VoiceIndex>();
            _vi.race = VoiceIndex.enRace.Borg;
            _vi.initRace();
        }

        if (STDLCMethods.FindInList(gameObject, _GDB.selectList))
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
                    if (_HP.curImpulseSystemHealth > _HP.maxImpulseSystemHealth * 0.125f && !_HP.ActiveImpulse &&
                        _HP.curCrew > 0)
                    {
                        if (Vector3.Distance(gameObject.transform.position, targetVector) > 400)
                        {
                            if (_HP.curWarpEngingSystemHealth > _HP.maxWarpEngingSystemHealth * 0.125 &&
                                !_HP.ActiveWarpEnging && _HP.curWarpCoreHealth > _HP.maxWarpCoreHealth * 0.125 &&
                                !_HP.ActiveWarpCore)
                            {
                                if (!_agent.CUBETYPE)
                                {
                                    if (!_agent.ForwardBlocked &&
                                        Vector3.Distance(_agent.RightSensor.transform.position, _agent.TargetVector) <
                                        Vector3.Distance(_agent.LeftSensor.transform.position, _agent.TargetVector) +
                                        (0.1f * _agent.RotationSpeed) &&
                                        Vector3.Distance(_agent.LeftSensor.transform.position, _agent.TargetVector) <
                                        Vector3.Distance(_agent.RightSensor.transform.position, _agent.TargetVector) +
                                        (0.1f * _agent.RotationSpeed) &&
                                        Vector3.Distance(_agent.UpSensor.transform.position, _agent.TargetVector) <
                                        Vector3.Distance(_agent.DownSensor.transform.position, _agent.TargetVector) +
                                        (0.1f * _agent.RotationSpeed) &&
                                        Vector3.Distance(_agent.DownSensor.transform.position, _agent.TargetVector) <
                                        Vector3.Distance(_agent.UpSensor.transform.position, _agent.TargetVector) +
                                        (0.1f * _agent.RotationSpeed))
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
                                        }
                                    }
                                    else
                                    {
                                        Warp = false;
                                        WarpEffect = false;
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
                                    }
                                }
                            }
                            else
                            {
                                if (Warp)
                                {
                                    Warp = false;
                                    WarpEffect = false;
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
                                WarpEffectDelay = 0.5f;
                            }
                        }
                    }
                    else
                    {
                        Warp = false;
                        WarpEffect = false;
                        WarpEffectDelay = 0.5f;
                        _agent.Warp = false;
                    }
                }
                else
                {
                    if (_HP.curImpulseSystemHealth > _HP.maxImpulseSystemHealth * 0.125f && !_HP.ActiveImpulse)
                    {
                        if (Vector3.Distance(gameObject.transform.position, targetVector) > 400)
                        {
                            if (_HP.curWarpEngingSystemHealth > _HP.maxWarpEngingSystemHealth * 0.125 ||
                                !_HP.ActiveWarpEnging)
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
                    }
                    else
                    {
                        Warp = false;
                    }
                }
            }
        }
        else
        {
            if (_HP.maxCrew > 0)
            {
                if (_HP.curImpulseSystemHealth > _HP.maxImpulseSystemHealth * 0.125f && !_HP.ActiveImpulse &&
                    _HP.curCrew > 0)
                {
                    if (Vector3.Distance(gameObject.transform.position, targetVector) > 400)
                    {
                        if (_HP.curWarpEngingSystemHealth > _HP.maxWarpEngingSystemHealth * 0.125 &&
                            !_HP.ActiveWarpEnging && _HP.curWarpCoreHealth > _HP.maxWarpCoreHealth * 0.125 &&
                            !_HP.ActiveWarpCore)
                        {
                            if (!_agent.CUBETYPE)
                            {
                                if (!_agent.ForwardBlocked &&
                                    Vector3.Distance(_agent.RightSensor.transform.position, _agent.TargetVector) <
                                    Vector3.Distance(_agent.LeftSensor.transform.position, _agent.TargetVector) +
                                    (0.1f * _agent.RotationSpeed) &&
                                    Vector3.Distance(_agent.LeftSensor.transform.position, _agent.TargetVector) <
                                    Vector3.Distance(_agent.RightSensor.transform.position, _agent.TargetVector) +
                                    (0.1f * _agent.RotationSpeed) &&
                                    Vector3.Distance(_agent.UpSensor.transform.position, _agent.TargetVector) <
                                    Vector3.Distance(_agent.DownSensor.transform.position, _agent.TargetVector) +
                                    (0.1f * _agent.RotationSpeed) &&
                                    Vector3.Distance(_agent.DownSensor.transform.position, _agent.TargetVector) <
                                    Vector3.Distance(_agent.UpSensor.transform.position, _agent.TargetVector) +
                                    (0.1f * _agent.RotationSpeed))
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
                                    }
                                }
                                else
                                {
                                    Warp = false;
                                    WarpEffect = false;
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
                                }
                            }
                        }
                        else
                        {
                            if (Warp)
                            {
                                Warp = false;
                                WarpEffect = false;
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
                            WarpEffectDelay = 0.5f;
                        }
                    }
                }
                else
                {
                    Warp = false;
                    WarpEffect = false;
                    WarpEffectDelay = 0.5f;
                }
            }
            else
            {
                if (_HP.curImpulseSystemHealth > _HP.maxImpulseSystemHealth * 0.125f && !_HP.ActiveImpulse)
                {
                    if (Vector3.Distance(gameObject.transform.position, targetVector) > 400)
                    {
                        if (_HP.curWarpEngingSystemHealth > _HP.maxWarpEngingSystemHealth * 0.125 ||
                            !_HP.ActiveWarpEnging)
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
                }
                else
                {
                    Warp = false;
                    //	gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * _agent.MovementSpeed;
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
            InitName();
        }

        SensorsLine.transform.position = gameObject.transform.position;
        _scr.radius = _es.VisionRadius;

        WeaponLine.transform.position = gameObject.transform.position;
        _wcr.radius = _AS.radiuse;
    }

    void OnDestroy()
    {
        ReInitName();

        Destroy(SensorsLine);
        Destroy(WeaponLine);
    }

    void InitName()
    {
        switch (classname)
        {
            case "Galactica":
                Name = NameCounter.Galactica.Names[Random.Range(0, NameCounter.Galactica.Names.Count)];
                NameCounter.Galactica.Names.Remove(Name);
                NameCounter.Galactica.CurShips -= 1;
                break;

            case "Defiant":
                Name = NameCounter.Defiant.Names[Random.Range(0, NameCounter.Defiant.Names.Count)];
                NameCounter.Defiant.Names.Remove(Name);
                NameCounter.Defiant.CurShips -= 1;
                break;
            case "Nova":
                Name = NameCounter.Nova.Names[Random.Range(0, NameCounter.Nova.Names.Count)];
                NameCounter.Nova.Names.Remove(Name);
                NameCounter.Nova.CurShips -= 1;
                break;
            case "Saber":
                Name = NameCounter.Saber.Names[Random.Range(0, NameCounter.Saber.Names.Count)];
                NameCounter.Saber.Names.Remove(Name);
                NameCounter.Saber.CurShips -= 1;
                break;

            case "Akira":
                Name = NameCounter.Akira.Names[Random.Range(0, NameCounter.Akira.Names.Count)];
                NameCounter.Akira.Names.Remove(Name);
                NameCounter.Akira.CurShips -= 1;
                break;
            case "Intrepid":
                Name = NameCounter.Intrepid.Names[Random.Range(0, NameCounter.Intrepid.Names.Count)];
                NameCounter.Intrepid.Names.Remove(Name);
                NameCounter.Intrepid.CurShips -= 1;
                break;
            case "SteamRunner":
                Name = NameCounter.Steamrunner.Names[Random.Range(0, NameCounter.Steamrunner.Names.Count)];
                NameCounter.Steamrunner.Names.Remove(Name);
                NameCounter.Steamrunner.CurShips -= 1;
                break;

            case "Luna":
                Name = NameCounter.Luna.Names[Random.Range(0, NameCounter.Luna.Names.Count)];
                NameCounter.Luna.Names.Remove(Name);
                NameCounter.Luna.CurShips -= 1;
                break;
            case "Prometheus":
                Name = NameCounter.Prometheuse.Names[Random.Range(0, NameCounter.Prometheuse.Names.Count)];
                NameCounter.Prometheuse.Names.Remove(Name);
                NameCounter.Prometheuse.CurShips -= 1;
                break;
            case "Nebula":
                Name = NameCounter.Nebula.Names[Random.Range(0, NameCounter.Nebula.Names.Count)];
                NameCounter.Nebula.Names.Remove(Name);
                NameCounter.Nebula.CurShips -= 1;
                break;
            case "Galaxy":
                Name = NameCounter.Galaxy.Names[Random.Range(0, NameCounter.Galaxy.Names.Count)];
                NameCounter.Galaxy.Names.Remove(Name);
                NameCounter.Galaxy.CurShips -= 1;
                break;
            case "Sovereign":
                Name = NameCounter.Sovereign.Names[Random.Range(0, NameCounter.Sovereign.Names.Count)];
                NameCounter.Sovereign.Names.Remove(Name);
                NameCounter.Sovereign.CurShips -= 1;
                break;
            case "Excalibur":
                Name = NameCounter.Excalibur.Names[Random.Range(0, NameCounter.Excalibur.Names.Count)];
                NameCounter.Excalibur.Names.Remove(Name);
                NameCounter.Excalibur.CurShips -= 1;
                break;
        }
    }
    void ReInitName()
    {
        switch (classname)
        {
            case "Galactica":
                NameCounter.Galactica.Names.Add(Name);
                NameCounter.Galactica.CurShips += 1;
                break;

            case "Defiant":
                NameCounter.Defiant.Names.Add(Name);
                NameCounter.Defiant.CurShips += 1;
                break;
            case "Nova":
                NameCounter.Nova.Names.Add(Name);
                NameCounter.Nova.CurShips += 1;
                break;
            case "Saber":
                NameCounter.Saber.Names.Add(Name);
                NameCounter.Saber.CurShips += 1;
                break;

            case "Akira":
                NameCounter.Akira.Names.Add(Name);
                NameCounter.Akira.CurShips += 1;
                break;
            case "Intrepid":
                NameCounter.Intrepid.Names.Add(Name);
                NameCounter.Intrepid.CurShips += 1;
                break;
            case "SteamRunner":
                NameCounter.Steamrunner.Names.Add(Name);
                NameCounter.Steamrunner.CurShips += 1;
                break;

            case "Luna":
                NameCounter.Luna.Names.Add(Name);
                NameCounter.Luna.CurShips += 1;
                break;
            case "Prometheus":
                NameCounter.Prometheuse.Names.Add(Name);
                NameCounter.Prometheuse.CurShips += 1;
                break;
            case "Nebula":
                NameCounter.Nebula.Names.Add(Name);
                NameCounter.Nebula.CurShips += 1;
                break;
            case "Galaxy":
                NameCounter.Galaxy.Names.Add(Name);
                NameCounter.Galaxy.CurShips += 1;
                break;
            case "Sovereign":
                NameCounter.Sovereign.Names.Add(Name);
                NameCounter.Sovereign.CurShips += 1;
                break;
            case "Excalibur":
                NameCounter.Excalibur.Names.Add(Name);
                NameCounter.Excalibur.CurShips += 1;
                break;
        }
    }
}