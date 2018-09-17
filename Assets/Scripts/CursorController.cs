using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CursorController : MonoBehaviour
{
    //
    public CursorInfo Abilitis;
    //Пока не создано

    public CursorInfo Attack;

    //
    public CursorInfo Colony;
    //Пока не создано

    public CursorInfo Guard;

    public CursorInfo Repair;

    public CursorInfo Move;

    public CursorInfo Hover;

    public CursorInfo Mine;

    //
    public CursorInfo Error;
    //Пока не создано

    public CursorInfo Idle;

    //
    public CursorInfo Transport;
	//Пока не создано


	private GlobalDB _GDB;
	private Select _s;

	public int numDepth = 1;
	public List<Texture2D> Cursors;
	public Texture2D Static_cursor;
	private int i;
	public float TimerChange;
	private Texture2D cur;
	public bool UseStaticCursor;
	private float TimerDown;

	public float MinusX;
	public float MinusY;

	public float SizeX = 25;
	public float SizeY = 25;

	private Ray _rayShipHover;
	private RaycastHit _hitShipHover;

	int layerMaskForShipHover = 1 << 16 | 1 << 17;
	// Use this for initialization
	void Awake()
	{
		_GDB = gameObject.GetComponent<GlobalDB>();
		_s = gameObject.GetComponent<Select>();

		TimerDown = 0;
	}

	// Update is called once per frame
	void Update()
	{
		UnityEngine.Cursor.visible = false;
		if (Cursors.Count > 0)
		{
			if (i >= Cursors.Count)
			{
				i = 0;
			}
		}
		float X;
		float Y;
		X = Screen.width / 100;
		Y = Screen.height / 100;

		if (Input.mousePosition.y < Y * 27 & Input.mousePosition.y > Y * 0 & Input.mousePosition.x < X * 100 & Input.mousePosition.x > X * 0)
		{
			SetCursor(Idle);
		}
		else
		{
			_rayShipHover = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(_rayShipHover, out _hitShipHover, 10000.0f, layerMaskForShipHover))
			{
				GameObject Target = _hitShipHover.transform.gameObject;

				if (_GDB.selectList.Count > 0)
				{
					if (Target.GetComponent<Stats>())
					{
						Stats _tst = Target.GetComponent<Stats>();

						if (!_tst.AI)
						{
							if (_s.GuardOrder)
							{
								SetCursor(Guard);
							}
							else if (_s.AttackOrder)
							{
							    SetCursor(Attack);
                            }
							else
							{
								SetCursor(Hover);
							}
						}
						else
						{
							if (_GDB.selectList[0].GetComponent<Stats>().AI || _GDB.selectList[0].GetComponent<Stats>().FreandAI || _GDB.selectList[0].GetComponent<Stats>().Neutral)
							{
							    SetCursor(Hover);
                            }
							else
							{
							    SetCursor(Attack);
                            }
						}
					}
					if (Target.GetComponent<Station>())
					{
						Station _tst = Target.GetComponent<Station>();

						if (!_tst.AI)
						{
							foreach (GameObject ships in _GDB.selectList)
							{
								HealthModule _hm = ships.GetComponent<HealthModule>();

								if (_s.GuardOrder)
								{
								    SetCursor(Guard);
                                }
								if (_s.AttackOrder)
								{
								    SetCursor(Attack);
                                }
								if (!_s.AttackOrder && !_s.GuardOrder)
								{
									if (NeedFix(_hm))
									{
										if (_tst.FixModule)
										{
											SetCursor(Repair);
										}
										else
										{
										    SetCursor(Hover);
                                        }
									}
									if (ships.GetComponent<Stats>().miner)
									{
										if (_tst.MineModule)
										{
											SetCursor(Mine);
										}
										else
										{
										    SetCursor(Hover);
                                        }
									}
									if (!ships.GetComponent<Stats>().miner && !NeedFix(_hm))
									{
									    SetCursor(Hover);
                                    }
								}
							}
						}
						else
						{
						    SetCursor(Attack);
                        }
					}
				}
				else
				{
					if (_GDB.activeObjectInterface == null)
					{
						if (Target.GetComponent<Stats>() || Target.GetComponent<Station>())
						{
						    SetCursor(Hover);
                        }
					}
					else
					{
						if (Target.GetComponent<Stats>())
						{
							Stats _tst = Target.GetComponent<Stats>();

							if (!_tst.AI)
							{
								if (_s.GuardOrder)
								{
								    SetCursor(Guard);
                                }
								else if (_s.AttackOrder)
								{
								    SetCursor(Attack);
                                }
								else
								{
								    SetCursor(Hover);
                                }
							}
							else
							{
							    SetCursor(Attack);
                            }
						}
						if (Target.GetComponent<Station>())
						{
							Station _tst = Target.GetComponent<Station>();

							if (!_tst.AI)
							{
								if (_s.GuardOrder)
								{
								    SetCursor(Guard);
                                }
								if (_s.AttackOrder)
								{
								    SetCursor(Attack);
                                }
								if (!_s.AttackOrder && !_s.GuardOrder)
								{
								    SetCursor(Hover);
                                }
							}
							else
							{
							    SetCursor(Attack);
                            }
						}
					}
				}
			}
			else
			{
				if (_GDB.selectList.Count > 0)
				{
					SetCursor(Move);
				}
				else
				{
				    SetCursor(Idle);
                }
			}
		}
	}

    void SetCursor(CursorInfo ci)
    {
        if (ci.Cursor.Length == 1)
        {
            UseStaticCursor = true;
            Static_cursor = ci.Cursor[0];
        }
        else
        {
            UseStaticCursor = false;
        }
      
        Cursors = ci.Cursor.ToList();
        TimerChange = ci.Speed;
        if (!ci.InCenter)
        {
            MinusX = ci.Size.x / 1000;
            MinusY = ci.Size.y / 1000;
        }
        else
        {
            MinusX = ci.Size.x / 2;
            MinusY = ci.Size.y / 2;
        }
        SizeX = ci.Size.x;
        SizeY = ci.Size.y;
    }

    void OnGUI()
	{
		GUI.depth = numDepth;
		Vector2 MP = Input.mousePosition;
		MP.y = Screen.height - MP.y;
		MP.x -= MinusX;
		MP.y -= MinusY;
		if (UseStaticCursor)
		{
			if (Static_cursor != null) GUI.DrawTexture(new Rect(MP.x, MP.y, SizeX, SizeY), Static_cursor);
		}
		else
		{
			if (Cursors.Count > 0)
			{
				GUI.DrawTexture(new Rect(MP.x, MP.y, SizeX, SizeY), cur);
				TimerDown -= Time.deltaTime;
				if (TimerDown <= 0)
				{
					if (Cursors[i] != null)
					{
						cur = Cursors[i];
					}
					i++;
					TimerDown = TimerChange;
				}
			}
		}
	}
	bool NeedFix(HealthModule _hm)
	{
		if (_hm.curHealth < _hm.maxHealth || _hm.curImpulseSystemHealth < _hm.maxImpulseSystemHealth || _hm.curLifeSupportSystemHealth < _hm.maxLifeSupportSystemHealth || _hm.curPrimaryWeaponSystemHealth < _hm.maxPrimaryWeaponSystemHealth || _hm.curTractorBeamSystemHealth < _hm.maxTractorBeamSystemHealth || _hm.curWarpEngingSystemHealth < _hm.maxWarpEngingSystemHealth || _hm.curWarpCoreHealth < _hm.maxWarpCoreHealth || _hm.curSecondaryWeaponSystemHealth < _hm.maxSecondaryWeaponSystemHealth && _hm.curCrew >= _hm.maxCrew && _hm.CurShilds >= _hm.Shilds)
		{
			return true;
		}
		return false;
	}
}

[System.Serializable]
public class CursorInfo
{
    public bool InCenter;
    public Texture2D[] Cursor;
    public float Speed;
    public Vector2 Size;
}