using UnityEngine;
using System.Collections;

public class ObjectID : MonoBehaviour {
	public GameObject Owner;
	public float ID = 0;
	public float IDDelay = 0.5f;
	// Use this for initialization
	void Start () {
		
	}
	void Awake(){

	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (IDDelay > 0) {
			IDDelay -= Time.deltaTime;
			//ID = Owner.GetComponent<ObjectDataToSave> ().SetID;
		}
		if (IDDelay <= 0) {
			if (ID == 0) {
				ID = Random.Range (1, 9999999);
			}
			gameObject.name = ID.ToString ();
			//Owner.GetComponent<ObjectDataToSave> ().GetID = ID;
		}
	}
}
