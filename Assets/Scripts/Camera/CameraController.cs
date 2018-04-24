using UnityEngine;
using System.Collections;

public class Cameras : MonoBehaviour {
	public Camera Camera1;
	public Camera Camera2;
	public int PlayTime;

	void Start () {
			Camera1.enabled = false;
			Camera2.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
