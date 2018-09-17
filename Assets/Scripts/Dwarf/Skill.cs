using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour
{
	public float Cost;

	public Sprite Icon;

	public LayerMask ShipLayer;

	public bool MaskitoShild;
	public GameObject MaskMesh;
	public Texture MaskitoShildTex;
	public GameObject HideShipMesh;
	public GameObject ShipMesh;
	public Texture2D MaskitoShipIcon;
	public Texture2D ShipIcon;
	public bool InMaskShild;


	public bool Healther;
	public Texture HealtherTex;

	public bool SpeedUp;
	public Texture SpeedUpTex;
	public float CurSpeed;
	public float MaxSpeed;

	public bool Tractor;
	public Texture TractorTex;
	public GameObject Target;
	private bool ObjectLocked;

	public bool Work;
	public bool CoolDownBool;
	public float Worktime;
	public float Timer;
	public float CoolDown;
	public float CoolDownTimer;

	public GUISkin mainSkin;
	public int numDepth = 1;

	public float XPosition = 220;
	public float YPosition = 70;
	public float XScale = 100;
	public float YScale = 100;

	public float Speed;

	//public float 

	private Stats _st;
	private HealthModule _hm;

	public int Number;
	// Use this for initialization
	void Start()
	{
		_st = gameObject.GetComponent<Stats>();
		_hm = gameObject.GetComponent<HealthModule>();
	}

	// Update is called once per frame
	void Update()
	{
		if (SpeedUp)
		{
			if (Work)
			{
				Timer -= Time.deltaTime;
				if (Timer > 0)
				{
					gameObject.GetComponent<MoveComponent>().MovementSpeed = MaxSpeed;
					_st.NormalSpeed = MaxSpeed;
				}
				if (Timer <= 0)
				{
					gameObject.GetComponent<MoveComponent>().MovementSpeed = CurSpeed;
					_st.NormalSpeed = CurSpeed;
					Work = false;
				}
			}
			if (CoolDownBool)
			{
				if (CoolDownTimer > 0)
				{
					CoolDownTimer -= Time.deltaTime;
				}
				else
				{
					CoolDownBool = false;
				}
			}
		}
		if (Healther)
		{
			if (Work)
			{
				Timer -= Time.deltaTime;
				if (Timer > 0)
				{
					if (_hm.curHealth < _hm.maxHealth)
					{
						_hm.curHealth += Time.deltaTime * Speed;
					}
					else
					{
						_hm.curHealth = _hm.maxHealth;
					}
				}
				if (Timer <= 0)
				{
					Work = false;
				}
			}
			if (CoolDownBool)
			{
				if (CoolDownTimer > 0)
				{
					CoolDownTimer -= Time.deltaTime;
				}
				else
				{
					CoolDownBool = false;
				}
			}
		}
		if (InMaskShild)
		{
			if (HideShipMesh != null)
			{
				if (HideShipMesh.activeSelf == false)
				{
					//_st.icon = MaskitoShipIcon;
					_st.InMaskito = true;
					ShipMesh.SetActive(false);
					MaskMesh.GetComponent<MaskitoShild>().MaskShild = true;
					HideShipMesh.SetActive(true);
				}
				else
				{
					//_st.icon = ShipIcon;
					_st.InMaskito = false;
					ShipMesh.SetActive(true);
					MaskMesh.GetComponent<MaskitoShild>().MaskShild = true;
					HideShipMesh.SetActive(false);
				}
				InMaskShild = false;
			}
		}
		if (gameObject.GetComponent<ActiveState>().Tractor.GetComponent<Tractor>().Use)
		{
			Tractor _tb = gameObject.GetComponent<ActiveState>().Tractor.GetComponent<Tractor>();
			if (_hm.maxCrew > 0)
			{
				if (_hm.curTractorBeamSystemHealth > _hm.maxTractorBeamSystemHealth * 0.125f && !_hm.ActiveTractor && _hm.curCrew > 0)
				{
					if (_tb.Target == null)
					{
						if (Input.GetMouseButtonDown(1))
						{
							Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
							RaycastHit hit;
							if (Physics.Raycast(ray, out hit, 10000, ShipLayer))
							{
								if (hit.transform.gameObject.GetComponent<Rigidbody>().mass <= gameObject.GetComponent<Rigidbody>().mass)
								{
									Target = hit.transform.gameObject;
								}
							}
						}
					}
				}
				else
				{
					Target = null;
					ObjectLocked = false;
					_tb.Use = false;
				}
			}
			else
			{
				if (_hm.curTractorBeamSystemHealth > _hm.maxTractorBeamSystemHealth * 0.125f && !_hm.ActiveTractor)
				{
					if (_tb.Target == null)
					{
						if (Input.GetMouseButtonDown(1))
						{
							Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
							RaycastHit hit;
							if (Physics.Raycast(ray, out hit, 10000, ShipLayer))
							{
								if (hit.transform.gameObject.GetComponent<Rigidbody>().mass < gameObject.GetComponent<Rigidbody>().mass)
								{
									Target = hit.transform.gameObject;
								}
							}
						}
					}
				}
				else
				{
					Target = null;
					ObjectLocked = false;
					_tb.Use = false;
				}
			}
			if (Target != null)
			{
				if (Target.tag == "Обломки" || Target.tag == "Dwarf" || Target.tag == "Enemy" || Target.tag == "Freand")
				{
					if (Target.tag == "Обломки")
					{
						if (!ObjectLocked)
						{
							if (Vector3.Distance(gameObject.transform.position, Target.transform.position) > _tb.Radius)
							{
								gameObject.GetComponent<MoveComponent>().Movement(Target.transform.position);
							}
							else
							{
								gameObject.GetComponent<MoveComponent>().Stop();
								_tb.Target = Target.transform.gameObject;
								_tb.StarShipOwner = null;
								ObjectLocked = true;
							}
						}
					}
					if (Target.tag == "Dwarf")
					{
						if (!ObjectLocked)
						{
							HealthModule _thm = Target.GetComponent<HealthModule>();
							if (_thm.Ship)
							{
								if (_thm.curImpulseSystemHealth <= 0)
								{
									if (Vector3.Distance(gameObject.transform.position, Target.transform.position) > _tb.Radius)
									{
										gameObject.GetComponent<MoveComponent>().Movement(Target.transform.position);
									}
									else
									{
										gameObject.GetComponent<MoveComponent>().Stop();
										_tb.Target = Target;
										_tb.StarShipOwner = Target;
										ObjectLocked = true;
									}
								}
							}
						}
					}
					if (Target.tag == "Enemy")
					{
						if (!ObjectLocked)
						{
							HealthModule _thm = Target.GetComponent<HealthModule>();
							if (_thm.Ship && _thm.CurShilds <= 0)
							{
								if (_thm.curImpulseSystemHealth <= 0 && _hm.CurShilds <= 0)
								{
									if (Vector3.Distance(gameObject.transform.position, Target.transform.position) > _tb.Radius)
									{
										gameObject.GetComponent<MoveComponent>().Movement(Target.transform.position);
									}
									else
									{
										gameObject.GetComponent<MoveComponent>().Stop();
										_tb.Target = Target;
										_tb.StarShipOwner = Target;
										ObjectLocked = true;
									}
								}
							}
							else
							{
								_tb.Target = null;
								Target = null;

								ObjectLocked = false;
							}
						}
					}
					if (Target.tag == "Freand")
					{
						if (!ObjectLocked)
						{
							HealthModule _thm = Target.GetComponent<HealthModule>();
							if (_thm.Ship && _thm.CurShilds <= 0)
							{
								if (_thm.curImpulseSystemHealth <= 0 && _hm.CurShilds <= 0)
								{
									if (Vector3.Distance(gameObject.transform.position, Target.transform.position) > _tb.Radius)
									{
										gameObject.GetComponent<MoveComponent>().Movement(Target.transform.position);
									}
									else
									{
										gameObject.GetComponent<MoveComponent>().Stop();
										_tb.Target = Target;
										_tb.StarShipOwner = Target;
										ObjectLocked = true;
									}
								}
							}
							else
							{
								_tb.Target = null;
								Target = null;

								ObjectLocked = false;
							}
						}
					}
				}
				else
				{
				//	_tb.Target = null;
					Target = null;

					ObjectLocked = false;
				}
			}
			else
			{
			//	_tb.Target = null;
				Target = null;

				ObjectLocked = false;
			}
		}
		else
		{
			Target = null;

			ObjectLocked = false;
		}
	}
	public void Active()
	{
		if (_st.isSelect)
		{
			if (GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>().selectList.Count == 1)
			{
				if (GameObject.FindGameObjectWithTag("MainUI").GetComponent<Select>().SpecialActive)
				{
					if (!gameObject.GetComponent<Stats>().AI && !gameObject.GetComponent<Stats>().FreandAI)
					{
						if (MaskitoShild)
						{
							InMaskShild = true;
						}
						if (Healther)
						{
							if (CoolDownTimer <= 0)
							{
								if (_hm.curEnergy - Cost > 0)
								{
									_hm.curEnergy -= Cost;
									Work = true;
									CoolDownBool = true;
									Timer = Worktime;
									CoolDownTimer = CoolDown;
								}
							}
							if (CoolDownTimer > 0)
							{
								//		GUI.Label (new Rect (XPosition, YPosition, XScale, YScale), System.String.Empty + CoolDownTimer.ToString ());
							}
						}
						if (SpeedUp)
						{
							if (CoolDownTimer <= 0)
							{
								if (_hm.curEnergy - Cost > 0)
								{
									_hm.curEnergy -= Cost;
									Work = true;
									CoolDownBool = true;
									Timer = Worktime;
									CoolDownTimer = CoolDown;
								}
							}
							if (CoolDownTimer > 0)
							{
								//	GUI.Label (new Rect (XPosition, YPosition, XScale, YScale), System.String.Empty + CoolDownTimer.ToString ());
							}
						}
						if (Tractor)
						{
							if (_hm.curTractorBeamSystemHealth > _hm.maxTractorBeamSystemHealth * 0.125f && !_hm.ActiveTractor)
							{
								gameObject.GetComponent<ActiveState>().Tractor.GetComponent<Tractor>().Use = !gameObject.GetComponent<ActiveState>().Tractor.GetComponent<Tractor>().Use;
							}
						}
					}
				}
			}
		}
	}
}