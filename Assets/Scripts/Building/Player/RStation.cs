using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RStation : MonoBehaviour {
	public bool AI;
	public bool FreandAI;
	public bool NeutralAgrass;
	public bool Neutral;
	
	public bool visible = false;
	
	public GUISkin mainSkin;
	public int numDepth = 1;
	
	public Sprite tex;
	
	public string nameTower;
	public float xp;
	public int curHealth;
	public int protection;

	private GlobalDB _GDB;
	private Select _SEL;
	public GameObject MeshFOW;

	public float Radius;

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

	GameObject[] gos;
	GameObject closest;
	// Use this for initialization
	void Awake ()
	{
		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
		_SEL = GameObject.FindGameObjectWithTag("MainUI").GetComponent<Select>();
		style = _SEL.style;
		gameObject.name = "RStation(Clone)";
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
	void Start () {
	
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
		gos = GameObject.FindGameObjectsWithTag ("Обломки");
		if (_GDB.selectList.Count > 0) {
			visible = false;
		}
		if (AI) {
			gameObject.tag = "Enemy";
		}
		if (!AI) {
			if(!FreandAI && !Neutral && !NeutralAgrass){
				gameObject.tag = "Dwarf";
			}
			if(FreandAI && !Neutral && !NeutralAgrass){
				gameObject.tag = "Freand";
			}
		}
		if (Neutral) {
			gameObject.tag = "Neutral";
		}
		if (NeutralAgrass) {
			gameObject.tag = "NeutralAgrass";
		}
		if (FindClosestEnemy () != null) {
			if (Vector3.Distance (gameObject.transform.position, FindClosestEnemy ().transform.position) <= Radius) {
				if (FreandAI) {
					Owner.GetComponent<GlobalAI>().Titanium += FindClosestEnemy ().GetComponent<DestroyedShip> ().Titanium;
					Owner.GetComponent<GlobalAI>().Dilithium += FindClosestEnemy ().GetComponent<DestroyedShip> ().Dilitium;
					Owner.GetComponent<GlobalAI>().Crew += FindClosestEnemy ().GetComponent<DestroyedShip> ().Humans;
				}
				if (AI) {
					Owner.GetComponent<GlobalAI>().Titanium += FindClosestEnemy ().GetComponent<DestroyedShip> ().Titanium;
					Owner.GetComponent<GlobalAI>().Dilithium += FindClosestEnemy ().GetComponent<DestroyedShip> ().Dilitium;
					Owner.GetComponent<GlobalAI>().Crew += FindClosestEnemy ().GetComponent<DestroyedShip> ().Humans;
				}
				if (!AI && !FreandAI) {
					_GDB.Titanium += FindClosestEnemy ().GetComponent<DestroyedShip> ().Titanium;
					_GDB.Dilithium += FindClosestEnemy ().GetComponent<DestroyedShip> ().Dilitium;
					_GDB.Humans += FindClosestEnemy ().GetComponent<DestroyedShip> ().Humans;
				}
				Destroy (FindClosestEnemy ());
			}
		}
	}
	public GameObject FindClosestEnemy()
	{            
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos)
		{
			float curDistance=Vector3.Distance(go.transform.position,position);  
			if (curDistance < distance)
			{
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}
}
