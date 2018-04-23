using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Nebula : MonoBehaviour {
	public float Radius;
	public List<GameObject> ActiveObj;


	GameObject[] gos1;
	GameObject closest1;

	GameObject[] gos2;
	GameObject closest2;

	GameObject[] gos3;
	GameObject closest3;

	public bool Damage;
	public float HealthDamage;

	public bool PrimaryWeaponDeactive;
	public bool SecondaryWeaponDeactive;

	public bool ImpulseDeactive;
	public bool WarpEngingDeactive;

	public bool LifeSupportDeactive;
	public bool SensorsDeactive;
	public bool TractorDeactive;
	public bool WarpCoreDeactive;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		Collider[] colls = Physics.OverlapSphere (transform.position, Radius);
		foreach (Collider coll in colls) {
			if (coll.GetComponent<HealthModule>()) {
				HealthModule hp = (HealthModule)coll.transform.GetComponent<HealthModule> ();
				if(Damage){
					hp.curHealth -= Time.deltaTime * HealthDamage;
				}
				if (PrimaryWeaponDeactive) {
					hp.ActivePrimaryWeapon = true;
				}
				if (SecondaryWeaponDeactive) {
					hp.ActiveSecondaryWeapon = true;
				}
				if (ImpulseDeactive) {
					hp.ActiveImpulse = true;
				}
				if (WarpEngingDeactive) {
					hp.ActiveWarpEnging = true;
				}
				if (LifeSupportDeactive) {
					hp.ActiveLifeSupport = true;
				}
				if (SensorsDeactive) {
					hp.ActiveSensors = true;
				}
				if (TractorDeactive) {
					hp.ActiveTractor = true;
				}
				if (WarpCoreDeactive) {
					hp.ActiveWarpCore = true;
				}
			}
		}
	}

	bool FindInList (GameObject obj)
	{
		foreach(GameObject selObj in ActiveObj)
		{
			if(selObj == obj)
				return true;
		}
		return false;
	}
}
