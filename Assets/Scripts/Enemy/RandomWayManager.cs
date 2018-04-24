using UnityEngine;
using System.Collections;

public class RandomWayManager : MonoBehaviour {
	private float Timer;
	private float X;
	private float Z;
	private Vector3 Target;
	// Use this for initialization
	void Start () {
		Timer = 300;
	}
	
	// Update is called once per frame
	void Update () {
		X = Random.Range (-10000, 10000);
		Z = Random.Range (-10000, 10000);
		Target.x = X;
		Target.z = Z;
		if (Timer <= 0) {
			if (gameObject.GetComponent<AIWayManager> ().WayPoints.Count == 1) {
				gameObject.GetComponent<AIWayManager> ().WayPoints [0].transform.position = Target;
			}
			if (gameObject.GetComponent<AIWayManager> ().WayPoints.Count == 2) {
				gameObject.GetComponent<AIWayManager> ().WayPoints [0].transform.position = Target;

				gameObject.GetComponent<AIWayManager> ().WayPoints [1].transform.position = Target;
			}
			if (gameObject.GetComponent<AIWayManager> ().WayPoints.Count == 3) {
				gameObject.GetComponent<AIWayManager> ().WayPoints [0].transform.position = Target;

				gameObject.GetComponent<AIWayManager> ().WayPoints [1].transform.position = Target;

				gameObject.GetComponent<AIWayManager> ().WayPoints [2].transform.position = Target;
			}
			if (gameObject.GetComponent<AIWayManager> ().WayPoints.Count == 4) {
				gameObject.GetComponent<AIWayManager> ().WayPoints [0].transform.position = Target;

				gameObject.GetComponent<AIWayManager> ().WayPoints [1].transform.position = Target;

				gameObject.GetComponent<AIWayManager> ().WayPoints [2].transform.position = Target;

				gameObject.GetComponent<AIWayManager> ().WayPoints [3].transform.position = Target;
			}
			if (gameObject.GetComponent<AIWayManager> ().WayPoints.Count == 5) {
				gameObject.GetComponent<AIWayManager> ().WayPoints [0].transform.position = Target;

				gameObject.GetComponent<AIWayManager> ().WayPoints [1].transform.position = Target;

				gameObject.GetComponent<AIWayManager> ().WayPoints [2].transform.position = Target;

				gameObject.GetComponent<AIWayManager> ().WayPoints [3].transform.position = Target;

				gameObject.GetComponent<AIWayManager> ().WayPoints [4].transform.position = Target;
			}
			Timer = 300;
		}
		Timer -= Time.deltaTime;
	}
}