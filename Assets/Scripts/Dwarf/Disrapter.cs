using UnityEngine;
using System.Collections;

public class Disrapter : MonoBehaviour {
	public Transform target;
	public GameObject Ship;
	public int damage;
	public float speed;

	public bool ImpulseSystemAttack;
	public bool LifeSupportSystemAttack;
	public bool PrimaryWeaponSystemAttack;
	public bool SensorsSystemAttack;
	public bool TractorBeamSystemAttack;
	public bool WarpEngingSystemAttack;
	public bool WarpCoreAttack;
	public bool SecondaryWeaponSystemAttack;

	private RaycastHit hit;
	private float Timer = 1f;
	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward * (Time.deltaTime * speed));
		if (target != null) {
			HealthModule _hm = target.GetComponent<HealthModule>();
			if (Vector3.Distance (gameObject.transform.position, target.position) <= 1) {
				if (_hm.CurСилаПоля <= 0) {
					_hm.curHealth -= (damage);
					if (!ImpulseSystemAttack & !LifeSupportSystemAttack & !PrimaryWeaponSystemAttack & !SensorsSystemAttack & !TractorBeamSystemAttack & !WarpEngingSystemAttack & !WarpCoreAttack & !SecondaryWeaponSystemAttack) {
						_hm.curImpulseSystemHealth -= Random.Range (10, damage);
						_hm.curLifeSupportSystemHealth -= Random.Range (10, damage);
						_hm.curPrimaryWeaponSystemHealth -= Random.Range (10, damage);
						_hm.curSensorsSystemHealth -= Random.Range (10, damage);
						_hm.curTractorBeamSystemHealth -= Random.Range (10, damage);
						_hm.curWarpEngingSystemHealth -= Random.Range (10, damage);
						_hm.curWarpCoreHealth -= Random.Range (10, damage);
						_hm.curSecondaryWeaponSystemHealth -= Random.Range (10, damage);
						_hm.curCrew -= Random.Range (1, damage/3);
					}
					if (ImpulseSystemAttack) {
						_hm.curImpulseSystemHealth -= Random.Range (20, damage);
						_hm.curLifeSupportSystemHealth -= Random.Range (0, damage);
						_hm.curPrimaryWeaponSystemHealth -= Random.Range (0, damage);
						_hm.curSensorsSystemHealth -= Random.Range (0, damage);
						_hm.curTractorBeamSystemHealth -= Random.Range (0, damage);
						_hm.curWarpEngingSystemHealth -= Random.Range (0, damage);
						_hm.curWarpCoreHealth -= Random.Range (0, damage);
						_hm.curSecondaryWeaponSystemHealth -= Random.Range (0, damage);
						_hm.curCrew -= Random.Range (1, damage/3);
					}
					if (LifeSupportSystemAttack) {
						_hm.curImpulseSystemHealth -= Random.Range (0, damage);
						_hm.curLifeSupportSystemHealth -= Random.Range (20, damage);
						_hm.curPrimaryWeaponSystemHealth -= Random.Range (0, damage);
						_hm.curSensorsSystemHealth -= Random.Range (0, damage);
						_hm.curTractorBeamSystemHealth -= Random.Range (0, damage);
						_hm.curWarpEngingSystemHealth -= Random.Range (0, damage);
						_hm.curWarpCoreHealth -= Random.Range (0, damage);
						_hm.curSecondaryWeaponSystemHealth -= Random.Range (0, damage);
						_hm.curCrew -= Random.Range (1, damage/3);
					}
					if (PrimaryWeaponSystemAttack) {
						_hm.curImpulseSystemHealth -= Random.Range (0, damage);
						_hm.curLifeSupportSystemHealth -= Random.Range (0, damage);
						_hm.curPrimaryWeaponSystemHealth -= Random.Range (20, damage);
						_hm.curSensorsSystemHealth -= Random.Range (0, damage);
						_hm.curTractorBeamSystemHealth -= Random.Range (0, damage);
						_hm.curWarpEngingSystemHealth -= Random.Range (0, damage);
						_hm.curWarpCoreHealth -= Random.Range (0, damage);
						_hm.curSecondaryWeaponSystemHealth -= Random.Range (0, damage);
						_hm.curCrew -= Random.Range (1, damage/3);
					}
					if (SensorsSystemAttack) {
						_hm.curImpulseSystemHealth -= Random.Range (0, damage);
						_hm.curLifeSupportSystemHealth -= Random.Range (0, damage);
						_hm.curPrimaryWeaponSystemHealth -= Random.Range (0, damage);
						_hm.curSensorsSystemHealth -= Random.Range (20, damage);
						_hm.curTractorBeamSystemHealth -= Random.Range (0, damage);
						_hm.curWarpEngingSystemHealth -= Random.Range (0, damage);
						_hm.curWarpCoreHealth -= Random.Range (0, damage);
						_hm.curSecondaryWeaponSystemHealth -= Random.Range (0, damage);
						_hm.curCrew -= Random.Range (1, damage/3);
					}
					if (TractorBeamSystemAttack) {
						_hm.curImpulseSystemHealth -= Random.Range (0, damage);
						_hm.curLifeSupportSystemHealth -= Random.Range (0, damage);
						_hm.curPrimaryWeaponSystemHealth -= Random.Range (0, damage);
						_hm.curSensorsSystemHealth -= Random.Range (0, damage);
						_hm.curTractorBeamSystemHealth -= Random.Range (20, damage);
						_hm.curWarpEngingSystemHealth -= Random.Range (0, damage);
						_hm.curWarpCoreHealth -= Random.Range (0, damage);
						_hm.curSecondaryWeaponSystemHealth -= Random.Range (0, damage);
						_hm.curCrew -= Random.Range (1, damage/3);
					}
					if (WarpEngingSystemAttack) {
						_hm.curImpulseSystemHealth -= Random.Range (0, damage);
						_hm.curLifeSupportSystemHealth -= Random.Range (0, damage);
						_hm.curPrimaryWeaponSystemHealth -= Random.Range (0, damage);
						_hm.curSensorsSystemHealth -= Random.Range (0, damage);
						_hm.curTractorBeamSystemHealth -= Random.Range (0, damage);
						_hm.curWarpEngingSystemHealth -= Random.Range (20, damage);
						_hm.curWarpCoreHealth -= Random.Range (0, damage);
						_hm.curSecondaryWeaponSystemHealth -= Random.Range (0, damage);
						_hm.curCrew -= Random.Range (1, damage/3);
					}
					if (WarpCoreAttack) {
						_hm.curImpulseSystemHealth -= Random.Range (0, damage);
						_hm.curLifeSupportSystemHealth -= Random.Range (0, damage);
						_hm.curPrimaryWeaponSystemHealth -= Random.Range (0, damage);
						_hm.curSensorsSystemHealth -= Random.Range (0, damage);
						_hm.curTractorBeamSystemHealth -= Random.Range (0, damage);
						_hm.curWarpEngingSystemHealth -= Random.Range (0, damage);
						_hm.curWarpCoreHealth -= Random.Range (20, damage);
						_hm.curSecondaryWeaponSystemHealth -= Random.Range (0, damage);
						_hm.curCrew -= Random.Range (1, damage/3);
					}
					if (SecondaryWeaponSystemAttack) {
						_hm.curImpulseSystemHealth -= Random.Range (0, damage);
						_hm.curLifeSupportSystemHealth -= Random.Range (0, damage);
						_hm.curPrimaryWeaponSystemHealth -= Random.Range (0, damage);
						_hm.curSensorsSystemHealth -= Random.Range (0, damage);
						_hm.curTractorBeamSystemHealth -= Random.Range (0, damage);
						_hm.curWarpEngingSystemHealth -= Random.Range (0, damage);
						_hm.curWarpCoreHealth -= Random.Range (0, damage);
						_hm.curSecondaryWeaponSystemHealth -= Random.Range (20, damage);
						_hm.curCrew -= Random.Range (1, damage/3);
					}
					Destroy (gameObject);
				}
				if (_hm.CurСилаПоля > 0) {
					_hm.curHealth -= (damage / 5);
					_hm.CurСилаПоля -= (damage / 2);
					if (!ImpulseSystemAttack & !LifeSupportSystemAttack & !PrimaryWeaponSystemAttack & !SensorsSystemAttack & !TractorBeamSystemAttack & !WarpEngingSystemAttack & !WarpCoreAttack & !SecondaryWeaponSystemAttack) {
						_hm.curImpulseSystemHealth -= Random.Range (1, damage / 5);
						_hm.curLifeSupportSystemHealth -= Random.Range (1, damage / 5);
						_hm.curPrimaryWeaponSystemHealth -= Random.Range (1, damage / 5);
						_hm.curSensorsSystemHealth -= Random.Range (1, damage / 5);
						_hm.curTractorBeamSystemHealth -= Random.Range (1, damage / 5);
						_hm.curWarpEngingSystemHealth -= Random.Range (1, damage / 5);
						_hm.curWarpCoreHealth -= Random.Range (1, damage / 5);
						_hm.curSecondaryWeaponSystemHealth -= Random.Range (1, damage / 5);
					}
					if (ImpulseSystemAttack) {
						_hm.curImpulseSystemHealth -= Random.Range (2, damage / 5);
						_hm.curLifeSupportSystemHealth -= Random.Range (0, damage / 5);
						_hm.curPrimaryWeaponSystemHealth -= Random.Range (0, damage / 5);
						_hm.curSensorsSystemHealth -= Random.Range (0, damage / 5);
						_hm.curTractorBeamSystemHealth -= Random.Range (0, damage / 5);
						_hm.curWarpEngingSystemHealth -= Random.Range (0, damage / 5);
						_hm.curWarpCoreHealth -= Random.Range (0, damage / 5);
						_hm.curSecondaryWeaponSystemHealth -= Random.Range (0, damage / 5);
					}
					if (LifeSupportSystemAttack) {
						_hm.curImpulseSystemHealth -= Random.Range (0, damage / 5);
						_hm.curLifeSupportSystemHealth -= Random.Range (2, damage / 5);
						_hm.curPrimaryWeaponSystemHealth -= Random.Range (0, damage / 5);
						_hm.curSensorsSystemHealth -= Random.Range (0, damage / 5);
						_hm.curTractorBeamSystemHealth -= Random.Range (0, damage / 5);
						_hm.curWarpEngingSystemHealth -= Random.Range (0, damage / 5);
						_hm.curWarpCoreHealth -= Random.Range (0, damage / 5);
						_hm.curSecondaryWeaponSystemHealth -= Random.Range (0, damage / 5);
					}
					if (PrimaryWeaponSystemAttack) {
						_hm.curImpulseSystemHealth -= Random.Range (0, damage / 5);
						_hm.curLifeSupportSystemHealth -= Random.Range (0, damage / 5);
						_hm.curPrimaryWeaponSystemHealth -= Random.Range (2, damage / 5);
						_hm.curSensorsSystemHealth -= Random.Range (0, damage / 5);
						_hm.curTractorBeamSystemHealth -= Random.Range (0, damage / 5);
						_hm.curWarpEngingSystemHealth -= Random.Range (0, damage / 5);
						_hm.curWarpCoreHealth -= Random.Range (0, damage / 5);
						_hm.curSecondaryWeaponSystemHealth -= Random.Range (0, damage / 5);
					}
					if (SensorsSystemAttack) {
						_hm.curImpulseSystemHealth -= Random.Range (0, damage / 5);
						_hm.curLifeSupportSystemHealth -= Random.Range (0, damage / 5);
						_hm.curPrimaryWeaponSystemHealth -= Random.Range (0, damage / 5);
						_hm.curSensorsSystemHealth -= Random.Range (2, damage / 5);
						_hm.curTractorBeamSystemHealth -= Random.Range (0, damage / 5);
						_hm.curWarpEngingSystemHealth -= Random.Range (0, damage / 5);
						_hm.curWarpCoreHealth -= Random.Range (0, damage / 5);
						_hm.curSecondaryWeaponSystemHealth -= Random.Range (0, damage / 5);
					}
					if (TractorBeamSystemAttack) {
						_hm.curImpulseSystemHealth -= Random.Range (0, damage / 5);
						_hm.curLifeSupportSystemHealth -= Random.Range (0, damage / 5);
						_hm.curPrimaryWeaponSystemHealth -= Random.Range (0, damage / 5);
						_hm.curSensorsSystemHealth -= Random.Range (0, damage / 5);
						_hm.curTractorBeamSystemHealth -= Random.Range (2, damage / 5);
						_hm.curWarpEngingSystemHealth -= Random.Range (0, damage / 5);
						_hm.curWarpCoreHealth -= Random.Range (0, damage / 5);
						_hm.curSecondaryWeaponSystemHealth -= Random.Range (0, damage / 5);
					}
					if (WarpEngingSystemAttack) {
						_hm.curImpulseSystemHealth -= Random.Range (0, damage / 5);
						_hm.curLifeSupportSystemHealth -= Random.Range (0, damage / 5);
						_hm.curPrimaryWeaponSystemHealth -= Random.Range (0, damage / 5);
						_hm.curSensorsSystemHealth -= Random.Range (0, damage / 5);
						_hm.curTractorBeamSystemHealth -= Random.Range (0, damage / 5);
						_hm.curWarpEngingSystemHealth -= Random.Range (2, damage / 5);
						_hm.curWarpCoreHealth -= Random.Range (0, damage / 5);
						_hm.curSecondaryWeaponSystemHealth -= Random.Range (0, damage / 5);
					}
					if (WarpCoreAttack) {
						_hm.curImpulseSystemHealth -= Random.Range (0, damage / 5);
						_hm.curLifeSupportSystemHealth -= Random.Range (0, damage / 5);
						_hm.curPrimaryWeaponSystemHealth -= Random.Range (0, damage / 5);
						_hm.curSensorsSystemHealth -= Random.Range (0, damage / 5);
						_hm.curTractorBeamSystemHealth -= Random.Range (0, damage / 5);
						_hm.curWarpEngingSystemHealth -= Random.Range (0, damage / 5);
						_hm.curWarpCoreHealth -= Random.Range (2, damage / 5);
						_hm.curSecondaryWeaponSystemHealth -= Random.Range (0, damage / 5);
					}
					if (SecondaryWeaponSystemAttack) {
						_hm.curImpulseSystemHealth -= Random.Range (0, damage / 5);
						_hm.curLifeSupportSystemHealth -= Random.Range (0, damage / 5);
						_hm.curPrimaryWeaponSystemHealth -= Random.Range (0, damage / 5);
						_hm.curSensorsSystemHealth -= Random.Range (0, damage / 5);
						_hm.curTractorBeamSystemHealth -= Random.Range (0, damage / 5);
						_hm.curWarpEngingSystemHealth -= Random.Range (0, damage / 5);
						_hm.curWarpCoreHealth -= Random.Range (0, damage / 5);
						_hm.curSecondaryWeaponSystemHealth -= Random.Range (2, damage / 5);
					}
					if (Physics.Raycast (transform.position, transform.position, out hit, 10)) {
						_hm.Поле.GetComponent<Renderer> ().enabled = true;
						_hm.Поле.GetComponent<Forcefield> ().Shot = true;
						_hm.Поле.GetComponent<Forcefield> ().PhaserHit = hit;
					}
					DestroyAlternative ();
				}
			}
		}
	}
	void DestroyAlternative(){
		DiactivateObject _d = gameObject.GetComponent<DiactivateObject> ();
		_d.Diactivate ();
	}
}