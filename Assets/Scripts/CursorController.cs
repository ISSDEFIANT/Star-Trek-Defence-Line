using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour {
	public GameObject Idle;
	public GameObject Attack;
	public GameObject Go;
	public GameObject Base;
	public GameObject Hover;
	public GameObject Repair;

	public bool IdleBool;
	public bool AttackBool;
	public bool GoBool;
	public bool BaseBool;
	public bool HoverBool;
	public bool RepairBool;

	private GlobalDB _GDB;
	private Select _s;
	// Use this for initialization
	void Start () {
		_GDB = GameObject.FindGameObjectWithTag ("MainUI").GetComponent<GlobalDB> ();
		_s = GameObject.FindGameObjectWithTag ("MainUI").GetComponent<Select> ();
	}
	
	// Update is called once per frame
	void Update () {
			if (_s.isSelect) {
				IdleBool = true;
				GoBool = false;
			}
			if (_GDB.selectList.Count >= 1) {
				if (_s.isSelect) {
				} else {
					if (AttackBool) {
					GoBool = false;
					} else {
						GoBool = true;
						IdleBool = false;
					}
				}
			}
			if (_GDB.selectList.Count == 0) {
				IdleBool = true;
				GoBool = false;
			}
		if (HoverBool) {
			Go.SetActive (false);
			Idle.SetActive (false);
			Attack.SetActive (false);
			Base.SetActive(false);
			Hover.SetActive(true);
			Repair.SetActive(false);
		}
		if (GoBool) {
			if(!HoverBool){
			Go.SetActive (true);
			Idle.SetActive (false);
			Attack.SetActive (false);
			Base.SetActive(false);
			Hover.SetActive(false);
			Repair.SetActive(false);
			}
		}
		if (IdleBool) {
			if(!HoverBool){
			Go.SetActive (false);
			Idle.SetActive (true);
			Attack.SetActive (false);
			Base.SetActive(false);
			Hover.SetActive(false);
			Repair.SetActive(false);
			}
		}
		if (AttackBool) {
			Go.SetActive (false);
			Idle.SetActive (false);
			Attack.SetActive (true);
			Base.SetActive(false);
			Hover.SetActive(false);
			Repair.SetActive(false);
		}
		if (BaseBool) {
			Go.SetActive (false);
			Idle.SetActive (false);
			Attack.SetActive (false);
			Base.SetActive(true);
			Hover.SetActive(false);
			Repair.SetActive(false);
		}
		if (RepairBool) {
			Go.SetActive (false);
			Idle.SetActive (false);
			Attack.SetActive (false);
			Base.SetActive(false);
			Hover.SetActive(false);
			Repair.SetActive(true);
		}
	}
}