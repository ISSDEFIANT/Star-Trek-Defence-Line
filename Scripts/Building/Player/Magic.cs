using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Magic : MonoBehaviour {
	public bool AI;
	public bool FreandAI;
	public bool NeutralAgrass;
	public bool Neutral;

	public bool visible = false;
	public bool shiptex = false;
	public bool weapontex = false;
	public bool main = false;
	public bool buildtex = false;

	public GUISkin mainSkin;
	public int numDepth = 1;
		
	public Sprite tex;
		
	public string nameMagic;
	public float xp;
    public int curHealth;
	public int protection;
	public int lvl;
		
	public int costUp;
	
	public Texture Tab1;
	public Texture Tab2;
	public Texture Tab3;

	public bool Scient;
	public float TimeScient;

	private GlobalDB _GDB;
	private Select _SEL;
	private float timer=0.1f;
	private float PlayerTimer;

	public GameObject MeshFOW;

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

	public bool ShipTecActive;
	public bool StationTecActive;
	public bool GlobalTecActive;

	public Research CurRes;
		// Use this for initialization
		void Start ()
		{
			_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
			_SEL = GameObject.FindGameObjectWithTag("MainUI").GetComponent<Select>();
		style = _SEL.style;
		gameObject.name = "Magic(Clone)";
		}
		
		void OnMouseDown ()
		{
			_SEL.ClearSelect();
			if(_GDB.activeObjectInterface != null)
				_GDB.deactivationInterface();
			_GDB.activeObjectInterface = gameObject;
			visible = true;
		    main = true;
			GameObject.FindGameObjectWithTag("MainUI").GetComponent<BackgroudUI>().pictureSelectObject = tex;
		}
	void OnMouseEnter(){
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
		if (AI) {
			GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().AttackBool = false;
			GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().GoBool = false;
			GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().IdleBool = false;
			GameObject.FindGameObjectWithTag("Coursor").GetComponent<CursorController>().BaseBool = false;
			
		}
	}
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
		if (AI) {
			gameObject.tag = "Enemy";
			if (!FindInAI (gameObject)) {
				Owner.GetComponent<GlobalAI> ().SciStations.Add (gameObject);
			}
		}
		if (!AI) {
			if(!FreandAI && !Neutral && !NeutralAgrass){
			gameObject.tag = "Dwarf";
			}
			if(FreandAI && !Neutral && !NeutralAgrass){
				gameObject.tag = "Freand";
				if (!FindInAI (gameObject)) {
					Owner.GetComponent<GlobalAI> ().SciStations.Add (gameObject);
				}
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
		if (!visible) {
			weapontex = false;
			buildtex = false;
			shiptex = false;
		}
		if (Scient) {
			TimeScient -= Time.deltaTime;
			PlayerTimer = TimeScient;
		}
		if (TimeScient <= 0) {
			Scient = false;
			TimeScient = 1;
			PlayerTimer = 0;
		}
	}
	void OnDestroy(){
		if (FindInAI (gameObject)) {
			Owner.GetComponent<GlobalAI> ().SciStations.Remove (gameObject);
		}
	}
	bool FindInAI (GameObject obj)
	{
		if (Owner != null) {
			foreach (GameObject selObj in Owner.GetComponent<GlobalAI>().SciStations) {
				if (selObj == obj)
					return true;
			}
			return false;
		}
		return false;
	}
}