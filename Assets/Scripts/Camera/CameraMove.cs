/// <summary>
/// Camera move.
/// Create by Sky Games
/// </summary>
using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	public int LevelArea = 100;

	public int speed = 100;

	public int curZoomSpeed;
	public int zoomSpeed = 1;
	public int ZoomMin = 20;
	public int ZoomMax = 100;

	public float minX = -360.0f;
	public float maxX = 360.0f;

	public float minY = -45.0f;
	public float maxY = 45.0f;

	public float sensX = 100.0f;
	public float sensY = 100.0f;

	float ZoomY = 0.0f;
	float rotationY = 0.0f;
	float rotationX = 0.0f;

	public GameObject Camera;
	public bool RotateBuilding;

	public GameObject LocalMinimap;
	public GameObject GlobalMinimap;

	public bool GlobalMapWork;
	public int Distance;

	public LayerMask UsingLayer;
	void Update () {
		if (gameObject.transform.position.y > 800) {
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x, 800, gameObject.transform.position.z);
		}
		RaycastHit hit;
		Vector3 RayVector = -Vector3.up;
		if (Physics.Raycast (GameObject.Find("Cube").transform.position, RayVector, out hit, 1000.0f,UsingLayer)) {
			if (curZoomSpeed != 0) {
				curZoomSpeed = (int)hit.distance;
				speed = (int)hit.distance * 2;
			}
		} else {
			Vector3 RayVector2 = Vector3.up;
			if (Physics.Raycast(GameObject.Find("Cube").transform.position, RayVector2, out hit, 1000.0f,UsingLayer)) {
				if (curZoomSpeed != 0) {
					curZoomSpeed = (int)hit.distance;
					speed = (int)hit.distance * 2;
				}
			}
		}
		if (curZoomSpeed == 0) {
			curZoomSpeed = 2;
		}
		if (speed == 0) {
			speed = 10;
		}


		var translation = Vector3.zero;
	
//		Debug.Log(transform.position.z);
//		Debug.Log(transform.position.x);
		// лево
		if ((int)Input.mousePosition.x < 2 || Input.GetKey (KeyCode.LeftArrow)){
			transform.position += transform.forward * Time.deltaTime * speed; 
	}
		// право
		if ((int)Input.mousePosition.x > Screen.width - 2 || Input.GetKey (KeyCode.RightArrow)){
			transform.position -= transform.forward * Time.deltaTime * speed; 
	}
		// верх 
		if (Input.mousePosition.y > Screen.height - 2 || Input.GetKey (KeyCode.UpArrow)){
			transform.position += transform.right * Time.deltaTime * speed;
	}
		// низ
		if (Input.mousePosition.y < 2 || Input.GetKey (KeyCode.DownArrow)) {
			transform.position -= transform.right * Time.deltaTime * speed;
		}
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		if (Input.GetKey (KeyCode.Plus)) {
			scroll = 0.1f	;
		}
		if (Input.GetKey (KeyCode.Equals)) {
			scroll = 0.1f	;
		}
		if (Input.GetKey (KeyCode.Minus)) {
			scroll = -0.1f;
		}
		if (scroll != 0) {
			if (!RotateBuilding) {
				transform.Translate (0, scroll * -curZoomSpeed, scroll * 0, Space.World);
				ZoomY = Mathf.Clamp (ZoomY, ZoomMin, ZoomMax);
			}
		}
		if (Input.GetMouseButton (2)) {
			rotationX += Input.GetAxis ("Mouse X") * sensX * Time.deltaTime;
			rotationY += Input.GetAxis ("Mouse Y") * sensY * Time.deltaTime;
			rotationY = Mathf.Clamp (rotationY, minY, maxY);
			Camera.transform.localEulerAngles = new Vector3 (0, 0, rotationY);
			transform.localEulerAngles = new Vector3 (0, rotationX, 0);
		}
		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			RotateBuilding = true;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift)) {
			RotateBuilding = false;
		}
	}
	void Start(){
		GlobalMinimap = GameObject.FindGameObjectWithTag ("GlobalMinimap");
	}
}