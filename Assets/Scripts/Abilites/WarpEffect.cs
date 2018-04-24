using UnityEngine;
using System.Collections;

public class WarpEffect : MonoBehaviour {
	public GameObject Ship;
	public float NormalScale;
	public float WarpScale;
	public float CurScale;

	public bool X;
	public bool Y;
	public bool Z;

	public GameObject WarpStar;
	[HideInInspector]
	public float Задержка;
	public bool Ok1;
	public bool Ok2;

	public GameObject WarpLight;
	public GameObject DestroyedWarpEffect;

	private bool StartLightStep1;
	private bool StartLightStep2;

	private HealthModule _hp;
	private ObjectTypeVisible _otv;

	public bool ModelScalerActive;
	// Use this for initialization
	void Awake (){
		DestroyedWarpEffect.transform.localRotation = Quaternion.Euler(new Vector3 (0, 0, 0));
	}

	void Start () {
		Задержка = 0.2f;
		StartLightStep1 = true;
		StartLightStep2 = false;

		_hp = Ship.GetComponent<HealthModule> ();
		_otv = Ship.GetComponent<ObjectTypeVisible> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (StartLightStep1) {
			if (Ship.GetComponent<Stats> ().WarpEffect) {
				foreach (Light obj in WarpLight.GetComponentsInChildren<Light> ()) {
					obj.gameObject.SetActive (true);
					if (obj.intensity < 10) {
						obj.intensity += 1;
					} else {
						StartLightStep1 = false;
						StartLightStep2 = true;
					}
				}
			} else {
				StartLightStep1 = true;
				StartLightStep2 = false;
				foreach (Light obj in WarpLight.GetComponentsInChildren<Light> ()) {
					obj.intensity = 0;
					obj.gameObject.SetActive (false);
				}
			}
		}
		if (StartLightStep2) {
			if (!Ship.GetComponent<Stats> ().WarpEffect) {
				foreach (Light obj in WarpLight.GetComponentsInChildren<Light> ()) {
					obj.gameObject.SetActive (true);
					if (obj.intensity > 0) {
						obj.intensity -= 1;
					} else {
						StartLightStep1 = true;
						StartLightStep2 = false;
					}
				}
			} else {
				StartLightStep1 = true;
				StartLightStep2 = false;
				foreach (Light obj in WarpLight.GetComponentsInChildren<Light> ()) {
					obj.intensity = 0;
					obj.gameObject.SetActive (false);
				}
			}
		}
		if (Ship.GetComponent<Stats> ().Warp) {
			if (CurScale < WarpScale) {
				if (Задержка > 0) {
					Задержка -= Time.deltaTime;
				}
				if (Задержка <= 0) {
					if (!Ok1) {
						if (_otv.IsVisible) {
							GameObject star = Instantiate (WarpStar, gameObject.transform.position, gameObject.transform.rotation);
							star.transform.GetChild (0).localScale = new Vector3 (_hp.ShipRadius / 5, _hp.ShipRadius / 5, _hp.ShipRadius / 5);
							star.transform.GetChild (1).localScale = new Vector3 (_hp.ShipRadius / 6, _hp.ShipRadius / 6, _hp.ShipRadius / 6);
						}
						Ok1 = true;
						Ok2 = false;
					}
					CurScale += Time.deltaTime/5;
				}
			}
			if(CurScale >= WarpScale){
				CurScale = WarpScale;
			}
		} else {
			if (CurScale > NormalScale) {
				CurScale -= Time.deltaTime / 5;
				if (!Ok2) {
					if (_otv.IsVisible) {
						GameObject star = Instantiate (WarpStar, gameObject.transform.position, gameObject.transform.rotation);
						star.transform.GetChild (0).localScale = new Vector3 (_hp.ShipRadius / 5, _hp.ShipRadius / 5, _hp.ShipRadius / 5);
						star.transform.GetChild (1).localScale = new Vector3 (_hp.ShipRadius / 6, _hp.ShipRadius / 6, _hp.ShipRadius / 6);
					}
					Ok2 = true;
					Ok1 = false;
				}
				Задержка = 0.2f;
			}
			if (CurScale < NormalScale) {
				CurScale = NormalScale;
			}
		}
		if (ModelScalerActive) {
			if (Z) {
				if (transform.localScale.z != CurScale) {
					transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, CurScale);
				}
			}
			if (Y) {
				if (transform.localScale.y != CurScale) {
					transform.localScale = new Vector3 (transform.localScale.x, CurScale, transform.localScale.z);
				}
			}
			if (X) {
				if (transform.localScale.x != CurScale) {
					transform.localScale = new Vector3 (CurScale, transform.localScale.y, transform.localScale.z);
				}
			}
		}

		if (_hp.curWarpEngingSystemHealth <= 0) {
			DestroyedWarpEffect.SetActive (true);
			if (DestroyedWarpEffect.transform.localRotation.eulerAngles == new Vector3 (0, 0, 0)) {
				DestroyedWarpEffect.transform.position = WarpLight.transform.GetChild (Random.Range (0, WarpLight.transform.childCount)).transform.position;
				DestroyedWarpEffect.transform.localRotation = Quaternion.Euler(new Vector3 (Random.Range (-360, 360), Random.Range (-360, 360), Random.Range (-360, 360)));
			}
		} else {
			DestroyedWarpEffect.SetActive (false);
			DestroyedWarpEffect.transform.localRotation = Quaternion.Euler(new Vector3 (0, 0, 0));
		}
	}
}
