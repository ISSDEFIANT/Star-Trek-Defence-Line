using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Select : MonoBehaviour
{
	public bool isSelect;
	//public bool Lock;

	public Texture Protect;
	public Texture Attack;
	public Texture Idle;

	public Texture ImpulseSystemAttack;
	public Texture LifeSupportSystemAttack;
	public Texture PrimaryWeaponSystemAttack;
	public Texture SensorsSystemAttack;
	public Texture TractorBeamSystemAttack;
	public Texture WarpEngingSystemAttack;
	public Texture WarpCoreAttack;
	public Texture SecondaryWeaponSystemAttack;

	public Texture NormalAttack;

	public Texture Crew;
	public Texture ImpulseSystem;
	public Texture LifeSupportSystem;
	public Texture PrimaryWeaponSystem;
	public Texture SensorsSystem;
	public Texture TractorBeamSystem;
	public Texture WarpEngingSystem;
	public Texture WarpCore;
	public Texture SecondaryWeaponSystem;

	private float _selX;
	private float _selY;
	private float _selWidth;
	private float _selHeight;

	private float _selXOld;
	private float _selYOld;

	public Texture2D texSelect;

	public GUISkin mainSkin;
	public int numDepth = 1;
	public int ButtonDepth = 0;

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
	private float BuilTime = 0.1f;
	private bool Building;
	public bool RPGTrue;

	public Texture Torpido1;
	public GameObject torpido1;
	public float ReloadTime1;

	public Texture Torpido2;
	public GameObject torpido2;
	public float ReloadTime2;

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
			if (!_ost.AI && !_ost.FreandAI && !_ost.Neutral && !_ost.NeutralAgrass)
			{
				_ost.Order = true;
				_ost.targetTransform = targetGO;
				_ost.instruction = Stats.enInstruction.attack;
				Instantiate(AttackMark, targetGO.transform.position, MoveMark.transform.rotation);
				if (!_ost.AI && !_ost.FreandAI && !_ost.Neutral && !_ost.NeutralAgrass)
				{
					if (!gameObject.GetComponent<AudioSource>().isPlaying)
					{
						gameObject.GetComponent<AudioSource>().clip = _GDB.selectList[0].GetComponent<Captan>().CurAttack[Random.Range(0, _GDB.selectList[0].GetComponent<Captan>().CurAttack.Count)];
						gameObject.GetComponent<AudioSource>().Play();
					}
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
			if (!_ost.AI && !_ost.FreandAI && !_ost.Neutral && !_ost.NeutralAgrass)
			{
				_ost.GuartTarget = GTarget.transform;
				_gthm.ShipsForDefence.Add(obj);
				if (i <= _gthm.ShipsForDefence.Count - 1)
				{
					Instantiate(GuardMark, GTarget.transform.position + (rotation * new Vector3(_gthm.ProtectPosition[i].x, 0, _gthm.ProtectPosition[i].y)), MoveMark.transform.rotation);
				}
				i++;
				if (!_ost.AI && !_ost.FreandAI && !_ost.Neutral && !_ost.NeutralAgrass)
				{
					if (!gameObject.GetComponent<AudioSource>().isPlaying)
					{
						gameObject.GetComponent<AudioSource>().clip = _GDB.selectList[0].GetComponent<Captan>().CurAttack[Random.Range(0, _GDB.selectList[0].GetComponent<Captan>().CurAttack.Count)];
						gameObject.GetComponent<AudioSource>().Play();
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
			if (!_ost.AI && !_ost.FreandAI && !_ost.Neutral && !_ost.NeutralAgrass)
			{
				Instantiate(MoveMark, new Vector3(targetVec.x + pos[i].x, targetVec.y, targetVec.z + pos[i].z), MoveMark.transform.rotation);
				_ost.Order = true;
				_ost.targetTransform = null;
				_ost.GuartTarget = null;
				_ost.instruction = Stats.enInstruction.move;
				_ost.targetVector = new Vector3(targetVec.x + pos[i].x, targetVec.y, targetVec.z + pos[i].z);
				obj.GetComponent<MoveComponent>().Movement(new Vector3(targetVec.x + pos[i].x, targetVec.y, targetVec.z + pos[i].z));
				_ost.StopOrder = true;
				i++;
				if (!gameObject.GetComponent<AudioSource>().isPlaying)
				{
					gameObject.GetComponent<AudioSource>().clip = _GDB.selectList[0].GetComponent<Captan>().CurMove[Random.Range(0, _GDB.selectList[0].GetComponent<Captan>().CurMove.Count)];
					gameObject.GetComponent<AudioSource>().Play();
				}
			}
		}
	}
	// Update is called once per frame

	int layerMaskForShipHover = 1 << 17;


	void ShipOrder()
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
										if (_GDB.selectList[0].GetComponent<Captan>().CurLocationInvalid.Count > 0)
										{
											gameObject.GetComponent<AudioSource>().clip = _GDB.selectList[0].GetComponent<Captan>().CurLocationInvalid[Random.Range(0, _GDB.selectList[0].GetComponent<Captan>().CurLocationInvalid.Count)];
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
											if (_GDB.selectList[0].GetComponent<Captan>().CurLocationInvalid.Count > 0)
											{
												gameObject.GetComponent<AudioSource>().clip = _GDB.selectList[0].GetComponent<Captan>().CurLocationInvalid[Random.Range(0, _GDB.selectList[0].GetComponent<Captan>().CurLocationInvalid.Count)];
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

	void Update()
	{
		float X;
		float Y;
		X = Screen.width / 100;
		Y = Screen.height / 100;
		_rayShipHover = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Input.GetMouseButtonDown(0))
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
						if (_hit.transform.gameObject.GetComponent<Stats>().SelectLock == false)
						{
							if (_GDB.selectList.Count <= 0)
							{
								if (!FindInSelectList(_hit.transform.gameObject))
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
								if (!FindInSelectList(_hit.transform.gameObject))
								{
									_GDB.selectList.Add(_hit.transform.gameObject);
								}
							}
							if (_GDB.selectList.Count == 1)
							{
								if (!Input.GetKey(KeyCode.LeftShift))
								{
									_GDB.selectList[0] = _hit.transform.gameObject;
								}
								else
								{
									if (!FindInSelectList(_hit.transform.gameObject))
									{
										_GDB.selectList.Add(_hit.transform.gameObject);
									}
								}
							}
							if (!_hit.transform.gameObject.GetComponent<Stats>().AI && !_hit.transform.gameObject.GetComponent<Stats>().FreandAI && !_hit.transform.gameObject.GetComponent<Stats>().Neutral && !_hit.transform.gameObject.GetComponent<Stats>().NeutralAgrass)
							{
								if (!gameObject.GetComponent<AudioSource>().isPlaying)
								{
									gameObject.GetComponent<AudioSource>().clip = _GDB.selectList[0].GetComponent<Captan>().CurSelect[Random.Range(0, _GDB.selectList[0].GetComponent<Captan>().CurSelect.Count)];
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
									gameObject.GetComponent<AudioSource>().clip = _GDB.activeObjectInterface.GetComponent<Captan>().CurSelect[Random.Range(0, _GDB.activeObjectInterface.GetComponent<Captan>().CurSelect.Count)];
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
									gameObject.GetComponent<AudioSource>().clip = _GDB.activeObjectInterface.GetComponent<Captan>().CurSelect[Random.Range(0, _GDB.activeObjectInterface.GetComponent<Captan>().CurSelect.Count)];
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
										if (!FindInSelectList(_hitWOTerrain.transform.gameObject))
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
										if (!FindInSelectList(_hitWOTerrain.transform.gameObject))
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
											if (!FindInSelectList(_hitWOTerrain.transform.gameObject))
											{
												_GDB.selectList.Add(_hitWOTerrain.transform.gameObject);
											}
										}
									}
									if (!_hitWOTerrain.transform.gameObject.GetComponent<Stats>().AI && !_hitWOTerrain.transform.gameObject.GetComponent<Stats>().FreandAI && !_hitWOTerrain.transform.gameObject.GetComponent<Stats>().Neutral && !_hitWOTerrain.transform.gameObject.GetComponent<Stats>().NeutralAgrass)
									{
										if (!gameObject.GetComponent<AudioSource>().isPlaying)
										{
											gameObject.GetComponent<AudioSource>().clip = _GDB.selectList[0].GetComponent<Captan>().CurSelect[Random.Range(0, _GDB.selectList[0].GetComponent<Captan>().CurSelect.Count)];
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
			ShipOrder();
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
								GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().HoverBool = false;
							}
							if (_GDB.selectList.Count >= 1)
							{
								GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().AttackBool = false;
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
						if (_GDB.selectList.Count >= 1 || _GDB.activeObjectInterface.GetComponent<WeaponModule>())
						{
							GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().AttackBool = true;
							GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().GoBool = false;
							GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().IdleBool = false;
							GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().BaseBool = false;
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
					GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().HoverBool = false;
				}
				if (_GDB.selectList.Count >= 1)
				{
					GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().AttackBool = false;
				}
				if (!_st.ShadowProjectorActive)
				{
					_LastHoveredShip = null;
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
					if (!FindInSelectList(obj) && _GDB.selectList.Count < 12)
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
								gameObject.GetComponent<AudioSource>().clip = _GDB.selectList[0].GetComponent<Captan>().CurSelect[Random.Range(0, _GDB.selectList[0].GetComponent<Captan>().CurSelect.Count)];
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

	// Проверяет присутствует-ли объект в списке selectList
	bool FindInSelectList(GameObject obj)
	{
		foreach (GameObject selObj in _GDB.selectList)
		{
			if (selObj == obj)
				return true;
			//	obj.GetComponent<Stats>().Proector.GetComponent<MeshRenderer>().enabled = false;
		}
		return false;
	}

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
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurMove[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurMove.Count)];
		gameObject.GetComponent<AudioSource>().Play();
	}
	public void PlaySelectSound(GameObject ActiveUnit)
	{
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurSelect[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurSelect.Count)];
		gameObject.GetComponent<AudioSource>().Play();
	}
	public void PlayAttackSound(GameObject ActiveUnit)
	{
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurAttack[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurAttack.Count)];
		gameObject.GetComponent<AudioSource>().Play();
	}
	public void PlayInvaliLocationSound(GameObject ActiveUnit)
	{
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurLocationInvalid[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurLocationInvalid.Count)];
		gameObject.GetComponent<AudioSource>().Play();
	}
	public void PlayIsUnderAttackSound(GameObject ActiveUnit)
	{
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurIsUnderAttack[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurIsUnderAttack.Count)];
		gameObject.GetComponent<AudioSource>().Play();
	}
	public void PlayFixSound(GameObject ActiveUnit)
	{
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurFix[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurFix.Count)];
		gameObject.GetComponent<AudioSource>().Play();
	}
	public void PlayLowResusesSound(GameObject ActiveUnit)
	{
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurLowResuses[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurLowResuses.Count)];
		gameObject.GetComponent<AudioSource>().Play();
	}
	public void PlayConstructingBeganSound(GameObject ActiveUnit)
	{
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurConstructingBegan[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurConstructingBegan.Count)];
		gameObject.GetComponent<AudioSource>().Play();
	}
	public void PlayConstructingEndSound(GameObject ActiveUnit)
	{
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurConstructingEnd[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurConstructingEnd.Count)];
		gameObject.GetComponent<AudioSource>().Play();
	}
	public void PlayConstructingCanseledSound(GameObject ActiveUnit)
	{
		gameObject.GetComponent<AudioSource>().clip = ActiveUnit.GetComponent<Captan>().CurConstructingCanseled[Random.Range(0, ActiveUnit.GetComponent<Captan>().CurConstructingCanseled.Count)];
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
			if (Order == "RedAlert")
			{
				_as.Agrass = true;
				_as.Protact = false;
				_as.Idle = false;
			}
			if (Order == "YellowAlert")
			{
				_as.Protact = true;
				_as.Agrass = false;
				_as.Idle = false;
			}
			if (Order == "GreenAlert")
			{
				_as.Idle = true;
				_as.Agrass = false;
				_as.Protact = false;
			}
			if (Order == "ImpulsAttack")
			{
				_as.LifeSupportSystemAttack = false;
				_as.ImpulseSystemAttack = true;
				_as.PrimaryWeaponSystemAttack = false;
				_as.SensorsSystemAttack = false;
				_as.TractorBeamSystemAttack = false;
				_as.WarpEngingSystemAttack = false;
				_as.WarpCoreAttack = false;
				_as.SecondaryWeaponSystemAttack = false;
			}
			if (Order == "WarpEngingAttack")
			{
				_as.LifeSupportSystemAttack = false;
				_as.ImpulseSystemAttack = false;
				_as.PrimaryWeaponSystemAttack = false;
				_as.SensorsSystemAttack = false;
				_as.TractorBeamSystemAttack = false;
				_as.WarpEngingSystemAttack = true;
				_as.WarpCoreAttack = false;
				_as.SecondaryWeaponSystemAttack = false;
			}
			if (Order == "PrimaryWeaponAttack")
			{
				_as.LifeSupportSystemAttack = false;
				_as.ImpulseSystemAttack = false;
				_as.PrimaryWeaponSystemAttack = true;
				_as.SensorsSystemAttack = false;
				_as.TractorBeamSystemAttack = false;
				_as.WarpEngingSystemAttack = false;
				_as.WarpCoreAttack = false;
				_as.SecondaryWeaponSystemAttack = false;
			}
			if (Order == "SecondaryWeaponAttack")
			{
				_as.LifeSupportSystemAttack = false;
				_as.ImpulseSystemAttack = false;
				_as.PrimaryWeaponSystemAttack = false;
				_as.SensorsSystemAttack = false;
				_as.TractorBeamSystemAttack = false;
				_as.WarpEngingSystemAttack = false;
				_as.WarpCoreAttack = false;
				_as.SecondaryWeaponSystemAttack = true;
			}
			if (Order == "LifeSupportAttack")
			{
				_as.LifeSupportSystemAttack = true;
				_as.ImpulseSystemAttack = false;
				_as.PrimaryWeaponSystemAttack = false;
				_as.SensorsSystemAttack = false;
				_as.TractorBeamSystemAttack = false;
				_as.WarpEngingSystemAttack = false;
				_as.WarpCoreAttack = false;
				_as.SecondaryWeaponSystemAttack = false;
			}
			if (Order == "TractorAttack")
			{
				_as.LifeSupportSystemAttack = false;
				_as.ImpulseSystemAttack = false;
				_as.PrimaryWeaponSystemAttack = false;
				_as.SensorsSystemAttack = false;
				_as.TractorBeamSystemAttack = true;
				_as.WarpEngingSystemAttack = false;
				_as.WarpCoreAttack = false;
				_as.SecondaryWeaponSystemAttack = false;
			}
			if (Order == "WarpCoreAttack")
			{
				_as.LifeSupportSystemAttack = false;
				_as.ImpulseSystemAttack = false;
				_as.PrimaryWeaponSystemAttack = false;
				_as.SensorsSystemAttack = false;
				_as.TractorBeamSystemAttack = false;
				_as.WarpEngingSystemAttack = false;
				_as.WarpCoreAttack = true;
				_as.SecondaryWeaponSystemAttack = false;
			}
			if (Order == "SensorAttack")
			{
				_as.LifeSupportSystemAttack = false;
				_as.ImpulseSystemAttack = false;
				_as.PrimaryWeaponSystemAttack = false;
				_as.SensorsSystemAttack = true;
				_as.TractorBeamSystemAttack = false;
				_as.WarpEngingSystemAttack = false;
				_as.WarpCoreAttack = false;
				_as.SecondaryWeaponSystemAttack = false;
			}
			if (Order == "NormalAttack")
			{
				_as.LifeSupportSystemAttack = false;
				_as.ImpulseSystemAttack = false;
				_as.PrimaryWeaponSystemAttack = false;
				_as.SensorsSystemAttack = false;
				_as.TractorBeamSystemAttack = false;
				_as.WarpEngingSystemAttack = false;
				_as.WarpCoreAttack = false;
				_as.SecondaryWeaponSystemAttack = false;
			}

			if (Order == "FullStop")
			{
				_as.isAttack = false;
				_st.targetTransform = null;
				_st.GuartTarget = null;
				_mc.Stop();
			}
			if (Order == "Attack")
			{
				OrderActive = true;
				AttackOrder = true;
			}
			if (Order == "Guard")
			{
				OrderActive = true;
				GuardOrder = true;
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