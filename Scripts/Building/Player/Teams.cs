using UnityEngine;
using System.Collections;

public class Teams : MonoBehaviour {
	public bool Station;
	public bool Mining;
	public bool Dock;
	public bool Tower;
	public bool SStation;
	public bool Sensor;
	public bool ShopStation;
	public float Timer = 0.1f;

	private Station _sb;
	// Use this for initialization
	void Start () {
		_sb = gameObject.GetComponent<Station>();
	}
	void LateUpdate(){
		if (Timer <= 0) {
			if (Station) {
				_sb.enabled = true;
			}
		}
	}
	// Update is called once per frame
	void Update () {
		if (Timer > 0) {
			Timer -= Time.deltaTime;
		}
			if (GameObject.FindGameObjectWithTag ("BuildingBuildingFreand") != null) {
			if (Vector3.Distance (gameObject.transform.position, GameObject.FindGameObjectWithTag ("BuildingBuildingFreand").transform.position) <= 3) {
				if (Station) {
					_sb.FreandAI = true;
				}
			}
		}
		if (GameObject.FindGameObjectWithTag ("BuildingBuildingEnemy") != null) {
		if (Vector3.Distance (gameObject.transform.position, GameObject.FindGameObjectWithTag ("BuildingBuildingEnemy").transform.position) <= 3) {
			if(Station){
				_sb.AI = true;
			}
		}
	}
}
}