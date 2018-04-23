using UnityEngine;
using System.Collections;

public class BaseMarker : MonoBehaviour {
	public float Сила;
	public GameObject Owner;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Owner.GetComponent<Station> ().AI) {
			gameObject.tag = "EnemyBaseMarker";
		}
		if (Owner.GetComponent<Station> ().FreandAI) {
			gameObject.tag = "FreandBaseMarker";
		}
		//Owner.GetComponent<Castle> ().СилаАтакующегоФлота = Сила;
	}
}
