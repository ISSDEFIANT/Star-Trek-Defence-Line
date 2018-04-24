using UnityEngine;
using System.Collections;

public class Transport : MonoBehaviour {
	public int ResurseCounte;

	public float Timer;

	public bool Dilitium;
	public bool Titanium;
	public bool Humans;

	public bool PlayerStation;
	public bool FreandStation;

	public bool OK;
	public bool DoIt;

	public Texture DilitiumTex;
	public Texture TitaniumTex;
	public Texture HumanTex;

	private Ray _ray;
	private RaycastHit _hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (DoIt) {
			if (Timer >= 2) {
				ResurseCounte += 10;
				Timer = 0;
			}
			if (Vector3.Distance (gameObject.transform.position, GameObject.FindGameObjectWithTag ("PlayerShopStation").transform.position) <= 5) {
				if (Dilitium) {
					GameObject.FindGameObjectWithTag ("PlayerShopStation").GetComponent<ShopTarget> ().Dilitium += ResurseCounte;
					ResurseCounte = 0;
				}
				if (Titanium) {
					GameObject.FindGameObjectWithTag ("PlayerShopStation").GetComponent<ShopTarget> ().Titenium += ResurseCounte;
					ResurseCounte = 0;
				}
				if (Humans) {
					GameObject.FindGameObjectWithTag ("PlayerShopStation").GetComponent<ShopTarget> ().Humans += ResurseCounte;
					ResurseCounte = 0;
				}

				FreandStation = true;
				PlayerStation = false;
				OK = false;
			}

			if (Vector3.Distance (gameObject.transform.position, GameObject.FindGameObjectWithTag ("FreandShopStation").transform.position) <= 5) {
				FreandStation = false;
				PlayerStation = true;
				OK = false;
			}

			if (PlayerStation) {
				if (!OK) {
					gameObject.GetComponent<MoveComponent> ().Movement (GameObject.FindGameObjectWithTag ("PlayerShopStation").transform.position);
					OK = true;
				}
				Timer += Time.deltaTime;
			}

			if (FreandStation) {
				if (!OK) {
					gameObject.GetComponent<MoveComponent> ().Movement (GameObject.FindGameObjectWithTag ("FreandShopStation").transform.position);
					OK = true;
				}
			}
			if (gameObject.GetComponent<Stats> ().Selected) {
				if (Input.GetMouseButtonDown (1)) {
					DoIt = false;
				}
			}
		} else {
			ResurseCounte = 0;
		}
	}
}