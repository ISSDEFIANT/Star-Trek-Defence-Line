using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopStation : MonoBehaviour {
	public bool AI;
	public bool FreandAI;
	public bool NeutralAgrass;
	public bool Neutral;

	public bool visible = false;

	public GUISkin mainSkin;
	public int numDepth = 1;

	public Sprite tex;

	public string nameStation;

	public float xp;
	public int curHealth;
	public int protection;

	public Texture TiteniumSellIcon;
	public Texture TiteniumBuyIcon;
	public Texture DilithiumSellIcon;
	public Texture DilithiumBuyIcon;

	public GameObject MeshFOW;

	public GameObject TegObj;

	private GlobalDB _GDB;
	private Select _SEL;
	private bool TransportController;

	public GameObject Owner;

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
	// Use this for initialization
	void Start ()
	{
		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
		_SEL = GameObject.FindGameObjectWithTag("MainUI").GetComponent<Select>();
		style = _SEL.style;
		gameObject.name = "ShopStation(Clone)";
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
	void OnMouseEnter(){
		if (!AI) {
			if (_GDB.selectList.Count >= 1) {
				if (_GDB.selectList [0].GetComponent<Stats> ().Transport) {
					TransportController = true;
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
		TransportController = false;
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
	
	// Update is called once per frame
	void Update () {
		if (Neutral) {
			if (Owner != null) {
				Owner = null;
			}
		}
		if (!visible && _GDB.activeObjectInterface == gameObject) {
			_GDB.activeObjectInterface = null;
		}
		if (AI) {
			gameObject.tag = "Enemy";
		}
		if (!AI) {
			if (TransportController) {
				if (Input.GetMouseButtonDown (1)) {
					_GDB.selectList [0].GetComponent<Transport> ().DoIt = true;
					_GDB.selectList [0].GetComponent<Transport> ().OK = false;
				}
			}
			if (!FreandAI && !Neutral && !NeutralAgrass) {
				gameObject.tag = "Dwarf";
				TegObj.tag = "PlayerShopStation";
			}
			if (FreandAI && !Neutral && !NeutralAgrass) {
				gameObject.tag = "Freand";
				TegObj.tag = "FreandShopStation";
			}
		}
		if (Neutral) {
			gameObject.tag = "Neutral";
		}
		if (NeutralAgrass) {
			gameObject.tag = "NeutralAgrass";
		}
		if (_GDB.selectList.Count > 0) {
			visible = false;
		}
	}
}