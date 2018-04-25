using UnityEngine;
using System.Collections;

public class VIDEOWarpCameraEffect : MonoBehaviour {
	public Vector3 Speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 Vector;
		Vector = new Vector3 (Random.Range (-Speed.x, Speed.x), Random.Range (-Speed.y, Speed.y), Random.Range (-Speed.z, Speed.z));
		gameObject.transform.Rotate(Speed, 10);
	}
}
