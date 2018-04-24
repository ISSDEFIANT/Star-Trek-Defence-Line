using UnityEngine;
using System.Collections;

public class Flag : MonoBehaviour {
	public GameObject FlagHalo;

	public float HaloForce = 0.2f;
	// Use this for initialization
	void Start () {
		FlagHalo.GetComponent<LensFlare> ().brightness = HaloForce;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.tag = "Flag";
		if (gameObject.GetComponent<Rigidbody> ()) {
			Destroy (gameObject.GetComponent<Rigidbody> ());
		}
	}
}
