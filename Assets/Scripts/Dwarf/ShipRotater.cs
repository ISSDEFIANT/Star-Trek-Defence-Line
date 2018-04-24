using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;

public class ShipRotater : MonoBehaviour {
	private Stats _st;
	public GameObject TargetNull;
	public float Timer = 1;
	// Use this for initialization
	void Start () {
		_st = gameObject.GetComponent<Stats> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_st.targetTransform != null) {
			if (Vector3.Distance (gameObject.transform.position, _st.targetVector) <= 1) {
				gameObject.GetComponent<LookatTarget> ().SetTarget (_st.targetTransform);
				gameObject.GetComponent<LookatTarget> ().enabled = true;
				Timer = 1;
			} else {
				gameObject.GetComponent<LookatTarget> ().enabled = false;
			}
		}
		if (_st.targetTransform == null) {
			if (Timer > 0) {
				Timer -= Time.deltaTime;
				gameObject.GetComponent<LookatTarget> ().SetTarget (TargetNull.transform);
			}
			if (Timer <= 0) {
				gameObject.GetComponent<LookatTarget> ().enabled = false;
			}
		}
	}
}