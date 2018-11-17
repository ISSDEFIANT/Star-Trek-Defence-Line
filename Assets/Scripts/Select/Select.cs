using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Select : MonoBehaviour
{
	public bool isSelect;
	//public bool Lock;

	public Texture Protect;

	private float _selX;
	private float _selY;
	private float _selWidth;
	private float _selHeight;

	private float _selXOld;
	private float _selYOld;

	public Texture2D texSelect;

	public List<Position> pos;


	private Vector2 _startPoint;
	private Vector2 _endPoint;

	private Ray _ray;
	private RaycastHit _hit;

	private Ray _rayWOTerrain;
	private RaycastHit _hitWOTerrain;

	private Ray _rayShipHover;
	private RaycastHit _hitShipHover;
	public GameObject _LastHoveredShip;
	public GameObject _CurHoveredShip;

	private GlobalDB _GDB;
	private bool Building;
	public bool RPGTrue;

	public bool MSDActive;
	public bool OrdersActive;
	public bool SpecialActive;
	public bool BuildActive;


	private float HealthBarLen;
	private float ShildBarLen;
	private float EnergyBarLen;

	public GameObject MoveMark;
	public GameObject AttackMark;
	public GameObject GuardMark;

	public GUIStyle style;
	[HideInInspector]
	public GameObject SaveObj;

	private List<float> radius;
	public float MaxRadius;

	private GameObject PlayerCamera;

	public bool OrderActive;
	public bool AttackOrder;
	public bool GuardOrder;

	public int LeftMClickCount = 0;
	public float LeftMClickTimer = 0.3f;

	public bool LockOnPatrolSetting;

	public bool LockSelection;

	[HideInInspector]
	public bool SettingPatrolWay;
	// Use this for initialization
	void Start()
	{
		_CurHoveredShip = null;

		radius = new List<float>();
		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
		MSDActive = true;
		if (!gameObject.GetComponent<AudioSource>())
		{
			gameObject.AddComponent<AudioSource>();
		}
		Time.timeScale = 1;

		PlayerCamera = GameObject.FindGameObjectWithTag("CAMERAMOVE");
	}
	void StateInSelectTarget(Transform targetGO)
	{
		foreach (GameObject obj in _GDB.selectList)
		{
			Stats _ost = obj.GetComponent<Stats>();
		    ActiveState _oast = obj.GetComponent<ActiveState>();
		    if (!_ost.AI && !_ost.FreandAI && !_ost.Neutral && !_ost.NeutralAgrass)
		    {
		        if (!_oast.ForcedFix)
		        {
		            _ost.Order = true;
		            _ost.targetTransform = targetGO;
		            _ost.instruction = Stats.enInstruction.attack;
		            obj.GetComponent<MoveComponent>().InPatrol = false;
		            obj.GetComponent<MoveComponent>().PatrolWay.Clear();
		            Instantiate(AttackMark, targetGO.transform.position, MoveMark.transform.rotation);
		            if (!_ost.AI && !_ost.FreandAI && !_ost.Neutral && !_ost.NeutralAgrass)
		            {
		                if (!gameObject.GetComponent<AudioSource>().isPlaying)
		                {
		                    gameObject.GetComponent<AudioSource>().clip = _GDB.selectList[0].GetComponent<Captan>()
		                        .CurCap.Attack[
		                            Random.Range(0, _GDB.selectList[0].GetComponent<Captan>().CurCap.Attack.Count)];
		                    gameObject.GetComponent<AudioSource>().Play();
		                }
		            }

		            obj.GetComponent<MoveComponent>().SetCurFleet(_GDB.selectList);
		        }
		    }
		}
	}
	void GuardEvent(Transform GTarget)
	{
		HealthModule _gthm = GTarget.GetComponent<HealthModule>();
		Quaternion rotation = GTarget.transform.rotation;

		int i = _gthm.ShipsForDefence.Count;
		foreach (GameObject obj in _GDB.selectList)
		{
			Stats _ost = obj.GetComponent<Stats>();
		    ActiveState _oast = obj.GetComponent<ActiveState>();
            if (!_ost.AI && !_ost.FreandAI && !_ost.Neutral && !_ost.NeutralAgrass)
			{
			    if (!_oast.ForcedFix)
			    {
			        obj.GetComponent<MoveComponent>().InPatrol = false;
			        obj.GetComponent<MoveComponent>().PatrolWay.Clear();
			        _ost.GuartTarget = GTarget.transform;
			        _gthm.ShipsForDefence.Add(obj);
			        if (i <= _gthm.ShipsForDefence.Count - 1)
			        {
			            Instantiate(GuardMark,
			                GTarget.transform.position +
			                (rotation * new Vector3(_gthm.ProtectPosition[i].x, 0, _gthm.ProtectPosition[i].y)),
			                MoveMark.transform.rotation);
			        }

			        i++;
			        if (!_ost.AI && !_ost.FreandAI && !_ost.Neutral && !_ost.NeutralAgrass)
			        {
			            if (!gameObject.GetComponent<AudioSource>().isPlaying)
			            {
			                gameObject.GetComponent<AudioSource>().clip = _GDB.selectList[0].GetComponent<Captan>()
			                    .CurCap.Attack[
			                        Random.Range(0, _GDB.selectList[0].GetComponent<Captan>().CurCap.Attack.Count)];
			                gameObject.GetComponent<AudioSource>().Play();
			            }
			        }
			    }
			}
		}
	}

	void StateInSelect(Vector3 targetVec)
	{
		int i = 0;
	    foreach (GameObject obj in _GDB.selectList)
	    {
	        Stats _ost = obj.GetComponent<Stats>();
	        ActiveState _oast = obj.GetComponent<ActiveState>();

            if (!_ost.AI && !_ost.FreandAI && !_ost.Neutral && !_ost.NeutralAgrass)
	        {
	            if (!_oast.ForcedFix)
	            {
	                if (!Input.GetKey(KeyCode.LeftShift))
	                {
	                    Vector3 relativePos = targetVec - _GDB.selectList[0].transform.position;
	                    Quaternion rotation = Quaternion.LookRotation(relativePos);

	                    Vector3 rotateVector = targetVec + (rotation * new Vector3(pos[i].x, 0, pos[i].z));

	                    Instantiate(MoveMark, new Vector3(rotateVector.x, targetVec.y, rotateVector.z),
	                        MoveMark.transform.rotation);
	                    _ost.Order = true;
	                    _ost.targetTransform = null;
	                    _ost.GuartTarget = null;
	                    _ost.instruction = Stats.enInstruction.move;
	                    _ost.targetVector = new Vector3(targetVec.x + pos[i].x, targetVec.y, targetVec.z + pos[i].z);
	                    obj.GetComponent<MoveComponent>()
	                        .Movement(new Vector3(rotateVector.x, targetVec.y, rotateVector.z));
	                    obj.GetComponent<MoveComponent>().InPatrol = false;
	                    obj.GetComponent<MoveComponent>().PatrolWay.Clear();
	                    _ost.StopOrder = true;
	                    i++;
	                    if (!gameObject.GetComponent<AudioSource>().isPlaying)
	                    {
	                        gameObject.GetComponent<AudioSource>().clip = _GDB.selectList[0].GetComponent<Captan>()
	                            .CurCap.Move[
	                                Random.Range(0, _GDB.selectList[0].GetComponent<Captan>().CurCap.Move.Count)];
	                        gameObject.GetComponent<AudioSource>().Play();
	                    }

	                    obj.GetComponent<MoveComponent>().SetCurFleet(_GDB.selectList);
	                }
	                else
	                {
	                    obj.GetComponent<MoveComponent>().AddPatrolPoint(new Vector3(targetVec.x + pos[i].x,
	                        targetVec.y, targetVec.z + pos[i].z));
	                    LockOnPatrolSetting = true;
	                }
	            }
	        }
	    }
	}
	// Update is called once per frame

	int layerMaskForShipHover = 1 << 17;


	void ShipOrder()
	{
		if (!LockSelection)
		{
			if (_GDB.selectList.Count != 0)
			{
				if (Input.mousePosition.y < 250 & Input.mousePosition.y > 0 & Input.mousePosition.x < 250 & Input.mousePosition.x > 0)
				{
					_ray = gameObject.GetComponent<MiniMap>().itsMinimapCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
				}
				else
				{
					_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				}
				if (Physics.Raycast(_ray, out _hit, 10000.0f))
				{
					if (OrderActive)
					{
						if (_hit.transform.tag == "Enemy" || _hit.transform.tag == "Dwarf" || _hit.transform.tag == "Freand" || _hit.transform.tag == "Neutral")
						{
							if (AttackOrder)
							{
								StateInSelectTarget(_CurHoveredShip.transform);
							}
							if (GuardOrder)
							{
								if (_CurHoveredShip.tag != "Enemy")
								{
									if (_CurHoveredShip.GetComponent<HealthModule>().ShipsForDefence.Count < 12)
									{
										GuardEvent(_CurHoveredShip.transform);
									}
									else
									{
										OrderActive = false;
										AttackOrder = false;
										GuardOrder = false;
									}
								}
							}
							OrderActive = false;
							AttackOrder = false;
							GuardOrder = false;
						}
						else
						{
							OrderActive = false;
							AttackOrder = false;
							GuardOrder = false;
						}
					}
					if (_hit.transform.gameObject.tag == "Enemy")
					{
						foreach (GameObject obj in _GDB.selectList)
						{
							if (!obj.GetComponent<Stats>().AI && !obj.GetComponent<Stats>().FreandAI)
							{
								StateInSelectTarget(_hit.transform);
							}
						}
					}
					if (_hit.transform.gameObject.tag == "Terrain")
					{
						if (_CurHoveredShip == null)
						{
							foreach (GameObject obj in _GDB.selectList)
							{
								if (!obj.GetComponent<Stats>().AI && !obj.GetComponent<Stats>().FreandAI)
								{
									StateInSelect(_hit.point);
								}
							}
						}
						else
						{
							if (!OrderActive)
							{
								if (_CurHoveredShip.tag == "Enemy")
								{
									foreach (GameObject obj in _GDB.selectList)
									{
										if (!obj.GetComponent<Stats>().AI && !obj.GetComponent<Stats>().FreandAI)
										{
											StateInSelectTarget(_CurHoveredShip.transform);
										}
									}
								}
								if (_hit.transform.gameObject.tag != "Enemy" && _hit.transform.gameObject.tag != "Terrain")
								{
									if (!_GDB.selectList[0].GetComponent<Stats>().miner)
									{
										if (!gameObject.GetComponent<AudioSource>().isPlaying)
										{
											if (_GDB.selectList[0].GetComponent<Captan>().CurCap.LocationInvalid.Count > 0)
											{
												gameObject.GetComponent<AudioSource>().clip = _GDB.selectList[0].GetComponent<Captan>().CurCap.LocationInvalid[Random.Range(0, _GDB.selectList[0].GetComponent<Captan>().CurCap.LocationInvalid.Count)];
												gameObject.GetComponent<AudioSource>().Play();
											}
										}
									}
									else
									{
										if (_hit.transform.gameObject.tag != "BuildingBuilding" && _hit.transform.gameObject.tag != "BuildingBuildingEnemy" && _hit.transform.gameObject.tag != "BuildingBuildingFreand")
										{
											if (!gameObject.GetComponent<AudioSource>().isPlaying)
											{
												if (_GDB.selectList[0].GetComponent<Captan>().CurCap.LocationInvalid.Count > 0)
												{
													gameObject.GetComponent<AudioSource>().clip = _GDB.selectList[0].GetComponent<Captan>().CurCap.LocationInvalid[Random.Range(0, _GDB.selectList[0].GetComponent<Captan>().CurCap.LocationInvalid.Count)];
													gameObject.GetComponent<AudioSource>().Play();
												}
											}
										}
									}
								}
							}
							else
							{
								if (_CurHoveredShip.tag == "Enemy" || _CurHoveredShip.tag == "Dwarf" || _CurHoveredShip.tag == "Freand" || _CurHoveredShip.tag == "Neutral")
								{
									foreach (GameObject obj in _GDB.selectList)
									{
										if (!obj.GetComponent<Stats>().AI && !obj.GetComponent<Stats>().FreandAI)
										{
											if (AttackOrder)
											{
												StateInSelectTarget(_CurHoveredShip.transform);
											}
											if (GuardOrder)
											{
												if (_CurHoveredShip.tag != "Enemy")
												{
													if (_CurHoveredShip.GetComponent<HealthModule>().ShipsForDefence.Count < 12)
													{
														GuardEvent(_CurHoveredShip.transform);
													}
													else
													{
														OrderActive = false;
														AttackOrder = false;
														GuardOrder = false;
													}
												}
											}
											OrderActive = false;
											AttackOrder = false;
											GuardOrder = false;
										}
										else
										{
											OrderActive = false;
											AttackOrder = false;
											GuardOrder = false;
										}
									}
								}
								else
								{
									OrderActive = false;
									AttackOrder = false;
									GuardOrder = false;
								}
							}
						}
					}
				}
			}
			if (_GDB.activeObjectInterface)
			{
				if (Input.mousePosition.y < 250 & Input.mousePosition.y > 0 & Input.mousePosition.x < 250 & Input.mousePosition.x > 0)
				{
					_ray = gameObject.GetComponent<MiniMap>().itsMinimapCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
				}
				else
				{
					_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				}
				if (Physics.Raycast(_ray, out _hit, 10000.0f))
				{
					if (!OrderActive)
					{
						if (_hit.transform.gameObject.tag == "Enemy")
						{
							if (_GDB.activeObjectInterface.GetComponent<WeaponModule>())
							{
								_GDB.activeObjectInterface.GetComponent<WeaponModule>().target = _hit.transform.gameObject;
							}
						}
						if (_hit.transform.gameObject.tag == "Terrain")
						{
							if (_CurHoveredShip == null)
							{

							}
							else
							{
								if (_CurHoveredShip.tag == "Enemy")
								{
									if (_GDB.activeObjectInterface.GetComponent<WeaponModule>())
									{
										_GDB.activeObjectInterface.GetComponent<WeaponModule>().target = _hit.transform.gameObject;
									}
								}
							}
						}
					}
					else
					{
						if (_hit.transform.gameObject.tag == "Enemy" || _hit.transform.gameObject.tag == "Dwarf" || _hit.transform.gameObject.tag == "Freand" || _hit.transform.gameObject.tag == "Neutral")
						{

							if (AttackOrder)
							{
								if (_GDB.activeObjectInterface.GetComponent<WeaponModule>())
								{
									_GDB.activeObjectInterface.GetComponent<WeaponModule>().target = _hit.transform.gameObject;
								}
							}
							OrderActive = false;
							AttackOrder = false;
							GuardOrder = false;
						}
						else if (_hit.transform.gameObject.tag == "Terrain")
						{
							if (_CurHoveredShip == null)
							{
								OrderActive = false;
								AttackOrder = false;
								GuardOrder = false;
							}
							else
							{
								if (_CurHoveredShip.tag == "Enemy" || _CurHoveredShip.tag == "Dwarf" || _CurHoveredShip.tag == "Freand" || _CurHoveredShip.tag == "Neutral")
								{
									if (AttackOrder)
									{
										if (_GDB.activeObjectInterface.GetComponent<WeaponModule>())
										{
											_GDB.activeObjectInterface.GetComponent<WeaponModule>().target = _hit.transform.gameObject;
										}
									}
									OrderActive = false;
									AttackOrder = false;
									GuardOrder = false;
								}
								else
								{
									OrderActive = false;
									AttackOrder = false;
									GuardOrder = false;
								}
							}
						}
						else
						{
							OrderActive = false;
							AttackOrder = false;
							GuardOrder = false;
						}
					}
				}
			}
		}
	}
	void SettingPatrol()
	{
		int i = 0;

		if (_GDB.selectList.Count > 0)
		{
			foreach (GameObject obj in _GDB.selectList)
			{
				Stats _ost = obj.GetComponent<Stats>();

				if (!_ost.AI && !_ost.FreandAI && !_ost.Neutral && !_ost.NeutralAgrass)
				{
					if (Input.GetMouseButtonDown(0))
					{
						Ray STR = Camera.main.ScreenPointToRay(Input.mousePosition);
						RaycastHit STRH;
						if (Physics.Raycast(STR, out STRH, 10000.0f))
						{
							if (STRH.transform.gameObject.tag == "Terrain")
							{
								obj.GetComponent<MoveComponent>().AddPatrolPoint(new Vector3(STRH.point.x + pos[i].x, STRH.point.y, STRH.point.z + pos[i].z));
								i++;
							}
						}
					}
					if (Input.GetMouseButtonUp(1))
					{
						ActivatePatrol();

						LockSelection = false;
						isSelect = false;
						SettingPatrolWay = false;
					}
				}
			}
		}
	}
	void LateUpdate()
	{

	}
	void Update()
	{
		if (SettingPatrolWay)
		{
			SettingPatrol();
			if (_GDB.selectList.Count == 0)
			{
				LockSelection = false;
				SettingPatrolWay = false;
			}
		}

		if (OrderActive)
		{
			if (Input.GetMouseButtonDown(0))
			{
				ShipOrder();
			}
			if (Input.GetMouseButtonDown(1))
			{
				OrderActive = false;
				AttackOrder = false;
				GuardOrder = false;
			}
		}
		if (!LockSelection)
		{
			float X;
			float Y;
			X = Screen.width / 100;
			Y = Screen.height / 100;
			_rayShipHover = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (LeftMClickCount > 0)
			{
				if (LeftMClickTimer > 0)
				{
					LeftMClickTimer -= Time.deltaTime;
				}
				else
				{
					LeftMClickCount = 0;
					LeftMClickTimer = 0.3f;
				}
			}

			if (!OrderActive)
			{
				if (Input.GetMouseButtonDown(0))
                {
                    SelectPro();
                }
            }

			if (Input.GetMouseButtonUp(0))
			{
				isSelect = false;
				if (Input.mousePosition.y < Y * 25 & Input.mousePosition.y > Y * 0 & Input.mousePosition.x < X * 14 & Input.mousePosition.x > X * 0)
				{
				}
				else
				{
					_endPoint = Input.mousePosition;
					FindSelect();
				}
			}

			if (isSelect)
			{
				_selX = Input.mousePosition.x;
				_selY = Screen.height - Input.mousePosition.y;
				_selWidth = _selXOld - Input.mousePosition.x;
				_selHeight = Input.mousePosition.y - _selYOld;
			}
			if (_GDB.selectList.Count == 0 && _GDB.activeObjectInterface == null)
			{
				MSDActive = true;
				OrdersActive = false;
				SpecialActive = false;
				BuildActive = false;
			}
			if (Input.GetMouseButtonDown(1))
			{
				if (!OrderActive)
				{
					ShipOrder();
				}
			}
			if (_GDB.selectList.Count != 0)
			{
				gameObject.GetComponent<BackgroudUI>().pictureSelectObject = _GDB.selectList[0].GetComponent<Stats>().icon;
			}

			List<GameObject> OldSelectList = new List<GameObject>();

			if (_GDB.selectList.Count == 0)
			{
				if (radius.Count > 0)
				{
					radius.Clear();
				}
			}
			if (OldSelectList != _GDB.selectList)
			{
				radius.Clear();
				OldSelectList = _GDB.selectList;
			}
			if (radius.Count < _GDB.selectList.Count)
			{
				foreach (GameObject obj in _GDB.selectList)
				{
					radius.Add(obj.GetComponent<HealthModule>().ShipRadius);
				}
			}
			if (radius.Count != 0)
			{
				MaxRadius = radius.Max();
			}
			if (_GDB.selectList.Count > 0)
			{
				if (_GDB.selectList.Count > 1)
				{
					pos[1].x = -1 * MaxRadius * 4;
					pos[1].z = 0;
					if (_GDB.selectList.Count > 2)
					{
						pos[2].x = 0;
						pos[2].z = -1 * MaxRadius * 4;
						if (_GDB.selectList.Count > 3)
						{
							pos[3].x = -1 * MaxRadius * 4;
							pos[3].z = -1 * MaxRadius * 4;
							if (_GDB.selectList.Count > 4)
							{
								pos[4].x = -1 * (MaxRadius * 4) * 2;
								pos[4].z = 0;
								if (_GDB.selectList.Count > 5)
								{
									pos[5].x = -1 * (MaxRadius * 4) * 2;
									pos[5].z = -1 * MaxRadius * 4;
									if (_GDB.selectList.Count > 6)
									{
										pos[6].x = 0;
										pos[6].z = -1 * (MaxRadius * 4) * 2;
										if (_GDB.selectList.Count > 7)
										{
											pos[7].x = -1 * MaxRadius * 4;
											pos[7].z = -1 * (MaxRadius * 4) * 2;
											if (_GDB.selectList.Count > 8)
											{
												pos[8].x = -1 * (MaxRadius * 4) * 2;
												pos[8].z = -1 * (MaxRadius * 4) * 2;
												if (_GDB.selectList.Count > 9)
												{
													pos[9].x = -1 * (MaxRadius * 4) * 3;
													pos[9].z = 0;
													if (_GDB.selectList.Count > 10)
													{
														pos[10].x = -1 * (MaxRadius * 4) * 3;
														pos[10].z = -1 * MaxRadius * 4;
														if (_GDB.selectList.Count > 11)
														{
															pos[11].x = -1 * (MaxRadius * 4) * 3;
															pos[11].z = -1 * (MaxRadius * 4) * 2;
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
				}
			}


			if (Physics.Raycast(_rayShipHover, out _hitShipHover, 10000.0f, layerMaskForShipHover))
			{
				_CurHoveredShip = _hitShipHover.transform.gameObject;
				if (_hitShipHover.transform.gameObject.GetComponent<Stats>())
				{
					if (_hitShipHover.transform.gameObject.GetComponent<ObjectTypeVisible>().IsVisible)
					{

						if (_LastHoveredShip != _CurHoveredShip)
						{
							if (_LastHoveredShip != null)
							{
								Stats _st2 = _LastHoveredShip.GetComponent<Stats>();
								_st2.ShadowProjectorActive = false;
								if (!_st2.SelectLock)
								{
									_st2.HoverCursore = false;
									_st2.Selected = false;
								}
								if (!_st2.ShadowProjectorActive)
								{
									_LastHoveredShip = _CurHoveredShip;
								}
							}
							else
							{
								_LastHoveredShip = _CurHoveredShip;
							}
						}

						Stats _st = _hitShipHover.transform.gameObject.GetComponent<Stats>();

						_st.ShadowProjectorActive = true;

						if (_st.AI)
						{
							if (_GDB.selectList.Count == 0)
							{
								if (!_st.SelectLock)
								{
									_st.HoverCursore = true;
									_st.Selected = true;
								}
							}
						}
						if (!_st.AI)
						{
							if (!_st.SelectLock)
							{
								_st.HoverCursore = true;
								_st.Selected = true;
							}
						}
					}
				}
			}
			else
			{
				_CurHoveredShip = null;
				if (_LastHoveredShip != null)
				{
					Stats _st = _LastHoveredShip.GetComponent<Stats>();
					_st.ShadowProjectorActive = false;
					if (!_st.SelectLock)
					{
						_st.HoverCursore = false;
						_st.Selected = false;
					}
					if (!_st.ShadowProjectorActive)
					{
						_LastHoveredShip = null;
					}
				}
			}
			if (LockOnPatrolSetting)
			{
				ActivatePatrol();
			}
		}

		if (Input.GetKeyDown(KeyCode.A))
		{
			SetOrder("Attack");
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			SetOrder("Guard");
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			SetOrder("Fix");
		}
		if (Input.GetKey(KeyCode.LeftShift))
		{
			if (Input.GetKey(KeyCode.Delete))
			{
				foreach (GameObject obj in _GDB.selectList)
				{
					HealthModule targetHealth = obj.GetComponent<HealthModule>();
					targetHealth.SelfDestructActive = !targetHealth.SelfDestructActive;
				}
			}
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Delete))
			{
                SetOrder("DeAssamble");
			}
		}
	}

    private void SelectPro()
    {
        isSelect = true;
        _selXOld = Input.mousePosition.x;
        _selYOld = Input.mousePosition.y;

        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        _rayWOTerrain = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _hit, 10000.0f))
        {
            _startPoint = Input.mousePosition;
            if (_hit.transform.gameObject.GetComponent<Stats>())
            {
                if (_hit.transform.gameObject.tag == "Dwarf" || _hit.transform.gameObject.tag == "Enemy" || _hit.transform.gameObject.tag == "Freand" || _hit.transform.gameObject.tag == "Neutral")
                {
                    isSelect = false;
                    LeftMClickCount += 1;
                    if (_hit.transform.gameObject.GetComponent<Stats>().SelectLock == false)
                    {
                        if (_GDB.selectList.Count <= 0)
                        {
                            if (!STDLCMethods.FindInList(_hit.transform.gameObject, _GDB.selectList))
                            {
                                _GDB.selectList.Add(_hit.transform.gameObject);
                            }
                        }
                        if (_GDB.selectList.Count > 1)
                        {
                            if (!Input.GetKey(KeyCode.LeftShift))
                            {
                                _GDB.selectList.Clear();
                            }
                            if (!STDLCMethods.FindInList(_hit.transform.gameObject, _GDB.selectList))
                            {
                                _GDB.selectList.Add(_hit.transform.gameObject);
                            }
                        }
                        if (_GDB.selectList.Count == 1)
                        {
                            if (LeftMClickCount == 2)
                            {
                                FindShipType(_hit.transform.gameObject.GetComponent<Stats>().classname);
                            }
                            else
                            {
                                if (!Input.GetKey(KeyCode.LeftShift))
                                {
                                    _GDB.selectList[0] = _hit.transform.gameObject;
                                }
                                else
                                {
                                    if (!STDLCMethods.FindInList(_hit.transform.gameObject, _GDB.selectList))
                                    {
                                        _GDB.selectList.Add(_hit.transform.gameObject);
                                    }
                                }
                            }
                        }
                        if (!_hit.transform.gameObject.GetComponent<Stats>().AI && !_hit.transform.gameObject.GetComponent<Stats>().FreandAI && !_hit.transform.gameObject.GetComponent<Stats>().Neutral && !_hit.transform.gameObject.GetComponent<Stats>().NeutralAgrass)
                        {
                            if (!gameObject.GetComponent<AudioSource>().isPlaying)
                            {
                                gameObject.GetComponent<AudioSource>().clip = _GDB.selectList[0].GetComponent<Captan>().CurCap.Select[Random.Range(0, _GDB.selectList[0].GetComponent<Captan>().CurCap.Select.Count)];
                                gameObject.GetComponent<AudioSource>().Play();
                            }
                        }
                    }
                }
            }
            if (_hit.transform.gameObject.GetComponent<Station>())
            {
                isSelect = false;
                if (_hit.transform.gameObject.GetComponent<ObjectTypeVisible>().IsVisible)
                {
                    if (_GDB.activeObjectInterface != null)
                    {
                        _GDB.activeObjectInterface.gameObject.GetComponent<Station>().visible = false;

                        if (_GDB.selectList.Count <= 0)
                        {
                            _GDB.activeObjectInterface = _hit.transform.gameObject;
                            _GDB.activeObjectInterface.gameObject.GetComponent<Station>().visible = true;
                        }
                        else
                        {
                            _GDB.selectList.Clear();

                            _GDB.activeObjectInterface = _hit.transform.gameObject;
                            _GDB.activeObjectInterface.gameObject.GetComponent<Station>().visible = true;
                        }
                        if (!_GDB.activeObjectInterface.GetComponent<Station>().AI && !_GDB.activeObjectInterface.GetComponent<Station>().FreandAI && !_GDB.activeObjectInterface.GetComponent<Station>().Neutral && !_GDB.activeObjectInterface.GetComponent<Station>().NeutralAgrass)
                        {
                            if (!gameObject.GetComponent<AudioSource>().isPlaying)
                            {
                                gameObject.GetComponent<AudioSource>().clip = _GDB.activeObjectInterface.GetComponent<Captan>().CurCap.Select[Random.Range(0, _GDB.activeObjectInterface.GetComponent<Captan>().CurCap.Select.Count)];
                                gameObject.GetComponent<AudioSource>().Play();
                            }
                        }
                    }
                    else
                    {
                        if (_GDB.selectList.Count <= 0)
                        {
                            _GDB.activeObjectInterface = _hit.transform.gameObject;
                            _GDB.activeObjectInterface.gameObject.GetComponent<Station>().visible = true;
                        }
                        else
                        {
                            _GDB.selectList.Clear();

                            _GDB.activeObjectInterface = _hit.transform.gameObject;
                            _GDB.activeObjectInterface.gameObject.GetComponent<Station>().visible = true;
                        }
                        if (!_GDB.activeObjectInterface.GetComponent<Station>().AI && !_GDB.activeObjectInterface.GetComponent<Station>().FreandAI && !_GDB.activeObjectInterface.GetComponent<Station>().Neutral && !_GDB.activeObjectInterface.GetComponent<Station>().NeutralAgrass)
                        {
                            if (!gameObject.GetComponent<AudioSource>().isPlaying)
                            {
                                gameObject.GetComponent<AudioSource>().clip = _GDB.activeObjectInterface.GetComponent<Captan>().CurCap.Select[Random.Range(0, _GDB.activeObjectInterface.GetComponent<Captan>().CurCap.Select.Count)];
                                gameObject.GetComponent<AudioSource>().Play();
                            }
                        }
                    }
                }
            }

            if (_hit.transform.gameObject.tag == "Terrain")
            {
                _GDB.activeObjectInterface = null;

                int layerMask = 1 << 17;
                if (Physics.Raycast(_rayWOTerrain, out _hitWOTerrain, 10000.0f, layerMask))
                {
                    if (_hitWOTerrain.transform.gameObject.GetComponent<Stats>())
                    {
                        if (_hitWOTerrain.transform.gameObject.tag == "Dwarf" || _hitWOTerrain.transform.gameObject.tag == "Enemy" || _hitWOTerrain.transform.gameObject.tag == "Freand" || _hitWOTerrain.transform.gameObject.tag == "Neutral")
                        {
                            isSelect = false;
                            if (_hitWOTerrain.transform.gameObject.GetComponent<Stats>().SelectLock == false)
                            {
                                if (_GDB.selectList.Count <= 0)
                                {
                                    if (!STDLCMethods.FindInList(_hitWOTerrain.transform.gameObject, _GDB.selectList))
                                    {
                                        _GDB.selectList.Add(_hitWOTerrain.transform.gameObject);
                                    }
                                }
                                if (_GDB.selectList.Count > 1)
                                {
                                    if (!Input.GetKey(KeyCode.LeftShift))
                                    {
                                        _GDB.selectList.Clear();
                                    }
                                    if (!STDLCMethods.FindInList(_hitWOTerrain.transform.gameObject, _GDB.selectList))
                                    {
                                        _GDB.selectList.Add(_hitWOTerrain.transform.gameObject);
                                    }
                                }
                                if (_GDB.selectList.Count == 1)
                                {
                                    if (!Input.GetKey(KeyCode.LeftShift))
                                    {
                                        _GDB.selectList[0] = _hitWOTerrain.transform.gameObject;
                                    }
                                    else
                                    {
                                        if (!STDLCMethods.FindInList(_hitWOTerrain.transform.gameObject, _GDB.selectList))
                                        {
                                            _GDB.selectList.Add(_hitWOTerrain.transform.gameObject);
                                        }
                                    }
                                }
                                if (!_hitWOTerrain.transform.gameObject.GetComponent<Stats>().AI && !_hitWOTerrain.transform.gameObject.GetComponent<Stats>().FreandAI && !_hitWOTerrain.transform.gameObject.GetComponent<Stats>().Neutral && !_hitWOTerrain.transform.gameObject.GetComponent<Stats>().NeutralAgrass)
                                {
                                    if (!gameObject.GetComponent<AudioSource>().isPlaying)
                                    {
                                        gameObject.GetComponent<AudioSource>().clip = _GDB.selectList[0].GetComponent<Captan>().CurCap.Select[Random.Range(0, _GDB.selectList[0].GetComponent<Captan>().CurCap.Select.Count)];
                                        gameObject.GetComponent<AudioSource>().Play();
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (!Input.GetKey(KeyCode.LeftShift))
                    {
                        _GDB.selectList.Clear();
                    }
                }
            }
        }
    }

    void ActivatePatrol()
	{
		int i = 0;
		foreach (GameObject obj in _GDB.selectList)
		{
			Stats _ost = obj.GetComponent<Stats>();

			if (!_ost.AI && !_ost.FreandAI && !_ost.Neutral && !_ost.NeutralAgrass)
			{
				if (!Input.GetKey(KeyCode.LeftShift))
				{
					_ost.Order = true;
					_ost.targetTransform = null;
					_ost.GuartTarget = null;
					_ost.instruction = Stats.enInstruction.move;
					obj.GetComponent<MoveComponent>().InPatrol = true;
					_ost.StopOrder = true;
					i++;
					if (!gameObject.GetComponent<AudioSource>().isPlaying)
					{
						gameObject.GetComponent<AudioSource>().clip = _GDB.selectList[0].GetComponent<Captan>().CurCap.Move[Random.Range(0, _GDB.selectList[0].GetComponent<Captan>().CurCap.Move.Count)];
						gameObject.GetComponent<AudioSource>().Play();
					}
					obj.GetComponent<MoveComponent>().SetCurFleet(_GDB.selectList);

					LockOnPatrolSetting = false;
				}
			}
		}
	}
	void OnGUI()
	{
		if (isSelect)
		{
			GUI.DrawTexture(new Rect(_selX, _selY, _selWidth, _selHeight), texSelect);
		}
	}
	// Выделяет попавшие в зону объекты
	void FindSelect()
	{
		foreach (GameObject obj in _GDB.dwarfList)
		{
			Stats _ost = obj.GetComponent<Stats>();

			Vector2 objpos = Camera.main.WorldToScreenPoint(obj.transform.position);

			if ((objpos.x > _startPoint.x && objpos.x < _endPoint.x) || (objpos.x < _startPoint.x && objpos.x > _endPoint.x))
			{
				if ((objpos.y > _startPoint.y && objpos.y < _endPoint.y) || (objpos.y < _startPoint.y && objpos.y > _endPoint.y))
				{
					if (!STDLCMethods.FindInList(obj, _GDB.selectList) && _GDB.selectList.Count < 12)
					{
						if (!_ost.AI && !_ost.FreandAI && !_ost.Neutral && !_ost.NeutralAgrass)
						{
							if (_ost.SelectLock == false)
							{
								_ost.Proector.GetComponent<MeshRenderer>().enabled = true;
								_GDB.selectList.Add(obj);
							}
						}
						if (!_ost.AI && !_ost.FreandAI)
						{
							if (!gameObject.GetComponent<AudioSource>().isPlaying)
							{
								gameObject.GetComponent<AudioSource>().clip = _GDB.selectList[0].GetComponent<Captan>().CurCap.Select[Random.Range(0, _GDB.selectList[0].GetComponent<Captan>().CurCap.Select.Count)];
								gameObject.GetComponent<AudioSource>().Play();
							}
						}
					}
				}
			}
		}
		if (_GDB.selectList.Count != 0)
		{
			if (_GDB.activeObjectInterface != null)
				_GDB.deactivationInterface();
		}
	}

	void FindShipType(string Type)
	{
		foreach (GameObject obj in _GDB.dwarfList)
		{
			Stats _ost = obj.GetComponent<Stats>();

			Vector2 objpos = Camera.main.WorldToScreenPoint(obj.transform.position);

			if ((objpos.x > 0 && objpos.x < Screen.width) || (objpos.x < 0 && objpos.x > Screen.width))
			{
				if ((objpos.y > 0 && objpos.y < Screen.height) || (objpos.y < 0 && objpos.y > Screen.height))
				{
					if (!STDLCMethods.FindInList(obj, _GDB.selectList) && _GDB.selectList.Count < 12)
					{
						if (_ost.classname == Type)
						{
							if (!_ost.AI && !_ost.FreandAI && !_ost.Neutral && !_ost.NeutralAgrass)
							{
								if (_ost.SelectLock == false)
								{
									_ost.Proector.GetComponent<MeshRenderer>().enabled = true;
									_GDB.selectList.Add(obj);
								}
							}
							if (!_ost.AI && !_ost.FreandAI)
							{
								if (!gameObject.GetComponent<AudioSource>().isPlaying)
								{
									gameObject.GetComponent<AudioSource>().clip = _GDB.selectList[0].GetComponent<Captan>().CurCap.Select[Random.Range(0, _GDB.selectList[0].GetComponent<Captan>().CurCap.Select.Count)];
									gameObject.GetComponent<AudioSource>().Play();
								}
							}
						}
					}
				}
			}
		}
		if (_GDB.selectList.Count != 0)
		{
			if (_GDB.activeObjectInterface != null)
				_GDB.deactivationInterface();
		}
	}

	// Проверяет присутствует-ли объект в списке selectLis

	// Очистка выделенных юнитив
	public void ClearSelect()
	{
		foreach (GameObject selObj in _GDB.selectList)
		{
			selObj.GetComponent<Stats>().Proector.GetComponent<MeshRenderer>().enabled = false;
		}
		_GDB.selectList.Clear();
	}
	public void PlayMoveSound(GameObject ActiveUnit)
	{
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurCap.Move[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurCap.Move.Count)];
		gameObject.GetComponent<AudioSource>().Play();
	}
	public void PlaySelectSound(GameObject ActiveUnit)
	{
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurCap.Select[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurCap.Select.Count)];
		gameObject.GetComponent<AudioSource>().Play();
	}
	public void PlayAttackSound(GameObject ActiveUnit)
	{
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurCap.Attack[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurCap.Attack.Count)];
		gameObject.GetComponent<AudioSource>().Play();
	}
	public void PlayInvaliLocationSound(GameObject ActiveUnit)
	{
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurCap.LocationInvalid[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurCap.LocationInvalid.Count)];
		gameObject.GetComponent<AudioSource>().Play();
	}
	public void PlayIsUnderAttackSound(GameObject ActiveUnit)
	{
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurCap.IsUnderAttack[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurCap.IsUnderAttack.Count)];
		gameObject.GetComponent<AudioSource>().Play();
	}
	public void PlayFixSound(GameObject ActiveUnit)
	{
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurCap.Fix[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurCap.Fix.Count)];
		gameObject.GetComponent<AudioSource>().Play();
	}
	public void PlayLowResusesSound(GameObject ActiveUnit)
	{
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurCap.LowResuses[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurCap.LowResuses.Count)];
		gameObject.GetComponent<AudioSource>().Play();
	}
	public void PlayConstructingBeganSound(GameObject ActiveUnit)
	{
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurCap.ConstructingBegan[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurCap.ConstructingBegan.Count)];
		gameObject.GetComponent<AudioSource>().Play();
	}
	public void PlayConstructingEndSound(GameObject ActiveUnit)
	{
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurCap.ConstructingEnd[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurCap.ConstructingEnd.Count)];
		gameObject.GetComponent<AudioSource>().Play();
	}
	public void PlayConstructingCanseledSound(GameObject ActiveUnit)
	{
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurCap.ConstructingCanseled[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurCap.ConstructingCanseled.Count)];
		gameObject.GetComponent<AudioSource>().Play();
	}

	public void MoveCameraToCuttentShip()
	{
		PlayerCamera.transform.position = new Vector3(_GDB.selectList[0].transform.position.x, PlayerCamera.transform.position.y, _GDB.selectList[0].transform.position.z);
	}

	public void SetOrder(string Order)
	{
	    foreach (GameObject selObj in _GDB.selectList)
	    {
	        ActiveState _as = selObj.GetComponent<ActiveState>();
	        Stats _st = selObj.GetComponent<Stats>();
	        MoveComponent _mc = selObj.GetComponent<MoveComponent>();

	       
	            switch (Order)
	            {
	                case "RedAlert":
	                    _as.Agrass = true;
	                    _as.Protact = false;
	                    _as.Idle = false;
	                    break;
	                case "YellowAlert":
	                    _as.Protact = true;
	                    _as.Agrass = false;
	                    _as.Idle = false;
	                    break;
	                case "GreenAlert":
	                    _as.Idle = true;
	                    _as.Agrass = false;
	                    _as.Protact = false;
	                    break;
	                case "ImpulsAttack":
	                    _as.TargetingAt = ActiveState.AttackType.ImpulseSystemAttack;
	                    break;
	                case "WarpEngingAttack":
	                    _as.TargetingAt = ActiveState.AttackType.WarpEngingSystemAttack;
	                    break;
	                case "PrimaryWeaponAttack":
	                    _as.TargetingAt = ActiveState.AttackType.PrimaryWeaponSystemAttack;
	                    break;
	                case "SecondaryWeaponAttack":
	                    _as.TargetingAt = ActiveState.AttackType.SecondaryWeaponSystemAttack;
	                    break;
	                case "LifeSupportAttack":
	                    _as.TargetingAt = ActiveState.AttackType.LifeSupportSystemAttack;
	                    break;
	                case "TractorAttack":
	                    _as.TargetingAt = ActiveState.AttackType.TractorBeamSystemAttack;
	                    break;
	                case "WarpCoreAttack":
	                    _as.TargetingAt = ActiveState.AttackType.WarpCoreAttack;
	                    break;
	                case "SensorAttack":
	                    _as.TargetingAt = ActiveState.AttackType.SensorsSystemAttack;
	                    break;
	                case "NormalAttack":
	                    _as.TargetingAt = ActiveState.AttackType.NormalAttack;
	                    break;

	                case "FullStop":
	                    _as.isAttack = false;
	                    _st.targetTransform = null;
	                    _st.GuartTarget = null;
	                    _mc.Stop();
	                    _mc.InPatrol = false;
	                    _mc.PatrolWay.Clear();
	                    break;
	                case "Attack":
	                    if (!_as.ForcedFix)
	                    {
	                        OrderActive = true;
	                        AttackOrder = true;
	                    }
	                    break;
	                case "Guard":
	                    if (!_as.ForcedFix)
	                    {
	                        OrderActive = true;
	                        GuardOrder = true;
	                    }
	                    break;
	                case "Patrol":
	                    if (!_as.ForcedFix)
	                    {
	                        LockSelection = true;
	                        SettingPatrolWay = true;
	                    }
	                    break;
	                case "Fix":
	                    _as.Fix();
	                    break;
	                case "DeAssamble":
	                    _as.DeAssamble();
	                    break;
	            }
	        }
	    
	}
}

[System.Serializable]
public class Position
{
	public float x;
	public float z;
}