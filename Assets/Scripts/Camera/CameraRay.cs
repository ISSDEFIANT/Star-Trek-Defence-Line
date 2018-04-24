using UnityEngine;
using System.Collections;

public class CameraRay : MonoBehaviour {
	public bool Lock;
	public bool Locker;
	public GameObject Box;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Locker) {
			//GameObject.FindGameObjectWithTag ("MainUI").GetComponent<Select> ().Lock = Lock;
		}
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		Ray Ship = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit Shiphit;


		if (Physics.Raycast (ray, out hit, 5)) {
			Lock = false;
			//GameObject.FindGameObjectWithTag ("MainUI").GetComponent<Select> ().isSelect = false;
		} else {
			Lock = true;
		}
		if (Physics.Raycast (Ship, out Shiphit, 10000)) {
			if(Shiphit.collider.tag == "Dwarf"){
			Locker = false;
			//GameObject.FindGameObjectWithTag ("MainUI").GetComponent<Select> ().Lock = false;
			}else{
				Locker = true;
			}
		}
	}
}
