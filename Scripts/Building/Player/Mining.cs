using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Mining : MonoBehaviour 
{
	public bool AI;
	public bool FreandAI;
	public bool NeutralAgrass;
	public bool Neutral;

	public bool visible = false;
	
	public GUISkin mainSkin;
	public int numDepth = 1;
	
	public Sprite tex;
	
	public string nameMining;
	public float xp;
	public int curHealth;
	public int protection;
	public int income;
	
	public float timer;
	public float As;
	public GameObject miner2;
	public bool miner;
	public float ХранениеРесурсов;
	public GameObject MeshFOW;
	public bool Titanium;
	public bool Dilithium;
	public bool Human;

	public GameObject Owner;

	private float _timerDown;
	
	private GlobalDB _GDB;
	private Select _SEL;
	private float timer2=0.1f;

	public bool MSDActive;
	public bool OrdersActive;
	public bool SpecialActive;
	public bool BuildActive;

	public Texture HealthBar;
	public Texture ShildBar;
	public Texture EnergyBar;

	public Texture GreenSphere1;
	public Texture GreenSphere2;
	public Texture GreenSphere3;
	public Texture GreenSphere4;
	public Texture GreenSphere5;
	public Texture GreenSphere6;
	public Texture GreenSphere7;
	public Texture GreenSphere8;
	public Texture BlackSphere;

	private float HealthBarLen;
	private float ShildBarLen;
	private float EnergyBarLen;

	public Texture Crew;
	public Texture ImpulseSystem;
	public Texture LifeSupportSystem;
	public Texture PrimaryWeaponSystem;
	public Texture SensorsSystem;
	public Texture TractorBeamSystem;
	public Texture WarpEngingSystem;
	public Texture WarpCore;
	public Texture SecondaryWeaponSystem;

	public Sprite ShipBluePrint;

	public GUIStyle style;

	public GameObject CurrentMiner;
	void Start () 
	{
		_timerDown = timer;
		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
		_SEL = GameObject.FindGameObjectWithTag("MainUI").GetComponent<Select>();
		style = _SEL.style;
		gameObject.name = "Mining(Clone)";
	}
	
	void OnMouseDown ()
	{
		_SEL.ClearSelect();
		if(_GDB.activeObjectInterface != null)
			_GDB.deactivationInterface();
		_GDB.activeObjectInterface = gameObject;
		visible = true;
		GameObject.FindGameObjectWithTag("MainUI").GetComponent<BackgroudUI>().pictureSelectObject = tex;
	}
	
	// Update is called once per frame
		void Update () 
	{
		if (Neutral) {
			if (Owner != null) {
				Owner = null;
			}
		}
		if (!visible && _GDB.activeObjectInterface == gameObject) {
			_GDB.activeObjectInterface = null;
		}
		if (_GDB.selectList.Count > 0) {
			visible = false;
		}
		if (!AI && !FreandAI && !Neutral) {
			if (As > 0) {
				if (Human) {
					_GDB.Humans += Time.deltaTime * 20;
					As -= Time.deltaTime * 20;
				}
				if (Dilithium) {
					_GDB.Dilithium += Time.deltaTime * 20;
					As -= Time.deltaTime * 20;
				}
				if (Titanium) {
					_GDB.Titanium += Time.deltaTime * 20;
					As -= Time.deltaTime * 20;
				}
			}
			if (Input.GetKeyDown (KeyCode.Mouse1)) {
				Ray ray = Camera.main.ScreenPointToRay (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 1));
				RaycastHit _hit;
				if (Physics.Raycast (ray, out _hit, Mathf.Infinity)) {
					if(_GDB.selectList.Count != 0){
						if (_GDB.selectList [0].GetComponent<Stats> ().miner) {
							if (_hit.transform.gameObject.GetComponent<Mining> ().miner == true) {
								if (_GDB.selectList.Count != 0) {
									if (_GDB.selectList [0].GetComponent<Stats> ().miner) {
										_GDB.selectList [0].GetComponent<Miner> ().enabled = true;
									}
								}
							}
						}
					}
				}
			}
		}
		if (AI) {
			if (As > 0) {
				if (Human) {
					Owner.GetComponent<GlobalAI>().Crew += Time.deltaTime * 20;
					As -= Time.deltaTime * 20;
				}
				if (Dilithium) {
					Owner.GetComponent<GlobalAI>().Dilithium += Time.deltaTime * 20;
					As -= Time.deltaTime * 20;
				}
				if (Titanium) {
					Owner.GetComponent<GlobalAI>().Titanium += Time.deltaTime * 20;
					As -= Time.deltaTime * 20;
				}
			}
			if (!FindInAI (gameObject)) {
				Owner.GetComponent<GlobalAI> ().Mines.Add (gameObject);
			}
		}
		if (FreandAI) {
			if (As > 0) {
				if (Human) {
					Owner.GetComponent<GlobalAI>().Crew += Time.deltaTime * 20;
					As -= Time.deltaTime * 20;
				}
				if (Dilithium) {
					Owner.GetComponent<GlobalAI>().Dilithium += Time.deltaTime * 20;
					As -= Time.deltaTime * 20;
				}
				if (Titanium) {
					Owner.GetComponent<GlobalAI>().Titanium += Time.deltaTime * 20;
					As -= Time.deltaTime * 20;
				}
			}
			if (!FindInAI (gameObject)) {
				Owner.GetComponent<GlobalAI> ().Mines.Add (gameObject);
			}
		}
		if (AI) {
			gameObject.tag = "Enemy";
		}
		if (!AI) {
			if (FreandAI && !Neutral && !NeutralAgrass) {
				gameObject.tag = "Freand";
			}
			if (!FreandAI && !Neutral && !NeutralAgrass) {
				gameObject.tag = "Dwarf";
			}
		}
		if (Neutral) {
			gameObject.tag = "Neutral";
		}
		if (NeutralAgrass) {
			gameObject.tag = "NeutralAgrass";
		}
	}
	void OnMouseEnter(){
		if (!AI) {
			if (_GDB.selectList.Count >= 1) {
				if (_GDB.selectList [0].GetComponent<Stats> ().miner) {
					GameObject.FindGameObjectWithTag ("Coursor").GetComponent<CursorController> ().AttackBool = false;
					GameObject.FindGameObjectWithTag ("Coursor").GetComponent<CursorController> ().GoBool = false;
					GameObject.FindGameObjectWithTag ("Coursor").GetComponent<CursorController> ().IdleBool = false;
					GameObject.FindGameObjectWithTag ("Coursor").GetComponent<CursorController> ().BaseBool = true;
				}
			}
		}
		if (AI) {
			if (_GDB.selectList.Count >= 1) {
				GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().AttackBool = true;
				GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().GoBool = false;
				GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().IdleBool = false;
				GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().BaseBool = false;
			}
		}
	}
	void OnMouseExit(){
		if (_GDB.selectList.Count >= 1) {
			GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().BaseBool = false;
		}
		if (AI) {
			GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().AttackBool = false;
			GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().GoBool = false;
			GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().IdleBool = false;
			GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().BaseBool = false;
		}
	}
	void OnDestroy(){
		if (FindInAI (gameObject)) {
			Owner.GetComponent<GlobalAI> ().Mines.Remove (gameObject);
		}
	}
	bool FindInAI (GameObject obj)
	{
		if (Owner != null) {
			foreach (GameObject selObj in Owner.GetComponent<GlobalAI>().Mines) {
				if (selObj == obj)
					return true;
			}
			return false;
		}
		return false;
	}
}