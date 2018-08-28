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
		
	public Sprite tex;
		
	public float xp;
	public int protection;

	public bool Scient;
	public float TimeScient;

	private GlobalDB _GDB;
	private Select _SEL;
	private float timer=0.1f;

	public GameObject Owner;

	private float HealthBarLen;
	private float ShildBarLen;
	private float EnergyBarLen;

	public Texture Crew;

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
			//PlayerTimer = TimeScient;
		}
		if (TimeScient <= 0) {
			Scient = false;
			TimeScient = 1;
			//PlayerTimer = 0;
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